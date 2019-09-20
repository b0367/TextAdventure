using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Items;

namespace TextAdventure
{

    public enum Actions
    {
        Up, Down, Left, Right, Take, Use, Quit
    }
    public class Player : HealthEntity
    {

        internal Entity NothingEntity = new DefaultEntity(-1, -1, "nothing", null, null);

        public List<Item> Inventory = new List<Item>();

        public Dictionary<Slots, Item> Equips = new Dictionary<Slots, Item>() {
            { Slots.Body, null },
            { Slots.Feet, null },
            { Slots.Head, null },
            { Slots.MainHand, null },
            { Slots.OffHand, null },
            { Slots.Misc1, null },
            { Slots.Misc2, null },
        };

        private Mutex Cursor = new Mutex();

        public Dictionary<Actions, List<string>> ActionWords = new Dictionary<Actions, List<string>>()
        {
            { Actions.Up, new string[]{"w", "up", "north" }.ToList() },
            { Actions.Down, new string[]{"s", "down", "south" }.ToList() },
            { Actions.Left, new string[]{"a", "left", "west" }.ToList() },
            { Actions.Right, new string[]{"d", "right", "east" }.ToList() },
            { Actions.Take, new string[]{"take", "grab", "pickup" }.ToList() },
            { Actions.Use, new string[]{ "use" }.ToList() },
            {Actions.Quit, new string[]{ "quit", "exit", "leave" }.ToList() }
        };
        internal bool Running = true;

        public Player(int x, int y, int mxhp, Room room) : base(x, y, mxhp, "Player", room, false, '@') { } //Create a Player at (x,y) named Player in the current room that's not navigable and seen as "P"

        public async void GetInput(Stream stream)
        {

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Here: " + (CurrentRoom.ImmutableMap[Y][X] == null ? "nothing" : (CurrentRoom.ImmutableMap[Y][X].item == null ? "nothing" : CurrentRoom.ImmutableMap[Y][X].item.ToString())));

            Console.WriteLine(CurrentRoom);

            Console.WriteLine(GetInventory());
            Console.WriteLine(GetHealth());
            byte[] charByte = new byte[256];
            while (true)
            {
                await stream.ReadAsync(charByte, 0, 256);
                string input = BytesToString(charByte);
                Console.Clear();
                if (ActionWords[Actions.Up].Contains(input))
                {
                    Move(0, -1);
                }
                else if (ActionWords[Actions.Down].Contains(input))
                {
                    Move(0, 1);
                }
                else if (ActionWords[Actions.Right].Contains(input))
                {
                    Move(1, 0);
                }
                else if (ActionWords[Actions.Left].Contains(input))
                {
                    Move(-1, 0);
                }
                else if (ActionWords[Actions.Take].Contains(input))
                {
                    if (CurrentRoom.ImmutableMap[Y][X] != null && CurrentRoom.ImmutableMap[Y][X].item != null)
                    {
                        TakeItem();
                    }
                    else
                    {
                        Console.Write("No item here!");
                    }
                }
                else if (ActionWords[Actions.Use].Contains(input.Split(' ')[0]))
                {
                    UseItem(input);
                }
                else if (ActionWords[Actions.Quit].Contains(input))
                {
                    Console.Clear();
                    Console.WriteLine("Goodbye!");
                    Thread.Sleep(1500);
                    Running = false;
                    return;
                }

                if (CurrentRoom.ImmutableMap[Y][X] is Enemy enemy)
                {
                    //battle or something
                    await Battle(this, enemy, stream);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Here: " + (
                        CurrentRoom.ImmutableMap[Y][X] == null ? "nothing" :
                       (CurrentRoom.ImmutableMap[Y][X].item == null ? CurrentRoom.ImmutableMap[Y][X].ToString() :
                        CurrentRoom.ImmutableMap[Y][X].ItemSecret ? "nothing..." : CurrentRoom.ImmutableMap[Y][X].item.ToString())));

                    Console.WriteLine(CurrentRoom);

                    Console.WriteLine(GetInventory());
                    Console.WriteLine(GetHealth());
                }


            }
        }

        private async Task Battle(Player player, Enemy enemy, Stream stream)
        {
            byte[] charByte = new byte[256];
            string input;

            while (player.CurrentHealth > 0 && enemy.CurrentHealth > 0)
            {
                await BattleScene(player, enemy);
                input = BytesToString(charByte);
                await stream.ReadAsync(charByte, 0, 256);
                switch (input)
                {
                    default:
                        Console.Write("");
                        break;
                }
            }

            return;
        }

        private Task BattleScene(Player player, Enemy enemy)
        {
            int PlayerHealthBarAmount = (player.CurrentHealth / player.MaxHealth) * 10;
            int EnemyHealthBarAmount = (enemy.CurrentHealth / enemy.MaxHealth) * 10;
            Console.WriteLine(enemy.CurrentHealth / enemy.MaxHealth);
            Console.CursorTop = 0;
            string output = "****************                        ░░░         \n* [" + new string('|', EnemyHealthBarAmount) + new string(' ', 10 - EnemyHealthBarAmount) + "] *                    ░░░░░░░░░░░     \n****************                  ░░░░░░   ░░░░░░   \n                                  ░░░░░  " + enemy.Representation + "  ░░░░░   \n                                  ░░░░░░   ░░░░░░   \n                                    ░░░░░░░░░░░     \n                                        ░░░     \n                        - VS -\n        ░░░\n    ░░░░░░░░░░░\n  ░░░░░░   ░░░░░░   \n  ░░░░░  " + player.Representation + "  ░░░░░   \n  ░░░░░░   ░░░░░░                 ****************\n    ░░░░░░░░░░░                   * [" + new string('|', PlayerHealthBarAmount) + new string(' ', 10 - PlayerHealthBarAmount) + "] *\n        ░░░                       ****************";
            int charindex = 0;
            string[] lines = output.Split('\n');
            int longest = 0;
            lines.ToList().ForEach(e => {
                if(e.Length > longest)
                {
                    longest = e.Length;
                }
            });
            while (charindex < longest) {
                for (int i = 0; i < lines.Length; i++)
                {
                    char[] chars = lines[i].ToCharArray();

                    Console.SetCursorPosition(charindex, i);
                    Console.Write(chars.Length > charindex ? chars[charindex] : '\0');
                    Thread.Sleep(7);
                }
                charindex++;
            }

            Console.WriteLine();

            return Task.CompletedTask;
        }


        //custom method just to remove non-letter characters
        private string BytesToString(byte[] charByte)
        {
            string output = "";
            foreach (byte b in charByte)
            {
                char c = Convert.ToChar(b);
                if (!char.IsLetterOrDigit(c) && c != ' ')
                    return output;
                output += c;
            }
            return output;
        }

        public void UseItem(string input)
        {
            string[] words = input.Split(' ');
            int slot;
            try
            {
                slot = Convert.ToInt32(words[words.Length - 1]);
            }
            catch (Exception e) when (e is OverflowException || e is FormatException) { Console.Write("Invalid item slot!"); return; }
            if (slot < Inventory.Count)
            {
                if (Inventory[slot] is Consumable)
                    (Inventory[slot] as Consumable).OnConsume(this, (Inventory[slot] as Consumable));
                else
                    Console.Write("This item can't be used!");
            }
            else
            {
                Console.Write("Invalid item slot!");
            }

        }

        public void TakeItem()
        {
            Console.Write("You take the " + CurrentRoom.ImmutableMap[Y][X].item + "!");
            if (Inventory.Contains(CurrentRoom.ImmutableMap[Y][X].item))
            {
                Item item = Inventory.Find(e => e.Equals(CurrentRoom.ImmutableMap[Y][X].item));
                (item as Consumable).Amount += (CurrentRoom.ImmutableMap[Y][X].item as Consumable).Amount;
            }
            else
            {
                Inventory.Add(CurrentRoom.ImmutableMap[Y][X].item);
            }
            CurrentRoom.ImmutableMap[Y][X].item = null;
            CurrentRoom.map[Y][X].item = null;
            CurrentRoom.map[Y][X].ReEvaluate();
            CurrentRoom.ImmutableMap[Y][X].item = null;
            CurrentRoom.ImmutableMap[Y][X].ReEvaluate();

        }

        public string GetInventory()
        {
            string output = "";
            int ind = 0;
            foreach (Item i in Inventory)
            {
                output += ind++ + ": | " + ((i is Consumable) ? (i as Consumable).Amount + " x " : "") + i.Name + " | ";
            }

            int chars = output.Length;

            output = new StringBuilder().Insert(0, "-", chars).ToString() + "\n" + output + "\n" + new StringBuilder().Insert(0, "-", chars).ToString();

            return output;
        }
        public string GetHealth()
        {
            string output = " | " + CurrentHealth + " / " + MaxHealth + " | ";


            int chars = output.Length;

            output = new StringBuilder().Insert(0, "-", chars).ToString() + "\n" + output + "\n" + new StringBuilder().Insert(0, "-", chars).ToString();

            return output;
        }
    }


}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextAdventure
{

    public enum Actions
    {
        Up, Down, Left, Right, Take
    }
    public class Player : Entity
    {

        internal Entity NothingEntity = new DefaultEntity("nothing");

        public List<Item> Inventory = new List<Item>();

        public int MaxHealth = 20;

        public int CurrentHealth = 1;

        public Dictionary<Slots, Item> Equips = new Dictionary<Slots, Item>() {
            { Slots.Body, null },
            { Slots.Feet, null },
            { Slots.Head, null },
            { Slots.MainHand, null },
            { Slots.OffHand, null },
            { Slots.Misc1, null },
            { Slots.Misc2, null },
        };

        public Dictionary<Actions, List<string>> ActionWords = new Dictionary<Actions, List<string>>()
        {
            { Actions.Up, new string[]{"w", "up", "north" }.ToList() },
            { Actions.Down, new string[]{"s", "down", "south" }.ToList() },
            { Actions.Left, new string[]{"a", "left", "west" }.ToList() },
            { Actions.Right, new string[]{"d", "right", "east" }.ToList() },
            { Actions.Take, new string[]{"take", "grab", "pickup" }.ToList() }
        };

        public Player(int x, int y, Room room) : base(x, y, "Player", room, false, 'P') { } //Create a Player at (x,y) named Player in the current room that's not navigable and seen as "P"

        public async void GetInput(Stream stream)
        {
            Console.Clear();
            Console.WriteLine(CurrentRoom);
            byte[] charByte = new byte[256];
            while (true)
            {
                await stream.ReadAsync(charByte, 0, 256);
                string input = BytesToString(charByte);
                Console.Clear();
                Console.WriteLine("Here: " + (CurrentRoom.ImmutableMap[Y][X] == null ? "nothing" : (CurrentRoom.ImmutableMap[Y][X].item == null ? "nothing" : CurrentRoom.ImmutableMap[Y][X].item.ToString())));
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
                }else if (ActionWords[Actions.Take].Contains(input))
                {
                    if(CurrentRoom.ImmutableMap[Y][X] != null && CurrentRoom.ImmutableMap[Y][X].item != null)
                    {
                        Console.WriteLine("There's an item here!");
                    }
                    else
                    {
                        Console.WriteLine("No item here!");
                    }
                }
                Console.WriteLine(CurrentRoom);
            }
        }


        //custom method just to remove non-letter characters
        private string BytesToString(byte[] charByte)
        {
            string output = "";
            foreach (byte b in charByte)
            {
                char c = Convert.ToChar(b);
                if (!char.IsLetterOrDigit(c))
                    return output;
                output += c;
            }
            return output;
        }
    }
}
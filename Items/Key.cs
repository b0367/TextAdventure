using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Items
{
    public class Key : Consumable
    {
        public Key(int amount, Player owner) : base("Key", owner, amount, (p, c) =>
        {
            Room r = p.CurrentRoom;
            if (r.ImmutableMap[p.Y][p.X] is Exit x)
            {
                if (x.Locked)
                {
                    (p.CurrentRoom.ImmutableMap[p.Y][p.X] as Exit).Locked = false;
                    (p.CurrentRoom.ImmutableMap[p.Y][p.X] as Exit).Enterable = true;
                    (p.CurrentRoom.ImmutableMap[p.Y][p.X] as Exit).Representation = 'O';
                    (p.CurrentRoom.ImmutableMap[p.Y][p.X] as Exit).Name = "Unlocked Door";
                    Console.Write("Unlocked the door! | ");
                    c.Decrement();
                }
                else
                {
                    Console.Write("This door is already unlocked!");
                }
            }
            else
            {
                Console.Write("Keys can only be used on doors!");
            }
        }, '✶')
        { }
    }
}

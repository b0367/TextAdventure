using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Items
{
    public class Consumable : Item
    {
        public int Amount;

        public Action<Player, Consumable> OnConsume;

        public Consumable(string name, Player owner, int amount, Action<Player, Consumable> onconsume, char representation = '░') : base(name, owner, Slots.None, representation)
        {
            Amount = amount;
            OnConsume = onconsume;
        }

        internal void Decrement()
        {

            Amount--;
            if(Amount == 0)
            {
                Console.WriteLine("Used your last " + Name);
            }
            else
            {
                Console.WriteLine("Used a " + Name);
            }
        }
    }
}

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
                Owner.Inventory.Remove(this);
            }
            else
            {
                Console.WriteLine("Used a " + Name);
            }
        }

        public override bool Equals(object obj)
        {
            return (obj as Consumable).Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            var hashCode = 805013783;
            hashCode = hashCode * -1521134295 + Amount.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Action<Player, Consumable>>.Default.GetHashCode(OnConsume);
            return hashCode;
        }
    }
}

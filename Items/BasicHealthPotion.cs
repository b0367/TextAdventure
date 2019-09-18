using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Items
{
    class BasicHealthPotion : Consumable
    {
        public BasicHealthPotion(int amount, Player owner) : base("Basic Health Potion", owner, amount, (p, c) => {
            if(p.CurrentHealth < p.MaxHealth)
            {
                p.CurrentHealth = Math.Min(p.CurrentHealth + 5, p.MaxHealth);
                c.Decrement();
            }
            else
            {
                Console.Write("You're already at full health!");
            }
        }, '⚗') { }
    }
}

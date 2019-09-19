using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Entities
{
    public class HealthEntity : Entity
    {
        public int MaxHealth;
        public int CurrentHealth;
        public bool Friendly;

        public HealthEntity(int x, int y, int maxhealth, string name, Room currentmap, bool navigable = true, char representation = '░', bool friendly = true) : base(x, y, name, currentmap, navigable, representation) { MaxHealth = maxhealth; CurrentHealth = maxhealth; Friendly = friendly; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Entities
{
    class Enemy : HealthEntity
    {
        public Enemy(int x, int y, int mxhp, string name, Room room, char representation = 'M') : base(x, y, mxhp, name, room, true, representation, false) { }
    }
}

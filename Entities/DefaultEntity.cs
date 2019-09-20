using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class DefaultEntity : Entity
    {
        public DefaultEntity(int x, int y, string name, Room room, Item iitem, bool navigable = true, char representation = '░', bool secret = false) : base(x, y, name, room, iitem, navigable, representation, secret) { } //Not used yet so I'm not gonna bother
    }
}

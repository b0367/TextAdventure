using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public abstract class Entity
    {
        public char Representation = ' ';

        public int X;

        public int Y;

        public string Name;

        public Room CurrentMap;

        public bool Navigable;

        public bool Move(int DeltaX, int DeltaY)
        {
            int NewX = X + DeltaX;
            int NewY = Y + DeltaY;

            if(NewX < 0 || NewY < 0 || NewX >= CurrentMap.Width || NewY >= CurrentMap.Height)
            {
                return false;
            }
            if(CurrentMap.Get(NewX, NewY).Here.Navigable)
            {
                X = NewX;
                Y = NewY;
                return true;
            }
            return false;
            
        }
    }
}

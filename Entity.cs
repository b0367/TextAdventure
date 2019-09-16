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

        public bool? Navigable;

        public static Entity Default = new DefaultEntity();

        public bool Move(int DeltaX, int DeltaY)
        {
            int NewX = X + DeltaX;
            int NewY = Y + DeltaY;

            int OldX = X;
            int OldY = Y;

            if (NewX < 0 || NewY < 0 || NewX >= CurrentMap.Width || NewY >= CurrentMap.Height)
            {
                return false;
            }
            if (CurrentMap.Get(NewX, NewY) == null)
            {
                X = NewX;
                Y = NewY;

                CurrentMap.Move(OldX, OldY, NewX, NewY);
                return true;
            }
            else
            {
                if (CurrentMap.Get(NewX, NewY).Navigable.HasValue && CurrentMap.Get(NewX, NewY).Navigable.Value)
                {

                    X = NewX;
                    Y = NewY;

                    CurrentMap.Move(OldX, OldY, NewX, NewY);
                    return true;
                }
            }

            return false;

        }

        public Entity(int x, int y, string name, Room currentmap, bool? navigable = true, char representation = '░')
        {
            X = x;
            Y = y;
            Representation = representation;
            Name = name;
            CurrentMap = currentmap;
            Navigable = navigable;
        }
    }
}

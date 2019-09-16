using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public abstract class Entity
    {
        public char Representation = ' '; //What the entity looks like

        public int X; //It's X location

        public int Y; //It's Y locations

        public string Name; //It's name

        public Room CurrentMap;

        public bool? Navigable; //Can it be moved into

        public static Entity Default = new DefaultEntity();

        public bool Move(int DeltaX, int DeltaY) //Mainly for players. It'll move the player on the board
        {
            //Changes entity's X and Y location so that it can move around the board
            int NewX = X + DeltaX; 
            int NewY = Y + DeltaY;

            //Stores the old X and Y values for later comparisons
            int OldX = X;
            int OldY = Y;

            //If the new location is out of the bounds of the room
            if (NewX < 0 || NewY < 0 || NewX >= CurrentMap.Width || NewY >= CurrentMap.Height)
            {
                return false;
            }
            //Clarification?
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

        public Entity(int x, int y, string name, Room currentmap, bool? navigable = true, char representation = '░') //Sets up a basic entity
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

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

        public int X; //Its X location

        public int Y; //Its Y locations

        public string Name; //Its name

        public Room CurrentRoom;

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
            if (NewX < 0 || NewY < 0 || NewX >= CurrentRoom.Width || NewY >= CurrentRoom.Height)
            {
                return false;
            }
            //Checking if the new tile is null; only because you can't get a null value's navigable
            if (CurrentRoom.Get(NewX, NewY) == null)
            {
                X = NewX;
                Y = NewY;

                CurrentRoom.Move(OldX, OldY, NewX, NewY);
                return true;
            }
            else
            {
                //HasValue is for nullable booleans, so you don't accidently check if null is true
                if (CurrentRoom.Get(NewX, NewY).Navigable.HasValue && CurrentRoom.Get(NewX, NewY).Navigable.Value)
                {
                    if(CurrentRoom.map[NewY][NewX] is Exit)
                    {
                        Exit exit = (Exit)CurrentRoom.map[NewY][NewX];
                        exit.NewMap(this);
                    }

                    X = NewX;
                    Y = NewY;

                    CurrentRoom.Move(OldX, OldY, NewX, NewY);
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
            CurrentRoom = currentmap;
            Navigable = navigable;
        }
    }
}

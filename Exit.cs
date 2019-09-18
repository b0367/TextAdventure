using System;

namespace TextAdventure
{
    public class Exit : Entity
    {
        public Room OutRoom;
        public Room InRoom;

        public Exit Out;

        //public bool Locked; not until items probably?

        public bool Enterable;

        public Exit(int x, int y, bool enterable, Room outroom, Room inroom, Exit oout = null) : base(x, y, "Exit", inroom, enterable, enterable ? 'O' : 'Ø') //Creates an Exit at (x,y) named "Exit" in the present room is navigable and is seen as "D"
        {
            //Sets the room you come in from and where you go out to
            Enterable = enterable;
            OutRoom = outroom;
            InRoom = inroom;
            Out = oout;
        }

        public void NewRoom(Player p, int OldX, int OldY)
        {
            p.CurrentRoom.map[OldY][OldX] = p.CurrentRoom.ImmutableMap[OldY][OldX];
            p.CurrentRoom = OutRoom;
            p.X = Out.X;
            p.Y = Out.Y;

            OutRoom.map[p.Y][p.X] = p;


        }
    }
}
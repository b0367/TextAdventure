using System;

namespace TextAdventure
{
    public class Exit : Entity
    {
        public Room OutRoom;
        public Room InRoom;

        public Exit Out;

        public Exit(int x, int y, Room outroom, Room inroom, Exit oout = null) : base(x, y, "Exit", inroom, true, 'D') //Creates an Exit at (x,y) named "Exit" in the present room is navigable and is seen as "D"
        {
            //Sets the room you come in from and where you go out to
            OutRoom = outroom;
            InRoom = inroom;
            Out = oout;
        }

        public void NewMap(Player p)
        {
            p.CurrentRoom.map[p.Y][p.X] = p.CurrentRoom.ImmutableMap[p.Y][p.X];
            p.CurrentRoom = OutRoom;
            p.X = Out.X;
            p.Y = Out.Y;
            OutRoom.map[Out.Y][Out.X] = p;

            Console.WriteLine(OutRoom.Get(Out.X, Out.Y));

            Console.Clear();
            Console.WriteLine(p.CurrentRoom);
        }
    }
}
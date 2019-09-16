namespace TextAdventure
{
    public class Exit : Entity
    {
        public Room Out;
        public Room In;

        public Exit(int x, int y, Room outroom, Room inroom) : base(x, y, "Exit", inroom, true, 'D') //Creates an Exit at (x,y) named "Exit" in the present room is navigable and is seen as "D"
        {
            //Sets the room you come in from and where you go out to
            Out = outroom;
            In = inroom;
        }
    }
}
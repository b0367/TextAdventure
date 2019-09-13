namespace TextAdventure
{
    public class Exit : Entity
    {
        public Room Out;
        public Room In;

        public Exit(int x, int y, Room outroom, Room inroom) : base(x, y, "Exit", inroom, true, 'D')
        {
            Out = outroom;
            In = inroom;
        }
    }
}
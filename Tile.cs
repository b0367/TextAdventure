namespace TextAdventure
{
    public class Tile
    {
        public Entity Here;

        public Room Parent;

        public int X { get; set; }
        public int Y { get; set; }

        public Tile(Room parent, int x, int y, Entity here = null)
        {
            Parent = parent;
            X = x;
            Y = y;
            Here = here;
        }
    }
}
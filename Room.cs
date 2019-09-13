using System.Collections.Generic;

namespace TextAdventure
{
    public class Room
    {
        public List<List<Tile>> map;

        public int Height;

        public int Width;

        public List<Exit> Exits;

        public Tile Get(int x, int y)
        {
            return map[y][x];
        }
    }
}
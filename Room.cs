using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAdventure
{
    public class Room
    {
        public List<List<Tile>> map;

        public List<List<Tile>> ImmutableMap { get; }

        public int Height;

        public int Width;

        public List<Exit> Exits;

        public Tile Get(int x, int y)
        {
            return map[y][x];
        }

        public Room AddPlayer(int x, int y)
        {
            map[y][x].Here = new Player(x, y, this);
            return this;
        }

        public Room AddWall(int x, int y)
        {
            map[y][x].Here = new Wall(x, y, this);
            ImmutableMap[x][y].Here = new Wall(x, y, this);
            return this;

        }

        public Room(int height, int width)
        {
            Height = height;
            Width = width;
            map = new List<List<Tile>>();
            ImmutableMap = new List<List<Tile>>();
            for (int y = 0; y < Height; y++)
            {
                map.Add(new List<Tile>());
                ImmutableMap.Add(new List<Tile>());
                for (int x = 0; x < Width; x++)
                {
                    map[y].Add(new Tile(this, x, y));
                    ImmutableMap[y].Add(new Tile(this, x, y));
                }
            }
        }

        public void Move(int x, int y, int newX, int newY)
        {
            map[newY][newX] = map[y][x];
            map[y][x] = ImmutableMap[y][x];
        }

        public override string ToString()
        {
            string output = "";
            map.ForEach((x) => {
                x.ForEach(e => output += e.Here == null ? '░' : e.Here.Representation);
                output += "\n";
            });
            return output;
        }
    }
}
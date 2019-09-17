using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAdventure
{
    public class Room
    {
        public List<List<Entity>> map;

        public List<List<Entity>> ImmutableMap { get; }

        public int Height;

        public int Width;

        public List<Exit> Exits;

        public Entity Get(int x, int y)
        {
            return map[y][x];
        }


        //Builder Methods; player has to be last for reasons
        public Player AddPlayer(int x, int y)
        {
            map[y][x] = new Player(x, y, this);
            return (Player) map[y][x];
        }

        //above
        public Room AddWall(int x, int y)
        {
            map[y][x] = new Wall(x, y, this);
            ImmutableMap[y][x] = new Wall(x, y, this);
            return this;

        }

        public Room AddExit(int x, int y, Room OutRoom, int ox, int oy)
        {
            Exit exit = new Exit(x, y, OutRoom, this);
            Exit oexit = new Exit(ox, oy, this, OutRoom);
            exit.Out = oexit;
            oexit.Out = exit;

            map[y][x] = exit;
            ImmutableMap[y][x] = exit;

            OutRoom.map[oy][ox] = oexit;
            OutRoom.ImmutableMap[oy][ox] = oexit;

            return this;
        }

        public Room(int height, int width)
        {
            Height = height;
            Width = width;
            map = new List<List<Entity>>();
            ImmutableMap = new List<List<Entity>>();
            for (int y = 0; y < Height; y++)
            {
                map.Add(new List<Entity>());
                ImmutableMap.Add(new List<Entity>());
                for (int x = 0; x < Width; x++)
                {
                    map[y].Add(null);
                    ImmutableMap[y].Add(null);
                }
            }
        }


        //Updating the room map; called in entity.move, as entity.move just changes the entity positions
        public void Move(int x, int y, int newX, int newY)
        {
            map[newY][newX] = map[y][x];
            map[y][x] = ImmutableMap[y][x];
        }

        public override string ToString()
        {
            string output = "";
            map.ForEach((x) => {
                x.ForEach(e => output += e == null ? Entity.Default.Representation : e.Representation);
                output += "\n";
            });
            return output;
        }
    }
}
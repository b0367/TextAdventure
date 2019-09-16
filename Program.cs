using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultEntity pushing = new DefaultEntity();
            Room r = new Room(5, 5);
            (int x, int y) = (1, 1);
            r.AddPlayer(1, 1).AddWall(0, 0).AddWall(1, 0).AddWall(2, 0);
            ConsoleKey input = ConsoleKey.Spacebar;
            do
            {
                Console.Clear();
                Entity entity;
                switch (input)
                {
                    case ConsoleKey.A:
                        entity = r.Get(x, y);
                        if (entity.Move(-1, 0))
                            x--;
                        break;
                    case ConsoleKey.D:
                        entity = r.Get(x, y);
                        if (entity.Move(1, 0))
                            x++;
                        break;
                    case ConsoleKey.W:
                        entity = r.Get(x, y);
                        if (entity.Move(0, -1))
                            y--;
                        break;
                    case ConsoleKey.S:
                        entity = r.Get(x, y);
                        if (entity.Move(0, 1))
                            y++;
                        break;
                    default: break;
                }
                Console.WriteLine(r);
                input = Console.ReadKey().Key;

            } while (input != ConsoleKey.Escape);

        }
    }
}

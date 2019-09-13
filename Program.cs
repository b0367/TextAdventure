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
            Room r = new Room(5, 5);
            (int x, int y) PlayerPos = (1, 1);
            r.AddPlayer(1, 1).AddWall(0, 0).AddWall(1, 0).AddWall(2, 0);
            ConsoleKeyInfo input;
            Entity entity = new Wall(0, 0, null);
            do
            {
                input = Console.ReadKey();
                Console.Clear();
                switch (input.Key)
                {
                    case ConsoleKey.A:
                        entity = r.Get(PlayerPos.x, PlayerPos.y).Here;
                        if (entity.Move(-1, 0))
                            PlayerPos.x--;
                        break;
                    case ConsoleKey.D:
                        entity = r.Get(PlayerPos.x, PlayerPos.y).Here;
                        if (entity.Move(1, 0))
                            PlayerPos.x++;
                        break;
                    case ConsoleKey.W:
                        entity = r.Get(PlayerPos.x, PlayerPos.y).Here;
                        if (entity.Move(0, -1))
                            PlayerPos.y--;
                        break;
                    case ConsoleKey.S:
                        entity = r.Get(PlayerPos.x, PlayerPos.y).Here;
                        if (entity.Move(0, 1))
                            PlayerPos.y++;
                        break;
                }
                Console.WriteLine(PlayerPos + " : " + (entity == null ? "entity is null" : entity.X + ", " + entity.Y));
                Console.WriteLine(r);
            } while (input.Key != ConsoleKey.Escape);

        }
    }
}

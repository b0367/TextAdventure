using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Items;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Room StartingRoom = new Room(10, 10);
            //StartingRoom.AddWall(0, 0).AddWall(1, 0).AddWall(2, 0).AddWall(3, 0).AddWall(4, 0).AddWall(4, 1).AddWall(4, 2).AddWall(0, 1).AddWall(0, 2);
            Room NewRoom = new Room(5, 5);
            
            Player p = StartingRoom.AddPlayer(1, 1);
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (r.Next(100) < 10)
                    {
                        Entity e = new Entity(j, i, "nothing", NewRoom, new Key(1, p), true, '░', false);
                        StartingRoom.ImmutableMap[j][i] = e;
                        StartingRoom.map[j][i] = e;
                    }
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (r.Next(100) < 25)
                    {
                        StartingRoom.AddWall(j, i);
                    }
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (r.Next(100) < 10)
                    {
                        Entity e = new Entity(j, i, "nothing", NewRoom, new BasicHealthPotion(1, p), true, '░', false);
                        StartingRoom.ImmutableMap[j][i] = e;
                        StartingRoom.map[j][i] = e;
                    }
                }
            }
            StartingRoom.AddPlayer(1, 1);
            StartingRoom.AddExit(4, 4, false, true, NewRoom, 1, 1, false, true);
            StartingRoom.ImmutableMap[4][4].item = null;
            StartingRoom.ImmutableMap[4][4].Representation = 'Ɵ';
            Stream stream = Console.OpenStandardInput();

            p.GetInput(stream);

            while (p.Running);

            stream.Dispose();

        }
    }
}

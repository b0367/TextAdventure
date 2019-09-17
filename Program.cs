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
            Room StartingRoom = new Room(10, 10);
            //StartingRoom.AddWall(0, 0).AddWall(1, 0).AddWall(2, 0).AddWall(3, 0).AddWall(4, 0).AddWall(4, 1).AddWall(4, 2).AddWall(0, 1).AddWall(0, 2);
            Room NewRoom = new Room(5, 5);
            Entity e = new Entity(0, 0, "ItemTile", NewRoom, new Consumable("Health Potion", 1, (p) => {
                
            }, 'H'), true, '░', false);
            StartingRoom.AddExit(4, 4, true, NewRoom, 1, 1, true);
            Player p = StartingRoom.AddPlayer(1, 1);

            p.GetInput(Console.OpenStandardInput());

            while (true);

        }
    }
}

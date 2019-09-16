using System;
using System.Collections.Generic;
using System.IO;
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
            Player p = r.AddWall(0, 0).AddWall(1, 0).AddWall(2, 0).AddPlayer(1, 1);

            p.GetInput(Console.OpenStandardInput());

            while (true);

        }
    }
}

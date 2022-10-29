using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int xLenght;
        public int yLength;
        public GridBox newBox;
        public GridBox currentgrid;
        public Grid(int Lines, int Columns)
        {
            xLenght = Lines;
            yLength = Columns;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                    grids.Add(newBox);
                for(int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (Columns * i + j));
                    Console.Write($"{newBox.Index}\n");
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void drawBattlefield(int Lines, int Columns)
        {
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    //currentgrid = new GridBox();
                    if (i == currentgrid.xIndex && j == currentgrid.yIndex )
                    {
                        currentgrid.ocupied = true;
                        Console.Write("[X]\t");
                        
                        //currentgrid.ocupied = false;
                    }
                    else
                    {
                        currentgrid.ocupied = false;
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

    }
}

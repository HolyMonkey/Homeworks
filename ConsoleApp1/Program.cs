using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;


namespace ConsoleApp2
{
    class Program
    {
        static Cell[,] map = new Cell[10, 10];
        static List<Cell> ClosedList = new List<Cell>();
        static List<Cell> OpenList = new List<Cell>();
        static List<Cell> NearbyCell = new List<Cell>();
        static void Main(string[] args)
        {
            int start_x = int.Parse(Console.ReadLine());
            int start_y = int.Parse(Console.ReadLine());
            int end_x = int.Parse(Console.ReadLine());
            int end_y = int.Parse(Console.ReadLine());

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[j, i] = new Cell('#', ConsoleColor.White);
                    Cell k = map[j, i];
                    k.H = (Math.Abs(j - end_x) + Math.Abs(i - end_y)) * 10;
                    k.G = 0;
                    k.x = j;
                    k.y = i;
                }
            }
            Cell start_cell = map[start_x, start_y];
            Cell end_cell = map[end_x, end_y];
            /* Cell[] er = GetNearby(map[start_x,start_y]);
             foreach (var u in er)
             {
             u.IsLocked = true;
             }*/
            PutBlocks(6);
            FindAllWay(start_cell, end_cell);
            Render();
            Console.ReadLine();
        }
        static void Render()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Cell cell = map[j, i];
                    Console.SetCursorPosition(j, i);
                    if (cell.IsLocked)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (cell.IsPath)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = cell.Color;
                    }

                    Console.Write(cell.Symbol);

                }
            }
        }
        /* static void FindPath(int x1, int y1, int x2, int y2)
         {

             Cell start = map[x1, y1];


             ClosedList.Add(map[x1, y1]);

             current_cell = start;
             current_cell = Get_Minimum_F(x1, y1);
             OpenList.Remove(map[x1, y1]);
             OpenList.Remove(current_cell);
             ClosedList.Add(current_cell);
             while (!OpenList.Contains(map[x2,y2]))
             {
                 FindAllWay(start, map[x2, y2]);


             }
         }*/
        static bool FindAllWay(Cell start, Cell finish)
        {
            Cell current_cell = start;
            Cell next_cell = null;

            while (!OpenList.Contains(finish))
            {

                next_cell = Get_Minimum_F(current_cell);
                next_cell.IsPath = true;

                FindAllWay(next_cell, finish);

            }
            return true;

        }
        static void PutBlocks(int number)
        {

            int x = 5;
            int y = 3;
            Cell k = map[x, y];
            k.IsLocked = true;
            for (int i = 0; i < number; i++)
            {
                Cell ki = map[x, y + i];
                ki.IsLocked = true;
            }


        }
        static Cell[] GetNearby(Cell current_cell)
        {
            int xi = 0, yi = 0;
            Cell currentcell = map[current_cell.x, current_cell.y];
            //if (currentcell.IsLocked)
            //{
            //  Console.WriteLine("не та точка");
            //}
            //else
            //{
            for (xi = currentcell.x - 1; xi <= currentcell.x + 1; xi++)
            {
                for (yi = currentcell.y - 1; yi <= currentcell.y + 1; yi++)
                {
                    if (xi < 0 || yi < 0 || xi > 9 || yi > 9)
                    {
                        continue;
                    }
                    else
                    {
                        Cell k = map[xi, yi];

                        if (k.IsLocked == false && k != currentcell)
                        {
                            if (!OpenList.Contains(k) && k != currentcell)
                            {
                                if (((xi == currentcell.x + 1) && (yi == currentcell.y + 1)) || ((xi == currentcell.x - 1) && (yi == currentcell.y - 1))
                                || ((xi == currentcell.x + 1) && (yi == currentcell.y - 1)) || ((xi == currentcell.x - 1) && (yi == currentcell.y + 1)))
                                {
                                    k.G += 14;
                                }
                                else
                                {
                                    k.G += 10;
                                }

                            }
                            k.F = k.G + k.H;
                            k = map[xi, yi];
                            k.Parent = currentcell;

                            NearbyCell.Add(k);
                            OpenList.Add(k);

                        }
                    }

                }
            }
            //}
            return NearbyCell.ToArray();
        }
        static Cell Get_Minimum_F(Cell currentcell)
        {

            NearbyCell.Clear();
            NearbyCell.AddRange(GetNearby(currentcell));
            NearbyCell.Sort();
            return NearbyCell.ElementAt(0);
        }



    }
    class Cell : IComparable<Cell>
    {
        public int F, G, H, x, y;
        public char Symbol = '@';
        public ConsoleColor Color;
        public bool IsLocked = false;
        public bool IsPath = false;
        public Cell Parent;

        public Cell(char symbol, ConsoleColor color)
        {
            Symbol = symbol;
            Color = color;
        }
        public int CompareTo(Cell o)
        {
            if (this.F < o.F)
            {
                return -1;
            }
            else if (this.F > o.F)
            {
                return 1;
            }
            else
                return 0;
        }
        public static bool operator >(Cell obj1, Cell obj2)
        {
            if (obj1.G > obj2.G)
                return true;
            return false;
        }
        public static bool operator <(Cell obj1, Cell obj2)
        {
            if (obj1.G < obj2.G)
                return true;
            return false;
        }
    }
}
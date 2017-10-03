using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miscellaneous
{
    class RPGv1
    {
        static object[][] Map = new object[0][];
        static object[][] Triggers = new object[0][];
        static char[] ScreenBuffer = new char[Console.WindowWidth * (Console.WindowHeight - 2)];
        static Random r = new Random();
        const int PLAYER = 0;

        static int AddObject(int x, int y, char visual)
        {
            Array.Resize(ref Map, Map.Length + 1);

            int index = Map.Length - 1;
            object[] gameObject = new object[3];
            Map[index] = gameObject;

            SetX(index, x);
            SetY(index, y);
            SetVisual(index, visual);
            return index;
        }

        static int GetX(int index)
        {
            return (int)Map[index][0];
        }

        static void SetX(int index, int x)
        {
            Map[index][0] = x;
        }

        static int GetY(int index)
        {
            return (int)Map[index][1];
        }

        static void SetY(int index, int y)
        {
            Map[index][1] = y;
        }

        static char GetVisual(int index)
        {
            return (char)Map[index][2];
        }

        static void SetVisual(int index, char visual)
        {
            Map[index][2] = visual;
        }

        static void ShowViewport(int scene_x, int scene_y, int width, int height)
        {
            for (int i = 0; i < Map.Length; i++)
            {
                if (GetX(i) > scene_x && GetY(i) > scene_y &&
                    GetX(i) < scene_x + width && GetY(i) < scene_y + height)
                {
                    int drawX = GetX(i) - scene_x;
                    int drawY = GetY(i) - scene_y;

                    WriteInBuffer(drawX, drawY, GetVisual(i));
                }
            }
        }

        static void Seed()
        {
            for (int i = 0; i < 1000; i++)
            {
                AddObject(r.Next(1, 200), r.Next(0, 200), '#');
            }
        }

        static void DrawBox(int console_x, int console_y, int width, int height)
        {
            for (int x = console_x; x < console_x + width; x++)
            {
                for (int y = console_y; y < console_y + height; y++)
                {
                    if (y == console_y || y == console_y + height - 1)
                    {
                        WriteInBuffer(x, y, '_');
                    }
                    if (x == console_x || x == console_x + width - 1)
                    {
                        WriteInBuffer(x, y, '\u007C');
                    }
                }
            }
        }

        static void WriteInBuffer(int x, int y, char symbol)
        {
            int index = x + Console.WindowWidth * y;
            ScreenBuffer[index] = symbol;
        }

        static void ClearBuffer()
        {
            for (int i = 0; i < ScreenBuffer.Length; i++)
            {
                ScreenBuffer[i] = ' ';
            }
        }

        static void DrawBuffer()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(ScreenBuffer);
        }

        static void AddTrigger(int id, string actionName)
        {
            Array.Resize(ref Triggers, Triggers.Length + 1);
            object[] trigger = new object[2];
            trigger[0] = id;
            trigger[1] = actionName;
            Triggers[Triggers.Length - 1] = trigger;
        }

        static int CheckCollision(int id)
        {
            for (int i = 0; i < Map.Length; i++)
            {
                if (id != i)
                {
                    if (GetX(i) == GetX(id) && GetY(i) == GetY(id))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        static string[] GetTriggerActions(int id)
        {
            string[] actions = new string[0];
            for (int i = 0; i < Triggers.Length; i++)
            {
                if ((int)Triggers[i][0] == id)
                {
                    Array.Resize(ref actions, actions.Length + 1);
                    actions[actions.Length - 1] = (string)Triggers[i][1];
                }
            }
            return actions;
        }


        public static void FakeMain()
        {
            Console.CursorVisible = false;

            AddObject(1, 1, '®');
            AddTrigger(AddObject(3, 3, '#'), "finish");
            AddTrigger(AddObject(4, 4, '@'), "jump");

            int temp = AddObject(5, 7, '!');
            AddTrigger(temp, "jump");
            AddTrigger(temp, "split_symbol");

            //Seed();

            int viewportX = 0, viewportY = 0;

            while (true)
            {
                ClearBuffer();
                DrawBox(0, 0, 51, 21);
                ShowViewport(viewportX, viewportY, 50, 20);
                DrawBuffer();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    SetX(PLAYER, GetX(PLAYER) + 1);
                    if (GetX(PLAYER) > viewportX + 50)
                    {
                        viewportX += 50;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    SetX(PLAYER, GetX(PLAYER) - 1);
                    if (GetX(PLAYER) < viewportX)
                    {
                        viewportX -= 50;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    SetY(PLAYER, GetY(PLAYER) + 1);
                    if (GetY(PLAYER) > viewportY + 20)
                    {
                        viewportY += 20;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    SetY(PLAYER, GetY(PLAYER) - 1);
                    if (GetY(PLAYER) < viewportY)
                    {
                        viewportY -= 20;
                    }
                }
                int collisedObject = CheckCollision(PLAYER);
                if (collisedObject != -1)
                {
                    string[] actions = GetTriggerActions(collisedObject);
                    for (int i = 0; i < actions.Length; i++)
                    {
                        if (actions[i] == "finish")
                        {
                            Console.Clear();
                            Console.WriteLine("Игра закончена!");
                            Console.ReadKey();
                            return;
                        }
                        else if (actions[i] == "split_symbol")
                        {
                            SetVisual(collisedObject, (char)r.Next(32, 50));
                        }
                        else if (actions[i] == "jump")
                        {
                            SetX(collisedObject, r.Next(2, 20));
                            SetY(collisedObject, r.Next(2, 15));
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Miscellaneous.Properties;

namespace Miscellaneous.RPG_LoadScene
{
    class LoadAndDraw
    {
        static void FakeMain(string[] args)
        {
            object[][] sceneObjects = new object[0][];

            //Загрузка файла с диска
            //string file = File.ReadAllText("test.txt");

            //Загрузка файла из файла ресурсов (Resources.resx)
            string file = Resources.Scene1;

            int objectStartIndex = -1;
            int objectEndIndex = -1;
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i] == '[')
                {
                    objectStartIndex = i;
                }

                if (objectStartIndex != -1)
                {
                    if (file[i] == ']')
                    {
                        objectEndIndex = i;
                        string objectContent = file.Substring(objectStartIndex + 1, (objectEndIndex - objectStartIndex) - 1);

                        //
                        int x = 0;
                        int y = 0;
                        char symbol = ' ';

                        string[] properties = objectContent.Split('\n');

                        for (int j = 0; j < properties.Length; j++)
                        {
                            string[] propertie = properties[j].Split('=');

                            if (propertie.Length == 2)
                            {
                                if (propertie[0].Trim() == "x")
                                {
                                    x = Convert.ToInt32(propertie[1].Trim());
                                }
                                if (propertie[0].Trim() == "y")
                                {
                                    y = Convert.ToInt32(propertie[1].Trim());
                                }
                                if (propertie[0].Trim() == "symbol")
                                {
                                    symbol = Convert.ToChar(propertie[1].Trim());
                                }
                            }
                        }

                        Array.Resize(ref sceneObjects, sceneObjects.Length + 1);
                        sceneObjects[sceneObjects.Length - 1] = new object[] { x, y, symbol };


                        //
                        //Console.WriteLine(objectContent);
                    }
                }
            }

            for (int i = 0; i < sceneObjects.Length; i++)
            {
                Console.SetCursorPosition((int)sceneObjects[i][0], (int)sceneObjects[i][1]);
                Console.Write((char)sceneObjects[i][2]);
            }


            Console.SetCursorPosition(0, 25);
        }
    }
}

using System;
using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        bool isPlaying = true;

        int playerPositionX;
        int playerPositionY;
        int playerDirectionX = 0;
        int playerDirectionY = 0;

        char[,] map = ReadMap("map", out playerPositionX, out playerPositionY);

        DrawMap(map);

        while (isPlaying)
        {
            Console.SetCursorPosition(playerPositionY, playerPositionX);
            Console.Write('@');

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                ChangeDerection(key, ref playerDirectionX, ref playerDirectionY);
            }

            if (map[playerPositionX + playerDirectionX, playerPositionY + playerDirectionY] != '#')
            {
                Move(ref playerPositionX, ref playerPositionY, playerDirectionX, playerDirectionY);
            }

            System.Threading.Thread.Sleep(250);
        }
    }

    static void ChangeDerection(ConsoleKeyInfo key, ref int playerDirectionX, ref int playerDirectionY)
    {
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:

                playerDirectionX = -1; 
                playerDirectionY = 0;

                break;

            case ConsoleKey.DownArrow:

                playerDirectionX = 1;
                playerDirectionY = 0;

                break;

            case ConsoleKey.LeftArrow:

                playerDirectionX = 0; 
                playerDirectionY = -1;

                break;

            case ConsoleKey.RightArrow:

                playerDirectionX = 0; 
                playerDirectionY = 1;

                break;
        }
    }

    static void Move(ref int positionX, ref int positionY, int directionX, int directionY)
    {
        Console.SetCursorPosition(positionY, positionX);
        Console.Write(" ");

        positionX += directionX;
        positionY += directionY;

        Console.SetCursorPosition(positionY, positionX);
        Console.Write("@");
    }

    static void DrawMap(char[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j]);
            }

            Console.WriteLine();
        }
    }

    static char[,] ReadMap(string mapName, out int playerPositionX, out int playerPositionY)
    {
        playerPositionX = 0;
        playerPositionY = 0;
        string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
        char[,] map = new char[newFile.Length, newFile[0].Length];

        for (int i = 0; i < newFile.Length; i++)
        {
            for (int j = 0; j < newFile[0].Length; j++)
            {
                map[i, j] = newFile[i][j];

                if (map[i, j] == '@')
                {
                    playerPositionX = i;
                    playerPositionY = j;
                }
            }
        }

        return map;
    }
}

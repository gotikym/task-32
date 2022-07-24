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
        int playerDerectionX = 0;
        int playerDerectionY = 0;

        char[,] map = ReadMap("map", out playerPositionX, out playerPositionY);

        DrawMap(map);

        while (isPlaying)
        {
            Console.SetCursorPosition(playerPositionY, playerPositionX);
            Console.Write('@');

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                ChangeDerection(key, ref playerDerectionX, ref playerDerectionY);
            }

            if (map[playerPositionX + playerDerectionX, playerPositionY + playerDerectionY] != '#')
            {
                Move(ref playerPositionX, ref playerPositionY, playerDerectionX, playerDerectionY);
            }

            System.Threading.Thread.Sleep(250);
        }
    }

    static void ChangeDerection(ConsoleKeyInfo key, ref int dX, ref int dY)
    {
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                dX = -1; dY = 0;
                break;
            case ConsoleKey.DownArrow:
                dX = 1; dY = 0;
                break;
            case ConsoleKey.LeftArrow:
                dX = 0; dY = -1;
                break;
            case ConsoleKey.RightArrow:
                break;
        }
    }

    static void Move(ref int x, ref int y, int dX, int dY)
    {
        Console.SetCursorPosition(y, x);
        Console.Write(" ");

        x += dX;
        y += dY;

        Console.SetCursorPosition(y, x);
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

using System;
using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        bool isPlaying = true;

        int playerX;
        int playerY;
        int playerDX = 0;
        int playerDY = 0;

        char[,] map = ReadMap("map", out playerX, out playerY);

        DrawMap(map);
       
        Play(playerX, playerY, playerDX,playerDY, map, isPlaying);
    }

    static void Play(int playerX, int playerY, int playerDX, int playerDY, char[,] map, bool isPlaying)
    {
        while (isPlaying)
        {
            Console.SetCursorPosition(playerY, playerX);
            Console.Write('@');

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                ChangeDerection(key, ref playerDX, ref playerDY);
            }

            if (map[playerX + playerDX, playerY + playerDY] != '#')
            {
                Move(ref playerX, ref playerY, playerDX, playerDY);
            }

            System.Threading.Thread.Sleep(250);
        }
    }

    static void ChangeDerection(ConsoleKeyInfo key, ref int DX, ref int DY)
    {
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                DX = -1; DY = 0;
                break;
            case ConsoleKey.DownArrow:
                DX = 1; DY = 0;
                break;
            case ConsoleKey.LeftArrow:
                DX = 0; DY = -1;
                break;
            case ConsoleKey.RightArrow:
                DX = 0; DY = 1;
                break;
        }
    }

    static void Move(ref int X, ref int Y, int DX, int DY)
    {
        Console.SetCursorPosition(Y, X);
        Console.Write(" ");

        X += DX;
        Y += DY;

        Console.SetCursorPosition(Y, X);
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



    static char[,] ReadMap(string mapName, out int playerX, out int playerY)
    {
        playerX = 0;
        playerY = 0;
        string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
        char[,] map = new char[newFile.Length, newFile[0].Length];

        for (int i = 0; i < newFile.Length; i++)
        {
            for (int j = 0; j < newFile[0].Length; j++)
            {
                map[i, j] = newFile[i][j];

                if (map[i, j] == '@')
                {
                    playerX = i;
                    playerY = j;
                }
            }
        }
        return map;
    }
}
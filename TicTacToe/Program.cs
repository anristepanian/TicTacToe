using System;
using System.Diagnostics;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TicTacToe!";
            int n;
            while (true)
            {
                Console.Write("Choose the level (3 to 7): ");
                n = Convert.ToInt16(Console.ReadLine());
                if (n > 2 && n < 8) break;
            }
            Console.WriteLine();
            char x = 'X';
            int[] pos = { 0, 0 };
            char o = 'O';
            bool is_won = false;
            bool is_draw = false;
            char[,] map = new char[n, n];
            char loc = '-';
            char place = '^';
            int move = 0;
            int win_c = 0;
            for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) map[i, j] = loc;
            map[0, 0] = place;
            DrawMap(n, map);
            while (true)
            {
                Console.WriteLine();
                Console.Write("Make a move:\n");
                ConsoleKey act = Console.ReadKey().Key;
                switch (act)
                {
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        Up();
                        DrawMap(n, map);
                        break;
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        Down();
                        DrawMap(n, map);
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        Left();
                        DrawMap(n, map);
                        break;
                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        Right();
                        DrawMap(n, map);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Enter();
                        DrawMap(n, map);
                        break;
                    default: Environment.Exit(0); break;
                }
                if (is_won)
                {
                    if (move %  2 == 0) Console.WriteLine("Player O won!");
                    else Console.WriteLine("Player X won!");
                    break;
                }
                if (is_draw)
                {
                    Console.WriteLine("Draw!");
                    break;
                }
            }
            Console.Beep();
            Console.WriteLine("That was fun! Wasn't it??\nIf you want to play again press R\nIf you want to quit press Q");
            while (true)
            {
                ConsoleKey res = Console.ReadKey().Key;
                if (res == ConsoleKey.R) { RestartApp(1, "TicTacToe.exe"); Environment.Exit(0); }
                else if (res == ConsoleKey.Q) Environment.Exit(0);
                else Console.WriteLine("\nOk. Try again!");
            }
            void Up()
            {
                if (pos[0] != 0)
                {
                    map[pos[0], pos[1]] = loc;
                    pos[0] -= 1;
                    loc = map[pos[0], pos[1]];
                    map[pos[0], pos[1]] = place;
                }
            }
            void Down()
            {
                if (pos[0] != n - 1)
                {
                    map[pos[0], pos[1]] = loc;
                    pos[0] += 1;
                    loc = map[pos[0], pos[1]];
                    map[pos[0], pos[1]] = place;
                }
            }
            void Left()
            {
                if (pos[1] != 0)
                {
                    map[pos[0], pos[1]] = loc;
                    pos[1] -= 1;
                    loc = map[pos[0], pos[1]];
                    map[pos[0], pos[1]] = place;
                }
            }
            void Right()
            {
                if (pos[1] != n - 1)
                {
                    map[pos[0], pos[1]] = loc;
                    pos[1] += 1;
                    loc = map[pos[0], pos[1]];
                    map[pos[0], pos[1]] = place;
                }
            }
            void Enter()
            {
                if (loc == '-')
                {
                    if (move % 2 == 0) { map[pos[0], pos[1]] = x; loc = x; }
                    else { map[pos[0], pos[1]] = o; loc = o; }
                    move++;
                    game();
                }
            }
            void game()
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n - 1; j++)
                    {
                        if (map[i, j] == map[i, j + 1] && map[i, j] != '-') win_c++;
                        else win_c = 0;
                        if (win_c == n - 1) { is_won = true; return; }
                    }
                    win_c = 0;
                }
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n - 1; j++)
                    {
                        if (map[j, i] == map[j + 1, i] && map[j, i] != '-') win_c++;
                        else win_c = 0;
                        if (win_c == n - 1) { is_won = true; return; }
                    }
                    win_c = 0;
                }
                for (int i = 0; i < n - 1; i++)
                {
                    if (map[i, i] == map[i + 1, i + 1] && map[i, i] != '-') win_c++;
                    else win_c = 0;
                    if (win_c == n - 1) { is_won = true; return; }
                }
                win_c = 0;
                for (int j = n - 1; j > 0; j--)
                {
                    if (map[n - j - 1, j] == map[n - j, j - 1] && map[n - j - 1, j] != '-') win_c++;
                    else win_c = 0;
                    if (win_c == n - 1) { is_won = true; return; }
                }
                win_c = 0;
                for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) if (map[i, j] == '-') return;
                is_draw = true;
            }
        }
        public static void DrawMap(int diff, char[,] map)
        {
            for (int i = 0; i < diff; i++)
            {
                for (int j = 0; j < diff; j++)
                    Console.Write(map[i, j] + "\t");
                Console.WriteLine("\n");
            }
        }
        public static void RestartApp(int pid, string applicationName)
        {
            Process process = null;
            try
            {
                process = Process.GetProcessById(pid);
                process.WaitForExit(1000);
            }
            catch (ArgumentException) { }
            Process.Start(applicationName, "");
        }
    }
}

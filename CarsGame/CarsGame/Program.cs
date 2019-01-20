using System;
using System.Threading;
using static System.Console;

namespace CarsGame
{
    class MainClass
    {
        /// <summary>
        /// The game grid.
        /// </summary>
        public static char[,] gameGrid = {
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ' },
                };

        /// <summary>
        /// The player.
        /// </summary>
        public static Player player = new Player();

        /// <summary>
        /// The last x.
        /// </summary>
        public static int lastX = -1;

        /// <summary>
        /// The game speed.
        /// </summary>
        public static int gameSpeed = 400;

        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            Run();
        }

        public static void Run()
        {
            SetUpConsole();

            while (true)
                Draw();
        }

        /// <summary>
        /// Sets up console.
        /// </summary>
        public static void SetUpConsole()
        {
            //Hides cursor
            CursorVisible = false;
        }

        /// <summary>
        /// Draw this instance.
        /// </summary>
        public static void Draw()
        {
            //Clears Console
            Clear();

            //Adds a new entitiy
            AddNewEntity();

            //Prints the whole field
            for (int y = 0; y < 15; y++)
            {
                ForegroundColor = ConsoleColor.Black;
                Write("| ");
                for (int x = 0; x < 5; x++)
                {
                    if (player.Y == y && player.X == x) 
                    {
                        ForegroundColor = ConsoleColor.Red;
                        Write('@'.ToString() + " "); 
                    }
                    else
                    {
                        ForegroundColor = ConsoleColor.Green;
                        Write(gameGrid[y, x].ToString() + " ");
                    }
                }
                ForegroundColor = ConsoleColor.Black;
                Write("|");

                WriteLine(ReturnInfo(y));
            }


            if (KeyAvailable) //Checks for input
            {
                ConsoleKeyInfo keyInfo = ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.A:
                        player.X--;
                        break;
                    case ConsoleKey.D:
                        player.X++;
                        break;
                    case ConsoleKey.Spacebar:
                        SpeedUpGame();
                        break;
                }
            }

            //Puts the thread into sleep mode
            Thread.Sleep(gameSpeed);

            //Updates the field
            UpdateField();
        }

        /// <summary>
        /// Returns the info.
        /// </summary>
        /// <returns>The info.</returns>
        /// <param name="y">The y coordinate.</param>
        public static string ReturnInfo(int y)
        {
            string infoString = "";
            switch (y)
            {
                case 0:
                    infoString = "   ###############";
                    break;
                case 1:
                    infoString = "   #   A - <--   #";
                    break;
                case 2:
                    infoString = "   #   D - -->   #";
                    break;
                case 3:
                    infoString = "   # SPC - Speed #";
                    break;
                case 4:
                    infoString = "   ###############";
                    break;
            }
            return infoString;
        }

        /// <summary>
        /// Updates the field.
        /// </summary>
        public static void UpdateField()
        {
            //Update all layers except the first one starting from the bottom
            for (int y = 14; y > 0; y--)
            {
                for (int x = 0; x < 5; x++)
                {
                    gameGrid[y, x] = gameGrid[y - 1, x];
                }
            }

            //Update the first layer of the field
            for (int x = 0; x < 5; x++)
            {
                gameGrid[0, x] = ' ';
            }

            if (gameGrid[player.Y, player.X] == '#') InvokeGameOver();
        }

        /// <summary>
        /// Adds the new entity.
        /// </summary>
        public static void AddNewEntity()
        {
            int xPos = GenerateNewNumber(5);

            while(xPos == lastX)
                xPos = GenerateNewNumber(5);
            lastX = xPos;

            gameGrid[0, xPos] = GenerateNewNumber(1000) % 2 == 0 ? ' ' : '#';

            SpeedUpGame();
        }

        /// <summary>
        /// Speeds up game.
        /// </summary>
        public static void SpeedUpGame()
        {
            if (gameSpeed - 1 >= 10)
                gameSpeed--;
        }

        /// <summary>
        /// Generates the new number.
        /// </summary>
        /// <returns>The new number.</returns>
        /// <param name="border">Border.</param>
        public static int GenerateNewNumber(int border)
        {
            Random rnd = new Random();
            return rnd.Next(0, border);
        }

        /// <summary>
        /// Invokes the end game.
        /// </summary>
        public static void InvokeGameOver()
        {
            Clear();

            WriteLine(@"
   ____                                 ___                          _ 
  / ___|   __ _   _ __ ___     ___     / _ \  __   __   ___   _ __  | |
 | |  _   / _` | | '_ ` _ \   / _ \   | | | | \ \ / /  / _ \ | '__| | |
 | |_| | | (_| | | | | | | | |  __/   | |_| |  \ V /  |  __/ | |    |_|
  \____|  \__,_| |_| |_| |_|  \___|    \___/    \_/    \___| |_|    (_)
                                                                       ");
            Environment.Exit(0); //Exits the program with exit code 0
        }
    }
}

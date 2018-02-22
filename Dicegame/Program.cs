using System;
using System.Linq;

namespace Dicegame
{
    class Program
    {
        // Initialize random obj for throwing dice
        static Random random = new Random();

        // Initialize bet type
        static string[] BET_TYPE = {

            "Big",
            "Small",
            "Odd",
            "Even",
            "All 1s",
            "All 2s",
            "All 3s",
            "All 4s",
            "All 5s",
            "All 6s",
            "Double 1s",
            "Double 2s",
            "Double 3s",
            "Double 4s",
            "Double 5s",
            "Double 6s",
            "Any triples",
            "4 or 17",
            "5 or 16",
            "6 or 15",
            "7 or 14",
            "8 or 13",
            "9 or 12",
            "10 or 11"

        };

        // Define the seperator
        const string SEPERATOR = "====================================================================================";

        // Define the initial money amount
        const int INITIAL_MONEY_AMOUNT = 200;

        // Define the maximum money amount
        const int MAXIMUM_MONEY_AMOUNT = 100000;

        static void Main(string[] args)
        {
            // Set the size and title to the console
            InitializeConsole();

            // Start a game
            GameExecute();

            // Keep the window open for player
            Pause();
        }

        /// <summary>
        /// Set the size and title to the console
        /// </summary>
        static void InitializeConsole()
        {
            Console.SetWindowSize(100, 20);
            Console.Title = "Dice Shooter";
        }

        /// <summary>
        /// Start a game
        /// </summary>
        static void GameExecute()
        {
            // Show some message for the player
            Greeting();

            // Initialize gameContinueFlg. It is true until player selects not to continue
            bool continueGameFlg = true;

            // Keep start a game until the player want to finish it
            while (continueGameFlg)
            {
                // Initialize player's money amount
                int playerMoneyAmount = INITIAL_MONEY_AMOUNT;

                // Keep a game until the player lost all money or won 
                while (continueGameFlg)
                {
                    // Play once
                    playerMoneyAmount = PlayDiceGame(playerMoneyAmount);

                    // Check player's wallet. If the player lost all money or reach the limit, stop this game
                    if (playerMoneyAmount <= 0)
                    {
                        WriteMessageWithColorfulLine("You've run out of money!", ConsoleColor.Yellow, ConsoleColor.Blue);
                        break;
                    }
                    else if (MAXIMUM_MONEY_AMOUNT <= playerMoneyAmount)
                    {
                        WriteMessageWithColorfulLine("Oh no, you broke the bank! Don't come back!", ConsoleColor.Yellow, ConsoleColor.Magenta);
                        break;
                    }

                }

                Pause();
                Console.Clear();

                // Ask the player whether play again
                if (!PlayerWantToPlayAgain())
                {
                    // Stop playing
                    continueGameFlg = false;
                }

                Console.Clear();

            }

            WriteMessageWithColorfulLine("See you again!");
        }
        
        /// <summary>
        /// Show some message for the player
        /// </summary>
        static void Greeting()
        {
            // Greeting player
            WriteMessageWithColorfulLine("††† Welcome to the Back Alley †††");

            // Show the game infomation in different color
            Console.ForegroundColor = ConsoleColor.Green;
            WriteMessageCenter("(press Enter to continue, press Ctrl+C to finish)");
            Console.ResetColor();

            Pause();
            Console.Clear();

            // Ask player name
            Console.Write("Tell me your name:");
            string playerName = GetPlayerInput();
            Console.Clear();

            // Greeting again
            Console.WriteLine("Welcome, {0}!", playerName);

            Pause();
            Console.Clear();
        }

        /// <summary>
        /// Write a message with colorful lines. This method will set default theme color
        /// </summary>
        /// <param name="message">message</param>
        static void WriteMessageWithColorfulLine(string message)
        {
            WriteMessageWithColorfulLine(message, ConsoleColor.DarkMagenta, ConsoleColor.Blue);
        }

        /// <summary>
        /// Write a message with colorful lines. This method allow to be set different color from the theme color
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="foregroundColor">color line foregroundColor</param>
        /// <param name="backgroundColor">color line backgroundColor</param>
        static void WriteMessageWithColorfulLine(string message, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write("░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█");
            Console.Write("▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█░▒");
            Console.WriteLine("█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓░▒");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            WriteMessageCenter(message);

            Console.ResetColor();
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█");
            Console.Write("▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█░▒");
            Console.WriteLine("█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓░▒");
            Console.WriteLine();
            Console.ResetColor();
        }

        /// <summary>
        /// Play the game onece
        /// </summary>
        /// <param name="playerMoneyAmount">all money amount in the wallet of player</param>
        /// <returns>remained money amount</returns>
        static int PlayDiceGame(int playerMoneyAmount)
        {
            // Show the initial money amount
            Console.WriteLine("You have: ${0}", playerMoneyAmount);

            // Show all alternative
            ShowAllBetType();

            // Ask the player which bet type to use
            Console.WriteLine("What bet would you like to make?");

            // Keep the selected bet type name
            int selectedBetTypeIndex = GetPlayerInputBetType();

            // Confirm the selected amount and ask how much to bet.
            Console.WriteLine("You selected\"{0}\", how much would you like to bet?", BET_TYPE[selectedBetTypeIndex-1]);
            int betAmount = GetPlayerInputBetAmount(playerMoneyAmount);
            
            Console.Clear();

            // Roll dice and get the result sum
            //int[] diceResults = RollDice();
            int[] diceResults = { 1, 1, 1};

            Console.Clear();

            // Initialize a variable to store odds
            int odds = 0;

            // judge if player won or not
            if (IsWin(selectedBetTypeIndex, diceResults, out odds))
            {
                // add result amount to player's wallet
                int returnedMoney = betAmount * odds;
                playerMoneyAmount += returnedMoney;

                // show a win message for player!
                Console.WriteLine("You won {0}! Your new total is {1}", returnedMoney, playerMoneyAmount);
            }
            else
            {
                // subtract the bet amount from the wallet of player
                playerMoneyAmount -= betAmount;

                // show lost message for player
                Console.WriteLine("You lost {0}! Your new total is {1}", betAmount, playerMoneyAmount);
            }

            Pause();
            Console.Clear();

            return playerMoneyAmount;
        }

        /// <summary>
        /// Roll 3 dices and give the result back
        /// </summary>
        /// <returns>Dice results</returns>
        static int[] RollDice()
        {
            // Show messages
            Console.WriteLine(SEPERATOR);
            Console.Write("Rolling dice...");

            // Rolling Dice animation
            Console.CursorVisible = false;
            char[] bars = { '/', '-', '\\', '|' };
            for (int i = 0; i < 100; i++)
            {
                Console.Write(bars[i % 4]);

                Console.SetCursorPosition(15, Console.CursorTop);

                System.Threading.Thread.Sleep(10);
            }
            Console.CursorVisible = true;

            Console.Clear();

            // Dice Rolling
            int[] diceResults = {
                random.Next(1, 7),
                random.Next(1, 7),
                random.Next(1, 7)
            };

            // Show Dice results
            Console.WriteLine(SEPERATOR);
            for (int i = 0; i < diceResults.Length; i++)
            {
                Console.WriteLine("Dice {0}: {1}", i, diceResults[i]);
            }

            // Show Sum of the dice results
            Console.WriteLine("Sum: {0}", diceResults.Sum());
            Console.WriteLine(SEPERATOR);

            Pause();

            return diceResults;

        }

        /// <summary>
        /// Show all bet type
        /// </summary>
        static void ShowAllBetType()
        {
            Console.WriteLine(SEPERATOR);

            int i = 1;

            // Write all bet type using Enum
            foreach (string betTypeName in BET_TYPE)
            {
                string option = i + ". " + betTypeName;
                

                // Format the options and change a line per 4 options
                if (i % 4 == 0)
                {
                    Console.WriteLine(option.PadRight(20, ' '));
                }
                else
                {
                    Console.Write(option.PadRight(20, ' '));
                }
                i++;
            }

            Console.WriteLine(SEPERATOR);
        }

        /// <summary>
        /// Judge the bet result
        /// </summary>
        /// <param name="selectedBetTypeIndex">bet type</param>
        /// <param name="diceResults">dice result array</param>
        /// <param name="odds">variable for put the result odds</param>
        /// <returns></returns>
        static bool IsWin(int selectedBetTypeIndex, int[] diceResults, out int odds)
        {
            // switch the judge condition by selected bet type
            switch (selectedBetTypeIndex)
            {
                // Big
                case 1:

                    // except a triple
                    if (IsAnyTriples(diceResults))
                    {
                        odds = 0;
                        return false;
                    }

                    odds = 1;
                    return IsSumBetweeXYInclusive(diceResults, 11, 17);
                
                // Small
                case 2:

                    // except a triple
                    if (IsAnyTriples(diceResults))
                    {
                        odds = 0;
                        return false;
                    }

                    odds = 1;
                    return IsSumBetweeXYInclusive(diceResults, 4, 10);

                // Odd
                case 3:

                    // except a triple
                    if (IsAnyTriples(diceResults))
                    {
                        odds = 0;
                        return false;
                    }

                    odds = 1;
                    return IsOdd(diceResults);

                // Even
                case 4:

                    // except a triple
                    if (IsAnyTriples(diceResults))
                    {
                        odds = 0;
                        return false;
                    }

                    odds = 1;
                    return IsEven(diceResults);

                // All 1s
                case 5:
                    odds = 180;
                    return IsSpecificTriples(diceResults, 1);

                // All 2s
                case 6:
                    odds = 180;
                    return IsSpecificTriples(diceResults, 2);

                // All 3s
                case 7:
                    odds = 180;
                    return IsSpecificTriples(diceResults, 3);

                // All 4s
                case 8:
                    odds = 180;
                    return IsSpecificTriples(diceResults, 4);

                // All 5s
                case 9:
                    odds = 180;
                    return IsSpecificTriples(diceResults, 5);

                // All 6s
                case 10:
                    odds = 180;
                    return IsSpecificTriples(diceResults, 6);

                // Double 1s
                case 11:
                    // except a triple
                    if (IsAnyTriples(diceResults))
                    {
                        odds = 0;
                        return false;
                    }
                    odds = 10;
                    return IsSpecificDoubles(diceResults, 1);

                // Double 2s
                case 12:
                    // except a triple
                    if (IsAnyTriples(diceResults))
                    {
                        odds = 0;
                        return false;
                    }
                    odds = 10;
                    return IsSpecificDoubles(diceResults, 2);

                // Double 3s
                case 13:
                    // except a triple
                    if (IsAnyTriples(diceResults))
                    {
                        odds = 0;
                        return false;
                    }
                    odds = 10;
                    return IsSpecificDoubles(diceResults, 3);

                // Double 4s
                case 14:
                    // except a triple
                    if (IsAnyTriples(diceResults))
                    {
                        odds = 0;
                        return false;
                    }
                    odds = 10;
                    return IsSpecificDoubles(diceResults, 4);

                // Double 5s
                case 15:
                    // except a triple
                    if (IsAnyTriples(diceResults))
                    {
                        odds = 0;
                        return false;
                    }
                    odds = 10;
                    return IsSpecificDoubles(diceResults, 5);

                // Double 6s
                case 16:
                    // except a triple
                    if (IsAnyTriples(diceResults))
                    {
                        odds = 0;
                        return false;
                    }
                    odds = 10;
                    return IsSpecificDoubles(diceResults, 6);

                // Any triples
                case 17:
                    odds = 30;
                    return IsAnyTriples(diceResults);

                // 4 or 17
                case 18:
                    odds = 60;
                    return IsThreeDiceTotalXorY(diceResults, 4, 17);

                // 5 or 16
                case 19:
                    odds = 30;
                    return IsThreeDiceTotalXorY(diceResults, 5, 16);

                // 6 or 15
                case 20:
                    odds = 18;
                    return IsThreeDiceTotalXorY(diceResults, 6, 15);

                // 7 or 14
                case 21:
                    odds = 12;
                    return IsThreeDiceTotalXorY(diceResults, 7, 14);

                // 8 or 13
                case 22:
                    odds = 8;
                    return IsThreeDiceTotalXorY(diceResults, 8, 13);

                // 9 or 12
                case 23:
                    odds = 7;
                    return IsThreeDiceTotalXorY(diceResults, 9, 12);

                // 10 or 11
                case 24:
                    odds = 6;
                    return IsThreeDiceTotalXorY(diceResults, 10, 11);


                default:
                    // Impossible
                    throw new SystemException();
            }
        }

        /// <summary>
        /// Ask player whether play the game again or not
        /// </summary>
        /// <returns>true: continue the game, false: finish the game</returns>
        static bool PlayerWantToPlayAgain()
        {

            Console.WriteLine("Would you like to play again?(y/n)");

            // Get player input
            string playerAnswer = GetPlayerInput();

            // Check if player want to play again or not. Unless player say y or yes, finish the game
            if (string.Equals(playerAnswer, "Yes", StringComparison.OrdinalIgnoreCase)
                || string.Equals(playerAnswer, "Y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get how much money does player want to bet
        /// </summary>
        /// <param name="playerMoneyAmount">the money amount in the player's poket</param>
        /// <returns>the amount player want to bet</returns>
        static int GetPlayerInputBetAmount(int playerMoneyAmount)
        {
            // Get player input
            int betAmount = GetPlayerInputInt();

            // Check bet amount is less than the money amount that player have
            if (playerMoneyAmount < betAmount)
            {
                WriteErrorMessage("There is only $"+ playerMoneyAmount + " you have!");
                WriteErrorMessage("How much would you like to bet?");
                return GetPlayerInputBetAmount(playerMoneyAmount);
            }
            else if (betAmount < 0)
            {
                WriteErrorMessage("You cannot bet minus amount:c");
                WriteErrorMessage("How much would you like to bet?");
                return GetPlayerInputBetAmount(playerMoneyAmount);
            }

            return betAmount;
        }

        /// <summary>
        /// Get which bet type does player want to select
        /// </summary>
        /// <returns>selected bet type number</returns>
        static int GetPlayerInputBetType()
        {
            // Get player input
            int selectedBetType = GetPlayerInputInt();

            // Check the input is in the Enum
            if (selectedBetType < 1 || BET_TYPE.Length < selectedBetType)
            {
                WriteErrorMessage("You selected No." + selectedBetType + " which is not in the bet list.");
                WriteErrorMessage("Please choose the defined bet type.");
                return GetPlayerInputBetType();
            }

            return selectedBetType;
        }


        /// <summary>
        /// Check the dice result. 
        /// </summary>
        /// <param name="diceResults">dice result array</param>
        /// <param name="x">min number of the range</param>
        /// <param name="y">max number of the range</param>
        /// <returns>True: sum is between the range</returns>
        static bool IsSumBetweeXYInclusive(int[] diceResults,int x, int y)
        {
            // Calculate the sum
            int sum = diceResults.Sum();

            // Return if the sum is in the range or not
            return x <= sum && sum <= y;
        }

        /// <summary>
        /// Check the dice result. 
        /// </summary>
        /// <param name="diceResults">dice result array</param>
        /// <param name="x">a number for judging hit</param>
        /// <param name="y">a number for judging hit</</param>
        /// <returns>True: sum is between the range</returns>
        static bool IsThreeDiceTotalXorY(int[] diceResults, int x, int y)
        {
            // Calculate the sum
            int sum = diceResults.Sum();

            // Return if the sum is in the range or not
            return x == sum || sum == y;
        }

        /// <summary>
        /// Check the dice result.
        /// </summary>
        /// <param name="diceResults">dice result array</param>
        /// <returns>True: the sum is odd</returns>
        static bool IsOdd(int[] diceResults)
        {
            // Calculate the sum
            int sum = diceResults.Sum();

            // Return if the sum is odd or not
            return sum % 2 == 1;
        }

        /// <summary>
        /// Check the dice result.
        /// </summary>
        /// <param name="diceResults">dice result array</param>
        /// <returns>True: the sum is even</returns>
        static bool IsEven(int[] diceResults)
        {
            // Calculate the sum
            int sum = diceResults.Sum();

            // Return if the sum is odd or not
            return sum % 2 == 0;
        }

        /// <summary>
        /// Check the dice result.
        /// </summary>
        /// <param name="diceResults">dice result array</param>
        /// <param name="specificNum">any num between 1 to 6</param>
        /// <returns>True: the dices are all same number with the specific num</returns>
        static bool IsSpecificTriples(int[] diceResults, int specificNum)
        {
            // Check all of the dice
            for (int i = 0; i < diceResults.Length; i++)
            {
                // if the dice result is not the specific num
                if (diceResults[i] != specificNum)
                {
                    return false;
                }
            }

            return true;

        }

        /// <summary>
        /// Check the dice result.
        /// </summary>
        /// <param name="diceResults">dice result array</param>
        /// <returns>True: the dices are all same number</returns>
        static bool IsAnyTriples(int[] diceResults)
        {
            // Store the first value
            int pre = diceResults[0];

            // Check all value whether it is as same as the first one or not
            for (int i = 1; i < diceResults.Length; i++)
            {
                if (pre != diceResults[i])
                {
                    return false;
                }
            }

            return true;

        }

        /// <summary>
        /// Check the dice result.
        /// </summary>
        /// <param name="diceResults">dice result array</param>
        /// <param name="specificNum">any num between 1 to 6</param>
        /// <returns>True: two of the dices are same number with the specific num</returns>
        static bool IsSpecificDoubles(int[] diceResults, int specificNum)
        {
            // Initialise a variable to count how many dice result are the specific num
            int count = 0;

            // Check all dices and count how many the specific num was appear
            for (int i = 0; i < diceResults.Length; i++)
            {
                if (diceResults[i] == specificNum)
                {
                    count += 1;
                }
            }

            // If the specific num appear 2 times
            if (count == 2)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///  Get Player input until Player inputs integer
        /// </summary>
        /// <returns>Player input (int)</returns>
        static int GetPlayerInputInt()
        {
            // Initialize a integer valuable for checking
            int parsedInput;

            // Get Player input
            string PlayerInput = GetPlayerInput();

            // Check Player input is integer or not
            if (!int.TryParse(PlayerInput, out parsedInput))
            {
                // It is not integer
                // Show error message for Player
                WriteErrorMessage("Please input integer");

                // Get Player input again
                return GetPlayerInputInt();
            }

            // Return the Player input as int
            return parsedInput;
        }


        /// <summary>
        ///  Get Player input until Player inputs something
        /// </summary>
        /// <returns>Player input (string)</returns>
        static string GetPlayerInput()
        {

            SetColorForInput();

            // Read Player input
            string PlayerInput = Console.ReadLine();

            // Check Player input has string or empty
            if (string.IsNullOrEmpty(PlayerInput))
            {
                // It is empty
                // Show error message for Player
                WriteErrorMessage("Please type something and press ENTER");

                // Get Player input again
                PlayerInput = GetPlayerInput();
            }

            Console.ResetColor();

            // Return the Player input as string
            return PlayerInput;
        }

        /// <summary>
        /// Set the text color for the player input
        /// </summary>
        static void SetColorForInput()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        /// <summary>
        /// Set the text color for the error message
        /// </summary>
        static void SetColorForError()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }

        /// <summary>
        /// Show a error message for the user 
        /// </summary>
        /// <param name="message">a error message</param>
        static void WriteErrorMessage(string message)
        {
            SetColorForError();
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Show a message for the user by centered style
        /// </summary>
        /// <param name="message">a message which want to be shown in centered style</param>
        static void WriteMessageCenter(string message)
        {
            // Find center by the console size and subtract the message length from it
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message);
        }

        /// <summary>
        ///  Pause the program for showing console
        /// </summary>
        static void Pause()
        {
            // Pause the program
            Console.ReadLine();
        }
    }
}

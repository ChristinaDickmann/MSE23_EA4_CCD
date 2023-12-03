using System;

namespace TicTacToe
{
    internal static class Program
    {
        private static GameState _gameState;
        private static ActivePlayer _activePlayer;

        private static readonly Random Random = new Random();

        private static readonly GameBoard GameBoard = new GameBoard();

        static void Main(string[] args)
        {
            CreateGameSetup();

            do
            {
                CreateGameRound();
            }
            while (_gameState is GameState.Play);

            CreateGameEnd();
        }

        private static void CreateGameSetup()
        {
            Console.WriteLine("Let's play Tic Tac Toe!");
            Console.WriteLine("Player: X  -  Computer: O");
            Console.WriteLine("Player will start.");

            _activePlayer = ActivePlayer.Player;
        }

        private static void CreateGameRound()
        {
            switch (_activePlayer)
            {
                case ActivePlayer.Player:
                    PlayerAction();
                    break;
                case ActivePlayer.Computer:
                    ComputerAction();
                    break;
                default:
                    Console.WriteLine("Unknown Exception: Active Player unclear. Please start again.");
                    break;
            }
        }

        private static void CreateGameEnd()
        {
            Console.Clear();
            GameBoard.DrawCurrentGrid();

            if (_gameState == GameState.Won)
                Console.WriteLine("The winner is {0}", _activePlayer);
            else
                Console.WriteLine("The game is tied.");
        }

        private static void PlayerAction()
        {
            Console.WriteLine('\n');
            Console.WriteLine("Player, please choose an empty field by typing a number between 1 and 9.");
            Console.WriteLine("This is the current grid:");
            GameBoard.DrawCurrentGrid();

            var input = Console.ReadLine();
            int.TryParse(input, out var chosenField);

            if (!InputAllowed(chosenField))
            {
                Console.WriteLine("Wrong input. You have to choose a number between 1 and 9.");
                return;
            }
            else
            {
                if (GameBoard.IsFieldFree(chosenField))
                {
                    GameBoard.SetField(chosenField, _activePlayer);
                }
                else
                {
                    Console.WriteLine("This field is already occupied. Please choose another.");
                    return;
                }
            }
            
            UpdateGameValues();
        }

        private static bool InputAllowed(int input)
        {
            return input >= 1 && input <= 9;
        }

        private static void ComputerAction()
        {
            var chosenField = ChooseField();

            GameBoard.SetField(chosenField, _activePlayer);

            Console.WriteLine('\n');
            Console.WriteLine("The computer has chosen field {0}", chosenField);

            UpdateGameValues();
        }

        private static void UpdateGameValues()
        {
            _gameState = GetCurrentGameState();

            if (_gameState != GameState.Play)
                return;

            switch (_activePlayer)
            {
                case ActivePlayer.Player:
                    _activePlayer = ActivePlayer.Computer;
                    break;
                case ActivePlayer.Computer:
                    _activePlayer = ActivePlayer.Player;
                    break;
                default:
                    Console.WriteLine("Unknown Exception: Active Player unclear. Please start again.");
                    break;
            }
        }

        private static GameState GetCurrentGameState()
        {
            if (IsGameWon())
                return GameState.Won;

            if (GameBoard.HasOnlyFullFields())
                return GameState.Tied;

            return GameState.Play;
        }

        private static bool IsGameWon()
        {
            return (GameBoard.HasFullRow()
                    || GameBoard.HasFullColumn()
                    || GameBoard.HasFullDiagonal());
        }

        private static int ChooseField()
        {
            var freeFields = GameBoard.GetFreeFields();
            var rand = Random.Next(0, freeFields.Count - 1);

            return freeFields[rand];
        }
    }
}

using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class GameBoard
    {
        private readonly char[] _startingValues = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private readonly char[] _gameFields = new char[9];

        private const char PlayerSign = 'X';
        private const char ComputerSign = 'O';

        public GameBoard()
        {
            _startingValues.CopyTo(_gameFields, 0);
        }

        public bool IsFieldFree(int field)
        {
            if (field < 1 || field > 9) 
                throw new ArgumentOutOfRangeException(nameof(field));

            return (_gameFields[field - 1] != PlayerSign)
                   && (_gameFields[field - 1] != ComputerSign);
        }

        public void SetField(int field, ActivePlayer activePlayer)
        {
            if (field < 1 || field > 9)
                throw new ArgumentOutOfRangeException(nameof(field));

            if (activePlayer == ActivePlayer.Computer)
                _gameFields[field - 1] = ComputerSign;
            else if (activePlayer == ActivePlayer.Player)
                _gameFields[field - 1] = PlayerSign;
        }

        public List<int> GetFreeFields()
        {
            var freeFields = new List<int>();
            for (var i = 0; i < _gameFields.Length; i++)
            {
                if (_gameFields[i] == _startingValues[i])
                {
                    freeFields.Add(i+1);
                }
            }

            return freeFields;
        }

        public void DrawCurrentGrid()
        {
            Console.WriteLine("   |   |   |");
            Console.WriteLine(" {0} | {1} | {2} |", _gameFields[0], _gameFields[1], _gameFields[2]);
            Console.WriteLine("___|___|___|");
            Console.WriteLine("   |   |   |");
            Console.WriteLine(" {0} | {1} | {2} |", _gameFields[3], _gameFields[4], _gameFields[5]);
            Console.WriteLine("___|___|___|");
            Console.WriteLine("   |   |   |");
            Console.WriteLine(" {0} | {1} | {2} |", _gameFields[6], _gameFields[7], _gameFields[8]);
            Console.WriteLine("   |   |   |");
        }

        public bool HasFullRow()
        {
            // Top row
            if (AreFieldsOfSameValue(0, 1, 2))
                return true;

            // Middle row
            if (AreFieldsOfSameValue(3, 4, 5))
                return true;

            // Bottom row
            if (AreFieldsOfSameValue(6, 7, 8))
                return true;

            return false;
        }

        public bool HasFullColumn()
        {
            // Left column
            if (AreFieldsOfSameValue(0, 3, 6))
                return true;

            // Middle column
            if (AreFieldsOfSameValue(1, 4, 7))
                return true;

            // Bottom column
            if (AreFieldsOfSameValue(2, 5, 8))
                return true;

            return false;
        }

        public bool HasFullDiagonal()
        {
            // Top left to bottom right
            if (AreFieldsOfSameValue(0, 4, 8))
                return true;

            // Bottom left to top right
            if (AreFieldsOfSameValue(2, 4, 6))
                return true;

            return false;
        }

        public bool HasOnlyFullFields()
        {
            if (IsStartingValue(0) && IsStartingValue(1) && IsStartingValue(2)
                && IsStartingValue(3) && IsStartingValue(4) && IsStartingValue(5)
                && IsStartingValue(6) && IsStartingValue(7) && IsStartingValue(8))
                return true;

            return false;
        }

        private bool AreFieldsOfSameValue(int fieldA, int fieldB, int fieldC)
        {
            return _gameFields[fieldA] == _gameFields[fieldB]
                   && _gameFields[fieldB] == _gameFields[fieldC];
        }

        private bool IsStartingValue(int field)
        {
            return _gameFields[field] == _startingValues[field];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateTicTacToeBot
{
    /// <summary>
    /// Handles everything that has to do with the field, such as storing 
    /// the current state and performing calculations on the field.
    /// </summary>
    class Field
    {
        public const String EmptyField = ".";
        public const String AvailableField = "-1";
        public const int COLS = 9;
        public const int ROWS = 9;

        public int MyId { get; set; }
        public int OpponentId { get; set; }      
        
        private String[,] board;
        private String[,] macroboard;

        public Field()
        {
            board = new String[COLS, ROWS];
            macroboard = new String[COLS / 3, ROWS / 3];
            ClearBoard();
        }

        /// <summary>
        /// Initialise field from comma separated String
        /// </summary>
        /// <param name="s"></param>
        public void ParseFromString(String s)
        {
            s = s.Replace(";", ",");
            String[] r = s.Split(',');
            int counter = 0;
            for (int y = 0; y < ROWS; y++)
            {
                for (int x = 0; x < COLS; x++)
                {
                    board[x, y] = r[counter];
                    counter++;
                }
            }
        }

        /// <summary>
        /// Initialise macroboard from comma separated String
        /// </summary>
        /// <param name="s"></param>
        public void ParseMacroboardFromString(String s)
        {
            String[] r = s.Split(',');
            int counter = 0;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    macroboard[x, y] = r[counter];
                    counter++;
                }
            }
        }

        public void ClearBoard()
        {
            for (int x = 0; x < COLS; x++)
            {
                for (int y = 0; y < ROWS; y++)
                {
                    board[x, y] = EmptyField;
                }
            }
        }

        public List<Move> GetAvailableMoves()
        {
            var moves = new List<Move>();

            for (int y = 0; y < ROWS; y++)
            {
                for (int x = 0; x < COLS; x++)
                {
                    if (IsInActiveMicroboard(x, y) && board[x, y] == EmptyField)
                    {
                        moves.Add(new Move(x, y));
                    }
                }
            }

            return moves;
        }

        public Boolean IsInActiveMicroboard(int x, int y)
        {
            return macroboard[x / 3, y / 3] == AvailableField;
        }
        

        /// <summary>
        /// Creates comma separated String with player ids for the microboards.
        /// </summary>
        /// <returns>String with player names for every cell, or 'empty' when cell is empty.</returns>
        override public String ToString()
        {
            var r = new StringBuilder("");
            int counter = 0;
            for (int y = 0; y < ROWS; y++)
            {
                for (int x = 0; x < COLS; x++)
                {
                    if (counter > 0)
                    {
                        r.Append(",");
                    }
                    r.Append(board[x, y]);
                    counter++;
                }
            }
            return r.ToString();
        }


        /// <summary>
        /// Checks whether the field is full
        /// </summary>
        /// <returns>Returns true when field is full, otherwise returns false</returns>
        public bool IsFull()
        {
            for (int x = 0; x < COLS; x++)
                for (int y = 0; y < ROWS; y++)
                    if (board[x, y] == EmptyField)
                        return false; // At least one cell is not filled

            // All cells are filled
            return true;
        }
        
        public bool IsEmpty()
        {
            for (int x = 0; x < COLS; x++)
            {
                for (int y = 0; y < ROWS; y++)
                {
                    if (board[x, y] != EmptyField)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Returns the player id for the given column and row
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public String GetPlayerId(int column, int row)
        {
            return board[column, row];
        }
    }
}

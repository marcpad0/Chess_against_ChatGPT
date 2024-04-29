using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Chess
{
    public abstract class ChessPiece
    {
        public bool Isblack { get; set; }
        public int row { get; set; }
        public int column { get; set; }

        public ChessPiece(bool Isblack, int row, int column)
        {
            this.Isblack = Isblack;
            this.row = row;
            this.column = column;
        }

        protected static void PrintMatrix(ChessPiece[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("----------------------------------------------------------");
        }

        public abstract List<(int, int)> GetAvailableMoves(ChessPiece[,] board);

        public ChessPiece Clone()
        {
            return (ChessPiece)this.MemberwiseClone();
        }
    }

    public class Pawn : ChessPiece
    {
        public bool EnPassantEligible { get; set; } 

        public Pawn(bool Isblack, int row, int column) : base(Isblack, row, column)
        {
        }

        public override string ToString()
        {
            return Isblack ? "P1" : "P2";
        }

        public override List<(int, int)> GetAvailableMoves(ChessPiece[,] board)
        {
            PrintMatrix(board);
            List<(int, int)> availableMoves = new List<(int, int)>();

            int direction = Isblack ? -1 : 1;

            if (IsOnBoard(row + direction, column) && board[row + direction, column] == null)
            {
                availableMoves.Add((row + direction, column));

                if ((Isblack && row == 6) || (!Isblack && row == 1))
                {
                    if (board[row + direction * 2, column] == null)
                    {
                        availableMoves.Add((row + direction * 2, column));
                        EnPassantEligible = true; 
                    }
                }
            }

            CheckCaptureMove(board, availableMoves, row + direction, column - 1);
            CheckCaptureMove(board, availableMoves, row + direction, column + 1);

            CheckEnPassantMove(board, availableMoves, row, column - 1, direction);
            CheckEnPassantMove(board, availableMoves, row, column + 1, direction);

            return availableMoves;
        }

        private void CheckCaptureMove(ChessPiece[,] board, List<(int, int)> availableMoves, int targetRow, int targetColumn)
        {
            if (IsOnBoard(targetRow, targetColumn) && board[targetRow, targetColumn] != null && board[targetRow, targetColumn].Isblack != Isblack)
            {
                availableMoves.Add((targetRow, targetColumn));
            }
        }

        private void CheckEnPassantMove(ChessPiece[,] board, List<(int, int)> availableMoves, int currentRow, int targetColumn, int direction)
        {
            if (IsOnBoard(currentRow, targetColumn) && board[currentRow, targetColumn] is Pawn pawn && pawn.Isblack != Isblack && pawn.EnPassantEligible)
            {
                availableMoves.Add((currentRow + direction, targetColumn));
            }
        }

        private bool IsOnBoard(int row, int column)
        {
            return row >= 0 && row < 8 && column >= 0 && column < 8;
        }

        public void PromotePawn(ChessPiece[,] board, int targetRow, int targetColumn, Type promotedPieceType)
        {
            board[row, column] = null;

            ChessPiece promotedPiece = (ChessPiece)Activator.CreateInstance(promotedPieceType, new object[] { Isblack, targetRow, targetColumn });
            board[targetRow, targetColumn] = promotedPiece;
        }

    }

    public class Tower : ChessPiece
    {
        public Tower(bool iswhite, int row, int column) : base(iswhite, row, column)
        {
        }

        public override string ToString()
        {
            return Isblack ? "T1" : "T2";
        }

        public override List<(int, int)> GetAvailableMoves(ChessPiece[,] board)
        {
            PrintMatrix(board);
            List<(int, int)> availableMoves = new List<(int, int)>();

            CheckDirection(board, availableMoves, 0, 1);
            CheckDirection(board, availableMoves, 0, -1);

            CheckDirection(board, availableMoves, 1, 0);
            CheckDirection(board, availableMoves, -1, 0);

            return availableMoves;
        }

        private void CheckDirection(ChessPiece[,] board, List<(int, int)> availableMoves, int rowOffset, int colOffset)
        {
            int newRow = row + rowOffset;
            int newCol = column + colOffset;

            while (IsOnBoard(newRow, newCol))
            {
                if (board[newRow, newCol] == null)
                {
                    availableMoves.Add((newRow, newCol));
                }
                else
                {
                    if (board[newRow, newCol].Isblack != Isblack)
                    {
                        availableMoves.Add((newRow, newCol));
                    }
                    break;
                }

                newRow += rowOffset;
                newCol += colOffset;
            }
        }

        private bool IsOnBoard(int row, int column)
        {
            return row >= 0 && row < 8 && column >= 0 && column < 8;
        }
    }

    public class Horse : ChessPiece
    {
        public Horse(bool iswhite, int row, int column) : base(iswhite, row, column)
        {
        }

        public override List<(int, int)> GetAvailableMoves(ChessPiece[,] board)
        {
            PrintMatrix(board);
            List<(int, int)> availableMoves = new List<(int, int)>();
            CheckMove(board, availableMoves, row + 2, column + 1);
            CheckMove(board, availableMoves, row + 2, column - 1);
            CheckMove(board, availableMoves, row - 2, column + 1);
            CheckMove(board, availableMoves, row - 2, column - 1);
            CheckMove(board, availableMoves, row + 1, column + 2);
            CheckMove(board, availableMoves, row + 1, column - 2);
            CheckMove(board, availableMoves, row - 1, column + 2);
            CheckMove(board, availableMoves, row - 1, column - 2);
            return availableMoves;
        }

        private void CheckMove(ChessPiece[,] board, List<(int, int)> availableMoves, int targetRow, int targetColumn)
        {
            if (IsOnBoard(targetRow, targetColumn) && (board[targetRow, targetColumn] == null || board[targetRow, targetColumn].Isblack != Isblack))
            {
                availableMoves.Add((targetRow, targetColumn));
            }
        }

        private bool IsOnBoard(int row, int column)
        {
            return row >= 0 && row < 8 && column >= 0 && column < 8;
        }

        public override string ToString()
        {
            return Isblack ? "H1" : "H2";
        }
    }

    public class Bishop : ChessPiece
    {
        public Bishop(bool iswhite, int row, int column) : base(iswhite, row, column)
        {
        }
        public override List<(int, int)> GetAvailableMoves(ChessPiece[,] board)
        {
            PrintMatrix(board);
            List<(int, int)> availableMoves = new List<(int, int)>();
            CheckDirection(board, availableMoves, 1, 1);
            CheckDirection(board, availableMoves, 1, -1);
            CheckDirection(board, availableMoves, -1, 1);
            CheckDirection(board, availableMoves, -1, -1);
            return availableMoves;
        }
        private void CheckDirection(ChessPiece[,] board, List<(int, int)> availableMoves, int rowOffset, int colOffset)
        {
            int newRow = row + rowOffset;
            int newCol = column + colOffset;
            while (IsOnBoard(newRow, newCol))
            {
                if (board[newRow, newCol] == null)
                {
                    availableMoves.Add((newRow, newCol));
                }
                else
                {
                    if (board[newRow, newCol].Isblack != Isblack)
                    {
                        availableMoves.Add((newRow, newCol));
                    }
                    break;
                }
                newRow += rowOffset;
                newCol += colOffset;
            }
        }
        private bool IsOnBoard(int row, int column)
        {
            return row >= 0 && row < 8 && column >= 0 && column < 8;
        }
        public override string ToString()
        {
            return Isblack ? "B1" : "B2";
        }
    }

    public class Queen : ChessPiece
    {
        public Queen(bool iswhite, int row, int column) : base(iswhite, row, column)
        {
        }
        public override List<(int, int)> GetAvailableMoves(ChessPiece[,] board)
        {
            PrintMatrix(board);
            List<(int, int)> availableMoves = new List<(int, int)>();
            CheckDirection(board, availableMoves, 1, 1);
            CheckDirection(board, availableMoves, 1, -1);
            CheckDirection(board, availableMoves, -1, 1);
            CheckDirection(board, availableMoves, -1, -1);
            CheckDirection(board, availableMoves, 0, 1);
            CheckDirection(board, availableMoves, 0, -1);
            CheckDirection(board, availableMoves, 1, 0);
            CheckDirection(board, availableMoves, -1, 0);
            return availableMoves;
        }
        private void CheckDirection(ChessPiece[,] board, List<(int, int)> availableMoves, int rowOffset, int colOffset)
        {
            int newRow = row + rowOffset;
            int newCol = column + colOffset;
            while (IsOnBoard(newRow, newCol))
            {
                if (board[newRow, newCol] == null)
                {
                    availableMoves.Add((newRow, newCol));
                }
                else
                {
                    if (board[newRow, newCol].Isblack != Isblack)
                    {
                        availableMoves.Add((newRow, newCol));
                    }
                    break;
                }
                newRow += rowOffset;
                newCol += colOffset;
            }
        }
        private bool IsOnBoard(int row, int column)
        {
            return row >= 0 && row < 8 && column >= 0 && column < 8;
        }
        public override string ToString()
        {
            return Isblack ? "Q1" : "Q2";
        }
    }

    public class King : ChessPiece
    {
        public King(bool iswhite, int row, int column) : base(iswhite, row, column)
        {
        }
        public override List<(int, int)> GetAvailableMoves(ChessPiece[,] board)
        {
            PrintMatrix(board);
            List<(int, int)> availableMoves = new List<(int, int)>();
            CheckMove(board, availableMoves, row + 1, column);
            CheckMove(board, availableMoves, row - 1, column);
            CheckMove(board, availableMoves, row, column + 1);
            CheckMove(board, availableMoves, row, column - 1);
            CheckMove(board, availableMoves, row + 1, column + 1);
            CheckMove(board, availableMoves, row + 1, column - 1);
            CheckMove(board, availableMoves, row - 1, column + 1);
            CheckMove(board, availableMoves, row - 1, column - 1);
            return availableMoves;
        }
        private void CheckMove(ChessPiece[,] board, List<(int, int)> availableMoves, int targetRow, int targetColumn)
        {
            if (IsOnBoard(targetRow, targetColumn) && (board[targetRow, targetColumn] == null || board[targetRow, targetColumn].Isblack != Isblack))
            {
                availableMoves.Add((targetRow, targetColumn));
            }
        }
        private bool IsOnBoard(int row, int column)
        {
            return row >= 0 && row < 8 && column >= 0 && column < 8;
        }
        public override string ToString()
        {
            return Isblack ? "K1" : "K2";
        }
    }
}
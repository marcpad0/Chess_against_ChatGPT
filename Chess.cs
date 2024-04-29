using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    internal class Chess
    {
        protected string[,] pathbox = new string[8, 8];
        private PictureBox[,] pictureBoxes = new PictureBox[8, 8];
        private Pawn[,] pawns = new Pawn[8, 8];
        private Tower[,] towers = new Tower[8, 8];
        private Horse[,] horses = new Horse[8, 8];
        private ChessPiece[,] chessPieces = new ChessPiece[8, 8];
        private Bishop[,] bishops = new Bishop[8, 8];
        private Queen[,] queens = new Queen[8, 8];
        private King[,] kings = new King[8, 8];

        protected int Size = 71;
        protected int X = 30;
        protected int Y = 533;

        private bool play_against_gpt = false;

        private Form Form1;
        private string OpenAikey { get; set; }

        private int selectedPawnRow = -1;

        private int selectedPawnColumn = -1;

        private int selectedTowerRow = -1;

        private int selectedTowerColumn = -1;

        private int selectedHorseRow = -1;

        private int selectedHorseColumn = -1;

        private int selectedBishopRow = -1;

        private int selectedBishopColumn = -1;

        private int selectedQueenRow = -1;

        private int selectedQueenColumn = -1;

        private int selectedKingRow = -1;

        private int selectedKingColumn = -1;

        private bool isWhiteTurn = true;

        private bool isPromoting = false;

        private int _maxAttempts = 6;

        public int MaxAttempts
        {
            get { return _maxAttempts; }
            set { _maxAttempts = value; }
        }

        private (int, int) PositionPromoting = (-1, -1);

        public Chess(Form form1, string key, int attempt, bool chagpt)
        {
            this.Form1 = form1;
            this.OpenAikey = key;
            this._maxAttempts = attempt;
            this.play_against_gpt = chagpt;
        }

        public Chess(Form form1)
        {
            this.Form1 = form1;
            this.OpenAikey = "";
            this._maxAttempts = 0;
            this.play_against_gpt = false;
        }

        public void ChangeMap(Image url)
        {
            Form1.BackgroundImage = url;
        }

        protected virtual void FullMatrix()
        {
            pathbox[0, 0] = "Piece/Tower_white.png";
            pathbox[0, 1] = "Piece/Horse_white.png";
            pathbox[0, 2] = "Piece/bishop_white.png";
            pathbox[0, 3] = "Piece/King_white.png";
            pathbox[0, 4] = "Piece/Queen_white.png";
            pathbox[0, 5] = "Piece/bishop_white.png";
            pathbox[0, 6] = "Piece/Horse_white.png";
            pathbox[0, 7] = "Piece/Tower_white.png";
            for (int j = 0; j < 8; j++)
            {
                pathbox[1, j] = "Piece/Pawn_white.png";
            }

            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    pathbox[i, j] = "";
                }
            }

            for (int j = 0; j < 8; j++)
            {
                pathbox[6, j] = "Piece/Pawn_black.png";
            }
            pathbox[7, 0] = "Piece/Tower_black.png";
            pathbox[7, 1] = "Piece/Horse_black.png";
            pathbox[7, 2] = "Piece/bishop_black.png";
            pathbox[7, 3] = "Piece/King_black.png";
            pathbox[7, 4] = "Piece/Queen_black.png";
            pathbox[7, 5] = "Piece/bishop_black.png";
            pathbox[7, 6] = "Piece/Horse_black.png";
            pathbox[7, 7] = "Piece/Tower_black.png";
        }

        public void init()
        {
            Form1.Text = "Chess - " + (isWhiteTurn ? "White" : "Black") + " turn";
            FullMatrix();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string pieceName = PieceName(i, j);
                    pictureBoxes[i, j] = initPictureBox(Form1, pathbox[i, j], Size, Size, X + (Size * j), Y - (Size * i), pieceName);
                    pictureBoxes[i, j].Click += PictureBox_Click;

                    string[] tagParts = pieceName.Split('_');
                    string pieceNameCheck = tagParts[0];

                    if (pieceNameCheck == "Pawn")
                    {
                        chessPieces[i, j] = new Pawn(i > 4, i, j);
                        pawns[i, j] = (Pawn)chessPieces[i, j];
                    }
                    else if (pieceNameCheck == "Tower")
                    {
                        chessPieces[i, j] = new Tower(i > 4, i, j);
                        towers[i, j] = (Tower)chessPieces[i, j];
                    }
                    else if (pieceNameCheck == "Horse")
                    {
                        chessPieces[i, j] = new Horse(i > 4, i, j);
                        horses[i, j] = (Horse)chessPieces[i, j];
                    }
                    else if (pieceNameCheck == "Bishop")
                    {
                        chessPieces[i, j] = new Bishop(i > 4, i, j);
                        bishops[i, j] = (Bishop)chessPieces[i, j];
                    }
                    else if (pieceNameCheck == "Queen")
                    {
                        chessPieces[i, j] = new Queen(i > 4, i, j);
                        queens[i, j] = (Queen)chessPieces[i, j];
                    }
                    else if (pieceNameCheck == "King")
                    {
                        chessPieces[i, j] = new King(i > 4, i, j);
                        kings[i, j] = (King)chessPieces[i, j];
                    }
                }
            }
        }

        private PictureBox initPictureBox(Form parentForm, string imagePath, int width, int height, int x, int y, string pieceName)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.ImageLocation = imagePath;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Location = new System.Drawing.Point(x, y);
            pictureBox.Size = new System.Drawing.Size(width, height);
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Parent = parentForm;
            pictureBox.Tag = pieceName;

            parentForm.Controls.Add(pictureBox);

            return pictureBox;
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (play_against_gpt && !isWhiteTurn)
            {
                return;
            }

            string pieceName = "";
            string pieceColor = "";
            PictureBox pictureBox = (PictureBox)sender;

            if (pictureBox.Tag != null)
            {
                string pieceTag = pictureBox.Tag.ToString();
                string[] tagParts = pieceTag.Split('_');

                if (tagParts.Length >= 2)
                {
                    pieceName = tagParts[0];
                    pieceColor = tagParts[1];
                }
                else
                {
                    Console.WriteLine("Invalid tag format: " + pieceTag);
                }
            }
            else
            {
                Console.WriteLine("Tag property is null for the PictureBox.");
            }

            if (pictureBox.BackColor == Color.LightGreen)
            {
                pieceName = "None";
            }

            if (pieceName == "Pawn")
            {
                selectedTowerRow = -1;
                selectedTowerColumn = -1;
                selectedHorseRow = -1;
                selectedHorseColumn = -1;
                selectedBishopRow = -1;
                selectedBishopColumn = -1;
                selectedQueenRow = -1;
                selectedQueenColumn = -1;
                selectedKingRow = -1;
                selectedKingColumn = -1;
                if ((isWhiteTurn && pieceColor == "white") || (!isWhiteTurn && pieceColor == "black"))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (pictureBoxes[i, j] == pictureBox)
                            {
                                List<(int, int)> availableMoves = pawns[i, j].GetAvailableMoves(chessPieces);

                                HighlightAvailableMoves(availableMoves);

                                selectedPawnRow = i;
                                selectedPawnColumn = j;

                                break;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else if (pictureBox.BackColor == Color.LightGreen && selectedPawnRow != -1 && selectedPawnColumn != -1)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (pictureBoxes[i, j] == pictureBox)
                        {
                            MovePawnToPosition(selectedPawnRow, selectedPawnColumn, i, j);

                            selectedPawnRow = -1;
                            selectedPawnColumn = -1;

                            return;
                        }
                    }
                }
            }

            if (pieceName == "Tower")
            {
                selectedHorseRow = -1;
                selectedHorseColumn = -1;
                selectedPawnRow = -1;
                selectedPawnColumn = -1;
                selectedBishopColumn = -1;
                selectedBishopRow = -1;
                selectedQueenRow = -1;
                selectedQueenColumn = -1;
                selectedKingRow = -1;
                selectedKingColumn = -1;
                if ((isWhiteTurn && pieceColor == "white") || (!isWhiteTurn && pieceColor == "black"))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (pictureBoxes[i, j] == pictureBox)
                            {
                                List<(int, int)> availableMoves = towers[i, j].GetAvailableMoves(chessPieces);
                                HighlightAvailableMoves(availableMoves);
                                selectedTowerRow = i;
                                selectedTowerColumn = j;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else if (pictureBox.BackColor == Color.LightGreen && selectedTowerRow != -1 && selectedTowerColumn != -1)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (pictureBoxes[i, j] == pictureBox)
                        {
                            MoveTowerToPosition(selectedTowerRow, selectedTowerColumn, i, j);
                            selectedTowerRow = -1;
                            selectedTowerColumn = -1;
                            return;
                        }
                    }
                }
            }

            if (pieceName == "Horse")
            {
                selectedPawnRow = -1;
                selectedPawnColumn = -1;
                selectedTowerRow = -1;
                selectedTowerColumn = -1;
                selectedBishopColumn = -1;
                selectedBishopRow = -1;
                selectedQueenColumn = -1;
                selectedQueenRow = -1;
                selectedKingRow = -1;
                selectedKingColumn = -1;
                if ((isWhiteTurn && pieceColor == "white") || (!isWhiteTurn && pieceColor == "black"))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (pictureBoxes[i, j] == pictureBox)
                            {
                                List<(int, int)> availableMoves = horses[i, j].GetAvailableMoves(chessPieces);
                                HighlightAvailableMoves(availableMoves);
                                selectedHorseRow = i;
                                selectedHorseColumn = j;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else if (pictureBox.BackColor == Color.LightGreen && selectedHorseRow != -1 && selectedHorseColumn != -1)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (pictureBoxes[i, j] == pictureBox)
                        {
                            MoveHorseToPosition(selectedHorseRow, selectedHorseColumn, i, j);
                            selectedHorseRow = -1;
                            selectedHorseColumn = -1;
                            return;
                        }
                    }
                }
            }

            if (pieceName == "Bishop")
            {
                selectedPawnRow = -1;
                selectedPawnColumn = -1;
                selectedTowerRow = -1;
                selectedTowerColumn = -1;
                selectedHorseRow = -1;
                selectedHorseColumn = -1;
                selectedQueenRow = -1;
                selectedQueenColumn = -1;
                selectedKingColumn = -1;
                selectedKingRow = -1;
                if ((isWhiteTurn && pieceColor == "white") || (!isWhiteTurn && pieceColor == "black"))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (pictureBoxes[i, j] == pictureBox)
                            {
                                List<(int, int)> availableMoves = bishops[i, j].GetAvailableMoves(chessPieces);
                                HighlightAvailableMoves(availableMoves);
                                selectedBishopRow = i;
                                selectedBishopColumn = j;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else if (pictureBox.BackColor == Color.LightGreen && selectedBishopRow != -1 && selectedBishopColumn != -1)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (pictureBoxes[i, j] == pictureBox)
                        {
                            MoveBishopToPosition(selectedBishopRow, selectedBishopColumn, i, j);
                            selectedBishopRow = -1;
                            selectedBishopColumn = -1;
                            return;
                        }
                    }
                }
            }

            if (pieceName == "Queen")
            {
                selectedPawnRow = -1;
                selectedPawnColumn = -1;
                selectedTowerRow = -1;
                selectedTowerColumn = -1;
                selectedHorseRow = -1;
                selectedHorseColumn = -1;
                selectedBishopRow = -1;
                selectedBishopColumn = -1;
                selectedKingColumn = -1;
                selectedKingRow = -1;
                if ((isWhiteTurn && pieceColor == "white") || (!isWhiteTurn && pieceColor == "black"))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (pictureBoxes[i, j] == pictureBox)
                            {
                                List<(int, int)> availableMoves = queens[i, j].GetAvailableMoves(chessPieces);
                                HighlightAvailableMoves(availableMoves);
                                selectedQueenRow = i;
                                selectedQueenColumn = j;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else if (pictureBox.BackColor == Color.LightGreen && selectedQueenRow != -1 && selectedQueenColumn != -1)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (pictureBoxes[i, j] == pictureBox)
                        {
                            MoveQueenToPosition(selectedQueenRow, selectedQueenColumn, i, j);
                            selectedQueenRow = -1;
                            selectedQueenColumn = -1;
                            return;
                        }
                    }
                }
            }

            if (pieceName == "King")
            {
                selectedPawnRow = -1;
                selectedPawnColumn = -1;
                selectedTowerRow = -1;
                selectedTowerColumn = -1;
                selectedHorseRow = -1;
                selectedHorseColumn = -1;
                selectedBishopRow = -1;
                selectedBishopColumn = -1;
                selectedQueenRow = -1;
                selectedQueenColumn = -1;
                if ((isWhiteTurn && pieceColor == "white") || (!isWhiteTurn && pieceColor == "black"))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (pictureBoxes[i, j] == pictureBox)
                            {
                                List<(int, int)> availableMoves = kings[i, j].GetAvailableMoves(chessPieces);
                                HighlightAvailableMoves(availableMoves);
                                selectedKingRow = i;
                                selectedKingColumn = j;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else if (pictureBox.BackColor == Color.LightGreen && selectedKingRow != -1 && selectedKingColumn != -1)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (pictureBoxes[i, j] == pictureBox)
                        {
                            MoveKingToPosition(selectedKingRow, selectedKingColumn, i, j);
                            selectedKingRow = -1;
                            selectedKingColumn = -1;
                            return;
                        }
                    }
                }
            }
        }

        private Menu Form2;

        private void ShowForm()
        {
            Form2 = new Menu(isWhiteTurn);
            Form2.ShowDialog();
        }

        private void MovePawnToPosition(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            pictureBoxes[toRow, toColumn].Image = pictureBoxes[fromRow, fromColumn].Image;
            pictureBoxes[toRow, toColumn].Tag = pictureBoxes[fromRow, fromColumn].Tag;
            pictureBoxes[fromRow, fromColumn].Image = null;
            pictureBoxes[fromRow, fromColumn].Tag = null;

            if (pawns[fromRow, fromColumn] is Pawn pawn && Math.Abs(toRow - fromRow) == 1 && Math.Abs(toColumn - fromColumn) == 1 && chessPieces[toRow, toColumn] == null)
            {
                int capturedPawnRow = fromRow;
                int capturedPawnColumn = toColumn;

                pictureBoxes[capturedPawnRow, capturedPawnColumn].Image = null;
                pictureBoxes[capturedPawnRow, capturedPawnColumn].Tag = null;
                chessPieces[capturedPawnRow, capturedPawnColumn] = null;
                pawns[capturedPawnRow, capturedPawnColumn] = null;
            }

            pawns[toRow, toColumn] = pawns[fromRow, fromColumn];
            pawns[toRow, toColumn].row = toRow;
            pawns[toRow, toColumn].column = toColumn;
            chessPieces[toRow, toColumn] = pawns[toRow, toColumn];
            chessPieces[fromRow, fromColumn] = null;
            pawns[fromRow, fromColumn] = null;

            if (pawns[toRow, toColumn] is Pawn && (toRow == 0 || toRow == 7))
            {
                int i = 0;
                var Parts = pictureBoxes[toRow, toColumn].Tag.ToString();
                string[] tagParts = Parts.Split('_');
                string pieceName = tagParts[0];
                string pieceColor = tagParts[1];
                string result = default;
                if (play_against_gpt && isWhiteTurn == false)
                {
                    PositionPromoting = (toRow, toColumn);
                    isPromoting = true;
                    SendRequest(OpenAikey, "You have reached the end with a pawn and can choose to promote it to any piece in this class. Write only the piece you want and nothing else.", Form2);
                }
                else
                {
                    Form1.Enabled = false;
                    Form1.Opacity = 0.85;

                    Thread thread = new Thread(ShowForm);
                    thread.Start();

                    while (i == 0)
                    {
                        try
                        {
                            result = Form2.Results();
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        if (result != null)
                        {
                            i++;
                            Form1.Enabled = true;
                            thread.Abort();
                            Form1.Opacity = 1;
                        }
                    }
                }

                string promotedPieceType = result;
                if (promotedPieceType == "Queen")
                {
                    pieceName = "Queen";
                    pictureBoxes[toRow, toColumn].Tag = pieceName + "_" + pieceColor;
                    pictureBoxes[toRow, toColumn].ImageLocation = isWhiteTurn ? "Piece/King_white.png" : "Piece/King_black.png";
                    pawns[toRow, toColumn].PromotePawn(chessPieces, toRow, toColumn, typeof(Queen));
                    queens[toRow, toColumn] = isWhiteTurn ? new Queen(false, toRow, toColumn) : new Queen(true, toRow, toColumn);
                }
                else if (promotedPieceType == "Tower")
                {
                    pieceName = "Tower";
                    pictureBoxes[toRow, toColumn].Tag = pieceName + "_" + pieceColor;
                    pictureBoxes[toRow, toColumn].ImageLocation = isWhiteTurn ? "Piece/Tower_white.png" : "Piece/Tower_black.png";
                    pawns[toRow, toColumn].PromotePawn(chessPieces, toRow, toColumn, typeof(Tower));
                    towers[toRow, toColumn] = isWhiteTurn ? new Tower(false, toRow, toColumn) : new Tower(true, toRow, toColumn);
                }
                else if (promotedPieceType == "Bishop")
                {
                    pieceName = "Bishop";
                    pictureBoxes[toRow, toColumn].Tag = pieceName + "_" + pieceColor;
                    pictureBoxes[toRow, toColumn].ImageLocation = isWhiteTurn ? "Piece/Bishop_white.png" : "Piece/Bishop_black.png";
                    pawns[toRow, toColumn].PromotePawn(chessPieces, toRow, toColumn, typeof(Bishop));
                    bishops[toRow, toColumn] = isWhiteTurn ? new Bishop(false, toRow, toColumn) : new Bishop(true, toRow, toColumn);
                }
                else if (promotedPieceType == "Horse")
                {
                    pieceName = "Horse";
                    pictureBoxes[toRow, toColumn].Tag = pieceName + "_" + pieceColor;
                    pictureBoxes[toRow, toColumn].ImageLocation = isWhiteTurn ? "Piece/Horse_white.png" : "Piece/Horse_black.png";
                    pawns[toRow, toColumn].PromotePawn(chessPieces, toRow, toColumn, typeof(Horse));
                    horses[toRow, toColumn] = isWhiteTurn ? new Horse(false, toRow, toColumn) : new Horse(true, toRow, toColumn);
                }
                else
                {
                    throw new Exception("Not Valid");
                }
            }

            FinalizeMove();
        }

        private void FinalizeMove()
        {
            ClearGreenHighlighting();
            if (IsInCheckmate(isWhiteTurn, chessPieces))
            {
                MessageBox.Show((isWhiteTurn ? "White" : "Black") + " wins by checkmate!");
                Form1.Hide();
                Task.Delay(1000).Wait();
                Victory victory = new Victory(isWhiteTurn);
                victory.Show();
            }
            else if (IsInStalemate(isWhiteTurn, chessPieces))
            {
                MessageBox.Show("Stalemate!");
                Task.Delay(1000).Wait();
                Environment.Exit(0);
            }
            else if (IsInCheck(isWhiteTurn, chessPieces))
            {
                MessageBox.Show((isWhiteTurn ? "Black" : "White") + " is in check!");
            }

            isWhiteTurn = !isWhiteTurn;
            Form1.Text = "Chess - " + (isWhiteTurn ? "White" : "Black") + " turn";

            if (play_against_gpt && !isWhiteTurn == true)
            {
                SendRequest(OpenAikey, "You are Magnus Carlsen, and this is your most important chess match ever. You are extremely attentive and accurate, without committing errors or getting distracted. You are playing with the black pieces, so do not attempt to move the white pieces. You must base your move on the image I provided. In the image, there is the chessboard where the various modifications of the game will take place. At the side, there are numbers and letters for annotation purposes, so use them accordingly. Give the best move using complete chess notation. Remember, it is strictly forbidden to use En passant, Castling, Pawn promotion, and any unusual rules. You don't have to use them during the notation, and neither can you use Castling, Pawn promotion, Draw offer, Check, Checkmate, or End of game; you can handle only movement of the piece. Both the start and end positions must be explicitly noted and include a dash. Write nothing else except the notation.", this.Form1);
            }
        }

        private void MoveTowerToPosition(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            pictureBoxes[toRow, toColumn].Image = pictureBoxes[fromRow, fromColumn].Image;
            pictureBoxes[toRow, toColumn].Tag = pictureBoxes[fromRow, fromColumn].Tag;
            pictureBoxes[fromRow, fromColumn].Image = null;
            pictureBoxes[fromRow, fromColumn].Tag = null;
            towers[toRow, toColumn] = towers[fromRow, fromColumn];
            towers[toRow, toColumn].row = toRow;
            towers[toRow, toColumn].column = toColumn;
            chessPieces[toRow, toColumn] = towers[toRow, toColumn];
            towers[fromRow, fromColumn] = null;
            chessPieces[fromRow, fromColumn] = null;
            FinalizeMove();
        }

        private void MoveHorseToPosition(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            pictureBoxes[toRow, toColumn].Image = pictureBoxes[fromRow, fromColumn].Image;
            pictureBoxes[toRow, toColumn].Tag = pictureBoxes[fromRow, fromColumn].Tag;
            pictureBoxes[fromRow, fromColumn].Image = null;
            pictureBoxes[fromRow, fromColumn].Tag = null;
            horses[toRow, toColumn] = horses[fromRow, fromColumn];
            horses[toRow, toColumn].row = toRow;
            horses[toRow, toColumn].column = toColumn;
            chessPieces[toRow, toColumn] = horses[toRow, toColumn];
            horses[fromRow, fromColumn] = null;
            chessPieces[fromRow, fromColumn] = null;
            FinalizeMove();
        }

        private void MoveBishopToPosition(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            pictureBoxes[toRow, toColumn].Image = pictureBoxes[fromRow, fromColumn].Image;
            pictureBoxes[toRow, toColumn].Tag = pictureBoxes[fromRow, fromColumn].Tag;
            pictureBoxes[fromRow, fromColumn].Image = null;
            pictureBoxes[fromRow, fromColumn].Tag = null;
            bishops[toRow, toColumn] = bishops[fromRow, fromColumn];
            bishops[toRow, toColumn].row = toRow;
            bishops[toRow, toColumn].column = toColumn;
            chessPieces[toRow, toColumn] = bishops[toRow, toColumn];
            bishops[fromRow, fromColumn] = null;
            chessPieces[fromRow, fromColumn] = null;
            FinalizeMove();
        }

        private void MoveQueenToPosition(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            pictureBoxes[toRow, toColumn].Image = pictureBoxes[fromRow, fromColumn].Image;
            pictureBoxes[toRow, toColumn].Tag = pictureBoxes[fromRow, fromColumn].Tag;
            pictureBoxes[fromRow, fromColumn].Image = null;
            pictureBoxes[fromRow, fromColumn].Tag = null;
            queens[toRow, toColumn] = queens[fromRow, fromColumn];
            queens[toRow, toColumn].row = toRow;
            queens[toRow, toColumn].column = toColumn;
            chessPieces[toRow, toColumn] = queens[toRow, toColumn];
            queens[fromRow, fromColumn] = null;
            chessPieces[fromRow, fromColumn] = null;
            FinalizeMove();
        }

        private void MoveKingToPosition(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            pictureBoxes[toRow, toColumn].Image = pictureBoxes[fromRow, fromColumn].Image;
            pictureBoxes[toRow, toColumn].Tag = pictureBoxes[fromRow, fromColumn].Tag;
            pictureBoxes[fromRow, fromColumn].Image = null;
            pictureBoxes[fromRow, fromColumn].Tag = null;
            kings[toRow, toColumn] = kings[fromRow, fromColumn];
            kings[toRow, toColumn].row = toRow;
            kings[toRow, toColumn].column = toColumn;
            chessPieces[toRow, toColumn] = kings[toRow, toColumn];
            kings[fromRow, fromColumn] = null;
            chessPieces[fromRow, fromColumn] = null;
            FinalizeMove();
        }

        private void ClearGreenHighlighting()
        {
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.BackColor = Color.Transparent;
            }
        }

        private void HighlightAvailableMoves(List<(int, int)> moves)
        {
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.BackColor = Color.Transparent;
            }

            foreach (var (row, column) in moves)
            {
                pictureBoxes[row, column].BackColor = Color.LightGreen;
            }
        }

        private string PieceName(int row, int col)
        {
            if (row == 0 || row == 7)
            {
                switch (col)
                {
                    case 0:
                    case 7:
                        return "Tower_" + (row == 0 ? "white" : "black");

                    case 1:
                    case 6:
                        return "Horse_" + (row == 0 ? "white" : "black");

                    case 2:
                    case 5:
                        return "Bishop_" + (row == 0 ? "white" : "black");

                    case 3:
                        return "Queen_" + (row == 0 ? "white" : "black");

                    case 4:
                        return "King_" + (row == 0 ? "white" : "black");

                    default:
                        return "";
                }
            }
            else if (row == 1 || row == 6)
            {
                return "Pawn_" + (row == 1 ? "white" : "black");
            }
            else
            {
                return "Not a Piece";
            }
        }

        private bool IsInCheck(bool isWhiteTurn, ChessPiece[,] chessPieces)
        {
            int kingRow = -1;
            int kingColumn = -1;
            for (int i = 0; i < chessPieces.GetLength(0); i++)
            {
                for (int j = 0; j < chessPieces.GetLength(1); j++)
                {
                    ChessPiece piece = chessPieces[i, j];
                    if (piece != null && piece.Isblack == isWhiteTurn && piece is King)
                    {
                        kingRow = i;
                        kingColumn = j;
                        break;
                    }
                }
            }
            if (kingRow == -1 || kingColumn == -1)
                return false;
            for (int i = 0; i < chessPieces.GetLength(0); i++)
            {
                for (int j = 0; j < chessPieces.GetLength(1); j++)
                {
                    ChessPiece piece = chessPieces[i, j];
                    if (piece != null && piece.Isblack != isWhiteTurn)
                    {
                        var moves = piece.GetAvailableMoves(chessPieces);
                        foreach (var move in moves)
                        {
                            if (move.Item1 == kingRow && move.Item2 == kingColumn)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsInCheckmate(bool isWhiteTurn, ChessPiece[,] chessPieces)
        {
            if (!IsInCheck(isWhiteTurn, chessPieces))
                return false;
            for (int i = 0; i < chessPieces.GetLength(0); i++)
            {
                for (int j = 0; j < chessPieces.GetLength(1); j++)
                {
                    ChessPiece piece = chessPieces[i, j];
                    if (piece != null && piece.Isblack == isWhiteTurn)
                    {
                        var moves = piece.GetAvailableMoves(chessPieces);
                        foreach (var move in moves)
                        {
                            ChessPiece temp = chessPieces[move.Item1, move.Item2];
                            chessPieces[move.Item1, move.Item2] = piece;
                            chessPieces[i, j] = null;
                            bool stillInCheck = IsInCheck(isWhiteTurn, chessPieces);
                            chessPieces[i, j] = piece;
                            chessPieces[move.Item1, move.Item2] = temp;
                            if (!stillInCheck)
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool IsInStalemate(bool isWhiteTurn, ChessPiece[,] chessPieces)
        {
            if (IsInCheck(isWhiteTurn, chessPieces))
                return false;

            for (int i = 0; i < chessPieces.GetLength(0); i++)
            {
                for (int j = 0; j < chessPieces.GetLength(1); j++)
                {
                    ChessPiece piece = chessPieces[i, j];
                    if (piece != null && piece.Isblack == isWhiteTurn)
                    {
                        var moves = piece.GetAvailableMoves(chessPieces);
                        foreach (var move in moves)
                        {
                            ChessPiece temp = chessPieces[move.Item1, move.Item2];
                            chessPieces[move.Item1, move.Item2] = piece;
                            chessPieces[i, j] = null;
                            bool stillInCheck = IsInCheck(isWhiteTurn, chessPieces);
                            chessPieces[i, j] = piece;
                            chessPieces[move.Item1, move.Item2] = temp;
                            if (!stillInCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private ((int, int), (int, int)) Convert_Annotazione(string move)
        {
            var (start, end) = ProcessChessMove(move);
            Console.WriteLine($"Mossa da {PositionToString(start)} a {PositionToString(end)}");
            return (start, end);
        }

        private (int, int) ConvertToMatrixPosition(string chessNotation)
        {
            if (char.IsUpper(chessNotation[0]) && char.IsLetter(chessNotation[1]))
            {
                chessNotation = chessNotation.Substring(1);
            }

            int col = chessNotation[0] - 'a';
            int row = chessNotation[1] - '1';
            return (row, col);
        }

        private ((int, int), (int, int)) ProcessChessMove(string move)
        {
            move = move.Replace('x', '-').Replace('+', ' ');
            if (char.IsUpper(move[0]) && char.IsUpper(move[1]))
            {
                move = move.Substring(1);
            }

            string[] parts = move.Split('-');
            (int, int) start = ConvertToMatrixPosition(parts[0]);
            (int, int) end = ConvertToMatrixPosition(parts[1]);
            return (start, end);
        }

        private static string PositionToString((int, int) position)
        {
            return $"({position.Item1}, {position.Item2})";
        }

        private static readonly HttpClient client = new HttpClient();

        private async Task SendRequest(string key, string prompt, Form form)
        {
            var attempt = 0;
            while (attempt < _maxAttempts)
            {
                try
                { 
                    string base64Image = default;
                    if (isPromoting)
                    {
                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        string screenshotsDirectory = Path.Combine(baseDirectory, "Screenshots");
                        string screenshotPath = Path.Combine(screenshotsDirectory, "Screenshot 2024-04-29 190612.png");

                        base64Image = await Task.Run(() => EncodeImageToBase64(LoadImageFromFile(screenshotPath)));
                    }
                    else
                    {
                        base64Image = await Task.Run(() => EncodeImageToBase64(GetScreenShotForm(form)));
                    }   
                    
                    var payload = GetPayload(base64Image, prompt);
                    var response = await PostRequest("https://api.openai.com/v1/chat/completions", payload, key);
                    Console.WriteLine(response);

                    var json = JObject.Parse(response);
                    var contents = json["choices"][0]["message"]["content"].ToString();
                    Console.WriteLine(contents);

                    if (isPromoting)
                    {
                        int toRow = PositionPromoting.Item1;
                        int toColumn = PositionPromoting.Item2;

                        var Parts = pictureBoxes[toRow, toColumn].Tag.ToString();
                        string[] tagParts = Parts.Split('_');
                        string pieceName = tagParts[0];
                        string pieceColor = tagParts[1];
                        string result = contents;

                        result = result.First().ToString().ToUpper() + result.Substring(1);

                        string promotedPieceType = result;
                        if (promotedPieceType == "Queen")
                        {
                            pieceName = "Queen";
                            pictureBoxes[toRow, toColumn].Tag = pieceName + "_" + pieceColor;
                            pictureBoxes[toRow, toColumn].ImageLocation = isWhiteTurn ? "Piece/King_white.png" : "Piece/King_black.png";
                            pawns[toRow, toColumn].PromotePawn(chessPieces, toRow, toColumn, typeof(Queen));
                            queens[toRow, toColumn] = isWhiteTurn ? new Queen(false, toRow, toColumn) : new Queen(true, toRow, toColumn);
                        }
                        else if (promotedPieceType == "Tower")
                        {
                            pieceName = "Tower";
                            pictureBoxes[toRow, toColumn].Tag = pieceName + "_" + pieceColor;
                            pictureBoxes[toRow, toColumn].ImageLocation = isWhiteTurn ? "Piece/Tower_white.png" : "Piece/Tower_black.png";
                            pawns[toRow, toColumn].PromotePawn(chessPieces, toRow, toColumn, typeof(Tower));
                            towers[toRow, toColumn] = isWhiteTurn ? new Tower(false, toRow, toColumn) : new Tower(true, toRow, toColumn);
                        }
                        else if (promotedPieceType == "Bishop")
                        {
                            pieceName = "Bishop";
                            pictureBoxes[toRow, toColumn].Tag = pieceName + "_" + pieceColor;
                            pictureBoxes[toRow, toColumn].ImageLocation = isWhiteTurn ? "Piece/Bishop_white.png" : "Piece/Bishop_black.png";
                            pawns[toRow, toColumn].PromotePawn(chessPieces, toRow, toColumn, typeof(Bishop));
                            bishops[toRow, toColumn] = isWhiteTurn ? new Bishop(false, toRow, toColumn) : new Bishop(true, toRow, toColumn);
                        }
                        else if (promotedPieceType == "Horse")
                        {
                            pieceName = "Horse";
                            pictureBoxes[toRow, toColumn].Tag = pieceName + "_" + pieceColor;
                            pictureBoxes[toRow, toColumn].ImageLocation = isWhiteTurn ? "Piece/Horse_white.png" : "Piece/Horse_black.png";
                            pawns[toRow, toColumn].PromotePawn(chessPieces, toRow, toColumn, typeof(Horse));
                            horses[toRow, toColumn] = isWhiteTurn ? new Horse(false, toRow, toColumn) : new Horse(true, toRow, toColumn);
                        }
                        PositionPromoting = (-1, -1);
                        isPromoting = false;
                        SendRequest(OpenAikey, "You are Magnus Carlsen, and this is your most important chess match ever. You are extremely attentive and accurate, without committing errors or getting distracted. You are playing with the black pieces, so do not attempt to move the white pieces. You must base your move on the image I provided. In the image, there is the chessboard where the various modifications of the game will take place. At the side, there are numbers and letters for annotation purposes, so use them accordingly. Give the best move using complete chess notation. Remember, it is strictly forbidden to use En passant, Castling, Pawn promotion, and any unusual rules. You don't have to use them during the notation, and neither can you use Castling, Pawn promotion, Draw offer, Check, Checkmate, or End of game; you can handle only movement of the piece. Both the start and end positions must be explicitly noted and include a dash. Write nothing else except the notation.", this.Form1);
                    }

                    if (ValidateChessMove(contents))
                    {
                        var move = Convert_Annotazione(contents);
                        bool ok = HandleBlackMove(move.Item1, move.Item2);
                        if (ok)
                        {
                            break;
                        }
                        else
                        {
                            attempt++;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid chess move received on attempt {attempt + 1}.");
                        attempt++;
                    }
                }
                catch
                {
                    attempt++;
                }
            }

            if (attempt == _maxAttempts)
            {
                Console.WriteLine("Failed to receive a valid chess move after maximum attempts. For continue the game i will choose a Randome Move");
                Random random = new Random();
                var piece = GetRandomPieceBlack();
                var moves = GetRandomMoveBlack(piece);
                HandleBlackMove(piece, moves);
            }
        }

        private Image LoadImageFromFile(string path)
        {
            Image image = Image.FromFile(path);
            return image;
        }

        private (int, int) GetRandomPieceBlack()
        {
            List<(int, int)> blackPieces = new List<(int, int)>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessPieces[i, j] != null && chessPieces[i, j].Isblack)
                    {
                        blackPieces.Add((i, j));
                    }
                }
            }
            Random random = new Random();
            int index = random.Next(0, blackPieces.Count);
            return blackPieces[index];
        }

        private (int, int) GetRandomMoveBlack((int, int) piece)
        {
            int pieceRow = piece.Item1;
            int pieceColumn = piece.Item2;
            ChessPiece chessPiece = chessPieces[pieceRow, pieceColumn];
            var moves = chessPiece.GetAvailableMoves(chessPieces);
            Random random = new Random();
            int index = random.Next(0, moves.Count);
            return moves[index];
        }

        private bool HandleBlackMove((int, int) piece, (int, int) newPosition)
        {
            int pieceRow = piece.Item1;
            int pieceColumn = piece.Item2;

            int newRow = newPosition.Item1;
            int newColumn = newPosition.Item2;

            ChessPiece chessPiece = chessPieces[pieceRow, pieceColumn];
            if (chessPiece == null)
            {
                Console.WriteLine("Invalid move: no piece found at the given position.");
                return false;
            }
            if (!chessPiece.Isblack)
            {
                Console.WriteLine("Invalid move: the piece at the given position is white.");
                return false;
            }
            var moves = chessPiece.GetAvailableMoves(chessPieces);
            if (!moves.Contains(newPosition))
            {
                Console.WriteLine("Invalid move: the piece cannot move to the given position.");
                return false;
            }

            if (typeof(Pawn) == chessPiece.GetType())
            {
                MovePawnToPosition(pieceRow, pieceColumn, newRow, newColumn);
                return true;
            }
            else if (typeof(Horse) == chessPiece.GetType())
            {
                MoveHorseToPosition(pieceRow, pieceColumn, newRow, newColumn);
                return true;
            }
            else if (typeof(Bishop) == chessPiece.GetType())
            {
                MoveBishopToPosition(pieceRow, pieceColumn, newRow, newColumn);
                return true;
            }
            else if (typeof(Tower) == chessPiece.GetType())
            {
                MoveTowerToPosition(pieceRow, pieceColumn, newRow, newColumn);
                return true;
            }
            else if (typeof(Queen) == chessPiece.GetType())
            {
                MoveQueenToPosition(pieceRow, pieceColumn, newRow, newColumn);
                return true;
            }
            else if (typeof(King) == chessPiece.GetType())
            {
                MoveKingToPosition(pieceRow, pieceColumn, newRow, newColumn);
                return true;
            }
            else
            {
                Console.WriteLine("Invalid move: the piece at the given position is not a valid chess piece.");
                return false;
            }
        }

        private bool ValidateChessMove(string move)
        {
            if (string.IsNullOrEmpty(move)) return false;

            move = move.Replace("+", "").Replace("#", "").Replace('x', '-');
            if (move == "O-O" || move == "O-O-O") return true;

            if (move.Contains('='))
            {
                var promotionParts = move.Split('=');
                if (promotionParts.Length != 2 || promotionParts[1].Length != 1 || !"NBRQ".Contains(promotionParts[1][0]))
                    return false;
                move = promotionParts[0];
            }

            var parts = move.Split('-');
            if (parts.Length != 2) return false;
            return IsValidChessPosition(parts[0]) && IsValidChessPosition(parts[1]);
        }

        private bool IsValidChessPosition(string position)
        {
            if (position.Length < 2 || position.Length > 3) return false;
            if ("KQRBN".Contains(position[0]) && char.IsLetter(position[1])) position = position.Substring(1);

            char col = position[0];
            char row = position[1];
            return col >= 'a' && col <= 'h' && row >= '1' && row <= '8';
        }

        private static StringContent GetPayload(string base64Image, string prompt)
        {
            var messages = new
            {
                model = "gpt-4-turbo",
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new { type = "text", text = prompt },
                            new { type = "image_url", image_url = new { url = $"data:image/jpeg;base64,{base64Image}" } }
                        }
                    }
                },
                max_tokens = 300
            };

            var json = JsonConvert.SerializeObject(messages);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static async Task<string> PostRequest(string url, StringContent payload, string key)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Headers.Add("Authorization", $"Bearer {key}");
                request.Content = payload;
                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }

        private string EncodeImageToBase64(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        private Image GetScreenShotForm(Form form)
        {
            if (form.InvokeRequired)
            {
                return (Image)form.Invoke(new Func<Image>(() => GetScreenShotForm(form)));
            }
            else
            {
                Rectangle formBounds = form.ClientRectangle;
                Bitmap bitmap = new Bitmap(formBounds.Width, formBounds.Height);

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    Point screenPoint = form.PointToScreen(Point.Empty);
                    g.CopyFromScreen(screenPoint, Point.Empty, formBounds.Size);
                }
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string screenshotsDirectory = Path.Combine(baseDirectory, "Screenshots");
                if (!Directory.Exists(screenshotsDirectory))
                {
                    Directory.CreateDirectory(screenshotsDirectory);
                }
                string filePath = Path.Combine(screenshotsDirectory, "form_screenshot.png");
                bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                return bitmap;
            }
        }
    }
}
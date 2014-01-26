using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tic_Tac_Chess
{
    public partial class Form1 : Form
    {
        public bool WhiteTurn = true;
        public string chessPieceToAdd = "";
        public string[,] chessPieceLocation = new string[3, 3];
        public bool[,] White = new bool[3, 3];
        public bool[,] Black = new bool[3, 3];
        public bool[,] PossiblePlacesToMove = new bool[3, 3];
        public int xPieceToMove = 0;
        public int yPieceToMove = 0;
        public int[] ChessPieceCount = { 8, 2, 2, 2, 1, 1, 8, 2, 2, 2, 1, 1 };
        Bitmap bitmapImage;
        Color[,] ImageArray = new Color[240, 240];
        Color[,] ChessPieceArray = new Color[60, 60];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void ClearPlacesToMove()
        {
            for (int row = 0; row < 240; row++)
                for (int col = 0; col < 240; col++)
                {
                    if (ImageArray[row, col] == Color.PaleGreen)
                        ImageArray[row, col] = Color.FromArgb(255, 255, 255, 255);
                }

            SetBitmapFromArray();
            TicTacToeBoard.Image = bitmapImage;
        }

        public void ClearAvailableSpaceToAdd()
        {
            int Width = TicTacToeBoard.Width;
            int Length = TicTacToeBoard.Height;
            Graphics g = Graphics.FromHwnd(TicTacToeBoard.Handle);
            SolidBrush whiteBrush = new SolidBrush(Color.White);

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (chessPieceLocation[i, j] == "")
                        g.FillRectangle(whiteBrush, i * Length / 3 + 1, j * Width / 3 + 1, 77, 77);
                }
        }

        public void SetPossiblePlacesToMoveToFalse()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    PossiblePlacesToMove[i, j] = false;
        }

        public void FindPossiblePlacesToMove(int row, int col)
        {
            SetPossiblePlacesToMoveToFalse();
            ClearPlacesToMove();
            if (chessPieceLocation[row, col] == "Pawn" && White[row, col] == true)
            {
                if (chessPieceLocation[row, col - 1] == "")
                    PossiblePlacesToMove[row, col - 1] = true;
                if (row - 1 > -1 && col - 1 > -1)
                    if (Black[row - 1, col - 1] == true)
                        PossiblePlacesToMove[row - 1, col - 1] = true;
                if (row + 1 < 3 && col - 1 > -1)
                    if (Black[row + 1, col - 1] == true)
                        PossiblePlacesToMove[row + 1, col - 1] = true;
            }

            if (chessPieceLocation[row, col] == "Pawn" && Black[row, col] == true)
            {
                if (chessPieceLocation[row, col + 1] == "")
                    PossiblePlacesToMove[row, col + 1] = true;
                if (row - 1 > -1 && col + 1 < 3)
                    if (White[row - 1, col + 1] == true)
                        PossiblePlacesToMove[row - 1, col + 1] = true;
                if (row + 1 < 3 && col + 1 < 3)
                    if (White[row + 1, col + 1] == true)
                        PossiblePlacesToMove[row + 1, col + 1] = true;
            }

            if (chessPieceLocation[row, col] == "Rook" && White[row, col] == true)
            {
                for (int i = row + 1; i < 3; i++)
                {
                    if (White[i, col] == true)
                        break;
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (White[i, col] == true)
                        break;
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (White[row, i] == true)
                        break;
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (White[row, i] == true)
                        break;
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            if (chessPieceLocation[row, col] == "Rook" && Black[row, col] == true)
            {
                for (int i = row + 1; i < 3; i++)
                {
                    if (Black[i, col] == true)
                        break;
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (Black[i, col] == true)
                        break;
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (Black[row, i] == true)
                        break;
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (Black[row, i] == true)
                        break;
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            if (chessPieceLocation[row, col] == "Bishop" && White[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
            }

            if (chessPieceLocation[row, col] == "Bishop" && Black[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
            }

            if (chessPieceLocation[row, col] == "Knight")
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (White[row, col] == true)
                            if (((Math.Abs(i - row) == 2 && Math.Abs(j - col) == 1) || (Math.Abs(i - row) == 1 && Math.Abs(j - col) == 2)) && (White[i, j] != true))
                                PossiblePlacesToMove[i, j] = true;
                        if (Black[row, col] == true)
                            if (((Math.Abs(i - row) == 2 && Math.Abs(j - col) == 1) || (Math.Abs(i - row) == 1 && Math.Abs(j - col) == 2)) && (Black[i, j] != true))
                                PossiblePlacesToMove[i, j] = true;
                    }

            if (chessPieceLocation[row, col] == "Queen" && White[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (White[i, j] == true)
                        break;
                    else if (Black[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                for (int i = row + 1; i < 3; i++)
                {
                    if (White[i, col] == true)
                        break;
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (White[i, col] == true)
                        break;
                    else if (Black[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (White[row, i] == true)
                        break;
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (White[row, i] == true)
                        break;
                    else if (Black[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            if (chessPieceLocation[row, col] == "Queen" && Black[row, col] == true)
            {
                int j = col + 1;
                for (int i = row + 1; i < 3 && j < 3; i++)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col + 1;
                for (int i = row - 1; i >= 0 && j < 3; i--)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j++;
                }
                j = col - 1;
                for (int i = row + 1; i < 3 && j >= 0; i++)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                j = col - 1;
                for (int i = row - 1; i >= 0 && j >= 0; i--)
                {
                    if (Black[i, j] == true)
                        break;
                    else if (White[i, j] == true)
                    {
                        PossiblePlacesToMove[i, j] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, j] = true;
                    j--;
                }
                for (int i = row + 1; i < 3; i++)
                {
                    if (Black[i, col] == true)
                        break;
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = row - 1; i >= 0; i--)
                {
                    if (Black[i, col] == true)
                        break;
                    else if (White[i, col] == true)
                    {
                        PossiblePlacesToMove[i, col] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[i, col] = true;
                }
                for (int i = col + 1; i < 3; i++)
                {
                    if (Black[row, i] == true)
                        break;
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
                for (int i = col - 1; i >= 0; i--)
                {
                    if (Black[row, i] == true)
                        break;
                    else if (White[row, i] == true)
                    {
                        PossiblePlacesToMove[row, i] = true;
                        break;
                    }
                    else
                        PossiblePlacesToMove[row, i] = true;
                }
            }

            if (chessPieceLocation[row, col] == "King")
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (White[row, col] == true)
                            if (((Math.Abs(i - row) == 1 && j == col) || (Math.Abs(j - col) == 1 && i == row) || (Math.Abs(i - row) == 1 && Math.Abs(j - col) == 1)) && (White[i, j] != true))
                                PossiblePlacesToMove[i, j] = true;
                        if (Black[row, col] == true)
                            if (((Math.Abs(i - row) == 1 && j == col) || (Math.Abs(j - col) == 1 && i == row) || (Math.Abs(i - row) == 1 && Math.Abs(j - col) == 1)) && (Black[i, j] != true))
                                PossiblePlacesToMove[i, j] = true;
                    }

            int Width = TicTacToeBoard.Width;
            int Length = TicTacToeBoard.Height;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (PossiblePlacesToMove[i, j] == true)
                        for (int k = i * Length / 3 + 1; k < i * Length / 3 + 78; k++)
                            for (int l = j * Width / 3 + 1; l < j * Width / 3 + 78; l++)
                                if (ImageArray[k, l] == Color.FromArgb(255, 255, 255, 255))
                                    ImageArray[k, l] = Color.PaleGreen;

            SetBitmapFromArray();
            TicTacToeBoard.Image = bitmapImage;
        }

        public void SetBitmapFromArray()
        {
            for (int y = 0; y < 240; y++)
            {
                for (int x = 0; x < 240; x++)
                {
                    bitmapImage.SetPixel(y, x, ImageArray[y, x]);
                }
            }
        }

        public void UpdatePieceCountTextBoxes()
        {
            if (WhiteTurn == true)
            {
                txtPawn.Text = ChessPieceCount[0].ToString();
                txtRook.Text = ChessPieceCount[1].ToString();
                txtKnight.Text = ChessPieceCount[2].ToString();
                txtBishop.Text = ChessPieceCount[3].ToString();
                txtQueen.Text = ChessPieceCount[4].ToString();
                txtKing.Text = ChessPieceCount[5].ToString();
            }
            else
            {
                txtPawn.Text = ChessPieceCount[6].ToString();
                txtRook.Text = ChessPieceCount[7].ToString();
                txtKnight.Text = ChessPieceCount[8].ToString();
                txtBishop.Text = ChessPieceCount[9].ToString();
                txtQueen.Text = ChessPieceCount[10].ToString();
                txtKing.Text = ChessPieceCount[11].ToString();
            }
        }

        public void StartNewGame()
        {
            TicTacToeBoard.Enabled = true;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    White[i, j] = false;
                    Black[i, j] = false;
                }
            WhiteTurn = true;

            Image TicTacToe = Image.FromFile(@"TicTacToeBoard.png");
            bitmapImage = new Bitmap(TicTacToe, 240, 240);
            TicTacToeBoard.Image = bitmapImage;

            for (int row = 0; row < 240; row++)
                for (int col = 0; col < 240; col++)
                {
                    ImageArray[row, col] = bitmapImage.GetPixel(row, col);
                }

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    chessPieceLocation[i, j] = "";
                }

            lblTurn.Text = "White's Turn";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartNewGame();
            int[] OriginalCount = { 8, 2, 2, 2, 1, 1, 8, 2, 2, 2, 1, 1 };
            for (int i = 0; i < 12; i++)
                ChessPieceCount[i] = OriginalCount[i];
            UpdatePieceCountTextBoxes();
        }

        public void LowerChessPieceCount()
        {
            if (chessPieceToAdd == "White Pawn")
                ChessPieceCount[0]--;
            else if (chessPieceToAdd == "White Rook")
                ChessPieceCount[1]--;
            else if (chessPieceToAdd == "White Knight")
                ChessPieceCount[2]--;
            else if (chessPieceToAdd == "White Bishop")
                ChessPieceCount[3]--;
            else if (chessPieceToAdd == "White Queen")
                ChessPieceCount[4]--;
            else if (chessPieceToAdd == "White King")
                ChessPieceCount[5]--;
            else if (chessPieceToAdd == "Black Pawn")
                ChessPieceCount[6]--;
            else if (chessPieceToAdd == "Black Rook")
                ChessPieceCount[7]--;
            else if (chessPieceToAdd == "Black Knight")
                ChessPieceCount[8]--;
            else if (chessPieceToAdd == "Black Bishop")
                ChessPieceCount[9]--;
            else if (chessPieceToAdd == "Black Queen")
                ChessPieceCount[10]--;
            else if (chessPieceToAdd == "Black King")
                ChessPieceCount[11]--;
        }

        public void AddChessPiece(int yLocation, int xLocation, string ChessPiece)
        {
            Image ChessPieceImage = Image.FromFile(@ChessPiece + ".bmp");
            Bitmap chessPiecebitmap = new Bitmap(ChessPieceImage, 60, 60);

            for (int row = 0; row < 60; row++)
                for (int col = 0; col < 60; col++)
                {
                    ChessPieceArray[row, col] = chessPiecebitmap.GetPixel(row, col);
                }

            int i = 0;
            for (int row = yLocation; row < yLocation + 60; row++)
            {
                int j = 0;
                for (int col = xLocation; col < xLocation + 60; col++)
                {
                    ImageArray[row, col] = ChessPieceArray[i, j];
                    j++;
                }
                i++;
            }
        }

        public void soWhoWins(string strWhoWins)
        {
            DialogResult dialogResult = MessageBox.Show(strWhoWins + " Would you like to start a new game?", "Good Game!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                StartNewGame();
            else
                Application.Exit();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int mouseX = e.X;
            int mouseY = e.Y;

            int WidthSquare = TicTacToeBoard.Width / 3;
            int LengthSquare = TicTacToeBoard.Height / 3;

            Color Pixel = ImageArray[mouseX, mouseY];

            if (WhiteTurn == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if ((mouseX < (i + 1) * LengthSquare) && (mouseX > i * LengthSquare) && (mouseY < (j + 1) * WidthSquare) && (mouseY > j * WidthSquare))
                        {
                            if (PossiblePlacesToMove[i, j] == true)
                            {
                                int yOriginal = yPieceToMove * LengthSquare + 1;
                                for (int m = i * LengthSquare + 1; m < i * LengthSquare + 78; m++)
                                {
                                    int xOriginal = xPieceToMove * WidthSquare + 1;
                                    for (int l = j * WidthSquare + 1; l < j * WidthSquare + 78; l++)
                                    {
                                        ImageArray[m, l] = ImageArray[yOriginal, xOriginal];
                                        xOriginal++;
                                    }
                                    yOriginal++;
                                }

                                for (yOriginal = yPieceToMove * LengthSquare + 1; yOriginal < yPieceToMove * LengthSquare + 78; yOriginal++)
                                    for (int xOriginal = xPieceToMove * WidthSquare + 1; xOriginal < xPieceToMove * WidthSquare + 78; xOriginal++)
                                        ImageArray[yOriginal, xOriginal] = Color.FromArgb(255, 255, 255, 255);

                                chessPieceLocation[i, j] = chessPieceLocation[yPieceToMove, xPieceToMove];
                                chessPieceLocation[yPieceToMove, xPieceToMove] = "";

                                if (i == 0 && chessPieceLocation[i, j] == "Pawn")
                                {

                                }

                                WhiteTurn = false;
                                White[i, j] = true;
                                Black[i, j] = false;
                                White[yPieceToMove, xPieceToMove] = false;
                                break;
                            }
                            else if (White[i, j] == true)
                            {
                                FindPossiblePlacesToMove(i, j);
                                xPieceToMove = j;
                                yPieceToMove = i;
                                return;
                            }
                            else if (chessPieceToAdd != "" && chessPieceLocation[i, j] == "")
                            {
                                int yPosition = i * LengthSquare + 10, xPosition = j * WidthSquare + 10;
                                if (chessPieceToAdd == "White Pawn" && xPosition < 160)
                                    return;

                                AddChessPiece(yPosition, xPosition, chessPieceToAdd);
                                chessPieceLocation[i, j] = chessPieceToAdd.Remove(0, 6);
                                WhiteTurn = false;
                                White[i, j] = true;
                                LowerChessPieceCount();
                                UpdatePieceCountTextBoxes();
                                break;
                            }
                        }
                    }
                    if (WhiteTurn == false)
                        break;
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if ((mouseX < (i + 1) * LengthSquare) && (mouseX > i * LengthSquare) && (mouseY < (j + 1) * WidthSquare) && (mouseY > j * WidthSquare))
                        {
                            if (PossiblePlacesToMove[i, j] == true)
                            {
                                int yOriginal = yPieceToMove * LengthSquare + 1;
                                for (int m = i * LengthSquare + 1; m < i * LengthSquare + 78; m++)
                                {
                                    int xOriginal = xPieceToMove * WidthSquare + 1;
                                    for (int l = j * WidthSquare + 1; l < j * WidthSquare + 78; l++)
                                    {
                                        ImageArray[m, l] = ImageArray[yOriginal, xOriginal];
                                        xOriginal++;
                                    }
                                    yOriginal++;
                                }

                                for (yOriginal = yPieceToMove * LengthSquare + 1; yOriginal < yPieceToMove * LengthSquare + 78; yOriginal++)
                                    for (int xOriginal = xPieceToMove * WidthSquare + 1; xOriginal < xPieceToMove * WidthSquare + 78; xOriginal++)
                                        ImageArray[yOriginal, xOriginal] = Color.FromArgb(255, 255, 255, 255);

                                SetPossiblePlacesToMoveToFalse();

                                chessPieceLocation[i, j] = chessPieceLocation[yPieceToMove, xPieceToMove];
                                chessPieceLocation[yPieceToMove, xPieceToMove] = "";
                                WhiteTurn = true;
                                Black[i, j] = true;
                                White[i, j] = false;
                                Black[yPieceToMove, xPieceToMove] = false;
                                break;
                            }
                            else if (Black[i, j] == true)
                            {
                                FindPossiblePlacesToMove(i, j);
                                xPieceToMove = j;
                                yPieceToMove = i;
                                return;
                            }
                            else if (chessPieceToAdd != "" && chessPieceLocation[i, j] == "")
                            {
                                int yPosition = i * LengthSquare + 10, xPosition = j * WidthSquare + 10;
                                if (chessPieceToAdd == "Black Pawn" && xPosition > 80)
                                    return;
                                AddChessPiece(yPosition, xPosition, chessPieceToAdd);
                                chessPieceLocation[i, j] = chessPieceToAdd.Remove(0, 6);
                                WhiteTurn = true;
                                Black[i, j] = true;
                                LowerChessPieceCount();
                                UpdatePieceCountTextBoxes();
                                break;
                            }
                        }
                    }
                    if (WhiteTurn == true)
                        break;
                }
            }

            SetBitmapFromArray();
            TicTacToeBoard.Image = bitmapImage;
            chessPieceToAdd = "";
            SetPossiblePlacesToMoveToFalse();
            ClearPlacesToMove();

            if (WhiteTurn == true)
                lblTurn.Text = "White's Turn";
            else
                lblTurn.Text = "Black's Turn";

            int iOThreeColumn = 0, iOThreeRow = 0, iXThreeColumn = 0, iXThreeRow = 0, k = 0;
            int iOForwardSlash = 0, iOBackwardSlash = 0, iXForwardSlash = 0, iXBackwardSlash = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (White[i, j] == true)
                        iOThreeColumn++;
                    if (White[j, i] == true)
                        iOThreeRow++;
                    if (Black[i, j] == true)
                        iXThreeColumn++;
                    if (Black[j, i] == true)
                        iXThreeRow++;
                }
                if (iOThreeColumn == 3 || iOThreeRow == 3)
                {
                    string strWhoWins = "Congratulations! White Wins!";
                    soWhoWins(strWhoWins);
                }
                else if (iXThreeColumn == 3 || iXThreeRow == 3)
                {
                    string strWhoWins = "Congratulations! Black Wins!";
                    soWhoWins(strWhoWins);
                }
                iOThreeColumn = 0;
                iOThreeRow = 0;
                iXThreeColumn = 0;
                iXThreeRow = 0;
            }

            for (int i = 0; i < 3; i++)
            {
                if (White[i, k] == true)
                    iOForwardSlash++;
                if (White[i, 2 - k] == true)
                    iOBackwardSlash++;
                if (Black[i, k] == true)
                    iXForwardSlash++;
                if (Black[i, 2 - k] == true)
                    iXBackwardSlash++;
                k++;
            }

            if (iOForwardSlash == 3 || iOBackwardSlash == 3)
            {
                string strWhoWins = "Congratulations! White Wins!";
                soWhoWins(strWhoWins);
            }

            if (iXForwardSlash == 3 || iXBackwardSlash == 3)
            {
                string strWhoWins = "Congratulations! Black Wins!";
                soWhoWins(strWhoWins);
            }

            int TotalNumberUsed = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (White[i, j] || Black[i, j] == true)
                        TotalNumberUsed++;
                }

            if (TotalNumberUsed == 9)
            {
                string strWhoWins = "It's a tie!";
                soWhoWins(strWhoWins);
            }


        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        public void FindAvailableSpaces(string WhitePiece, string BlackPiece)
        {
            if (TicTacToeBoard.Enabled == true)
            {
                SetPossiblePlacesToMoveToFalse();
                ClearPlacesToMove();
                if (TicTacToeBoard.Enabled == true)
                {
                    if (WhiteTurn == true)
                        chessPieceToAdd = WhitePiece;
                    else
                        chessPieceToAdd = BlackPiece;
                }

                int Width = TicTacToeBoard.Width;
                int Length = TicTacToeBoard.Height;
                Graphics g = Graphics.FromImage(TicTacToeBoard.Image);
                SolidBrush yellowBrush = new SolidBrush(Color.Linen);

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (chessPieceToAdd == "White Pawn")
                        {
                            if (chessPieceLocation[i, 2] == "")
                                g.FillRectangle(yellowBrush, i * Length / 3 + 1, 2 * Width / 3 + 1, 77, 77);
                        }
                        else if (chessPieceToAdd == "Black Pawn")
                        {
                            if (chessPieceLocation[i, 0] == "")
                                g.FillRectangle(yellowBrush, i * Length / 3 + 1, 1, 77, 77);
                        }
                        else
                        {
                            if (chessPieceLocation[i, j] == "")
                                g.FillRectangle(yellowBrush, i * Length / 3 + 1, j * Width / 3 + 1, 77, 77);
                        }
                    }
            }
        }

        public void ShowNoMorePieceMessageBox()
        {
            MessageBox.Show("Sorry, you do not have that piece anymore to place on the board.");
        }

        private void btnPawn_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[0] == 0)
                ShowNoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[6] == 0)
                ShowNoMorePieceMessageBox();
            else
                FindAvailableSpaces("White Pawn", "Black Pawn");
        }

        private void btnRook_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[1] == 0)
                ShowNoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[7] == 0)
                ShowNoMorePieceMessageBox();
            else
                FindAvailableSpaces("White Rook", "Black Rook");
        }

        private void btnKnight_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[2] == 0)
                ShowNoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[8] == 0)
                ShowNoMorePieceMessageBox();
            else
                FindAvailableSpaces("White Knight", "Black Knight");
        }

        private void btnBishop_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[3] == 0)
                ShowNoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[9] == 0)
                ShowNoMorePieceMessageBox();
            else
                FindAvailableSpaces("White Bishop", "Black Bishop");
        }

        private void btnQueen_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[4] == 0)
                ShowNoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[10] == 0)
                ShowNoMorePieceMessageBox();
            else
                FindAvailableSpaces("White Queen", "Black Queen");
        }

        private void btnKing_Click(object sender, EventArgs e)
        {
            if (WhiteTurn == true && ChessPieceCount[5] == 0)
                ShowNoMorePieceMessageBox();
            else if (WhiteTurn == false && ChessPieceCount[11] == 0)
                ShowNoMorePieceMessageBox();
            else
                FindAvailableSpaces("White King", "Black King");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPlacesToMove();
            ClearAvailableSpaceToAdd();
            SetPossiblePlacesToMoveToFalse();
            chessPieceToAdd = "";
        }
    }
}

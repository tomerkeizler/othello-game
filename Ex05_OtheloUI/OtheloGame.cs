using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Ex05_OtheloUI
{
    public partial class OtheloGame : Form
    {
        //--------------- Class members ---------------

        private FormSettings m_SettingsForm = new FormSettings();
        private Game m_Game;
        private int m_Rounds = 1;
        private OtheloTile[,] m_RegularMatrix;
        private GroupBox m_GroupBoxMatrix = new GroupBox();


        //--------------- Constructor ---------------

        public OtheloGame()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            m_SettingsForm.ShowDialog();

            if (m_SettingsForm.DialogResult == DialogResult.OK)
            {
                m_Game = new Game(m_SettingsForm.BoardSize, m_SettingsForm.GameType);
                InitializeOtheloMatrix(); // initialize the UI board
                startGame();
            }
            else
            {
                Close();
            }
        }


        //--------------- Matrix methods ---------------

        private void InitializeOtheloMatrix()
        {
            int boardSize = m_SettingsForm.BoardSize;
            m_RegularMatrix = new OtheloTile[boardSize, boardSize];

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    m_RegularMatrix[i, j] = new OtheloTile(new Tile(i, j));
                    m_RegularMatrix[i, j].Size = new Size(50, 50);
                    m_RegularMatrix[i, j].Location = new System.Drawing.Point(50 * j, 50 * i);
                    m_RegularMatrix[i, j].BorderStyle = BorderStyle.Fixed3D;
                    m_RegularMatrix[i, j].Click += new EventHandler(OtheloTile_Click);
                    m_GroupBoxMatrix.Controls.Add(m_RegularMatrix[i, j]);

                    m_RegularMatrix[i, j].Enabled = true;
                }
            }

            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Size = new Size(40 + 50 * boardSize, 60 + 50 * boardSize);
            m_GroupBoxMatrix.Size = new Size(50 * boardSize, 50 * boardSize);
            m_GroupBoxMatrix.Location = new System.Drawing.Point(10, 10);
            Controls.Add(m_GroupBoxMatrix);
        }


        private void updateMatrixUI()
        {
            for (int i = 0; i < m_SettingsForm.BoardSize; i++)
            {
                for (int j = 0; j < m_SettingsForm.BoardSize; j++)
                {
                    if (m_Game.Board[i, j].Color == Game.Colors.RED)
                    {
                        m_RegularMatrix[i, j].Image = Properties.Resources.CoinRed;
                    }
                    else if (m_Game.Board[i, j].Color == Game.Colors.YELLOW)
                    {
                        m_RegularMatrix[i, j].Image = Properties.Resources.CoinYellow;
                    }
                    m_RegularMatrix[i, j].BackColor = Color.Empty;
                    m_RegularMatrix[i, j].Enabled = false;
                    m_RegularMatrix[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            Refresh();
        }


        public void clearMatrixUI()
        {
            for (int i = 0; i < m_SettingsForm.BoardSize; i++)
            {
                for (int j = 0; j < m_SettingsForm.BoardSize; j++)
                {
                    m_RegularMatrix[i, j].BackColor = Color.Empty;
                    m_RegularMatrix[i, j].Image = null;
                }
            }
        }


        private void markPossibleMoves()
        {
            foreach (Tile currentMove in m_Game.ValidMovesOfCurrentPlayer)
            {
                m_RegularMatrix[currentMove.Row, currentMove.Col].BackColor = Color.Green;
                m_RegularMatrix[currentMove.Row, currentMove.Col].Enabled = true;
                m_RegularMatrix[currentMove.Row, currentMove.Col].Click += new EventHandler(OtheloTile_Click);
            }
        }


        //--------------- General gameplay methods ---------------

        private void startGame()
        {
            m_Game.ClearGameDetails(); // clear and initialize the logical board
            updateMatrixUI(); // clear and initialize the UI board
            Text = string.Format("Othelo - {0}'s turn", m_Game.CurrentPlayer.Color.ToString());

            m_Game.CountPossibleMoves();
            markPossibleMoves(); // color the possible moves in green
        }


        private void switchTurnsUI()
        {
            m_Game.ClearPossibleMovesOfCurrentPlayer();
            m_Game.SwitchTurns();
            Text = string.Format("Othelo - {0}'s turn", m_Game.CurrentPlayer.Color.ToString());
        }

        
        private void OtheloTile_Click(object sender, EventArgs e)
        {
            if ((sender as OtheloTile).BackColor == Color.Green)
            {
                playHumanMove((sender as OtheloTile).GameTile);
            }
        }


        private void endGame()
        {
            Player winner = m_Game.CheckWinner();
            string message;

            if (winner == null)
            {
                m_Game.CurrentPlayer.IncreaseWins();
                m_Game.OppositePlayer.IncreaseWins();
                message = "This is a tie - we have 2 winners !";
            }
            else
            {
                winner.IncreaseWins();
                Player loser = m_Game.GetPlayers[(int)m_Game.SwitchColorValue(winner.Color)];
                message = string.Format($"{winner.Color.ToString()} Won!! ({winner.Score}/{loser.Score}) ({winner.NumOfWins}/{m_Rounds}) \nDo you wish to play again?");
            }

            MessageBoxButtons yesNoButtons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, "Othelo - GAME OVER", yesNoButtons);

            if (result == DialogResult.Yes)
            {
                m_Rounds++;
                m_Game.ClearPossibleMovesOfCurrentPlayer();///////////
                clearMatrixUI();////////
                startGame();
            }
            else
            {
                Environment.Exit(0);
            }
        }


        //--------------- Specific moves methods ---------------

        private void playMove(Tile i_tilePlayed)
        {
            m_Game.Play(i_tilePlayed.Row, i_tilePlayed.Col);
            updateMatrixUI();
            switchTurnsUI();

            // the next player has at least one possible move
            if (m_Game.CountPossibleMoves())
            {
                markPossibleMoves();
            }

            // the next player has no possible moves, but the game board is not full yet
            else if (!m_Game.IsBoardFull())
            {
                StringBuilder noMovesMsg = new StringBuilder();
                noMovesMsg.Append(m_Game.CurrentPlayer.Color.ToString());
                noMovesMsg.Append(" has no possible moves. the turn goes to ");
                noMovesMsg.Append(m_Game.OppositePlayer.Color.ToString());
                MessageBox.Show(noMovesMsg.ToString());

                switchTurnsUI();

                if (m_Game.CountPossibleMoves())
                {
                    markPossibleMoves();
                }
                else
                {
                    m_Game.IsThereAnyPossibleMove = false;
                }
            }

            // conntinue the game while:
            // 1) at least one of the players can make a move
            // 2) the board is not full yet
            if (m_Game.IsThereAnyPossibleMove && !m_Game.IsBoardFull())
            {
                if (!m_Game.CurrentPlayer.isHumanPlayer())
                {
                    playComputerMove();
                }
            }
            else // GAME OVER
            {
                endGame();
            }
        }


        private void playComputerMove()
        {
            playMove(m_Game.GenerateRandomTile());
        }


        private void playHumanMove(Tile i_tileClicked)
        {
            playMove(i_tileClicked);
        }

    }
}

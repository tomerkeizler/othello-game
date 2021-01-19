using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05_OtheloUI
{
    public class Game
    {
        public enum Colors
        {
            RED,
            YELLOW,
            NONE
        }

        public enum PossibleDirections
        {
            UP,
            UP_RIGHT,
            RIGHT,
            DOWN_RIGHT,
            DOWN,
            DOWN_LEFT,
            LEFT, UP_LEFT
        }

        public static Tile[] s_AdvanceDirections = new Tile[] { new Tile(-1, 0), new Tile(-1, 1), new Tile(0, 1), new Tile(1, 1), new Tile(1, 0), new Tile(1, -1), new Tile(0, -1), new Tile(-1, -1) };


        // Class members

        private Tile[,] m_Board;
        private Player[] m_Players = new Player[2];
        private Colors m_CurrentPlayer = Colors.RED;
        private List<Tile> m_ValidMovesOfCurrentPlayer = new List<Tile>();
        private bool m_IsThereAnyPossibleMove = true;

        // Constructor

        public Game(int i_BoardSize, Player.PlayerTypes i_GameType)
        {
            m_Board = new Tile[i_BoardSize, i_BoardSize];
            m_Players[0] = new Player(Colors.RED, Player.PlayerTypes.PLAYER);
            m_Players[1] = new Player(Colors.YELLOW, i_GameType);

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    m_Board[row, col] = new Tile(row, col);
                }
            }
        }


        /////////////////////////////////////////////
        // --------- Getters and Setters --------- //
        /////////////////////////////////////////////

        public Tile[,] Board
        {
            get { return m_Board; }
        }

        public Player[] GetPlayers
        {
            get { return m_Players; }
        }

        public Player CurrentPlayer
        {
            get { return m_Players[(int)m_CurrentPlayer]; }
        }

        public Player OppositePlayer
        {
            get { return m_Players[(int)SwitchColorValue(m_CurrentPlayer)]; }
        }

        public List<Tile> ValidMovesOfCurrentPlayer
        {
            get { return m_ValidMovesOfCurrentPlayer; }
        }

        public bool IsThereAnyPossibleMove
        {
            get { return m_IsThereAnyPossibleMove; }
            set { m_IsThereAnyPossibleMove = value; }
        }

        public int BoardSize
        {
            get { return m_Board.GetLength(0); }
        }


        ///////////////////////////////////////
        // --------- Other methods --------- //
        ///////////////////////////////////////

        public bool IsInBoardRange(int row, int col)
        {
            return (row >= 0 && row < BoardSize) && (col >= 0 && col < BoardSize);
        }


        public bool IsValidTile(ref Tile io_TileToPlay)
        {
            io_TileToPlay.FlipsOfPossibleMove.Clear(); // clear possible moves from the previous turn of the game
            List<Tile> possibleFlipsToAdd = new List<Tile>();
            bool isValidMove = false;

            // ensure that the requested tile is currently empty
            if (io_TileToPlay.Color == Colors.NONE)
            {
                // check throguh each direction whether there are tiles to flip
                int tileCounter, nextRow, nextCol;
                foreach (Tile direction in s_AdvanceDirections)
                {
                    tileCounter = 0;
                    nextRow = io_TileToPlay.Row + direction.Row;
                    nextCol = io_TileToPlay.Col + direction.Col;
                    if (IsInBoardRange(nextRow, nextCol))
                    {
                        while (IsInBoardRange(nextRow, nextCol) && IsDifferentColors(m_Board[nextRow, nextCol].Color, m_CurrentPlayer))
                        {
                            possibleFlipsToAdd.Add(new Tile(nextRow, nextCol));
                            tileCounter++;
                            nextRow += direction.Row;
                            nextCol += direction.Col;
                        }
                    }

                    if (tileCounter > 0 && IsInBoardRange(nextRow, nextCol) && m_Board[nextRow, nextCol].Color == m_CurrentPlayer)
                    {
                        io_TileToPlay.FlipsOfPossibleMove.AddRange(possibleFlipsToAdd);
                        isValidMove = true; // found at least one direction with possible flips
                    }
                    possibleFlipsToAdd.Clear();
                }
            }
            return isValidMove;
        }


        public bool CountPossibleMoves()
        {
            bool areThereMoves = false;
            Tile currentTile;

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    currentTile = m_Board[row, col];

                    // check if the current tile is a valid move
                    if (IsValidTile(ref currentTile))
                    {
                        // add the current tile to the list of possible moves of the current player
                        m_ValidMovesOfCurrentPlayer.Add(currentTile);
                        areThereMoves = true;
                    }
                }
            }
            return areThereMoves;
        }


        public void UpdateBoard(int i_row, int i_col)
        {
            // perform the new move on the board
            m_Board[i_row, i_col].Color = m_Players[(int)m_CurrentPlayer].Color;

            // flip tiles on the board according to the new move
            List<Tile> tilesToFlip = m_Board[i_row, i_col].FlipsOfPossibleMove;
            foreach (Tile toFlip in tilesToFlip)
            {
                Board[toFlip.Row, toFlip.Col].Flip();
            }

            // update the score of the current player
            m_Players[(int)m_CurrentPlayer].Score += tilesToFlip.Count + 1;

            // update the score of the opponent player
            Colors opponent = SwitchColorValue(m_CurrentPlayer);
            m_Players[(int)opponent].Score -= tilesToFlip.Count;
        }


        public Tile GenerateRandomTile()
        {
            int numberOfOptions = ValidMovesOfCurrentPlayer.Count();
            int randomTileNumber = new Random().Next(0, numberOfOptions);
            return ValidMovesOfCurrentPlayer.ElementAt(randomTileNumber);
        }


        public bool Play(int i_Row, int i_Col)
        {
            bool hasPlayed = false;

            if (m_ValidMovesOfCurrentPlayer.Contains(new Tile(i_Row, i_Col)))
            {
                hasPlayed = true; // this is a valid move
                UpdateBoard(i_Row, i_Col); // perform the requested move
            }

            return hasPlayed;
        }


        public void ClearPossibleMovesOfCurrentPlayer()
        {
            // clear the list of possible moves of the current player
            ValidMovesOfCurrentPlayer.Clear();

            // for each tile - clear the list of possible flips in case of a move
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    m_Board[row, col].FlipsOfPossibleMove.Clear();
                }
            }
        }


        public bool IsBoardFull()
        {
            return (CurrentPlayer.Score + OppositePlayer.Score) == Board.Length;
        }


        public void ClearGameDetails()
        {
            m_Players[0].Score = 2;
            m_Players[1].Score = 2;

            m_CurrentPlayer = Colors.RED;
            m_ValidMovesOfCurrentPlayer.Clear();

            m_IsThereAnyPossibleMove = true;

            // clear all of the board tiles
            foreach (Tile currentTile in m_Board)
            {
                currentTile.Color = Colors.NONE;
            }

            // initialize the central 4 tiles on the board
            int middleOfBoard = BoardSize / 2;
            m_Board[middleOfBoard - 1, middleOfBoard - 1].Color = Colors.RED;
            m_Board[middleOfBoard - 1, middleOfBoard].Color = Colors.YELLOW;
            m_Board[middleOfBoard, middleOfBoard - 1].Color = Colors.YELLOW;
            m_Board[middleOfBoard, middleOfBoard].Color = Colors.RED;
        }


        public void SwitchTurns()
        {
            if (m_CurrentPlayer == Colors.RED)
            {
                m_CurrentPlayer = Colors.YELLOW;
            }
            else
            {
                m_CurrentPlayer = Colors.RED;
            }
        }


        public Colors SwitchColorValue(Colors i_Color)
        {
            Colors oppositeColor = Colors.NONE;
            if (i_Color == Colors.RED)
            {
                oppositeColor = Colors.YELLOW;
            }
            else if (i_Color == Colors.YELLOW)
            {
                oppositeColor = Colors.RED;
            }
            return oppositeColor;
        }


        public bool IsDifferentColors(Colors i_Color1, Colors i_Color2)
        {
            return (i_Color1 == Colors.RED && i_Color2 == Colors.YELLOW)
                || (i_Color1 == Colors.YELLOW && i_Color2 == Colors.RED);
        }


        public Player CheckWinner()
        {
            Player winner;
            if (CurrentPlayer.Score > OppositePlayer.Score)
            {
                winner = CurrentPlayer;
            }
            else if (OppositePlayer.Score > CurrentPlayer.Score)
            {
                winner = OppositePlayer;
            }
            else
            {
                winner = null;
            }
            return winner;
        }
        
    }
}

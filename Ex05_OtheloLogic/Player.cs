using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05_OtheloUI
{
    public class Player
    {
        public enum PlayerTypes
        {
            PLAYER,
            COMPUTER
        }

        private Game.Colors m_Color;
        private int m_Score;
        private PlayerTypes m_PlayerType;
        private int m_NumOfWins;


        public Player(Game.Colors i_color, PlayerTypes i_PlayerType)
        {
            m_Color = i_color;
            m_Score = 2;
            m_PlayerType = i_PlayerType;
        }

        public Game.Colors Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public char BoardSign
        {
            get { return (m_Color == Game.Colors.RED) ? 'O' : 'X'; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public int NumOfWins
        {
            get { return m_NumOfWins; }
        }

        public void IncreaseWins()
        {
            m_NumOfWins++;
        }

        public PlayerTypes PlayerType
        {
            get { return m_PlayerType; }
            set { m_PlayerType = value; }
        }

        public bool isHumanPlayer()
        {
            return PlayerType == PlayerTypes.PLAYER;
        }

    }
}

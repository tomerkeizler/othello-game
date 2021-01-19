using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05_OtheloUI
{
    public class Tile : IEquatable<Tile>
    {
        private int m_Row;
        private int m_Col;
        private Game.Colors m_Color;
        // flips that will be done if the current player puts a coin in this tile
        private List<Tile> m_FlipsOfPossibleMove = new List<Tile>();


        public Tile(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
            m_Color = Game.Colors.NONE;
        }

        public Tile(int i_Row, int i_Col, Game.Colors i_Color)
        {
            m_Row = i_Row;
            m_Col = i_Col;
            m_Color = i_Color;
        }

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int Col
        {
            get { return m_Col; }
            set { m_Col = value; }
        }

        public Game.Colors Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public List<Tile> FlipsOfPossibleMove
        {
            get { return m_FlipsOfPossibleMove; }
        }

        public void Flip()
        {
            if (m_Color == Game.Colors.RED)
            {
                m_Color = Game.Colors.YELLOW;
            }
            else if (m_Color == Game.Colors.YELLOW)
            {
                m_Color = Game.Colors.RED;
            }
        }

        public char printTile()
        {
            if (m_Color == Game.Colors.YELLOW)
            {
                return 'X';
            }
            else if (Color == Game.Colors.RED)
            {
                return 'O';
            }
            return '\0';
        }

        public bool Equals(Tile otherTile)
        {
            return m_Row == otherTile.m_Row && m_Col == otherTile.m_Col;
        }


    }
}

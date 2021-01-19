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
    partial class FormSettings : Form
    {
        public static int[] s_PossibleBoardSizes = new int[] { 6, 8, 10, 12 };
        private int m_BoardSizeIndex = 0;
        Player.PlayerTypes m_GameType;

        public int BoardSize
        {
            get { return s_PossibleBoardSizes[m_BoardSizeIndex]; }
        }

        public Player.PlayerTypes GameType
        {
            get { return m_GameType; }
        }

        public FormSettings()
        {
            StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
        }

        private void sizeAdjustButton_Click(object sender, EventArgs e)
        {
            if (m_BoardSizeIndex == 3)
            {
                m_BoardSizeIndex = 0;
            }
            else
            {
                m_BoardSizeIndex++;
            }

            StringBuilder buttonText = new StringBuilder("Board size: ");
            buttonText.Append(BoardSize);
            buttonText.Append("x");
            buttonText.Append(BoardSize);
            buttonText.Append(" (click to increase)");
            sizeAdjustButton.Text = buttonText.ToString();
        }

        private void playAgainstButtons_Click(object sender, EventArgs e)
        {
            if (sender == playAgainstComButton)
            {
                m_GameType = Player.PlayerTypes.COMPUTER;
            }
            else if(sender == playAgainstFriendButton)
            {
                m_GameType = Player.PlayerTypes.PLAYER;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

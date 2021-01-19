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
    partial class OtheloTile : PictureBox
    {
        private readonly Tile r_Tile;

        public Tile GameTile
        {
            get { return r_Tile; }
        }

        public OtheloTile(Tile i_Tile) : base()
        {
            r_Tile = i_Tile;
            Enabled = false;
            Text = string.Empty;
            BackgroundImage = null;
        }

    }
}

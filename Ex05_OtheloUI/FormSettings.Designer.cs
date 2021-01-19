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
    public partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sizeAdjustButton = new System.Windows.Forms.Button();
            this.playAgainstComButton = new System.Windows.Forms.Button();
            this.playAgainstFriendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sizeAdjustButton
            // 
            this.sizeAdjustButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sizeAdjustButton.Location = new System.Drawing.Point(90, 46);
            this.sizeAdjustButton.Name = "sizeAdjustButton";
            this.sizeAdjustButton.Size = new System.Drawing.Size(544, 51);
            this.sizeAdjustButton.TabIndex = 0;
            this.sizeAdjustButton.Text = "Board size: 6x6 (click to increase)";
            this.sizeAdjustButton.UseVisualStyleBackColor = true;
            this.sizeAdjustButton.Click += new System.EventHandler(this.sizeAdjustButton_Click);
            // 
            // playAgainstComButton
            // 
            this.playAgainstComButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playAgainstComButton.Location = new System.Drawing.Point(24, 132);
            this.playAgainstComButton.Name = "playAgainstComButton";
            this.playAgainstComButton.Size = new System.Drawing.Size(313, 54);
            this.playAgainstComButton.TabIndex = 1;
            this.playAgainstComButton.Text = "Play against the computer";
            this.playAgainstComButton.UseVisualStyleBackColor = true;
            this.playAgainstComButton.Click += new System.EventHandler(this.playAgainstButtons_Click);
            // 
            // playAgainstFriendButton
            // 
            this.playAgainstFriendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playAgainstFriendButton.Location = new System.Drawing.Point(366, 132);
            this.playAgainstFriendButton.Name = "playAgainstFriendButton";
            this.playAgainstFriendButton.Size = new System.Drawing.Size(313, 54);
            this.playAgainstFriendButton.TabIndex = 2;
            this.playAgainstFriendButton.Text = "Play against your friend";
            this.playAgainstFriendButton.UseVisualStyleBackColor = true;
            this.playAgainstFriendButton.Click += new System.EventHandler(this.playAgainstButtons_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 237);
            this.Controls.Add(this.playAgainstFriendButton);
            this.Controls.Add(this.playAgainstComButton);
            this.Controls.Add(this.sizeAdjustButton);
            this.Name = "FormSettings";
            this.Text = "FormSettings";
            this.ResumeLayout(false);

        }

        #endregion

        private Button sizeAdjustButton;
        private Button playAgainstComButton;
        private Button playAgainstFriendButton;
    }
}
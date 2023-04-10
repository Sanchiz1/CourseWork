﻿namespace Billiard
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            Main = new PictureBox();
            Menu = new PictureBox();
            Start = new Button();
            Resume = new Button();
            MenuButton = new Button();
            Player = new Label();
            timer3 = new System.Windows.Forms.Timer(components);
            Scores = new Label();
            ResultLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)Main).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Menu).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1;
            timer1.Tick += timer1_Tick;
            // 
            // timer2
            // 
            timer2.Enabled = true;
            timer2.Interval = 1;
            timer2.Tick += timer2_Tick;
            // 
            // Main
            // 
            Main.Dock = DockStyle.Fill;
            Main.Location = new Point(0, 0);
            Main.Name = "Main";
            Main.Size = new Size(1468, 721);
            Main.TabIndex = 0;
            Main.TabStop = false;
            Main.Paint += Main_Paint;
            Main.MouseClick += Main_MouseClick;
            Main.MouseDown += Main_MouseDown;
            Main.MouseMove += Main_MouseMove;
            Main.MouseUp += Main_MouseUp;
            // 
            // Menu
            // 
            Menu.Anchor = AnchorStyles.None;
            Menu.BackColor = Color.FromArgb(255, 192, 128);
            Menu.Location = new Point(12, 12);
            Menu.MaximumSize = new Size(700, 700);
            Menu.Name = "Menu";
            Menu.Size = new Size(700, 700);
            Menu.TabIndex = 1;
            Menu.TabStop = false;
            // 
            // Start
            // 
            Start.BackColor = Color.SaddleBrown;
            Start.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            Start.ForeColor = SystemColors.ButtonFace;
            Start.Location = new Point(194, 298);
            Start.Name = "Start";
            Start.Size = new Size(302, 84);
            Start.TabIndex = 2;
            Start.Text = "New game";
            Start.UseVisualStyleBackColor = false;
            Start.Click += Start_Click;
            // 
            // Resume
            // 
            Resume.BackColor = Color.SaddleBrown;
            Resume.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            Resume.ForeColor = SystemColors.ButtonFace;
            Resume.Location = new Point(194, 182);
            Resume.Name = "Resume";
            Resume.Size = new Size(302, 84);
            Resume.TabIndex = 3;
            Resume.Text = "Resume";
            Resume.UseVisualStyleBackColor = false;
            Resume.Click += Resume_Click;
            // 
            // MenuButton
            // 
            MenuButton.BackColor = Color.FromArgb(255, 192, 128);
            MenuButton.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            MenuButton.ForeColor = Color.SaddleBrown;
            MenuButton.Location = new Point(12, 12);
            MenuButton.Name = "MenuButton";
            MenuButton.Size = new Size(156, 84);
            MenuButton.TabIndex = 5;
            MenuButton.Text = "Menu";
            MenuButton.UseVisualStyleBackColor = false;
            MenuButton.Click += MenuButton_Click;
            // 
            // Player
            // 
            Player.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Player.AutoSize = true;
            Player.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            Player.ForeColor = SystemColors.ButtonFace;
            Player.Location = new Point(1197, 9);
            Player.Name = "Player";
            Player.Size = new Size(259, 37);
            Player.TabIndex = 6;
            Player.Text = "Second player turn";
            // 
            // timer3
            // 
            timer3.Enabled = true;
            timer3.Interval = 1;
            timer3.Tick += timer3_Tick;
            // 
            // Scores
            // 
            Scores.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Scores.AutoSize = true;
            Scores.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            Scores.ForeColor = SystemColors.ButtonFace;
            Scores.Location = new Point(1197, 59);
            Scores.Name = "Scores";
            Scores.Size = new Size(214, 60);
            Scores.TabIndex = 7;
            Scores.Text = "First player score:\r\nSecond player score:";
            // 
            // ResultLabel
            // 
            ResultLabel.BackColor = Color.FromArgb(255, 192, 128);
            ResultLabel.Font = new Font("Segoe Script", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            ResultLabel.ForeColor = Color.SaddleBrown;
            ResultLabel.Location = new Point(92, 99);
            ResultLabel.Name = "ResultLabel";
            ResultLabel.Size = new Size(531, 80);
            ResultLabel.TabIndex = 9;
            ResultLabel.Text = "label1";
            ResultLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SaddleBrown;
            ClientSize = new Size(1468, 721);
            Controls.Add(ResultLabel);
            Controls.Add(Scores);
            Controls.Add(Player);
            Controls.Add(MenuButton);
            Controls.Add(Resume);
            Controls.Add(Start);
            Controls.Add(Menu);
            Controls.Add(Main);
            Name = "Form1";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)Main).EndInit();
            ((System.ComponentModel.ISupportInitialize)Menu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private PictureBox Main;
        private PictureBox Menu;
        private Button Start;
        private Button Resume;
        private Button MenuButton;
        private Label Player;
        private System.Windows.Forms.Timer timer3;
        private Label Scores;
        private Label ResultLabel;
    }
}
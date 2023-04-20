using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using Microsoft.VisualBasic.Devices;
using NAudio.Wave;

namespace Billiard
{
    public partial class Form1 : Form
    {
        public Game game;
        public Settings settings;
        public WaveOut waveOut = new WaveOut();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            Main.Visible = false;
            Start.TabStop = false;
            Start.FlatStyle = FlatStyle.Flat;
            Start.FlatAppearance.BorderSize = 0;
            Resume.TabStop = false;
            Resume.FlatStyle = FlatStyle.Flat;
            Resume.FlatAppearance.BorderSize = 0;
            Settings.TabStop = false;
            Settings.FlatStyle = FlatStyle.Flat;
            Settings.FlatAppearance.BorderSize = 0;
            MenuButton.TabStop = false;
            MenuButton.FlatStyle = FlatStyle.Flat;
            MenuButton.FlatAppearance.BorderSize = 0;
            Menu.Left = (this.ClientSize.Width - Menu.Width) / 2;
            Menu.Top = (this.ClientSize.Height - Menu.Height) / 2;
            Start.Parent = Menu;
            Start.Left = (Start.Parent.Width - Start.Width) / 2;
            Resume.Parent = Menu;
            Resume.Left = (Resume.Parent.Width - Resume.Width) / 2;
            Settings.Parent = Menu;
            Settings.Left = (Resume.Parent.Width - Resume.Width) / 2;
            MenuButton.Parent = Main;
            Player.Parent = Main;
            Scores.Parent = Main;
            ResultLabel.Parent = Menu;
            ResultLabel.Left = (ResultLabel.Parent.Width - ResultLabel.Width) / 2;
            ResultLabel.Visible = false;
            SettingsBox.Left = (this.ClientSize.Width - Menu.Width) / 2;
            SettingsBox.Top = (this.ClientSize.Height - Menu.Height) / 2;
            SettingsBox.Visible = false;
            checkBoxMusic.Parent = SettingsBox;
            checkBoxMusic.Left = (checkBoxMusic.Parent.Width - checkBoxMusic.Width) / 2;
            checkBoxSoundEffects.Parent = SettingsBox;
            checkBoxSoundEffects.Left = (checkBoxSoundEffects.Parent.Width - checkBoxSoundEffects.Width) / 2;
            checkBoxAiming.Parent = SettingsBox;
            checkBoxAiming.Left = (checkBoxAiming.Parent.Width - checkBoxAiming.Width) / 2;
            SettingsLabel.Parent = SettingsBox;
            SettingsLabel.Left = (SettingsLabel.Parent.Width - SettingsLabel.Width) / 2;
            MenuSettingButton.Parent = SettingsBox;
            MenuSettingButton.Left = (MenuSettingButton.Parent.Width - MenuSettingButton.Width) / 2;
            MenuSettingButton.TabStop = false;
            MenuSettingButton.FlatStyle = FlatStyle.Flat;
            game = new Game();
            settings = new Settings();
            //if (settings.music) Sound.MakeSoundOnLoop(Application.StartupPath + "\\Sounds/sound2.wav");
            var audioFile = new WaveFileReader(Application.StartupPath + "\\Sounds/sound2.wav");
            var loopingStream = new LoopStream(audioFile);
            waveOut.DeviceNumber = 0;
            waveOut.Init(loopingStream);
            waveOut.Play();
        }
        private void Main_Paint(object sender, PaintEventArgs e)
        {
            game.Drawing(e);
        }
        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            game.MouseDown(e);
        }
        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            game.MouseMove(e);
        }
        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            game.MouseUp(e);
        }
        private void Main_MouseClick(object sender, MouseEventArgs e)
        {
            game.MouseClick(e);
        }
        private void MenuButton_Click(object sender, EventArgs e)
        {
            Main.Visible = false;
            Menu.Visible = true;
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
        }
        private void Start_Click(object sender, EventArgs e)
        {
            game.StartGame();
            timer1.Start();
            timer2.Start();
            timer3.Start();
        }
        private void Resume_Click(object sender, EventArgs e)
        {
            game.Resume_Click();
            timer1.Start();
            timer2.Start();
            timer3.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.timer1_Tick(e);
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            game.timer2_Tick(e);

        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            game.timer3_Tick(e);
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Menu.Visible = false;
            SettingsBox.Visible = true;
        }

        private void MenuSettingButton_Click(object sender, EventArgs e)
        {
            Menu.Visible = true;
            SettingsBox.Visible = false;
        }

        private void checkBoxMusic_CheckedChanged(object sender, EventArgs e)
        {
            settings.music = !settings.music;
            if (settings.music)
            {
                var audioFile = new WaveFileReader(Application.StartupPath + "\\Sounds/sound2.wav");
                var loopingStream = new LoopStream(audioFile);
                waveOut.DeviceNumber = 0;
                waveOut.Init(loopingStream);
                waveOut.Play();
            }
            else
            {
                waveOut.Stop();
            }
        }

        private void checkBoxAiming_CheckedChanged(object sender, EventArgs e)
        {
            settings.aim = !settings.aim;
        }

        private void checkBoxSoundEffects_CheckedChanged(object sender, EventArgs e)
        {
            settings.soundEffects = !settings.soundEffects;
        }
    }
}
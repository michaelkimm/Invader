using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invaders
{
    public partial class Form1 : Form
    {
        int animationTimerInterval = 30;
        int gameTimerInterval = 10;

        Rectangle border = new Rectangle(0, 0, 800, 800);
        Game game;
        bool isGameOver = false;
        List<Keys> keysPressed = new List<Keys>();

        int animationCell = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeTimer();

            game = new Game(border);
            game.GameOver += GameOver;
            game.GameRestart += GameRestart;
        }

        void InitializeTimer()
        {
            animationTimer.Interval = animationTimerInterval;
            gameTimer.Interval = gameTimerInterval;

            animationTimer.Start();
            gameTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            animationCell++;
            if (animationCell == 4)
                animationCell = 0;
            game.Twinkle();
            this.Refresh();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            game.Go();

            ScoreLabel.Text = game.Score.ToString();

            foreach (Keys key in keysPressed)
            {
                if (key == Keys.Left)
                {
                    game.MovePlayer(Direction.Left);
                    return;
                }
                else if (key == Keys.Right)
                {
                    game.MovePlayer(Direction.Right);
                    return;
                }
            }
        }

        private void Invaders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                Application.Exit();

            if (isGameOver)
            {
                if (e.KeyCode == Keys.S)
                {
                    // 게임을 초기화 하고 타이머를 재 시작 하는 코드
                    // game.GameStart(this, EventArgs.Empty);
                    game.RestartGame();
                    isGameOver = false;
                    return;
                }
            }

            if (e.KeyCode == Keys.Space)
                game.FireShot();
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
            keysPressed.Add(e.KeyCode);
        }

        private void Invaders_KeyUp(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
        }

        private void Invaders_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
        }

        void GameOver(object sender, EventArgs e)
        {
            ActivateGameOverText(true);
            isGameOver = true;
            gameTimer.Stop();
        }

        void GameRestart(object sender, EventArgs e)
        {
            ActivateGameOverText(false);
            gameTimer.Start();
        }

        void ActivateGameOverText(bool value)
        {
            GameOverLabel.Visible = value;
            RestartLabel.Visible = value;
        }
    }
}

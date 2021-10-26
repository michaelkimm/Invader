using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    public enum Direction { None, Left, Up, Right, Down };

    class Game
    {
        int score = 0;
        int livesLeft = 2;
        int wave = -1;
        int frameSkipped = 0;
        Rectangle boundaries = new Rectangle(0, 0, 400, 300);
        Random random;
        Direction invaderDirection;
        List<Invader> invaders;

        PlayerShip playerShip;
        List<Shot> playerShots;
        List<Shot> invaderShots;

        Stars stars;

        public event EventHandler GameOver;

        public Game (Rectangle border)
        {
            boundaries = border;

            playerShip = new PlayerShip(new Point(100, 100));

            StartGame();
        }

        public void Draw(Graphics g, int animationCell)
        {
            stars.Draw(g);
            for (int i = 0; i < invaders.Count; i++)
            {
                invaders[i].Draw(g);
            }
            playerShip.Draw(g);
            for (int i = 0; i < playerShots.Count; i++)
            {
                playerShots[i].Draw(g);
            }
            for (int i = 0; i < invaderShots.Count; i++)
            {
                invaderShots[i].Draw(g);
            }
        }

        void StartGame()
        {

        }

        void NextWave()
        {
            //Invader invader = new Invader();
            //invaders.Add(invader);

            //invaderDirection = Direction.Right;

            wave++;
        }

        public void Twinkle()
        {
            stars.Twinkle();
        }

        public void MovePlayer(Direction dir)
        {
            playerShip.Move(dir);
        }
        public void FireShot()
        {

        }
        public void Go()
        {
            // 플레이어가 죽으면 다른 게임 오브젝트 Stop
            if (!playerShip.Alive)
                return;

            // 총알 Move
            foreach (Shot shot in playerShots)
            {
                shot.Move();
            }

            foreach (Shot shot in invaderShots)
            {
                shot.Move();
            }

            // 침입자 Move
            foreach (Invader invader in invaders)
            {
                invader.Move();
            }

            // 탄알 명중 검사 & 후처리
            CheckInvaderCollision();
            CheckPlayerCollision();
        }

        void CheckInvaderCollision()
        {
            for (int i = (invaders.Count - 1); i >= 0; i--)
            {
                for (int j = (playerShots.Count - 1); j >= 0; j--)
                {
                    if (!invaders[i].Area.Contains(playerShots[j].Location))
                        continue;

                    // 총알 & 적 파괴
                    playerShots.RemoveAt(j);
                    invaders.RemoveAt(i);
                    break;
                }
            }
        }

        void CheckPlayerCollision()
        {
            for (int j = (invaderShots.Count - 1); j >= 0; j--)
            {
                if (!playerShip.Area.Contains(invaderShots[j].Location))
                    continue;

                // 총알 & 적 파괴
                playerShip.Alive = false;
                livesLeft--;
                if (livesLeft == 0)
                {
                    // GameOver();
                }

                invaderShots.RemoveAt(j);
                break;
            }
        }
    }
}

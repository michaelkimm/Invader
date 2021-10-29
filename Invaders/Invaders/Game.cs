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
        public int Score { get; private set; }
        int livesLeft = 2;
        int wave = -1;
        int invaderFrameSkipped = 10;
        int currentInvaderFrame = 0;

        int starFrameSkipped = 10;
        int currentStarFrame = 0;

        Rectangle boundaries;
        Random random = new Random();
        Direction invaderDirection = Direction.None;
        List<Invader> invaders = new List<Invader>();
        Point firstInvaderLocation = new Point(50, 100);

        int invaderInterval = 60;

        PlayerShip playerShip;
        Point playerStartLocation;
        List<Shot> playerShots = new List<Shot>();
        List<Shot> invaderShots = new List<Shot>();

        Stars stars;

        int animationCell = 0;

        public event EventHandler GameOver;
        public event EventHandler GameRestart;

        public Game (Rectangle border)
        {
            boundaries = border;

            playerStartLocation = new Point((border.Right + border.Left) / 2, border.Bottom - 75);
            playerShip = new PlayerShip(Properties.Resources.player,
                                        playerStartLocation,
                                        boundaries);
            
            // invaders.Add(invader);

            stars = new Stars(boundaries);
            StartGame();
        }

        public void Draw(Graphics g)
        {
            stars.Draw(g);
            for (int i = 0; i < invaders.Count; i++)
            {
                invaders[i].Draw(g, animationCell);
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
            NextWave();
        }

        public void RestartGame()
        {
            livesLeft = 2;
            playerShip.Alive = true;
            if (GameRestart != null)
                GameRestart(this, EventArgs.Empty);
        }

        void NextWave()
        {
            //Invader invader = new Invader();
            //invaders.Add(invader);
            int colCount = 6;
            int rowCount = 5;

            
            firstInvaderLocation = new Point(boundaries.Left + firstInvaderLocation.X, boundaries.Top + firstInvaderLocation.Y);

            for (int  j = 0; j < rowCount; j++)
            {
                for (int i = 0; i < colCount; i++)
                {
                    Point location = new Point(firstInvaderLocation.X + i * invaderInterval, firstInvaderLocation.Y + j * invaderInterval);
                    Invader invader = new Invader(location,
                                                  boundaries,
                                                  (ShipType)j);
                    invaders.Add(invader);
                }
            }

            invaderDirection = Direction.Right;
            wave++;
        }

        public void Twinkle()
        {
            if (currentStarFrame++ != starFrameSkipped)
                return;
            currentStarFrame = 0;

            stars.Twinkle();
        }

        public void MovePlayer(Direction dir)
        {
            if (!playerShip.Alive)
                return;
            playerShip.Move(dir);
        }
        public void FireShot()
        {
            if (!playerShip.Alive)
                return;
            playerShots.Add(playerShip.FireShot());
        }
        public void Go()
        {
            //// 플레이어가 죽으면 다른 게임 오브젝트 Stop
            //if (!playerShip.Alive)
            //    return;

            // 적이 총알 생성
            ReturnFire();

            // 총알 Move
            for (int i = 0; i < playerShots.Count; i++)
            {
                if (!playerShots[i].Move())
                    playerShots.RemoveAt(i);
            }
            for (int i = 0; i < invaderShots.Count; i++)
            {
                if (!invaderShots[i].Move())
                    invaderShots.RemoveAt(i);
            }

            // 침입자 Move
            MoveInvadersByRule();

            // 탄알 명중 검사 & 후처리
            CheckInvaderCollision();
            CheckPlayerCollision();
        }

        void ReturnFire()
        {
            if (invaders.Count == 0)
                return;

            if (invaderShots.Count >= wave + 1)
                return;

            var enemyCols = from enemy in invaders
                            group enemy by enemy.Location.X into result
                            orderby result.Key descending
                            select result;

            var enemyCol = enemyCols.ElementAt(random.Next(0, enemyCols.ToList().Count));
            Invader invader = enemyCol.Last<Invader>();

            invaderShots.Add(invader.FireShot());
        }

        void MoveInvadersByRule()
        {
            if (currentInvaderFrame++ != invaderFrameSkipped)
                return;
            currentInvaderFrame = 0;

            // Linq 쿼리 이용하여 오른쪽 경게 100픽셀 안에 있는지 확인 후, 해당 인베이더 존재하면 침입자 밑으로 움직이게하고 이동 방향 변경
            if (invaderDirection == Direction.Right)
                ResposeToBorderRight();

            // 왼쪽 경계도 진행
            if (invaderDirection == Direction.Left)
                ResposeToBorderLeft();

            // 예정된 방향으로 진행
            MoveInvader(invaderDirection);

            animationCell++;
            if (animationCell == 4)
                animationCell = 0;
        }

        void ResposeToBorderRight()
        {
            var result = from enemys in invaders
                         where enemys.Location.X > (boundaries.Right - 100)
                         select enemys;
            if (result.Count<Invader>() > 0)
            {
                MoveInvader(Direction.Down);
                invaderDirection = Direction.Left;
            }
        }

        void ResposeToBorderLeft()
        {
            var result = from enemys in invaders
                         where enemys.Location.X < (boundaries.Left + 100)
                         select enemys;
            if (result.Count<Invader>() > 0)
            {
                MoveInvader(Direction.Down);
                invaderDirection = Direction.Right;
            }
        }

        void MoveInvader(Direction direction)
        {
            foreach (Invader invader in invaders)
            {
                invader.Move(direction);
            }
        }

        void CheckInvaderCollision()
        {
            for (int i = (invaders.Count - 1); i >= 0; i--)
            {
                if (!invaders[i].Alive)
                    continue;

                var result = from enemy in invaders
                             where enemy.Location.Y > boundaries.Bottom
                             select enemy;

                if (result.ToList().Count > 0)
                    GameOver(this, EventArgs.Empty);


                for (int j = (playerShots.Count - 1); j >= 0; j--)
                {
                    if (!invaders[i].Area.Contains(playerShots[j].Location))
                        continue;

                    // 총알 & 적 파괴
                    playerShots.RemoveAt(j);
                    // invaders.RemoveAt(i);
                    invaders[i].Alive = false;
                    Score += invaders[i].Score;
                    break;
                }
            }
        }

        void CheckPlayerCollision()
        {
            for (int j = (invaderShots.Count - 1); j >= 0; j--)
            {
                if (!playerShip.Alive)
                    return;

                if (!playerShip.Area.Contains(invaderShots[j].Location))
                    continue;

                // 총알 & 적 파괴
                playerShip.Alive = false;
                livesLeft--;
                if (livesLeft == 0)
                {
                    // GameOver();
                    GameOver(this, EventArgs.Empty);
                }

                invaderShots.RemoveAt(j);
                break;
            }
        }
    }
}

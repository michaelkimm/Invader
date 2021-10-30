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
        int wave = 0;
        const int INVADER_FRAME_SKIPPED = 10;
        int currentInvaderFrame = 0;

        const int STAR_FRAME_SKIPPED = 0;
        int currentStarFrame = 0;
        
        // 환경 설정
        Rectangle boundaries;
        Random random = new Random();

        // 인베이더 정의
        Direction invaderDirection = Direction.Right;
        List<Invader> invaders = new List<Invader>();
        Queue<Invader> inactiveInvaders = new Queue<Invader>();

        Point firstInvaderLocation;
        Point firstInvaderLocationOffset = new Point(50, 100);    // 첫번째 인베이더 위치 오프셋 to Boundary
        const int INVADER_INTERVAL = 60;                          // 인베이더간 간격
        const int COLUMN_COUNT = 6;                               // 열 갯수
        const int ROW_COUNT = (int)ShipType.Star + 1;             // 행 갯수
        int animationCell = 0;                                    // 보여줄 이미지 프레임 인덱스

        // 플레이어 정의
        PlayerShip playerShip;
        Point playerStartLocation;                                // 플레이어 시작 위치, 화면 크기에 의존

        // 총알 정의
        List<Shot> playerShots = new List<Shot>();
        Queue<Shot> inactivePlayerShot = new Queue<Shot>();
        const int MAX_PLAYERSHOT = 3;

        List<Shot> invaderShots = new List<Shot>();
        Queue<Shot> inactiveInvaderShot = new Queue<Shot>();

        // 배경 정의
        Stars stars;

        // 이벤트
        public event EventHandler GameOver;
        public event EventHandler GameRestart;

        public Game (Rectangle border)
        {
            // 환경 설정
            boundaries = border;

            // 객체 초기화
            InitializeGameObject();
            
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

        void InitializeGameObject()
        {
            // 플레이어 초기화
            playerStartLocation = new Point((boundaries.Right + boundaries.Left) / 2, boundaries.Bottom - 75);
            playerShip = new PlayerShip(Properties.Resources.player, playerStartLocation, boundaries);

            // 인베이더 초기화
            firstInvaderLocation = new Point(boundaries.Left + firstInvaderLocationOffset.X, boundaries.Top + firstInvaderLocationOffset.Y);

            for (int j = 0; j < ROW_COUNT; j++)
            {
                for (int i = 0; i < COLUMN_COUNT; i++)
                {
                    Console.WriteLine(j + ", " + i);
                    Point location = new Point(firstInvaderLocation.X + i * INVADER_INTERVAL, firstInvaderLocation.Y + j * INVADER_INTERVAL);
                    Invader invader = new Invader(location, boundaries, (ShipType)j);
                    invaders.Add(invader);
                }
            }

            // 배경 초기화
            stars = new Stars(boundaries);
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
            if (inactiveInvaders.Count == 0)
                return;
            ResetInvader();
            wave++;
        }

        void ResetInvader()
        {
            for (int j = 0; j < ROW_COUNT; j++)
            {
                for (int i = 0; i < COLUMN_COUNT; i++)
                {
                    // 인베이더 Alive
                    invaders.Add(inactiveInvaders.Dequeue());
                    invaders[j * COLUMN_COUNT + i].ResetPosition();
                }
            }
            invaderDirection = Direction.Right;
        }

        public void Twinkle()
        {
            if (currentStarFrame++ != STAR_FRAME_SKIPPED)
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

            if (playerShots.Count >= MAX_PLAYERSHOT)
                return;

            ShotFromPool(playerShip, playerShots, inactivePlayerShot);
        }

        void PlayerShotFromPool()
        {
            if (inactivePlayerShot.Count == 0)
            {
                playerShots.Add(playerShip.FireShot());
                return;
            }
            else if (inactivePlayerShot.Count >= 1)
            {
                Shot reusableShot = inactivePlayerShot.Dequeue();
                playerShots.Add(playerShip.FireShot(reusableShot));
            }
        }

        public void Go()
        {
            if (invaders.Count == 0)
                NextWave();

            // 적이 총알 생성
            ReturnFire();

            // 총알 Move
            for (int i = 0; i < playerShots.Count; i++)
            {
                if (!playerShots[i].Move())
                {
                    inactivePlayerShot.Enqueue(playerShots[i]);
                    playerShots.RemoveAt(i);
                }
            }
            for (int i = 0; i < invaderShots.Count; i++)
            {
                if (!invaderShots[i].Move())
                {
                    inactiveInvaderShot.Enqueue(invaderShots[i]);
                    invaderShots.RemoveAt(i);
                }
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

            if (invaderShots.Count >= wave + 1 || random.Next(10) < (10 - wave))
                return;

            var enemyCols = from enemy in invaders
                            where enemy.Alive 
                            group enemy by enemy.Location.X into result
                            orderby result.Key descending
                            select result;

            if (enemyCols.ToList().Count == 0)
                return;

            var enemyCol = enemyCols.ElementAt(random.Next(0, enemyCols.ToList().Count));
            Invader invader = enemyCol.Last<Invader>();

            ShotFromPool(invader, invaderShots, inactiveInvaderShot);
        }

        void ShotFromPool(IShooter Shooter,  List<Shot> activeShots, Queue<Shot> inactiveShots)
        {
            if (inactiveShots.Count == 0)
            {
                activeShots.Add(Shooter.FireShot());
                return;
            }
            else if (inactiveShots.Count >= 1)
            {
                Shot reusableShot = inactiveShots.Dequeue();
                activeShots.Add(Shooter.FireShot(reusableShot));
            }
        }

        void InvaderShotFromPool(Invader invader)
        {
            if (inactiveInvaderShot.Count == 0)
            {
                invaderShots.Add(invader.FireShot());
                return;
            }
            else if (inactiveInvaderShot.Count >= 1)
            {
                Shot reusableShot = inactiveInvaderShot.Dequeue();
                invaderShots.Add(invader.FireShot(reusableShot));
            }
        }

        void MoveInvadersByRule()
        {
            if (currentInvaderFrame++ != INVADER_FRAME_SKIPPED)
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

                    Score += invaders[i].Score;

                    // 총알 & 적 파괴
                    inactivePlayerShot.Enqueue(playerShots[j]);
                    playerShots.RemoveAt(j);

                    inactiveInvaders.Enqueue(invaders[i]);
                    invaders.RemoveAt(i);
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

                inactiveInvaderShot.Enqueue(invaderShots[j]);
                invaderShots.RemoveAt(j);
                break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame10
{
    public class LifeBoard:GameEntity
    {
        private bool[,] current;
        private bool[,] next;
        private int cellWidth;
        private int cellHeight;
        private int boardWidth;
        private int boardHeight;
        private float fps = 50;
        bool paused = true;

        SoundEffect walkerSound;
        SoundEffect tumblerSound;
        SoundEffect spaceShipSound;
        SoundEffect smallExploderSound;
        SoundEffect gosperSound;

        Song backgroundSong;

        public bool Paused
        {
            get { return paused; }
            set
            {
                if (!value)
                {
                    MediaPlayer.Resume();

                }
                else
                {
                    MediaPlayer.Pause();
                }
                paused = value;
            }
        }

        
        public LifeBoard():base()
        {
        }

        public void On(int x, int y)
        {
            if ((x >= 0) && (x < boardWidth) && (y >= 0) && (y < boardHeight))
            {
                current[y, x] = true;
            }
        }

        public Vector2 ScreenToCell(int screenX, int screenY)
        {
            Vector2 ret = new Vector2();
            ret.X = screenX / cellWidth;
            ret.Y = screenY / cellHeight;
            return ret;
        }

        public void Off(int x, int y)
        {
            
            if ((x >= 0) && (x < boardWidth) && (y >= 0) && (y < boardHeight))
            {
                current[y, x] = false;
            }
        }

        public void MakeSmallExploder(int x, int y)
        {

        }

        public void MakeGosperGun(int x, int y)
        {
            gosperSound.Play();
            On(x + 23, y);
            On(x + 24, y);
            On(x + 34, y);
            On(x + 35, y);

            On(x + 22, y + 1);
            On(x + 24, y + 1);
            On(x + 34, y + 1);
            On(x + 35, y + 1);

            On(x + 0, y + 2);
            On(x + 1, y + 2);
            On(x + 9, y + 2);
            On(x + 10, y + 2);
            On(x + 22, y + 2);
            On(x + 23, y + 2);

            On(x + 0, y + 3);
            On(x + 1, y + 3);
            On(x + 8, y + 3);
            On(x + 10, y + 3);

            On(x + 8, y + 4);
            On(x + 9, y + 4);
            On(x + 16, y + 4);
            On(x + 17, y + 4);

            On(x + 16, y + 5);
            On(x + 18, y + 5);

            On(x + 16, y + 6);

            On(x + 35, y + 7);
            On(x + 36, y + 7);

            On(x + 35, y + 8);
            On(x + 37, y + 8);

            On(x + 35, y + 9);

            On(x + 24, y + 12);
            On(x + 25, y + 12);
            On(x + 26, y + 12);

            On(x + 24, y + 13);

            On(x + 25, y + 14);


        }

        public void MakeLightWeightSpaceShip(int x, int y)
        {
            spaceShipSound.Play();
            On(x + 1, y);
            On(x + 2, y);
            On(x + 3, y);
            On(x + 4, y);

            On(x, y + 1);
            On(x + 4, y + 1);

            On(x + 4, y + 2);

            On(x, y + 3);
            On(x + 3, y + 3);
        }


        public void MakeTumbler(int x, int y)
        {
            tumblerSound.Play();
            On(x + 1, y);
            On(x + 2, y);
            On(x + 4, y);
            On(x + 5, y);
            
            On(x + 1, y + 1);
            On(x + 2, y + 1);
            On(x + 4, y + 1);
            On(x + 5, y + 1);

            On(x + 2, y + 2);
            On(x + 4, y + 2);

            On(x, y + 3);
            On(x + 2, y + 3);
            On(x + 4, y + 3);            
            On(x + 6, y + 3);

            On(x, y + 4);
            On(x + 2, y + 4);
            On(x + 4, y + 4);
            On(x + 6, y + 4);

            On(x, y + 5);
            On(x + 1, y + 5);
            On(x + 5, y + 5);
            On(x + 6, y + 5);

            
        }


        public void MakeGlider(int x, int y)
        {
            walkerSound.Play();
            current[y, x + 1] = true;
            current[y + 1, x + 2] = true;
            current[y + 2, x] = true;
            current[y + 2, x + 1] = true;
            current[y + 2, x + 2] = true;
        }

        public void RandomBoard()
        {
            int live = (boardHeight * boardWidth) / 2;
            Random r = new Random(DateTime.Now.Millisecond);
            while (live > 0)
            {
                int x = r.Next() % boardWidth;
                int y = r.Next() % boardHeight;
                if (!current[y, x])
                {
                    current[y, x] = true;
                    live--;
                }
            }          
        }

        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("whitedot");
            walkerSound = Game1.Instance.Content.Load<SoundEffect>("124919__altemark__highfreqloop");
            tumblerSound = Game1.Instance.Content.Load<SoundEffect>("6340__jovica__ppg-019-brassy-g-3");
            backgroundSong = Game1.Instance.Content.Load<Song>("08 - Duktus");
            spaceShipSound = Game1.Instance.Content.Load<SoundEffect>("1956__miulew__mechanic3");
            gosperSound = Game1.Instance.Content.Load<SoundEffect>("3007__jovica__stab-096-mastered-16-bit");
            cellWidth = Sprite.Width;
            cellHeight = Sprite.Height;

            boardWidth = Game1.Instance.Width / cellWidth;
            boardHeight = Game1.Instance.Height / cellHeight;

            current = new bool[boardHeight, boardWidth];
            next = new bool[boardHeight, boardWidth];
            RandomBoard();            
            MediaPlayer.Play(backgroundSong);
            MediaPlayer.Pause();
        }

        int CountLiveCellsSurrounding(int x, int y)
        {
            int count = 0;
            
            // Check to the left
            if ((x > 0) && (current[y, x - 1]))
            {
                count++;
            }
            // Check above left
            if ((x > 0) && (y > 0) && (current[y - 1, x - 1]))
            {
                count++;
            }
            // Check above
            if ((y > 0) && (current[y - 1, x]))
            {
                count++;
            }
            // Check above right
            if ((y > 0) && (x < (boardWidth - 1)) && (current[y - 1, x + 1]))
            {
                count++;
            }
            // Check right
            if ((x < (boardWidth - 1)) && (current[y, x + 1]))
            {
                count++;
            }

            // Check bottom right
            if ((y < (boardHeight - 1) ) && (x < (boardWidth - 1)) && (current[y + 1, x + 1]))
            {
                count++;
            }

            // Check bottom 
            if ((y < (boardHeight - 1)) && (current[y + 1, x]))
            {
                count++;
            }

            // Check bottom left 
            if ((x >0) && (y < (boardHeight - 1)) && (current[y + 1, x - 1]))
            {
                count++;
            }

            return count;
        }

        public void Clear()
        {
            ClearBoard(current);
        }

        private void ClearBoard(bool[,] board)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    board[y, x] = false;
                }
            }
        }

        float elapsed = 0;
        
        public override void Update(GameTime gameTime)
        {
            float needsToPass = 1.0f / fps;
            float timeDelta = (float) gameTime.ElapsedGameTime.TotalSeconds;

            elapsed += timeDelta;
            if (!paused && (elapsed > needsToPass))
            {
                ClearBoard(next);

                for (int x = 0; x < boardWidth; x++)
                {
                    for (int y = 0; y < boardHeight; y++)
                    {
                        int count = CountLiveCellsSurrounding(x, y);
                        if (current[y, x])
                        {
                            // Any live cell with less than 2 live neighbours dies
                            if (count < 2)
                            {
                                next[y, x] = false;
                            }

                            // Any live cell with 2 or 3 live neighbours survives
                            if ((count == 2) || (count == 3))
                            {
                                next[y, x] = true;
                            }

                            // Any live cell > 3 live neighbours dies due to overcrowding
                            if (count > 3)
                            {
                                next[y, x] = false;
                            }
                        }
                        else
                        {
                            // Any dead cell with exactly 3 neighbours comes to life due to reproduction
                            if (count == 3)
                            {
                                next[y, x] = true;
                            }
                        }
                    }
                }
                // Swap current and next
                bool[,] temp;
                temp = current;
                current = next;
                next = temp;
                elapsed = 0.0f;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    Vector2 pos = new Vector2(x * cellWidth, y * cellHeight);                    
                    if (current[y, x])
                    {
                        if (paused)
                        {
                            Game1.Instance.spriteBatch.Draw(Sprite, pos, Color.GhostWhite);
                        }
                        else
                        {
                            Game1.Instance.spriteBatch.Draw(Sprite, pos, Color.Black);
                        }
                        
                    }                 
                }
            }
        }
    }
}

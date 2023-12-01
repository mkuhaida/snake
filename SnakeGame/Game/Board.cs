using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame {
    class Board {
        public const int Width = 40;
        public const int Height = 40;

        public const int Size = 10;

        // 0 .. 39 — легальный диапазон значений

        public Snake Snake { get; set; }
        public Pos Apple { get; set; }
        public Queue<Pos> ApplesForReplay { get; set; } = new Queue<Pos>();
        public Pos SnakeStartPosForReplay { get; set; }

        public Board () {
            Snake = new Snake(this);
            PlaceApple();
            SnakeStartPosForReplay = Snake.Head;
        }
        public Board (Brain brain) {
            Snake = new Snake(this, brain);
            PlaceApple();
            SnakeStartPosForReplay = Snake.Head;
        }

        public Board(Board oldBoard)
        {
            Snake = new Snake(this, oldBoard.Snake.Brain, oldBoard.SnakeStartPosForReplay);
            ApplesForReplay = oldBoard.ApplesForReplay;
            Apple = ApplesForReplay.Dequeue();
        }

        public void PlaceApple()
        {
            if (Game.bestPreviousSnake == null)
            {
                Apple = RandomPosition();
                ApplesForReplay.Enqueue(Apple);
            }
            else
            {
                Apple = ApplesForReplay.Dequeue();
            }


        }

        public Pos RandomPosition() => new Pos (
            Rng.GetInt (0, Width), Rng.GetInt (0, Height));

        public static void Clear(Graphics g) =>
            g.FillRectangle (Brushes.Black, 0, 0, Width * Size, Height * Size);

        public void DrawApple(Graphics g) {
            if (!Snake.IsAlive)
                return;
            DrawCell (g, Brushes.Red, Apple.X, Apple.Y);
        }

        public void DrawCell(Graphics g, Brush brush, int x, int y) =>
            g.FillRectangle (brush, x * Size, y * Size, Size, Size);

        public bool Contains(Pos pos) =>
            pos.X >= 0 && pos.Y >= 0 && pos.X < Width && pos.Y < Height;
    }
}

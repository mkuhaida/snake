using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Game
    {
        public const int Worlds = 200;
        public const int KeepTopN = 0;

        private IReadOnlyList<Board> boards;
        public static Board bestPreviousSnake;
        private bool replayOfBestSnake = false;
        private Font font;
        private int generation = 1;
        public static List<double> BestGenerationScores;
        public static List<int> BestAmountsOfApples;
        public static List<int> BestLifetime;

        public int ProcessFrames { get; set; } = 1;

        public Game () {
            boards = Enumerable.Range(0, Worlds)
                .Select(i => new Board())
                .ToList();
            font = new Font ("Helvetica", 16);
            BestGenerationScores = new List<double>();
            BestAmountsOfApples = new List<int>();
            BestLifetime = new List<int>();
        }

        public void Draw(Graphics g)
        {
            Board.Clear (g);
            foreach (Board board in boards)
            {
                board.DrawApple(g);
                board.Snake.Draw(g);
            }
            if (bestPreviousSnake != null)
            {
                bestPreviousSnake.DrawApple(g);
                bestPreviousSnake.Snake.Draw(g);
            }
            
            IReadOnlyList<Snake> alives = boards
                .Select(board => board.Snake)
                .Where(snake => snake.IsAlive)
                .ToList();
            if (alives.Any())
            {
                Snake best = alives.ArgMax(board => board.Score);
                g.DrawString($"Поколение: {generation}\r\n" + best.Report(),
                    font, Brushes.Black, new Point(Board.Width * Board.Size + 2, 2));
                if (BestGenerationScores.Count == generation)
                {
                    BestGenerationScores[generation - 1] = best.Score;
                    if (best.Apples > BestAmountsOfApples[generation - 1])
                        BestAmountsOfApples[generation - 1] = best.Apples;

                    if (best.Lifetime > BestLifetime[generation - 1])
                        BestLifetime[generation - 1] = best.Lifetime;
                }
                else if (BestGenerationScores.Count < generation)
                {
                    BestGenerationScores.Add(best.Score);
                    BestAmountsOfApples.Add(best.Apples);
                    BestLifetime.Add(best.Lifetime);
                }
            }
        }

        public void Step()
        {
            if (bestPreviousSnake != null)
            {
                bestPreviousSnake.Snake.Step();
            }
            else
            {
                Parallel.ForEach(boards, board =>
                {
                    for (int i = 0; i < ProcessFrames; i++)
                        board.Snake.Step();
                });
                if (!boards.Any(board => board.Snake.IsAlive))
                {
                    var bestBoard = boards.OrderByDescending(x => x.Snake.Score).FirstOrDefault();
                    bestPreviousSnake = new Board(bestBoard);
                }
            }

            if (!boards.Any(board => board.Snake.IsAlive) && bestPreviousSnake != null && !bestPreviousSnake.Snake.IsAlive)
            {
                bestPreviousSnake = null;
                CreateNextGeneration();
            }
        }
        private void CreateNextGeneration() 
        {
            Population population = new Population(boards.Select(board => board.Snake));
            boards = population.KeepTopN(KeepTopN)
                    .Concat (Enumerable.Range(0, Worlds - KeepTopN)
                    .AsParallel()
                    .Select(i => population.CreateNewChild())
                    .ToList())
                .Select (brain => new Board (brain))
                .ToList ();
            generation++;
        }
    }
}

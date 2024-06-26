﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame {
    class Population {
        private readonly IReadOnlyList<(Brain brain, double score)> scores;
        private double bestScore;

        //public 

        public Population (IEnumerable<Snake> snakes) {
            scores = snakes
                .Select(snake => (snake.Brain, snake.Score))
                .ToList();
            bestScore = scores.Select(x => x.score).Max();
        }

        public IReadOnlyList<Brain> KeepTopN (int count) =>
            scores
                .Where((brain, score) => score == bestScore)
                .Select(x => x.brain)
                .Take(count)
                .ToList();

        public Brain CreateNewChild() 
        {
            Brain mom = ChooseParent();
            Brain dad = ChooseParent();
            return Brain.Cross(mom, dad);
        }

        private Brain ChooseParent() {
            var bestParrents = scores.OrderByDescending(x => x.score).Take(50);

            double seed = Rng.GetDouble(0, bestParrents.Select(x => x.score).Sum());

            foreach ((Brain brain, double score) pair in bestParrents) {
                seed -= pair.score;
                if (seed < 0)
                    return pair.brain;
            }
            throw new InvalidOperationException ("No matching snake found");
        }
    }
}

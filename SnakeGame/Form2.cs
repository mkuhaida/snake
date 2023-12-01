using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            //chart1.Series[1].Points.Clear();
            //chart1.Series[2].Points.Clear();
            List<double> scores = Game.BestGenerationScores;
            //List<int> apples = Game.BestAmountsOfApples;
            //List<int> lifetime = Game.BestLifetime;

            chart1.Series[0].Points.AddXY(0, 0);
            //chart1.Series[1].Points.AddXY(0, 0);
            //chart1.Series[2].Points.AddXY(0, 0);
            for (int i = 0; i < scores.Count - 1; i++)
            {
                chart1.Series[0].Points.AddXY(i + 1, (int)(scores[i + 1] / 100));
                //chart1.Series[1].Points.AddXY(i + 1, apples[i + 1] * 100);
                //chart1.Series[2].Points.AddXY(i + 1, lifetime[i + 1]);
            }
        }
    }
}

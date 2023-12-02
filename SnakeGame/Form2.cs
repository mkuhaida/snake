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
            List<double> scores = Game.BestGenerationScores;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    chart1.Series[0].Points.Clear();
                    chart1.Series[0].Points.AddXY(0, 0);
                    for (int i = 0; i < scores.Count - 1; i++)
                    {
                        chart1.Series[0].Points.AddXY(i + 1, (int)(scores[i + 1] / 100));
                    }
                    break;
                case 1:
                    chart2.Series[0].Points.Clear();
                    List<int> apples = Game.BestAmountsOfApples;

                    chart2.Series[0].Points.AddXY(0, 0);
                    for (int i = 0; i < scores.Count - 1; i++)
                    {
                        chart2.Series[0].Points.AddXY(i + 1, apples[i + 1]);
                    }
                    break;
                default:
                    chart1.Series[0].Points.Clear();
                    chart1.Series[0].Points.AddXY(0, 0);
                    for (int i = 0; i < scores.Count - 1; i++)
                    {
                        chart1.Series[0].Points.AddXY(i + 1, (int)(scores[i + 1] / 100));
                    }
                    break;
            }
        }

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            chart1.Size = new System.Drawing.Size(Width - 35, Height * 4 / 5); 
            button1.Location = new System.Drawing.Point((Width - button1.Width) / 3, Height * 5 / 6);
            comboBox1.Location = new System.Drawing.Point((Width - button1.Width) * 2 / 3, Height * 5 / 6);
            button1.Size = new System.Drawing.Size(154, ClientSize.Height / 15);
            comboBox1.Size = new System.Drawing.Size(154, ClientSize.Height / 10);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            chart1.Size = new System.Drawing.Size(Width - 35, Height * 4 / 5);
            button1.Location = new System.Drawing.Point((Width - button1.Width) / 3, Height * 5 / 6);
            comboBox1.Location = new System.Drawing.Point((Width - button1.Width) * 2 / 3, Height * 5 / 6);
            button1.Size = new System.Drawing.Size(154, ClientSize.Height / 15);
            comboBox1.Size = new System.Drawing.Size(154, ClientSize.Height / 10);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    chart1.Visible = true;
                    chart2.Visible = false;
                    break;
                case 1: 
                    chart1.Visible = false;
                    chart2.Visible = true;
                    break;
            }
        }
    }
}

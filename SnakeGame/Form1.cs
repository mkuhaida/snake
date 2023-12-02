using System;
using System.Windows.Forms;

namespace SnakeGame {
    public partial class Form1 : Form {
        private Game game = new Game();
        private float interval;

        public Form1 () {
            InitializeComponent ();
        }

        private void timer_Tick (object sender, EventArgs e) {
            game.Step ();
            Invalidate ();
        }

        private void Form1_Paint (object sender, PaintEventArgs e) =>
            game.Draw (e.Graphics);

        private void Form1_KeyPress (object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '[')
                game.ProcessFrames = Math.Max (1, game.ProcessFrames - 1);
            else if (e.KeyChar == ']')
                game.ProcessFrames++;
        }
        private void Faster () {
            interval = Math.Max (interval * 0.8f, 10);
            timer.Interval = (int) interval;
        }
        private void Slower () {
            interval = interval / 0.8f;
            timer.Interval = (int) interval;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 EvolutionGraph = new Form2();
            EvolutionGraph.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Faster();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Slower();
        }
    }
}

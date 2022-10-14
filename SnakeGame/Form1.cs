using System;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Game.GameEngine gameEngine;

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            richTextBox1.IsAccessible = false;
            gameEngine = new Game.GameEngine(pictureBox1, 17);            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            gameEngine.Keyboard(e);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            numericUpDown2.Enabled = false;
            checkBox1.Enabled = false;
            gameEngine.CoinAdd(Convert.ToInt32(numericUpDown2.Value), checkBox1.Checked);
            gameEngine.StartGame(new Graphics.SPoint(100, 100, Graphics.DirectionFlags.Right), label7, richTextBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gameEngine.RestartGame(new Graphics.SPoint(100, 100, Graphics.DirectionFlags.Right), label7, richTextBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Game.GameEngine.LoopTasks = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gameEngine.ExitGame();
        }
    }
}

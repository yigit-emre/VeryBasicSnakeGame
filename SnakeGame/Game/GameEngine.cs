using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace SnakeGame.Game
{
    class GameEngine : Graphics.GraphicEngine
    {
        // ***** Private Properties *****

        private int TakenScore = 0;
        private bool TakenaddBody = true;

        // ***** Public Properties ******

        public static bool LoopTasks = true;
        public static bool addBody { get; set; }
        public static int Score { get; set; }

        // **** Public Methods ******

        public GameEngine(PictureBox Screen, int Partsize = 17) : base(Screen, Partsize)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            addBody = false;
            Score = 0;
        }

        public async void StartGame(Graphics.SPoint startPoint, Label scoreLabel, RichTextBox console) 
        {           
            StartGraphics(startPoint);
            await ScreenBoards(scoreLabel,console);
        }

        public async void RestartGame(Graphics.SPoint startPoint, Label scoreLabel, RichTextBox console) 
        {
            if (LoopTasks == true)
            {
                LoopTasks = false;
                Thread.Sleep(500);
            }

            console.Clear();
            LoopTasks = true;
            ClearLists();
            Score = 0;
            additonalScore = 0;
            CoinAdd(TakenScore, TakenaddBody);
            StartGraphics(startPoint);
            await ScreenBoards(scoreLabel, console);
        }

        public void Keyboard(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                AddDirection(new Graphics.SPoint(Head.centerPoint.X, Head.centerPoint.Y, Graphics.DirectionFlags.Up));
            }
            if (e.KeyCode == Keys.S)
            {
                AddDirection(new Graphics.SPoint(Head.centerPoint.X, Head.centerPoint.Y, Graphics.DirectionFlags.Down));
            }
            if (e.KeyCode == Keys.D)
            {
                AddDirection(new Graphics.SPoint(Head.centerPoint.X, Head.centerPoint.Y, Graphics.DirectionFlags.Right));
            }
            if (e.KeyCode == Keys.A)
            {
                AddDirection(new Graphics.SPoint(Head.centerPoint.X, Head.centerPoint.Y, Graphics.DirectionFlags.Left));
            }
        }

        public void CoinAdd(int score, bool addBody = true)
        {
            TakenScore = score;
            TakenaddBody = addBody;
        }

        public void ExitGame() 
        {
            LoopTasks = false;
            Thread.Sleep(500);
            Application.Exit();
        }

        // **** Private Methods ****        

        private Task ScreenBoards(Label scoreLabel, RichTextBox console) 
        {
            return Task.Factory.StartNew(() => 
            {
                while (LoopTasks)
                {                   
                    scoreLabel.Text = Score.ToString();
                    if (additonalScore == 0)
                    {
                        AddCoin(TakenScore, TakenaddBody);
                    }
                }
                console.Clear();
                console.Text += $"LoopTasks: {LoopTasks} \n";
                console.Text += $"HeadX: {Head.centerPoint.X} # HeadY: {Head.centerPoint.Y} \n";
                console.Text += $"TakenScore: {TakenScore} \n";
                console.Text += $"TakenBody: {TakenaddBody} \n";
                console.Text += $"AdditionalScore: {additonalScore} \n";
                scoreLabel.Text = Score.ToString();
            }, TaskCreationOptions.LongRunning);
        }
    }
}

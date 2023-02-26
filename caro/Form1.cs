namespace caro
{
    public partial class Form1 : Form
    {
        private string last_turn = "Y";
        private Button[][] board = new Button[10][];
        public int[][] directions = new int[][] { 
            new int[] { 0, 1 }, 
            new int[] { 1, 0 }, 
            new int[] { 0, -1 }, 
            new int[] { -1, 0 }, 
            new int[] { 1, 1 }, 
            new int[] { 1, -1 }, 
            new int[] { -1, 1 }, 
            new int[] { -1, -1 } 
        };
        private int lengthWin = 5 ;
        private bool end_game=false;
        public Form1()
        {
            InitializeComponent();
            draw_game();
        }

        void draw_game()
        {

            var btn_size = main_game.Width / 12;
            var btn_margin = 5;
            Button old_btn = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int j = 0; j < 10; j++)
            {
                board[j] = new Button[] {
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = "" },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = "" },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = "" },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = "" },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = "" },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = "" },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = "" },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = "" },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = ""},
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = ""},};

                for (int i = 0; i < 10; i++)
                {
                    Button btn = board[j][i];
                    btn.Click += btn_click;
                    btn.Location = new Point(old_btn.Location.X + old_btn.Width + btn_margin, old_btn.Location.Y);

                    main_game.Controls.Add(btn);

                    old_btn = btn;

                }
                old_btn.Location = new Point(0, old_btn.Location.Y + btn_margin + btn_size);
                old_btn.Width = 0;
                old_btn.Height = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void btn_click(object sender, EventArgs e)
        {
            if (this.end_game == true) return;
            Button btn = sender as Button;
            if (btn.Text == "X" || btn.Text == "Y")
            {
                return;
            }
            if (this.last_turn == "X")
            {
                btn.Text = "Y";
                last_turn = "Y";
            } else
            {
                btn.Text = "X";
                last_turn = "X";
            }
            string winnner = this.checkWinner(this.board);
            if ( winnner != null)
            {
                this.end_game = true;
            }
        }

        string moveOneDirection(Button[][] board, int row, int col, int[] dir, int count = 1)
        {
            if (board[row][col].Text == "") return null;
            if (count == this.lengthWin) return board[row][col].Text;
            if (row + dir[0] == board.Length || col + dir[1] == board.Length 
                || row + dir[0] < 0 || col + dir[1] < 0) return null;

            string winner = null;
            if (board[row + dir[0]][col + dir[1]].Text == board[row][col].Text)
            {
                winner = moveOneDirection(board, row + dir[0], col + dir[1], dir, count + 1);
            };

            return winner;
         }
        string moveAllDirection(Button[][] board, int row, int col)
        {
            string winner = null;

            foreach (int[] dir in this.directions)
            {
                int nextrow = row + dir[0];
                int nextcol = col + dir[1];
                int nextdirx = row + dir[0] * (this.lengthWin - 1);
                int nextdiry = col + dir[1] * (this.lengthWin - 1);

                if (nextdirx >= board.Length || nextdiry >= board.Length || nextdirx < 0 || nextdiry < 0) continue;

                if (board[nextrow][nextcol].Text == board[row][col].Text)
                {
                    winner = this.moveOneDirection(board, row, col, dir);
                    if (winner != null) return winner;
                }
            }
            return winner;
            
        }
        string checkWinner(Button[][] board)
        {
            string winner = null;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (board[i][j].Text == "") continue;
                    winner = this.moveAllDirection(board, i, j);
                    
                    if (winner != null)
                    {
                        return winner;
                    }
                }
            }
            
            int openSpots = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (board[i][j].Text == "")
                    {
                        openSpots++;
                    }
                }
            }
            if (winner == null && openSpots == 0)
            {
                return "tie";
            }
            else
            {
                return winner;
            }
        }
    }

    
}
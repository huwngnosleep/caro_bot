using System.Security.Cryptography;
using System.Windows.Forms;
using System.Linq;
using System;
using caro;
using System.Reflection;

namespace caro
{

    public partial class Form1 : Form
    {
        private string winner = null;
        private int Infinity = 999999;
        private int null_flag = 3214123;
        private int max_depth = 3;
        private string turn = "X";
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
        private string blank = "-";
        private int lengthWin = 5;
        private bool end_game = false;
        Score scores = new Score();
        private string ai = "O";
        private string human = "X";
        public Form1()
        {
            InitializeComponent();
            draw_game();
        }

        void draw_game()
        {
            timer_bar.Step = -10;
            timer_bar.Value = 100;
            timer_bar.Minimum = 0;
            game_timer.Interval = 1000;

            var btn_size = main_game.Width / 12;
            var btn_margin = 5;
            Button old_btn = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int j = 0; j < 10; j++)
            {
                board[j] = new Button[] {
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = blank },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = blank },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = blank },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = blank },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = blank },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = blank },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = blank },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = blank },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = blank },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = blank },
                 new Button() { Width = btn_size, Height = btn_size, Location = new Point(0, 0), Text = blank },};

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

                game_timer.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        bool hasAdjacent(Button[][] board, int i, int j)
        {
            foreach (int[] dir in this.directions)
            {
                int nextDirX = i + dir[0];
                int nextDirY = j + dir[1];
                if (nextDirX >= board.Length || nextDirY >= board.Length || nextDirX < 0 || nextDirY < 0) continue;
                if (board[i + dir[0]][j + dir[1]].Text == ai || board[i + dir[0]][j + dir[1]].Text == human)
                {
                    return true;
                }
            }

            return false;
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
                    if (board[i][j].Text == blank) continue;
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
                    if (board[i][j].Text == blank)
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

        PointEval evaluate(string current, Button[][] board, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int x5, int y5)
        {
            string[] pattern100 = new string[] { "-XXXX", "X-XXX", "XX-XX", "XXX-X", "XXXX-", "OOOO-", "OOO-O", "OO-OO", "O-OOO", "-OOOO" };
            string[] pattern75 = new string[] {"-XXX-",
            "-XX-X-", "-X-XX-", "-X-XX",
            "XXX--", "-XX-X", "XX-X-", "-XX-X-", "--XXX",
            "-OOO-",
            "-OO-O", "-O-OO", "OO-O-", "OO-O-",
            "OOO--", "-OO-O", "OO-O-", "-OO-O-", "--OOO"
            };

            string str = $"{board[x1][y1].Text}{board[x2][y2].Text}{board[x3][y3].Text}{board[x4][y4].Text}{board[x5][y5].Text}";
            if (pattern100.Contains(str))
            {
                if (str.Contains(current))
                {
                    if (board[x3][y3].Text == blank) return new PointEval { x = x3, y = y3, eval = 99999 };
                    if (board[x2][y2].Text == blank) return new PointEval { x = x2, y = y2, eval = 99999 };
                    if (board[x4][y4].Text == blank) return new PointEval { x = x4, y = y4, eval = 99999 };
                    if (board[x1][y1].Text == blank) return new PointEval { x = x1, y = y1, eval = 99999 };
                    if (board[x5][y5].Text == blank) return new PointEval { x = x5, y = y5, eval = 99999 };
                }
                else
                {
                    if (board[x3][y3].Text == blank) return new PointEval { x = x3, y = y3, eval = 99900 };
                    if (board[x2][y2].Text == blank) return new PointEval { x = x2, y = y2, eval = 99900 };
                    if (board[x4][y4].Text == blank) return new PointEval { x = x4, y = y4, eval = 99900 };
                    if (board[x1][y1].Text == blank) return new PointEval { x = x1, y = y1, eval = 99900 };
                    if (board[x5][y5].Text == blank) return new PointEval { x = x5, y = y5, eval = 99900 };
                }
            }

            if (pattern75.Contains(str))
            {
                if (str.Contains(current))
                {
                    if (board[x3][y3].Text == blank) return new PointEval { x = x3, y = y3, eval = 90001 };
                    if (board[x2][y2].Text == blank) return new PointEval { x = x2, y = y2, eval = 90001 };
                    if (board[x4][y4].Text == blank) return new PointEval { x = x4, y = y4, eval = 90001 };
                    if (board[x1][y1].Text == blank) return new PointEval { x = x1, y = y1, eval = 90001 };
                    if (board[x5][y5].Text == blank) return new PointEval { x = x5, y = y5, eval = 90001 };
                }
                else
                {
                    if (board[x3][y3].Text == blank) return new PointEval { x = x3, y = y3, eval = 90000 };
                    if (board[x2][y2].Text == blank) return new PointEval { x = x2, y = y2, eval = 90000 };
                    if (board[x4][y4].Text == blank) return new PointEval { x = x4, y = y4, eval = 90000 };
                    if (board[x1][y1].Text == blank) return new PointEval { x = x1, y = y1, eval = 90000 };
                    if (board[x5][y5].Text == blank) return new PointEval { x = x5, y = y5, eval = 90000 };
                }
            }

            if (str.Contains(current))
            {
                if (pattern100.Contains(current))
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == '-')
                        {
                            if (i == 2) return new PointEval { x = x3, y = y3, eval = 100 };
                            if (i == 1) return new PointEval { x = x2, y = y2, eval = 100 };
                            if (i == 3) return new PointEval { x = x4, y = y4, eval = 100 };
                            if (i == 0) return new PointEval { x = x1, y = y1, eval = 100 };
                            if (i == 4) return new PointEval { x = x5, y = y5, eval = 100 };
                        }
                    }

                }

                if (pattern75.Contains(current))
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == '-')
                        {
                            if (i == 2) return new PointEval { x = x3, y = y3, eval = 75 };
                            if (i == 1) return new PointEval { x = x2, y = y2, eval = 75 };
                            if (i == 3) return new PointEval { x = x4, y = y4, eval = 75 };
                            if (i == 0) return new PointEval { x = x1, y = y1, eval = 75 };
                            if (i == 4) return new PointEval { x = x5, y = y5, eval = 75 };
                        }
                    }

                }
            }
            return null;
        }
        int minimax(Button[][] board, int depth, bool isMaximizing, int alpha, int beta)
        {

            if (depth == this.max_depth) return null_flag;

            // caseCount++
            string winner = this.checkWinner(board);

            if (winner == "X")
            {
                return this.scores.X - depth;
            }
            else if (winner == "O")
            {
                return this.scores.O - depth;
            }
            else if (winner == "tie")
            {
                return this.scores.tie - depth;
            }

            // evaluating
            PointEval evalValue = new PointEval();
            if (depth >= 1)
            {
                for (int i = 0; i < board.Length; i++)
                {
                    for (int j = 0; j < board.Length; j++)
                    {
                        foreach (int[] dir in this.directions)
                        {
                            int nextDirX = i + dir[0] * (this.lengthWin - 1);
                            int nextDirY = j + dir[1] * (lengthWin - 1);
                            if (nextDirX >= board.Length || nextDirY >= board.Length || nextDirX < 0 || nextDirY < 0) continue;
                            PointEval evalResult = evaluate(
                                isMaximizing ? ai : human, board,
                                i, j,
                                i + dir[0], j + dir[1],
                                i + 2 * dir[0], j + 2 * dir[1],
                                i + 3 * dir[0], j + 3 * dir[1],
                                nextDirX, nextDirY
                            );
                            if (evalResult != null && evalResult.eval > evalValue.eval)
                            {
                                evalValue = new PointEval() { eval = evalResult.eval, x = evalResult.x, y = evalResult.y };
                            }
                        }
                    }
                }
            }
            if (evalValue.eval > 0)
            {
                return evalValue.eval;
            }

            if (isMaximizing)
            {
                int bestScore = -99999999;
                for (int i = 0; i < board.Length; i++)
                {
                    for (int j = 0; j < board.Length; j++)
                    {
                        if (evalValue.eval > 0)
                        {
                            if (i != evalValue.x || j != evalValue.y) continue;
                        }
                        if (!hasAdjacent(board, i, j)) continue;
                        if (board[i][j].Text != blank) continue;
                        board[i][j].Text = ai;
                        int score = minimax(board, depth + 1, false, alpha, beta);
                        board[i][j].Text = blank;
                        bestScore = Math.Max(score, bestScore);
                        alpha = Math.Max(alpha, score);
                        if (beta <= alpha) return bestScore;
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = 99999999;
                for (int i = 0; i < board.Length; i++)
                {
                    for (int j = 0; j < board.Length; j++)
                    {
                        if (evalValue.eval > 0)
                        {
                            if (i != evalValue.x || j != evalValue.y) continue;
                        }
                        if (!hasAdjacent(board, i, j)) continue;
                        if (board[i][j].Text != blank) continue;
                        board[i][j].Text = human;
                        int score = minimax(board, depth + 1, true, alpha, beta);
                        board[i][j].Text = blank;
                        bestScore = Math.Min(score, bestScore);
                        beta = Math.Min(beta, score);
                        if (beta <= alpha) return bestScore;
                    }
                }
                return bestScore;
            }
        }

        PointEval bestMove(Button[][] board)
        {

            int[][] scoreboard = new int[10][];

            for (int j = 0; j < 10; j++)
            {
                scoreboard[j] = new int[] {
                 -Infinity,
                 -Infinity,
                 -Infinity,
                 -Infinity,
                 -Infinity,
                 -Infinity,
                 -Infinity,
                 -Infinity,
                 -Infinity,
                 -Infinity,};
            }

            int[][] evalBoard = new int[10][];
            for (int j = 0; j < 10; j++)
            {
                evalBoard[j] = new int[] {
                 0,
                 0,
                 0,
                 0,
                 0,
                 0,
                 0,
                 0,
                 0,
                 0,};
            }

            int bestScore = -Infinity;
            Move move = new Move();
            int alpha = -Infinity;
            int beta = Infinity;

            PointEval evalValue = new PointEval() { x = 0, y = 0, eval = -Infinity };
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board.Length; j++)
                {
                    foreach (int[] dir in directions)
                    {
                        int nextDirX = i + dir[0] * (lengthWin - 1);
                        int nextDirY = j + dir[1] * (lengthWin - 1);
                        if (nextDirX >= board.Length || nextDirY >= board.Length || nextDirX < 0 || nextDirY < 0) continue;
                        PointEval evalResult = evaluate(
                            ai, board,
                            i, j,
                            i + dir[0], j + dir[1],
                            i + 2 * dir[0], j + 2 * dir[1],
                            i + 3 * dir[0], j + 3 * dir[1],
                            nextDirX, nextDirY
                        );
                        if (evalResult != null)
                        {
                            if (evalResult.eval > evalBoard[evalResult.x][evalResult.y])
                            {
                                evalBoard[evalResult.x][evalResult.y] = evalResult.eval;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board.Length; j++)
                {
                    if (evalBoard[i][j] > evalValue.eval)
                    {
                        evalValue.x = i;
                        evalValue.y = j;
                        evalValue.eval = evalBoard[i][j];

                    }
                }
            }
            if (evalValue.eval > 0)
            {
                board[evalValue.x][evalValue.y].Text = turn;
                return new PointEval() { x = evalValue.x, y = evalValue.y };
            }


            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board.Length; j++)
                {
                    if (!hasAdjacent(board, i, j)) continue;
                    if (board[i][j].Text == blank)
                    {
                        board[i][j].Text = ai;
                        int score = minimax(board, 0, false, alpha, beta);
                        if (score == null_flag)
                        {
                            board[i][j].Text = blank;
                            continue;
                        }
                        scoreboard[i][j] = score;
                        board[i][j].Text = blank;

                        if (score > bestScore)
                        {
                            bestScore = score;
                            move.i = i;
                            move.j = j;

                        }
                    }
                    else
                    {
                        scoreboard[i][j] = -Infinity;
                    }
                }
            }
            Random rnd = new Random();
            while (true)
            {
                if (bestScore == -Infinity)
                {

                    move.i = board.Length / 2;
                    move.j = board.Length / 2;
                    break;
                }

                int randomX = (int)Math.Floor(rnd.NextDouble() * board.Length);
                int randomY = (int)Math.Floor(rnd.NextDouble() * board.Length);
                if (scoreboard[randomX][randomY] == bestScore)
                {
                    if (!hasAdjacent(board, randomX, randomY)) continue;
                    move.i = randomX;
                    move.j = randomY;
                    break;
                }
            }

            return new PointEval { x = move.i, y = move.j };
        }

        void update_color(Button btn, String turn)
        {
            btn.ForeColor = Color.Black;
            if(turn == human)
            {
                btn.BackColor = Color.Yellow;

            } else
            {
                btn.BackColor = Color.Orange;
            }
        }

        void go(Button btn)
        {
            btn.Text = this.turn;
            this.update_color(btn, this.turn);
        }
        void btn_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (this.end_game == true || this.turn == ai) return;
            if (btn.Text == ai || btn.Text == human)
            {
                return;
            }
            this.go(btn);
            string winner = this.checkWinner(this.board);
            if (winner != null)
            {
                this.end_game = true;
                this.winner = winner;
                this.finish_game();
                return;
            }
            switch_turn(ai);
            PointEval ai_move = bestMove(this.board);
            this.go(board[ai_move.x][ai_move.y]);
            winner = this.checkWinner(this.board);
            if (winner != null)
            {
                this.end_game = true;
                this.winner = winner;
                this.finish_game();
                return;
            }
            switch_turn(human);
        }

        
        private void switch_turn(string next_turn)
        {
            game_timer.Stop();
            turn_text.Text = $"Đến lượt {next_turn}";
            this.turn = next_turn;
            timer_bar.Value = 100;
            game_timer.Start();
        }
        private void game_timer_Tick(object sender, EventArgs e)
        {
            timer_bar.PerformStep();
            if (timer_bar.Value == timer_bar.Minimum)
            {
                if (this.turn == ai)
                {
                    this.winner = human;
                }
                else
                {
                    this.winner = ai;
                }
                finish_game();
            }
        }

        void finish_game()
        {
            // MessageBox.Show($"{this.winner} WIN");
            turn_text.Text = $"{this.winner} WIN";
            game_timer.Stop();
            if(this.winner == human) { turn_text.ForeColor = Color.Green; }
            else { turn_text.ForeColor= Color.Red; }    
        }
        private void timer_bar_Click(object sender, EventArgs e)
        {

        }

        private void timer_bar_Validated(object sender, EventArgs e)
        {
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }
    }

}


using System;
using caro;
namespace caro
{
	public class Class1
	{
		public Class1()
		{
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

}

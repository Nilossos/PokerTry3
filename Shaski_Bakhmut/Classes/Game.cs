using Shaski_Bakhmut.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shaski_Bakhmut
{
    public class Game
    {
        public Checker[,] BoardPrevent { get; set; }
        public Coordinate[] Board { get; private set; } = new Coordinate[64];
        public List<Player> Players { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Move> Moves { get; set; }
        public Player Winner { get; set; }
        public Checker[] Checkers { get; private set; } = new Checker[24];

        public Game()
        {
            //Доска Бахмута
            BoardPrevent = new Checker[8, 8];
            Players = new List<Player>();
            Moves = new List<Move>();
            GenerateBoard();
        }
        public void CreatePlayer(string name)
        {
            bool isBlackPlayer = Players.FirstOrDefault() != null;
            int offset = isBlackPlayer ? 5 * 64 / 8 + 1 : 0;

            Player player = new Player(name, isBlackPlayer ? SideType.Black :SideType.White);
            AppendPlayer(player);

            for (int i = 0; i < 12; i++)
            {
                int coordinageId = i * 2;
                coordinageId += offset;
                if (i >= 4 && i < 8)
                    if (isBlackPlayer)
                        coordinageId--;
                    else
                        coordinageId++;
                AppendChecker(new Checker(Board[coordinageId], player));
            }

            CurrentPlayer = Players.First();
        }

        public void AppendPlayer(Player player)
        {
            Players.Add(player);
        }

        private void AppendChecker(Checker checker)
        {
            int freeSlotIndex = Array.IndexOf(Checkers, null);

            if (freeSlotIndex == -1)
            {
                throw new InvalidOperationException("Maximum number of checkers reached!");
            }

            Checkers[freeSlotIndex] = checker;
        }

        public void StartGame(Player player1, Player player2)
        {
            Players.Add(player1);
            Players.Add(player2);
            if (player1.Color == SideType.White) { CurrentPlayer = player1; }
            else CurrentPlayer = player2;
        }

        public void GenerateBoardPrevent()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 != 0) 
                    {
                        if (i < 3)
                        {
                            BoardPrevent[i, j] = new Checker(new List<int> { i, j }, SideType.White);
                        }
                        else if (i > 4)
                        {
                            BoardPrevent[i, j] = new Checker(new List<int> { i, j }, SideType.Black);
                        }
                        else
                        {
                            BoardPrevent[i, j] = null; 
                        }
                    }
                    else
                    {
                        BoardPrevent[i, j] = null; 
                    }
                }
            }
        }

        private void GenerateBoard()
        {
            for (int i = 0; i < 64; i++)
            {
                int x = i % 8;
                int y = i / 8;
                Board[i] = new Coordinate(x, y);
            }
        }

        private string ConvertToChessNotation(int row, int col)
        {
            return $"{(char)('H' - col)}{1 + row}";
        }

        public void AddTurn(Checker piece, List<int> startPosition, List<int> intermediatePosition, List<int> endPosition)
        {
            string start = ConvertToChessNotation(startPosition[0], startPosition[1]);
            string end = ConvertToChessNotation(endPosition[0], endPosition[1]);
            Move turn = new Move(piece, start, intermediatePosition, end);
            Moves.Add(turn);
        }

        public List<(int, int)> FindPossibleTurns(int startRow, int startColumn)
        {
            List<(int, int)> possibleTurns = new List<(int, int)>();
            Checker piece = BoardPrevent[startRow, startColumn];

            if (piece == null || piece.Color != CurrentPlayer.Color)
            {
                return possibleTurns;
            }

            List<(int, int)> captures = FindPossibleCaptures(piece, startRow, startColumn);
            if (captures.Count > 0)
            {
                return captures;
            }

            int[] rowDirections = { -1, 1 };
            int[] colDirections = { -1, 1 };

            if (piece.Type == CheckerType.Regular)
            {
                foreach (int rowDir in rowDirections)
                {
                    foreach (int colDir in colDirections)
                    {
                        int newRow = startRow + rowDir;
                        int newCol = startColumn + colDir;

                        if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8 &&
                            BoardPrevent[newRow, newCol] == null &&
                            ((piece.Color == SideType.White && rowDir == 1) || (piece.Color == SideType.Black && rowDir == -1)))
                        {
                            possibleTurns.Add((newRow, newCol));
                        }
                    }
                }
            }

            if (piece.Type == CheckerType.Lady)
            {
                foreach (int rowDir in rowDirections)
                {
                    foreach (int colDir in colDirections)
                    {
                        for (int i = 1; i < 8; i++)
                        {
                            int newRow = startRow + i * rowDir;
                            int newCol = startColumn + i * colDir;

                            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                            {
                                if (BoardPrevent[newRow, newCol] == null)
                                {
                                    possibleTurns.Add((newRow, newCol));
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return possibleTurns;
        }

        public List<(int, int)> FindPossibleCaptures(Checker piece, int startRow, int startColumn, bool isCaptureMove = false, HashSet<(int, int)> visited = null)
        {
            List<(int, int)> captures = new List<(int, int)>();
            int[] rowDirections = { -1, 1 };
            int[] colDirections = { -1, 1 };

            // Инициализация множества посещённых клеток
            if (visited == null)
            {
                visited = new HashSet<(int, int)>();
            }

            if (piece.Type == CheckerType.Regular)
            {
                foreach (int rowDir in rowDirections)
                {
                    foreach (int colDir in colDirections)
                    {
                        int newRow = startRow + 2 * rowDir;
                        int newCol = startColumn + 2 * colDir;
                        int midRow = startRow + rowDir;
                        int midCol = startColumn + colDir;

                        if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8 &&
                            BoardPrevent[newRow, newCol] == null &&
                            BoardPrevent[midRow, midCol] != null &&
                            BoardPrevent[midRow, midCol].Color != piece.Color &&
                            !BoardPrevent[midRow, midCol].Killed)
                        {
                            captures.Add((newRow, newCol));
                        }
                    }
                }
            }
            else if (piece.Type == CheckerType.Lady)
            {
                List<(int, int)> secondaryCaptures = new List<(int, int)>();
                List<(int, int)> allFreeCells = new List<(int, int)>();

                foreach (int rowDir in rowDirections)
                {
                    foreach (int colDir in colDirections)
                    {
                        int checkRow = startRow + rowDir;
                        int checkCol = startColumn + colDir;

                        while (checkRow >= 0 && checkRow < 8 && checkCol >= 0 && checkCol < 8)
                        {
                            if (BoardPrevent[checkRow, checkCol] != null)
                            {
                                if (BoardPrevent[checkRow, checkCol].Color != piece.Color && !BoardPrevent[checkRow, checkCol].Killed)
                                {
                                    int captureRow = checkRow + rowDir;
                                    int captureCol = checkCol + colDir;

                                    // Собираем все свободные поля за захваченной шашкой
                                    while (captureRow >= 0 && captureRow < 8 && captureCol >= 0 && captureCol < 8 && BoardPrevent[captureRow, captureCol] == null)
                                    {
                                        allFreeCells.Add((captureRow, captureCol));
                                        captureRow += rowDir;
                                        captureCol += colDir;
                                    }
                                }
                                break;
                            }
                            checkRow += rowDir;
                            checkCol += colDir;
                        }
                    }
                }

                // Проверяем, какие из свободных полей ведут к дальнейшим захватам
                foreach (var cell in allFreeCells)
                {
                    if (!visited.Contains(cell))
                    {
                        visited.Add(cell);
                        var furtherCaptures = FindPossibleCaptures(piece, cell.Item1, cell.Item2, true, visited);
                        if (furtherCaptures.Count > 0)
                        {
                            secondaryCaptures.Add(cell);
                        }
                        visited.Remove(cell);
                    }
                }

                // Если есть захваты, ведущие к следующим захватам, добавляем их
                if (secondaryCaptures.Count > 0)
                {
                    captures.AddRange(secondaryCaptures);
                }
                else // Иначе добавляем все свободные поля
                {
                    captures.AddRange(allFreeCells);
                }
            }

            return captures;
        }
        public List<(int, int)> FindPossibleCaptures(SideType playerColor)
        {
            List<(int, int)> captures = new List<(int, int)>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Checker piece = BoardPrevent[i, j];
                    if (piece != null && piece.Color == playerColor)
                    {
                        captures.AddRange(FindPossibleCaptures(piece, i, j));
                    }
                }
            }

            return captures;
        }

    }
}
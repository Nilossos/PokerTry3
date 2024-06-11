using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaski_Bakhmut
{
    public class Player
    {
        public string Name { get; set; }
        public SideType Side { get; set; }
        public List<(int, int)> PossibleCaptures { get; private set; } = new List<(int, int)>();
        public bool IsInMultiCapture { get; set; } = false;
        public (int, int)? MultiCaptureStart { get; set; } = null;

        public Player(string name, SideType color)
        {
            Name = name;
            Side = color;
        }

        public void ChooseColor(SideType color)
        {
            Side = color;
        }

        public static SideType RandomColor()
        {
            Random random = new Random();
            Array values = Enum.GetValues(typeof(SideType));
            return (SideType)values.GetValue(random.Next(values.Length));
        }

        public Checker ChoosePiece(int row, int column, Game game)
        {
            return game.BoardPrevent[row, column];
        }

        public bool MakeMove(int startRow, int startColumn, int endRow, int endColumn, Game game)
        {
            Checker piece = game.BoardPrevent[startRow, startColumn];
            if (piece == null || piece.Color != this.Side)
            {
                return false;
            }

            bool isMoveValid = false;
            int rowDiff = Math.Abs(endRow - startRow);
            int colDiff = Math.Abs(endColumn - startColumn);

            // Проверка на выход за пределы поля
            if (endRow < 0 || endRow >= 8 || endColumn < 0 || endColumn >= 8)
            {
                return false;
            }

            // Проверка на использование не пустого поля
            if (game.BoardPrevent[endRow, endColumn] != null)
            {
                return false;
            }

            // Проверка на обязательные взятия
            List<(int, int)> mandatoryCaptures = game.FindPossibleCaptures(this.Side);

            if (mandatoryCaptures.Count > 0 && !mandatoryCaptures.Contains((endRow, endColumn)))
            {
                return false; // Если взятие обязательно, обычный ход запрещен
            }

            bool isCaptureMove = false;

            if (piece.Type == CheckerType.Regular)
            {
                if (rowDiff == 1 && colDiff == 1)
                {
                    // Обычный ход
                    if (mandatoryCaptures.Count == 0)
                    {
                        if ((piece.Color == SideType.White && endRow > startRow) || (piece.Color == SideType.Black && endRow < startRow))
                        {
                            isMoveValid = true;
                            game.BoardPrevent[endRow, endColumn] = game.BoardPrevent[startRow, startColumn];
                            game.BoardPrevent[startRow, startColumn] = null;
                            game.BoardPrevent[endRow, endColumn].Position = new List<int> { endRow, endColumn };

                            if ((piece.Color == SideType.White && endRow == 7) || (piece.Color == SideType.Black && endRow == 0))
                            {
                                game.BoardPrevent[endRow, endColumn].Type = CheckerType.Lady;
                            }
                        }
                    }
                }
                else if (rowDiff == 2 && colDiff == 2)
                {
                    // Ход с захватом
                    int midRow = (startRow + endRow) / 2;
                    int midColumn = (startColumn + endColumn) / 2;
                    Checker capturedPiece = game.BoardPrevent[midRow, midColumn];

                    if (capturedPiece != null && capturedPiece.Color != this.Side && !capturedPiece.Killed)
                    {
                        isMoveValid = true;
                        isCaptureMove = true;
                        capturedPiece.Killed = true;
                        game.BoardPrevent[endRow, endColumn] = game.BoardPrevent[startRow, startColumn];
                        game.BoardPrevent[startRow, startColumn] = null;
                        game.BoardPrevent[endRow, endColumn].Position = new List<int> { endRow, endColumn };

                        if ((piece.Color == SideType.White && endRow == 7) || (piece.Color == SideType.Black && endRow == 0))
                        {
                            game.BoardPrevent[endRow, endColumn].Type = CheckerType.Lady;
                        }

                        PossibleCaptures.Clear();
                        PossibleCaptures.AddRange(game.FindPossibleCaptures(piece, endRow, endColumn, isCaptureMove));

                        if (PossibleCaptures.Count > 0 && isCaptureMove)
                        {
                            IsInMultiCapture = true;
                            MultiCaptureStart = (endRow, endColumn);
                            return true; // Принудительный захват продолжается
                        }
                    }
                }
            }
            else if (piece.Type == CheckerType.Lady)
            {
                if (rowDiff == colDiff)
                {
                    int rowDirection = (endRow - startRow) / rowDiff;
                    int colDirection = (endColumn - startColumn) / colDiff;
                    bool pathClear = true;
                    bool captureMove = false;
                    int captureRow = -1, captureCol = -1;

                    for (int i = 1; i < rowDiff; i++)
                    {
                        int checkRow = startRow + i * rowDirection;
                        int checkCol = startColumn + i * colDirection;
                        Checker checkPiece = game.BoardPrevent[checkRow, checkCol];
                        if (checkPiece != null)
                        {
                            if (checkPiece.Color != this.Side && !captureMove && !checkPiece.Killed)
                            {
                                captureMove = true;
                                captureRow = checkRow;
                                captureCol = checkCol;
                            }
                            else
                            {
                                pathClear = false;
                                break;
                            }
                        }
                    }

                    if (pathClear)
                    {
                        isMoveValid = true;
                        game.BoardPrevent[endRow, endColumn] = game.BoardPrevent[startRow, startColumn];
                        game.BoardPrevent[startRow, startColumn] = null;
                        game.BoardPrevent[endRow, endColumn].Position = new List<int> { endRow, endColumn };

                        if (captureMove)
                        {
                            isCaptureMove = true;
                            game.BoardPrevent[captureRow, captureCol].Killed = true;
                        }

                        PossibleCaptures.Clear();
                        PossibleCaptures.AddRange(game.FindPossibleCaptures(piece, endRow, endColumn, isCaptureMove));

                        if (PossibleCaptures.Count > 0 && isCaptureMove)
                        {
                            IsInMultiCapture = true;
                            MultiCaptureStart = (endRow, endColumn);
                            return true; // Принудительный захват продолжается
                        }
                    }
                }
            }

            if (isMoveValid)
            {
                IsInMultiCapture = false;
                MultiCaptureStart = (-1, -1);

                // Удаление всех захваченных шашек
                for (int row = 0; row < 8; row++)
                {
                    for (int col = 0; col < 8; col++)
                    {
                        Checker p = game.BoardPrevent[row, col];
                        if (p != null && p.Killed)
                        {
                            game.BoardPrevent[row, col] = null;
                        }
                    }
                }

                game.AddTurn(piece, new List<int> { startRow, startColumn }, new List<int> { endRow, endColumn }, new List<int> { endRow, endColumn });
            }

            return isMoveValid;
        }


    }
}
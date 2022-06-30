using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Sum10
{
    public class MyClassSum10 : Control
    {
        //конструктор
        public MyClassSum10() : base()
        {
            _objectSize = (MinSize - 10 * CellPadding) / 10;
            _cellPadding = 2;
            numbersColor.Add(Color.FromArgb(255, 0, 0));
            numbersColor.Add(Color.FromArgb(255, 116, 0));
            numbersColor.Add(Color.FromArgb(7, 114, 161));
            numbersColor.Add(Color.FromArgb(255, 229, 0));
            numbersColor.Add(Color.FromArgb(149, 236, 0));
            numbersColor.Add(Color.FromArgb(255, 83, 0));
            numbersColor.Add(Color.FromArgb(166, 70, 0));
            numbersColor.Add(Color.FromArgb(255, 153, 0));
            numbersColor.Add(Color.FromArgb(72, 3, 111));
            numbersColor.Add(Color.FromArgb(7, 114, 161));
            //text
            textColor.Add(Color.FromArgb(0, 204, 0));
            textColor.Add(Color.FromArgb(0, 153, 153));
            textColor.Add(Color.FromArgb(255, 135, 0));
            textColor.Add(Color.FromArgb(79, 16, 173));
            textColor.Add(Color.FromArgb(210, 0, 107));
            textColor.Add(Color.FromArgb(0, 171, 111));
            textColor.Add(Color.FromArgb(0, 103, 92));
            textColor.Add(Color.FromArgb(13, 88, 166));
            textColor.Add(Color.FromArgb(166, 166, 0));
            textColor.Add(Color.FromArgb(0, 171, 111));
            updateArray();
        }
        //перпеменные
        List<Color> numbersColor = new List<Color>();
        List<Color> textColor = new List<Color>();
        private int[,] _cell = new int[10, 10];
        private List<int> Coordinate = new List<int>();
        private int _totalSum, sum, _xCor, _yCor, _cellPadding, _objectSize, buffer;
        //Свойства
        public int CellPadding
        {
            get => _cellPadding;
            set
            {
                if (value <= 2)
                    value = 2;
                else if (value > 20)
                    value = 20;

                if (value != _cellPadding)
                {
                    _cellPadding = value;
                    Invalidate();
                }
            }
        }
        private int ObjectSize
        {
            get => _objectSize;
            set
            {
                if (value <= 30)
                    value = 30;
                else if (value > 500)
                    value = 500;

                if (value != _objectSize)
                {
                    if (value >= 30 && _objectSize >= 30)
                        value = (MinSize - _cellPadding * 10) / 10;
                    _objectSize = value;
                    Invalidate();
                }
            }
        }
        public int TotalSum
        {
            get => _totalSum;
            set
            {
                if (value < 0)
                    value = 0;
                if (value != _totalSum)
                {
                    _totalSum = value;
                    Invalidate();
                }
            }
        }
        private int MinSize => Math.Min(Width, Height);
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        //функция изменения размеров визуального компонента
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (width > Math.Min(height, width))
                width = height;
            else
                height = width;
            base.SetBoundsCore(x, y, width, height, specified);
            _objectSize = (Math.Min(Size.Width, Size.Height) - 10 * _cellPadding) / 10;
        }
        //функция изменение размеров ячеек
        public void ChangeSize(EventArgs e) => ObjectSize = (MinSize - 10 * CellPadding) / 10;
        //функция рисования
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect;
            //text
            int fontSize = ObjectSize / 4;
            Font font = new Font("Segoe Script", fontSize);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            //osnova
            for (int i = 0; i < _cell.GetLength(0); ++i)
                for (int j = 0; j < _cell.GetLength(1); ++j)
                    if (_cell[i, j] != 0)
                    {
                        int resSizeP = ObjectSize + CellPadding;
                        rect = new Rectangle(resSizeP * j, resSizeP * i, ObjectSize, ObjectSize);
                        e.Graphics.FillRectangle(new SolidBrush(numbersColor[_cell[i, j]]), rect);
                        e.Graphics.DrawString(_cell[i, j].ToString(), font, new SolidBrush(textColor[_cell[i, j]]), rect, sf);
                    }

            if (Coordinate.Count > 0)
                for (int i = 0; Coordinate.Count - i != 0; i = i + 2)
                {
                    int resSizeP = ObjectSize + CellPadding;
                    Brush dopObjectColor = new SolidBrush(Color.White);
                    rect = new Rectangle(resSizeP * Coordinate[i + 1], resSizeP * Coordinate[i], ObjectSize, ObjectSize);
                    e.Graphics.FillRectangle(dopObjectColor, rect);
                    e.Graphics.DrawString(_cell[Coordinate[i], Coordinate[i + 1]].ToString(), font, new SolidBrush(Color.Black), rect, sf);
                }
        }
        //функция нажатия на левую клавишу манипулятора "мышь"
        public void onClickListener(Point MousePos)
        {
            Point MousePos1 = PointToClient(MousePos);
            _xCor = MousePos1.Y / (_objectSize + _cellPadding);
            _yCor = MousePos1.X / (_objectSize + _cellPadding);

            if (_xCor >= 10) _xCor = 9;
            if (_yCor >= 10) _yCor = 9;

            if (_cell[_xCor, _yCor] != 0)
            {
                Coordinate.Add(_xCor);
                Coordinate.Add(_yCor);
                if (Coordinate.Count > 2)
                {
                    buffer = 0;
                    //проверка на рядом или нет
                    for (int i = 0; Coordinate.Count - i - 2 != 0; i = i + 2)
                    {
                        if (Coordinate[i] == Coordinate[i + 2]
                            && Coordinate[i + 1] + 1 == Coordinate[i + 3])
                            buffer++;
                        else if (Coordinate[i] == Coordinate[i + 2]
                            && Coordinate[i + 1] - 1 == Coordinate[i + 3])
                            buffer++;
                        else if (Coordinate[i] + 1 == Coordinate[i + 2]
                            && Coordinate[i + 1] == Coordinate[i + 3])
                            buffer++;
                        else if (Coordinate[i] - 1 == Coordinate[i + 2]
                            && Coordinate[i + 1] == Coordinate[i + 3])
                            buffer++;
                    }
                    //если рядом то считаем, нет чистим 1 элемент
                    if (Coordinate.Count / 2 - 1 == buffer)
                    {
                        sum = 0;
                        for (int i = 0; Coordinate.Count - i != 0; i = i + 2)
                            sum += _cell[Coordinate[i], Coordinate[i + 1]];
                        //сумма = 10, если больше, то чистим список
                        if (sum == 10)
                        {
                            TotalSum += Coordinate.Count * 10;
                            for (int i = 0; Coordinate.Count - i != 0; i = i + 2)
                                _cell[Coordinate[i], Coordinate[i + 1]] -= 1;
                            Coordinate.Clear();
                            //опускаем вниз, если на элементе было 1
                            for (int k = 0; k < 10; k++)
                                for (int i = _cell.GetLength(0) - 1; i != 0; --i)
                                    for (int j = _cell.GetLength(1) - 1; j != -1; --j)
                                        if (_cell[i, j] == 0)
                                        {
                                            buffer = _cell[i, j];
                                            _cell[i, j] = _cell[i - 1, j];
                                            _cell[i - 1, j] = buffer;
                                        }
                            if (WinOrLose() == false)
                            {
                                int buf = 0;
                                for (int i = _cell.GetLength(0) - 1; i != 0; --i)
                                    for (int j = _cell.GetLength(1) - 1; j != -1; --j)
                                        buf += _cell[i, j];
                                if (buf == 0)
                                    MessageBox.Show("Победа", "Внимание", MessageBoxButtons.OK);
                                else
                                    MessageBox.Show("Поражение", "Внимание", MessageBoxButtons.OK);
                            }
                        }
                        else if (sum > 10) Coordinate.Clear();
                    }
                    else
                    {
                        Coordinate.RemoveAt(Coordinate.Count - 1);
                        Coordinate.RemoveAt(Coordinate.Count - 1);
                    }
                }
            }
            Invalidate();
        }
        //функция определения победы или поражения
        public bool WinOrLose()
        {
            for (int i = 0; i < _cell.GetLength(0); i++)
                for (int j = 0; j < _cell.GetLength(1); j++)
                    if (_cell[i, j] > 0 && CheckCell(i, j, _cell[i, j]))
                        return true;
            return false;
        }
        //функция основного алгоритма проверки на сумму 10
        private bool CheckCell(int Row, int Col, int Value)
        {
            List<CellPath> paths = new List<CellPath>();
            CellPath path = new CellPath();
            path.path.Add(new Cell() { Row = Row, Col = Col, Value = Value });
            paths.Add(path);
            int i = 0;
            while (i < paths.Count)
            {
                path = paths[i];
                if (CheckSum10(paths[i]) == 10) return true;
                if (CheckSum10(paths[i]) <= 10)
                {
                    Cell head = path.path[paths[i].path.Count - 1];
                    Row = head.Row;
                    Col = head.Col;
                    if (Row - 1 >= 0 && _cell[Row - 1, Col] > 0 && (!IsCellExists(Row - 1, Col, _cell[Row - 1, Col], path)))
                        paths.Add(MakePath(Row - 1, Col, _cell[Row - 1, Col], path));
                    if (Col - 1 >= 0 && _cell[Row, Col - 1] > 0 && (!IsCellExists(Row, Col - 1, _cell[Row, Col - 1], path)))
                        paths.Add(MakePath(Row, Col - 1, _cell[Row, Col - 1], path));
                    if (Row + 1 <= _cell.GetLength(0) - 1 && _cell[Row + 1, Col] > 0 && (!IsCellExists(Row + 1, Col, _cell[Row + 1, Col], path)))
                        paths.Add(MakePath(Row + 1, Col, _cell[Row + 1, Col], path));
                    if (Col + 1 <= _cell.GetLength(1) - 1 && _cell[Row, Col + 1] > 0 && (!IsCellExists(Row, Col + 1, _cell[Row, Col + 1], path)))
                        paths.Add(MakePath(Row, Col + 1, _cell[Row, Col + 1], path));
                }
                i++;
            }
            return false;
        }

        //функция проверяет возможность суммы десяти в элементов пути
        private int CheckSum10(CellPath path)
        {
            int buf = 0;
            foreach (var sum in path.path)
                buf += sum.Value;
            return buf;
        }

        //функция проверки, что заданая ячейка не встречается по пути
        private bool IsCellExists(int Row, int Col, int Value, CellPath path)
        {
            Cell cell = new Cell() { Row = Row, Col = Col, Value = Value };
            foreach (var item in path.path)
                if (item.Equals(cell))
                    return true;
            return false;
        }
        //функция добавления нового пути
        private CellPath MakePath(int Row, int Col, int Value, CellPath path)
        {
            Cell cell = new Cell() { Row = Row, Col = Col, Value = Value };
            CellPath cellPath = new CellPath();
            for (int i = 0; i < path.path.Count; ++i)
                cellPath.path.Add(path.path[i]);
            cellPath.path.Add(cell);
            return cellPath;
        }
        //функция обновления поля, обнуления счета и очистка списка
        public void updateArray()
        {
            var rand = new Random();
            for (int i = 9; i < _cell.GetLength(0); i += 1)
                for (int j = 0; j < _cell.GetLength(1); j += 1)
                    _cell[i, j] = rand.Next(1, 10);
            if (WinOrLose() == false)
                MessageBox.Show("Поле создано неудачно!", "Внимание", MessageBoxButtons.OK);
            Coordinate.Clear();
            _totalSum = 0;
        }
    }
}

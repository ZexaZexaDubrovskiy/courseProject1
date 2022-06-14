﻿using System;
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
        private int MinSize => Math.Min(Width, Height);
        private int[,] _cell = new int[10, 10];
        private int buffer;
        private int _objectSize;
        private int _cellPadding;
        private int _xCor;
        private int _yCor;
        private List<int> Coordinate = new List<int>();
        private bool _RadomOrNet = false;
        private int sum;
        private int cnt;
        private int _totalSum;
        private bool _currentState = false;
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
                    {
                        value = (MinSize - _cellPadding * 10) / 10;
                    }
                    _objectSize = value;
                    Invalidate();
                }
            }
        }
        public int TotalSum
        {
            get => _totalSum;

            private set
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
        //оптимизация отображения
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        //функция изменения размеров окна
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (width > Math.Min(height, width))
                width = height;
            else
                height = width;
            base.SetBoundsCore(x, y, width, height, specified);
            _objectSize = (Math.Min(Size.Width, Size.Height) - 10 * _cellPadding) / 10; 
        }
        //изменение размеров ячеек
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
                        rect = new Rectangle(resSizeP * i, resSizeP * j, ObjectSize, ObjectSize);
                        e.Graphics.FillRectangle(new SolidBrush(numbersColor[_cell[i, j]]), rect);
                        e.Graphics.DrawString(_cell[i, j].ToString(), font, new SolidBrush(textColor[_cell[i, j]]), rect, sf);
                    }

            if (Coordinate.Count > 0)
                for (int i = 0; Coordinate.Count - i != 0; i = i + 2)
                {
                    int resSizeP = ObjectSize + CellPadding;
                    Brush dopObjectColor = new SolidBrush(Color.White);
                    rect = new Rectangle(resSizeP * Coordinate[i], resSizeP * Coordinate[i + 1], ObjectSize, ObjectSize);
                    e.Graphics.FillRectangle(dopObjectColor, rect);
                    e.Graphics.DrawString(_cell[Coordinate[i], Coordinate[i + 1]].ToString(), font, new SolidBrush(Color.Black), rect, sf);
                }
        }
        //нажатие на левую клавишу мыши
        public void onClickListener(Point MousePos)
        {
            //получение координат
            Point MousePos1 = PointToClient(MousePos);
            _xCor = MousePos1.X / (_objectSize + _cellPadding);
            _yCor = MousePos1.Y / (_objectSize + _cellPadding);

            //если не 0
            if (_cell[_xCor, _yCor] != 0)
            {
                //Клик вне компонента выбирает самый крайни
                if (_xCor >= 10)
                    _xCor = 9;
                if (_yCor >= 10)
                    _yCor = 9;

                //добавление в лист проверка на 0
                _currentState = false;
                Coordinate.Add(_xCor);
                Coordinate.Add(_yCor);
                for (int i = 0; Coordinate.Count - i - 2 != 0; i = i + 2)
                    if (_xCor == Coordinate[i] && _yCor == Coordinate[i + 1])
                        _currentState = true;
                //если больше 1 элемента
                if (_currentState == false)
                {
                    //проверка начиная со 2 элемента
                    if (Coordinate.Count % 2 == 0 && Coordinate.Count > 2)
                    {
                        _RadomOrNet = false;
                        cnt = 0;
                        //проверка на рядом или нет
                        for (int i = 0; Coordinate.Count - i - 2 != 0; i = i + 2)
                        {
                            if (Coordinate[i] == Coordinate[i + 2]
                                && Coordinate[i + 1] + 1 == Coordinate[i + 3])
                            {
                                cnt++;
                            }
                            else if (Coordinate[i] == Coordinate[i + 2]
                                && Coordinate[i + 1] - 1 == Coordinate[i + 3])
                            {
                                cnt++;
                            }
                            else if (Coordinate[i] + 1 == Coordinate[i + 2]
                                && Coordinate[i + 1] == Coordinate[i + 3])
                            {
                                cnt++;
                            }
                            else if (Coordinate[i] - 1 == Coordinate[i + 2]
                                && Coordinate[i + 1] == Coordinate[i + 3])
                            {
                                cnt++;
                            }
                        }
                        if (Coordinate.Count / 2 - 1 == cnt)
                            _RadomOrNet = true;
                        //если рядом то считаем, нет чистим 1 элемент
                        if (_RadomOrNet)
                        {
                            //сумма всех элементов
                            sum = 0;
                            for (int i = 0; Coordinate.Count - i != 0; i = i + 2)
                            {
                                sum += _cell[Coordinate[i], Coordinate[i + 1]];
                            }
                            //сумма = 10, если больше, то чистим список
                            if (sum == 10)
                            {
                                for (int i = 0; Coordinate.Count - i != 0; i = i + 2)
                                    _cell[Coordinate[i], Coordinate[i + 1]] -= 1;
                                //даем очки 
                                TotalSum += Coordinate.Count * 10;
                                //опускаем вниз, если на элементе было 1
                                Coordinate.Clear();
                                int k = 0;
                                while (k != 10)
                                {
                                    for (int i = _cell.GetLength(0) - 1; i != -1; --i)
                                    {
                                        for (int j = _cell.GetLength(1) - 1; j != 0; --j)
                                        {
                                            if (_cell[i, j] == 0)
                                            {
                                                buffer = _cell[i, j];
                                                _cell[i, j] = _cell[i, j - 1];
                                                _cell[i, j - 1] = buffer;
                                            }
                                        }
                                    }
                                    k++;
                                }
                                if (WinOrLose() == false)
                                {
                                    MessageBox.Show("Вы проиграли", "Внимание", MessageBoxButtons.OK);
                                }
                            }
                            else if (sum > 10)
                                Coordinate.Clear();
                        }
                        else
                        {
                            Coordinate.Reverse();
                            Coordinate.RemoveAt(0);
                            Coordinate.RemoveAt(0);
                            Coordinate.Reverse();
                        }
                    }
                }
                else
                {
                    Coordinate.Reverse();
                    Coordinate.RemoveAt(0);
                    Coordinate.RemoveAt(0);
                    Coordinate.Reverse();
                }
            }
            Invalidate();
        }
        //функция определения победы или поражения
        public bool WinOrLose()
        {
            List<CellPath> paths = new List<CellPath>();
            for (int i = 0; i < _cell.GetLength(0); i++)
                for (int j = 0; j < _cell.GetLength(1); j++)
                {
                    if (_cell[i, j] > 0)
                    {
                        Cell cell = new Cell() { Row = i, Col = j };
                        var lis = new List<Cell>();
                        lis.Add(cell);
                        CellPath cl = new CellPath();
                        cl.path = fuckingRecursion(lis, cell, lis);
                        paths.Add(cl);
                    }
                }
            foreach (var item in paths)
                if (item.GetPathValue10() == 10)
                    return true;
            return false;
        }
     
        private List<Cell> fuckingRecursion(List<Cell> path, Cell buf, List<Cell> allPath)
        {
            var lis = new List<Cell>();
            if (buf.Row - 1 >= 0 && _cell[buf.Row - 1, buf.Col] > 0) 
            {
                Cell cell1 = new Cell(buf.Col, buf.Row - 1, _cell[buf.Row - 1, buf.Col]);
                if (!check(allPath, cell1))
                {
                    lis.Add(cell1);
                    allPath.Add(cell1);
                    path.AddRange(fuckingRecursion(lis, cell1, allPath));
                }
            } 
            if (buf.Col - 1 >= 0 && _cell[buf.Row, buf.Col - 1] > 0)
            {
                Cell cell1 = new Cell(buf.Col - 1, buf.Row, _cell[buf.Row, buf.Col - 1]);
                if (!check(allPath, cell1))
                {
                    lis.Add(cell1);
                    allPath.Add(cell1);
                    path.AddRange(fuckingRecursion(lis, cell1, allPath));
                }
            } 
            if (buf.Row + 1 <= _cell.GetLength(0) - 1 && _cell[buf.Row + 1, buf.Col] > 0)
            {
                Cell cell1 = new Cell(buf.Col, buf.Row + 1, _cell[buf.Row + 1, buf.Col]);
                if (!check(allPath, cell1))
                {
                    lis.Add(cell1);
                    allPath.Add(cell1);
                    path.AddRange(fuckingRecursion(lis, cell1, allPath));
                }
            }  
            if (buf.Col + 1 <= _cell.GetLength(0) - 1 && _cell[buf.Row, buf.Col + 1] > 0)
            {
                Cell cell1 = new Cell(buf.Col + 1, buf.Row, _cell[buf.Row, buf.Col + 1]);
                if (!check(allPath, cell1))
                {
                    lis.Add(cell1);
                    allPath.Add(cell1);
                    path.AddRange(fuckingRecursion(lis, cell1, allPath));
                }
            }
            return path;
        }

        public bool check(List<Cell> path, Cell currentCell)
        {
            foreach (var item in path)
                if (item.Equals(currentCell))
                    return true;
            return false;
        }
        //обновление поля
        public void updateArray()
        {
            var rand = new Random();
            for (int i = 0; i < _cell.GetLength(0); ++i)
                for (int j = 8; j < _cell.GetLength(1); ++j)
                    _cell[i, j] = rand.Next(5, 9);
            if (Coordinate.Count != 0)
                Coordinate.Clear();
            if (_totalSum != 0)
                _totalSum = 0;
        }
    }
}

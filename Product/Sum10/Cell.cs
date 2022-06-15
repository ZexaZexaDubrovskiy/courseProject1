namespace Sum10
{
    public class Cell
    {
        public Cell() { }
        public Cell(int col, int row, int value)
        {
            Row = row;
            Col = col;
            Value = value;
        }
        public int Row { get; set; }
        public int Col { get; set; }
        public int Value { get; set; }
        public override bool Equals(object obj)
        {
            return obj is Cell cell &&
                   Row == cell.Row &&
                   Col == cell.Col &&
                   Value == cell.Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sum10
{
    public class CellPath
    {
        public CellPath()
        {
            path = new List<Cell>();
        }
        public List<Cell> path;
        //пробежаться по каждой ячейке и сложить все значения
        public int GetPathValue()
        {
            int buf = 0;
            path.ForEach(x => buf += x.Value);
            return buf;
        }
    }
}

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
        public int GetPathValue()
        {
            int buf = 0;
            path.ForEach(x => buf += x.Value);
            return buf;
        }
        public int GetPathValue10()
        {
            int buf = 0;
            foreach(var x in path)
            {
                buf += x.Value;
                if (buf == 10)
                    return buf;
            }
            return buf;
        }
    }
}

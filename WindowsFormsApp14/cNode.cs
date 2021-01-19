using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp14
{
    public class cNode
    {
        public string name;
        public int r, c;
        public int heurScore;
        public bool isVisted = false;
        public string path = "";
        public List<string> paths = new List<string>();
        public List<cNode> connectedTo = new List<cNode>();
        public List<string> direct = new List<string>();
    }
}

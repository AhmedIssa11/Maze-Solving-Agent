using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp14
{
    public class Greedy
    {
        Queue<cNode> nodeQ = new Queue<cNode>();
        List<string> Path = new List<string>();

        //Greedy Search
        public void SearchGraph(cNode root, ref string path, ref bool foundGoal)
        {
            //MessageBox.Show("" + root.name);
            if (root == null) { return; }
            root.isVisted = true;
            //path += root.name + "=>";
            root.connectedTo.Sort((x, y) => x.heurScore.CompareTo(y.heurScore));


            foreach (var node in root.connectedTo)
            {
                //MessageBox.Show(node.name);
                //node.path += root.path + root.name;
                node.paths.AddRange(root.paths);
                node.paths.Add(root.name);


                //MessageBox.Show("" + node.name + ":" + node.path);
                nodeQ.Enqueue(node);

            }

            nodeQ = new Queue<cNode>(nodeQ.OrderBy(q => q.heurScore));
            nodeQ = new Queue<cNode>(nodeQ.Distinct());

            /*foreach (var id in nodeQ)
            {
                MessageBox.Show("q:" + id.name);               
            }*/

            cNode n = nodeQ.Dequeue();
            if (n.heurScore == 0) { foundGoal = true; Path.AddRange(n.paths);
               Path.Add(n.name); ; return; }

            SearchGraph(n, ref path, ref foundGoal);
        }

        public void printPath(ref List<string> listPath)
        {
            listPath = Path.Distinct().ToList();
        }
    }
}

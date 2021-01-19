using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp14
{
    

    public partial class Form1 : Form
    {
        int driver = 0;
        Timer tt = new Timer();
        Random rr = new Random();
        int r, g, b;
        List<cNode> original = new List<cNode>();
        string pp =  "a b f g c d n m q p u v b2 c2 k2" ;
        int row, col, ct = 0;
        int posr = 0, posc = 0, pathcostct=0;
        List<string> Opened = new List<string>();
        int timer = 0;
        List<string> listP = new List<string>();
        List<int> rows = new List<int>();
        List<int> cols = new List<int>();
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load1;
            KeyDown += Form1_KeyDown;
            tt.Tick += Tt_Tick;
        }

        void animate()
        {
            for(int i=0;i<driver;i++)
            {
                dataGridView1.Rows[rows[i]].Cells[cols[i]].Style.BackColor = clr;
            }
        }
        private void Tt_Tick(object sender, EventArgs e)
        {
            if(timer%2==0&&driver<rows.Count-1)
            {
                driver++;
                
            }
            
            
            
            timer++;

            animate();
        }

        List<cNode> listOfAllNodes = new List<cNode>();
        List<cNode> listOfNeededNodes = new List<cNode>();

        void hardCodedInput()
        {
            ///list of all nodes with heur
            //string[] inputLines = { "a 1", "b 2", "c 3", "d 4", "e 5", "f 6", "g 7", "h 8", "i 9", "j 10", "k 11", "l 11" , "m 12", "n 13", "o 14", "p 15", "q 16", "r 17", "s 18", "t 19", "u 20", "v 21", "w 22", "x 23", "y 24", "z 25", "a2 26", "b2 27", "c2 28", "d2 29", "f2 30", "g2 31", "h2 32", "i2 33", "j2 34", "k2 35" , "l2 36" ,"m2 37"};//length=38
            string[] inputLines = { "a 1", "b 2", "c 3", "d 4", "e 5", "f 6", "g 7", "h 8", "i 9", "j 10", "k 11", "l 11", "m 12", "n 13", "o 14", "p 15", "q 16", "r 17", "s 18", "t 19", "u 20", "v 21", "w 22", "x 23", "y 24", "z 25", "a2 26", "b2 27", "c2 28", "d2 29", "f2 30", "g2 31", "h2 32", "i2 0", "j2 34", "k2 5", "l2 36", "m2 37" };//length=38

            //Heuristic Cost
            for (int i = 0; i < inputLines.Length; i++)
            {
                string[] words = inputLines[i].Split(' ');
                
                cNode n = new cNode();
                cNode pnn = new cNode();
                n.name = words[0];
                n.heurScore = int.Parse(words[1]);
            
                pnn.name = words[0];
                pnn.heurScore = int.Parse(words[1]);

                for (int r = 0; r < dataGridView1.Rows.Count; r++)
                {
                    for (int c = 0; c < dataGridView1.Rows[r].Cells.Count; c++)
                    {
                        if(dataGridView1.Rows[r].Cells[c].Value!= null && dataGridView1.Rows[r].Cells[c].Value.ToString()== words[0].ToString())
                        {
                            ct++;
                            n.r = r;
                            n.c = c;
                            pnn.r = r;
                            pnn.c = c;
                            //Text = ct + "";
                        }
                    }
                }


                listOfAllNodes.Add(n);
                original.Add(pnn);
                //MessageBox.Show(n.name);
            }


            //
            ////
            ///NODE CONNECTEDTO DIRECT
            //string[] inputLines2 = { "a l2 r", "a b d", "l2 m2 d", "m2 e l", "b f r", "f g d", "g i r", "i h u", "h l r", "g c l", "c d d", "d n r", "n m u", "m q r", "q r d", "q p u", "p o u", "o k l", "k j u", "j s r", "s t d", "t x r", "x y d", "y a2 r", "a2 z u", "x w u", "j2 h2 d", "h2 d2 l", "d2 f2 d", "f2 j2 r", "j2 i2 u", "p u r", "u v d", "v b2 r", "b2 f2 r", "b2 c2 d", "c2 k2 r", "r c2 r" };
            string[] inputLines2 = { "a l2 r", "a b d", "l2 m2 d", "m2 e l", "b f r", "f g d", "g i r", "i h u", "h l r", "g c l", "c d d", "d n r", "n m u", "m q r", "q r d", "q p u", "p o u", "o k l", "k j u", "j s r", "s t d", "t x r", "x y d", "y a2 r", "a2 z u", "x w u", "j2 h2 d", "h2 d2 l", "d2 f2 d", "f2 j2 r", "j2 i2 u", "p u r", "u v d", "v b2 r", "b2 f2 r", "b2 c2 d", "c2 k2 r", "r c2 r" };

            //Graph (ConnectedTo)
            for (int i = 0; i < inputLines2.Length; i++)
            {
                string[] words = inputLines2[i].Split(' ');

                cNode n1 = listOfAllNodes.Find(x => x.name == words[0]);
                cNode n2 = listOfAllNodes.Find(x => x.name == words[1]);
                cNode pnn1 = original.Find(x => x.name == words[0]);
                cNode pnn2 = original.Find(x => x.name == words[1]);

                n1.direct.Add(words[2]);
                n1.connectedTo.Add(n2);

                pnn1.direct.Add(words[2]);
                pnn1.connectedTo.Add(pnn2);
                //MessageBox.Show(""+n1.name +"/"+n2.name);
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Down)
            {
                posr++;
            }

            if (e.KeyCode == Keys.Up)
            {
                posr--;
            }


            if (e.KeyCode == Keys.Right)
            {
                posc++;
            }



            if (e.KeyCode == Keys.Left)
            {
                posc--;
            }
            dataGridView1.Rows[posr].Cells[posc].Style.BackColor = Color.Red;
        }


        private void Form1_Load1(object sender, EventArgs e)
        {
        }
        
        void copy()
        {
            for (int i = 0; i < listOfAllNodes.Count; i++)
            {
                cNode pnn = new cNode();
                pnn.c = listOfAllNodes[i].c;
                pnn.r = listOfAllNodes[i].r;
                pnn.direct = listOfAllNodes[i].direct;
                for(int z=0;z<listOfAllNodes[i].connectedTo.Count;z++)
                {
                    cNode pnn2 = new cNode();
                    
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<cNode> temp = new List<cNode>();

            // start Greedy 
            string path = "";
            bool foundGoal = false;
            Greedy greedySearch = new Greedy();
            greedySearch.SearchGraph(listOfAllNodes[0], ref path, ref foundGoal);
            greedySearch.printPath(ref listP);
            
            path = "";
            foreach (string s in listP)
            {
                path += s +" ";
            }
            string[] pm = path.Split();
            listOfAllNodes = original;

            draw(path);
            /*MessageBox.Show(path);*/
            // end greedy
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                radioButton1.Text = "Animated";

            }
            else
            {
                radioButton1.Text = "Not Animated";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bfs breadth = new bfs();
            breadth.search("k2", listOfAllNodes, Opened);
            breadth.dataentery(listOfNeededNodes, Opened, listOfAllNodes);
            draw(listOfNeededNodes[listOfNeededNodes.Count - 1].path);
        }

        public void updateDataGridView(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Refresh();

            string[] row = { "", "", "", "", "", "", "" };
           
          
            for (int i = 0; i < 11; i++)
            {
                dataGridView.Rows.Add(row);
                
            }
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                dataGridView1.Rows[i].Height = 30;
            }
            for (int i = 0; i < 21; i++) 
            {
                dataGridView1.Columns[i].Width = 30;
               
            }

            dataGridView1.Rows[1].Cells[1].Style.BackColor = Color.Red;
            dataGridView1.Rows[9].Cells[19].Style.BackColor = Color.Green;
            dataGridView1.Rows[5].Cells[19].Style.BackColor = Color.Green;
            //dataGridView1.Rows[0].Cells[0].Style.BackColor=Color.Black;
            //dataGridView1.Rows[0].Cells[1].Style.BackColor = Color.White;
            for (int i=1;i<dataGridView1.Rows.Count;i++)
            {
                dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Black;
                dataGridView1.Rows[i].Cells[20].Style.BackColor = Color.Black;
            }

            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Rows[0].Cells[i].Style.BackColor = Color.Black;
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[i].Style.BackColor = Color.Black;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[i].Style.BackColor = Color.Black;
            }
            /*test1*/



            dataGridView1.Rows[2].Cells[6].Style.BackColor = Color.Black;

            dataGridView1.Rows[6].Cells[2].Style.BackColor = Color.Black;
            dataGridView1.Rows[6].Cells[4].Style.BackColor = Color.Black;
            
            dataGridView1.Rows[8].Cells[6].Style.BackColor = Color.Black;
            dataGridView1.Rows[8].Cells[8].Style.BackColor = Color.Black;
       
            dataGridView1.Rows[1].Cells[5].Style.BackColor = Color.Black;
            dataGridView1.Rows[2].Cells[5].Style.BackColor = Color.Black;
            dataGridView1.Rows[3].Cells[5].Style.BackColor = Color.Black;

            /*end of test 1*/

            dataGridView1.Rows[2].Cells[12].Style.BackColor = Color.Black;
            dataGridView1.Rows[2].Cells[10].Style.BackColor = Color.Black;
            dataGridView1.Rows[4].Cells[12].Style.BackColor = Color.Black;
            dataGridView1.Rows[4].Cells[16].Style.BackColor = Color.Black;
            dataGridView1.Rows[4].Cells[14].Style.BackColor = Color.Black;

            dataGridView1.Rows[6].Cells[16].Style.BackColor = Color.Black;
            dataGridView1.Rows[2].Cells[18].Style.BackColor = Color.Black;
            dataGridView1.Rows[6].Cells[18].Style.BackColor = Color.Black;
           // dataGridView1.Rows[9].Cells[13].Style.BackColor = Color.Black;




            dataGridView1.Rows[6].Cells[1].Style.BackColor = Color.Black;

            dataGridView1.Rows[6].Cells[6].Style.BackColor = Color.Black;

            dataGridView1.Rows[6].Cells[7].Style.BackColor = Color.Black;

            dataGridView1.Rows[6].Cells[8].Style.BackColor = Color.Black;
            dataGridView1.Rows[5].Cells[10].Style.BackColor = Color.Black;
            dataGridView1.Rows[5].Cells[11].Style.BackColor = Color.Black;

            dataGridView1.Rows[8].Cells[14].Style.BackColor = Color.Black;

            dataGridView1.Rows[5].Cells[8].Style.BackColor = Color.Black;

            dataGridView1.Rows[2].Cells[3].Style.BackColor = Color.Black;
            dataGridView1.Rows[2].Cells[2].Style.BackColor = Color.Black;

            dataGridView1.Rows[4].Cells[3].Style.BackColor = Color.Black;
            dataGridView1.Rows[4].Cells[2].Style.BackColor = Color.Black;

            dataGridView1.Rows[8].Cells[3].Style.BackColor = Color.Black;
            dataGridView1.Rows[8].Cells[2].Style.BackColor = Color.Black;


            dataGridView1.Rows[4].Cells[5].Style.BackColor = Color.Black;
            dataGridView1.Rows[4].Cells[4].Style.BackColor = Color.Black;

            dataGridView1.Rows[8].Cells[5].Style.BackColor = Color.Black;
            dataGridView1.Rows[8].Cells[4].Style.BackColor = Color.Black;


           // dataGridView1.Rows[4].Cells[7].Style.BackColor = Color.Black;
            dataGridView1.Rows[4].Cells[6].Style.BackColor = Color.Black;

            dataGridView1.Rows[4].Cells[7].Style.BackColor = Color.Black;
            

            dataGridView1.Rows[2].Cells[9].Style.BackColor = Color.Black;
            dataGridView1.Rows[2].Cells[8].Style.BackColor = Color.Black;


            dataGridView1.Rows[4].Cells[11].Style.BackColor = Color.Black;
            dataGridView1.Rows[4].Cells[10].Style.BackColor = Color.Black;

            dataGridView1.Rows[8].Cells[11].Style.BackColor = Color.Black;
            dataGridView1.Rows[8].Cells[10].Style.BackColor = Color.Black;


            dataGridView1.Rows[6].Cells[13].Style.BackColor = Color.Black;
            dataGridView1.Rows[6].Cells[12].Style.BackColor = Color.Black;

            dataGridView1.Rows[8].Cells[13].Style.BackColor = Color.Black;
            dataGridView1.Rows[8].Cells[12].Style.BackColor = Color.Black;

            dataGridView1.Rows[2].Cells[15].Style.BackColor = Color.Black;
            dataGridView1.Rows[2].Cells[14].Style.BackColor = Color.Black;

            dataGridView1.Rows[6].Cells[15].Style.BackColor = Color.Black;
            dataGridView1.Rows[6].Cells[14].Style.BackColor = Color.Black;

            dataGridView1.Rows[8].Cells[17].Style.BackColor = Color.Black;
            dataGridView1.Rows[8].Cells[16].Style.BackColor = Color.Black;

            dataGridView1.Rows[2].Cells[17].Style.BackColor = Color.Black;
            dataGridView1.Rows[2].Cells[16].Style.BackColor = Color.Black;


            dataGridView1.Rows[4].Cells[19].Style.BackColor = Color.Black;
            dataGridView1.Rows[4].Cells[18].Style.BackColor = Color.Black;

            dataGridView1.Rows[8].Cells[19].Style.BackColor = Color.Black;
            dataGridView1.Rows[8].Cells[18].Style.BackColor = Color.Black;


            dataGridView1.Rows[3].Cells[2].Style.BackColor = Color.Black;

            dataGridView1.Rows[5].Cells[4].Style.BackColor = Color.Black;

            dataGridView1.Rows[1].Cells[6].Style.BackColor = Color.Black;
            dataGridView1.Rows[3].Cells[6].Style.BackColor = Color.Black;
            dataGridView1.Rows[7].Cells[6].Style.BackColor = Color.Black;

            dataGridView1.Rows[9].Cells[8].Style.BackColor = Color.Black;
            dataGridView1.Rows[4].Cells[8].Style.BackColor = Color.Black;

            dataGridView1.Rows[7].Cells[10].Style.BackColor = Color.Black;
            dataGridView1.Rows[3].Cells[10].Style.BackColor = Color.Black;

            dataGridView1.Rows[1].Cells[12].Style.BackColor = Color.Black;
            dataGridView1.Rows[5].Cells[12].Style.BackColor = Color.Black;
            //dataGridView1.Rows[9].Cells[12].Style.BackColor = Color.Black;


            dataGridView1.Rows[3].Cells[14].Style.BackColor = Color.Black;
           // dataGridView1.Rows[7].Cells[14].Style.BackColor = Color.Black;

            dataGridView1.Rows[3].Cells[16].Style.BackColor = Color.Black;
            dataGridView1.Rows[5].Cells[16].Style.BackColor = Color.Black;


            dataGridView1.Rows[5].Cells[18].Style.BackColor = Color.Black;
      
            dataGridView1.Rows[1].Cells[1].Value = "a"  ;
            dataGridView1.Rows[5].Cells[1].Value = "b"  ;
            dataGridView1.Rows[7].Cells[1].Value = "c"  ;
            dataGridView1.Rows[9].Cells[1].Value = "d"  ;
            dataGridView1.Rows[3].Cells[3].Value = "e"  ;
            dataGridView1.Rows[5].Cells[3].Value = "f"  ;
            dataGridView1.Rows[7].Cells[3].Value = "g"  ;
            dataGridView1.Rows[5].Cells[5].Value = "h"  ;
            dataGridView1.Rows[7].Cells[5].Value = "i"  ;
            dataGridView1.Rows[1].Cells[7].Value = "j"  ;
            dataGridView1.Rows[3].Cells[7].Value = "k"  ;
            dataGridView1.Rows[5].Cells[7].Value = "l"  ;
            dataGridView1.Rows[7].Cells[7].Value = "m"  ;
            dataGridView1.Rows[9].Cells[7].Value = "n"  ;
            dataGridView1.Rows[3].Cells[9].Value = "o"  ;
            dataGridView1.Rows[6].Cells[9].Value = "p"  ;
            dataGridView1.Rows[7].Cells[9].Value = "q"  ;
            dataGridView1.Rows[9].Cells[9].Value = "r"  ;
            dataGridView1.Rows[1].Cells[11].Value = "s" ;
            dataGridView1.Rows[3].Cells[11].Value = "t" ;
            dataGridView1.Rows[6].Cells[11].Value = "u" ;
            dataGridView1.Rows[7].Cells[11].Value = "v" ;
            dataGridView1.Rows[1].Cells[13].Value = "w" ;
            dataGridView1.Rows[3].Cells[13].Value = "x" ;
            dataGridView1.Rows[5].Cells[13].Value = "y" ;
            dataGridView1.Rows[3].Cells[15].Value = "z" ;
            dataGridView1.Rows[5].Cells[15].Value = "a2";
            dataGridView1.Rows[7].Cells[15].Value = "b2";
            dataGridView1.Rows[9].Cells[15].Value = "c2";
            dataGridView1.Rows[3].Cells[17].Value = "d2";
            dataGridView1.Rows[7].Cells[17].Value = "f2";
            dataGridView1.Rows[1].Cells[19].Value = "g2";
            dataGridView1.Rows[3].Cells[19].Value = "h2";
            dataGridView1.Rows[5].Cells[19].Value = "i2";
            dataGridView1.Rows[7].Cells[19].Value = "j2";
            dataGridView1.Rows[9].Cells[19].Value = "k2";
            dataGridView1.Rows[1].Cells[4].Value = "l2";
            dataGridView1.Rows[3].Cells[4].Value = "m2";






        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateDataGridView(dataGridView1);
            hardCodedInput();
           // rows.RemoveAt(0);
           // cols.RemoveAt(0);
            tt.Start();
            //draw(pp);

            // cs();
        }



        Color clr;
        void draw(string p)
        {
            clr = Color.FromArgb(rr.Next(256), rr.Next(256), rr.Next(256));
            lb_path.Text = "";
            //p = p.Replace(" ", " -> ");
            lb_path.Text += p;
            MessageBox.Show(p);
            string[] path = p.Split();
            for (int i = 0; i < path.Length; i++)
            {
                for (int z = 0; z < listOfAllNodes.Count; z++)
                {
                    if (i != path.Length - 1)
                    {
                        string first = path[i];
                        string second = path[i + 1];

                        if (listOfAllNodes[z].name == first)///catch first letter
                        {
                            for (int m = 0; m < listOfAllNodes[z].connectedTo.Count; m++)//connected list
                            {
                                if (listOfAllNodes[z].connectedTo[m].name == second)
                                {

                                    //putcolor(first,second,)
                                    putcolor(first, second, listOfAllNodes[z].direct[m], listOfAllNodes[z].r, listOfAllNodes[z].c);
                                }
                            }
                        }
                    }
                }
            }
            dataGridView1.Rows[1].Cells[1].Style.BackColor = Color.Red;
            dataGridView1.Rows[9].Cells[19].Style.BackColor = Color.Green;
            dataGridView1.Rows[5].Cells[19].Style.BackColor = Color.Green;
        }


        void putcolor(string first, string second, string dir, int row, int col)
        {
            if (dir == "l")//move left  col--
            {
                for (int c = col; c > 0; c--)
                {
                    pathcostct++;
                    rows.Add(row);
                    cols.Add(c);
                    //dataGridView1.Rows[row].Cells[c].Style.BackColor = clr;
                    if (dataGridView1.Rows[row].Cells[c].Value != null && dataGridView1.Rows[row].Cells[c].Value.ToString() == second)
                    {
                        break;
                    }
                }
            }


            if (dir == "r")//move right  col++
            {
                for (int c = col; c < dataGridView1.Rows[row].Cells.Count; c++)
                {
                    pathcostct++;
                    rows.Add(row);
                    cols.Add(c);
                    //dataGridView1.Rows[row].Cells[c].Style.BackColor = clr;
                    if (dataGridView1.Rows[row].Cells[c].Value != null && dataGridView1.Rows[row].Cells[c].Value.ToString() == second)
                    {
                        break;
                    }
                }
            }




            if (dir == "u")//move up row--
            {
                for (int r = row; r > 0; r--)
                {
                    rows.Add(r);
                    cols.Add(col);

                    //dataGridView1.Rows[r].Cells[col].Style.BackColor = clr;
                    pathcostct++;
                    if (dataGridView1.Rows[r].Cells[col].Value != null && dataGridView1.Rows[r].Cells[col].Value.ToString() == second)
                    {
                        break;
                    }
                }
            }



            if (dir == "d")//move down row++
            {
                for (int r = row; r < dataGridView1.Rows.Count; r++)
                {
                    pathcostct++;
                   // dataGridView1.Rows[r].Cells[col].Style.BackColor = clr;

                    rows.Add(r);
                    cols.Add(col);

                    if (dataGridView1.Rows[r].Cells[col].Value != null && dataGridView1.Rows[r].Cells[col].Value.ToString() == second)
                    {
                        break;
                    }
                }
            }

        }



        /*void cs()
        {
            for(int r=0;r<dataGridView1.Rows.Count;r++)
            {
                for(int c =0; c<dataGridView1.Rows[r].Cells.Count; c++)
                {
                    dataGridView1.Rows[r].Cells[c].Value = "r=" + r + "c =" +  c;
                }
            }
        }
*/

    }
}

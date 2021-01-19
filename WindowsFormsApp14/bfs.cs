using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp14
{
    class bfs
    {
       
            List<string> path = new List<string>();
            public void dataentery(List<cNode> listOfNeededNodes, List<string> Open, List<cNode> listOfAllNodes)
            {


                for (int r = 0; r < Open.Count; r++)
                {
                    for (int i = 0; i < listOfAllNodes.Count; i++)
                    {

                        if (listOfAllNodes[i].name == Open[r] && listOfAllNodes[i].isVisted == false)
                        {
                            cNode pnn = new cNode();
                            pnn.connectedTo = listOfAllNodes[i].connectedTo;
                            pnn.direct = listOfAllNodes[i].direct;
                            pnn.isVisted = listOfAllNodes[i].isVisted;
                            pnn.name = listOfAllNodes[i].name;
                            pnn.r = listOfAllNodes[i].r;
                            pnn.c = listOfAllNodes[i].c;
                            pnn.path = listOfAllNodes[i].path;
                            listOfNeededNodes.Add(pnn);
                        }

                    }
                }

            }

            public void search(string k, List<cNode> listOfAllNodes, List<string> Open)
            {

                listOfAllNodes[0].path += listOfAllNodes[0].name;
                Open.Add("a");

                int found = 0;
                for (int r = 0; r < Open.Count; r++)
                {
                    for (int i = 0; i < listOfAllNodes.Count; i++)
                    {
                        if (listOfAllNodes[i].name == Open[r] && listOfAllNodes[i].isVisted == false)
                        {
                            listOfAllNodes[i].isVisted = true;

                            for (int z = 0; z < listOfAllNodes[i].connectedTo.Count; z++)
                            {

                                Open.Add(listOfAllNodes[i].connectedTo[z].name);

                                listOfAllNodes[i].connectedTo[z].path += listOfAllNodes[i].path + " " + listOfAllNodes[i].connectedTo[z].name;


                                if (listOfAllNodes[i].connectedTo[z].name == k)
                                {

                                    found = 1;
                                    break;
                                }
                            }
                            break;
                        }

                    }
                    if (found == 1)
                    {
                        break;
                    }
                }


                for (int i = 0; i < listOfAllNodes.Count; i++)
                {
                    listOfAllNodes[i].isVisted = false;
                }



            }
    }

    
}

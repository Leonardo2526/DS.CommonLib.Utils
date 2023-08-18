using FrancoGustavo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPA
{
    static class PathRefinement
    {
        public static void Refine(List<AbstractNode> abstractPath)
        {
            AbstractGraph.Path = new List<AbstractNode>();

            for (int i = 0; i < abstractPath.Count -1; i++)
            {
                AbstractNode node1 = abstractPath[i];
                AbstractNode node2 = abstractPath[i+1];

                List<AbstractNode> intraPath = AbstractGraph.IntraPathes[node1.Id, node2.Id];

                if (intraPath == null)
                {
                    //AbstractGraph.Path.Add(node1);
                    AbstractGraph.Path.Add(node2);
                }
                else
                {
                    intraPath.RemoveAt(0);
                    AbstractGraph.Path.AddRange(intraPath);
                }
            }
          
        }
	}

}


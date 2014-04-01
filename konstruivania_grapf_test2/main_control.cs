using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace konstruivania_grapf_test2
{
    class main_control :Window
    {
       public int first = 0;
       public int tekyshui;
       public bool mouse_down = false;
       public bool editing = true;
       public bool mouse_down_right = false;
       public List<vershuna> elipsu = new List<vershuna>();
       public List<Edge> rebra = new List<Edge>();
       public int from=-1;
       public int to=-1;
       public bool zvorotnii = false;
       public bool enable_conect = true;

       public void add_rebro()//зєднання двох вершин
       {
           Edge a = new Edge(this);
           rebra.Add(a);
            zvorotnii = true;
           Edge b = new Edge(this);
           rebra.Add(b);
           zvorotnii = false;
           from = -1;
           to = -1;

       }

       public void deikstra_run()//запуск алгоритму дейкстри
       {
           double[,] G = new double[elipsu.Count, elipsu.Count];

           /* Set the connections and weights based on each edge in the collection */
           foreach (Edge edge in rebra)
           {
               G[edge.from, edge.to] = edge.weight;
           }

           /* Runs dijkstra */
           Dijkstra dijk = new Dijkstra(G, first);
           double[] dist = dijk.dist;
           int[] path = dijk.path;
           string str = "";
           /* Prints the shortest distances on the nodes */
           for (int i = 0; i < dist.Count(); i++)
           {
               if (dist[i].ToString() == "бесконечность")
               {
                   elipsu[i].lb_vershunu.Content = "∞";
               }
               else 
               {
                   elipsu[i].lb_vershunu.Content = dist[i].ToString();
               }

                            
           
           }
          

         
         }





       public void add_vershuna(int _x, int _y) //створення вершини
       {
           vershuna a=new vershuna(_x,_y, elipsu.Count, this); 
           elipsu.Add(a);

          
          
       }
    }
}

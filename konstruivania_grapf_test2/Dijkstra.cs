using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace konstruivania_grapf_test2
{
    class Dijkstra
    {
       
        public double[] dist { get; private set; }
        public int[] path { get; private set; }
              
        private List<int> queue = new List<int>();


        private void Initialize(int s, int len)  //обробка прийнятих даних, ініціалізація початковими даними структури(побудова матриці) для подалльшого використання
        {
            dist = new double[len];
            path = new int[len];
                    
            for (int i = 0; i < len; i++)
            {
                dist[i] = Double.PositiveInfinity;

                queue.Add(i);
            }

   
            dist[s] = 0;
            path[s] = -1;
        }


        private int GetNextVertex() //пошук наступної вершини
        {
            double min = Double.PositiveInfinity;
            int Vertex = -1;

          
            foreach (int j in queue)
            {
                if (dist[j] <= min)
                {
                    min = dist[j];
                    Vertex = j;
                }
            }

            queue.Remove(Vertex);

            return Vertex;

        }


        public Dijkstra(double[,] G, int s) //перевірка графа на помилки
        {
            //перевірка на правильність графа
            if (G.GetLength(0) < 1 || G.GetLength(0) != G.GetLength(1))
            {
                throw new ArgumentException("Graph error, wrong format or no nodes to compute");
            }

            int len = G.GetLength(0);

            Initialize(s, len);

            while (queue.Count > 0)
            {
                int u = GetNextVertex();

                /* Find the nodes that u connects to and perform relax */
                for (int v = 0; v < len; v++)
                {
                    //перевірка на правильність ребер у графі
                    if (G[u, v] < 0)
                    {
                        throw new ArgumentException("Graph contains negative edge(s)");
                    }

                  
                    if (G[u, v] > 0)
                    {
                       
                        if (dist[v] > dist[u] + G[u, v])
                        {
                            dist[v] = dist[u] + G[u, v];
                            path[v] = u;
                        }
                    }
                }
            }
        }
    }
}

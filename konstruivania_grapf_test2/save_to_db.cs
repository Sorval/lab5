using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.Common;

namespace konstruivania_grapf_test2
{
     [Serializable]
    class save_to_db
    {
         public int x = 0;
         public int x_reber = 0;
         public int[] Ax ;
         public int[] Ay;
         public int[] from;
         public int[] to;
         public void save_to_DB()//динамічне виділення памяті під збереження графа у файл
        {
            Ax = new int[x];
            Ay = new int[x];
            from = new int[x_reber];
            to = new int[x_reber];
        }
         
         private void create_db()
         {
             
             
         }

    }
}

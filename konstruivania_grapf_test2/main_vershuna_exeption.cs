using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace konstruivania_grapf_test2
{
    class main_vershuna_exeption : Exception
    {
        private string erorr;
        public main_vershuna_exeption()//ініціалізація типу помилки
        {
            erorr = "Це головна вершина або не зєднана з іншими\nвершинами які б в свою чергу зєднувались з головною\nвершиною, тому до неї найкоротший шлях завжди 0 або безкінечність";
            
        }

        public string ToString()//виведення помилки
        {
            return erorr;
        }
    }
}

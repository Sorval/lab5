using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace konstruivania_grapf_test2
{
    class Edge : Window
    {
        public int from { get; private set; }
        public int to { get; private set; }
        public Double weight { get; private set; }
        public Line rebro;
        public Label lb_vershunu;

        public Edge(main_control a) //повна ініціалізація ребра
        {
            if (a.zvorotnii == false)
            {
                this.from = a.from;
                this.to = a.to;
                this.weight = this.from + this.to;
                rebro = new Line();
                rebro.X1 = a.elipsu[from].blueRectangle.Margin.Left + 14;
                rebro.Y1 = a.elipsu[from].blueRectangle.Margin.Top + 14;
                rebro.X2 = a.elipsu[to].blueRectangle.Margin.Left + 14;
                rebro.Y2 = a.elipsu[to].blueRectangle.Margin.Top + 14;
                SolidColorBrush blackBrush = new SolidColorBrush();
                blackBrush.Color = Colors.Black;
                rebro.Stroke = blackBrush;
                rebro.StrokeThickness = 4;
                rebro.ToolTip = this.weight;
                lb_vershunu = new Label();
                lb_vershunu.Content = this.weight.ToString();
                lb_vershunu.Margin = new Thickness((rebro.X1 + rebro.X2) / 2 + 15, (rebro.Y1 + rebro.Y2) / 2 + 15, 0, 0);
            }
            else if (a.zvorotnii == true)
            {
                this.from = a.to;
                this.to = a.from;
                this.weight = this.from + this.to;
                rebro =a.rebra[a.rebra.Count - 1].rebro;
                lb_vershunu = new Label();
            }
            // rebro.

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace konstruivania_grapf_test2
{
    class vershuna : Window
    {
       
        public Point p;
        public int ident;
        public int dist;
        public  Ellipse blueRectangle;
        public Label lb_vershunu;
        main_control rez;
        public int x, y,id;
        List<int> mas_rebers_index=new List<int>();

        public vershuna(int _x, int _y, int _id, main_control a)//ініціалізація початкових даних вершини
        {
            x = _x;
            y = _y;
            id =_id;
            rez = a;
            ident = id;
            p.X = x;
            p.Y = y;
            add_vershuna();
        }
        public void add_redbrechko(object sender, RoutedEventArgs e)//створення ребра у графі
       {
           Ellipse testik = (Ellipse)sender;
           if (rez.editing)
           {
               if (rez.enable_conect == true)
               {
                   if (rez.from == -1)
                   {
                       rez.from = int.Parse(testik.Name.ToString().Replace("n", ""));
                       blueRectangle.StrokeThickness = 4;
                   }
                   else if (rez.to == -1)
                   {
                       rez.to = int.Parse(testik.Name.ToString().Replace("n", ""));
                       blueRectangle.StrokeThickness = 6;
                       rez.add_rebro();
                       rez.enable_conect = false;
                   }
               }
           }
           
       }
       protected void lb_vershunu_MouseDown(object sender, RoutedEventArgs e)//виділення красним найкоротший шлях від вибраної вершини до головної
       {
           List<int> value_versh = new List<int>();
           List<int> id_rebra = new List<int>();
           List<int> nomer_vershunu = new List<int>();
           int vudilena_verchuna=int.Parse(lb_vershunu.Uid);
        
           SolidColorBrush redBrush = new SolidColorBrush();
           redBrush.Color = Colors.Red;
           SolidColorBrush blackBrush = new SolidColorBrush();
           blackBrush.Color = Colors.Black;
           for (int i = 0; i < rez.rebra.Count; i++)//красимо все линии в чорный 
           {
               rez.rebra[i].rebro.Stroke = blackBrush;
           }

           while (true)
           {
               

               for (int i = 0; i < rez.rebra.Count; i++)//
               {

                   if (rez.rebra[i].from == vudilena_verchuna)
                   {
                       try
                       {
                           if (rez.elipsu[rez.rebra[i].from].lb_vershunu.Content.ToString().Equals("∞"))
                           { throw new main_vershuna_exeption(); }
                           else
                           {
                               if (int.Parse(rez.elipsu[rez.rebra[i].to].lb_vershunu.Content.ToString()) < int.Parse(rez.elipsu[vudilena_verchuna].lb_vershunu.Content.ToString()))
                               {
                                   value_versh.Add(int.Parse(rez.elipsu[rez.rebra[i].to].lb_vershunu.Content.ToString()));
                                   id_rebra.Add(i);
                                   nomer_vershunu.Add(rez.rebra[i].to);

                               }

                           }
                       }
                       catch (main_vershuna_exeption ex)
                       {

                           MessageBox.Show(ex.ToString());
                           vudilena_verchuna = rez.first;
                       }
                   }


                   if (rez.rebra[i].to == vudilena_verchuna)
                   {
                       //try
                       try
                       {
                           if (rez.elipsu[rez.rebra[i].from].lb_vershunu.Content.ToString().Equals("∞"))
                           { throw new main_vershuna_exeption(); }
                           else
                           {
                               if (int.Parse(rez.elipsu[rez.rebra[i].from].lb_vershunu.Content.ToString()) < int.Parse(rez.elipsu[vudilena_verchuna].lb_vershunu.Content.ToString()))
                               {

                                   value_versh.Add(int.Parse(rez.elipsu[rez.rebra[i].from].lb_vershunu.Content.ToString()));
                                   id_rebra.Add(i);
                                   nomer_vershunu.Add(rez.rebra[i].from);

                               }
                           }
                       }

                       catch (main_vershuna_exeption ex)
                       {

                           MessageBox.Show(ex.ToString());
                           vudilena_verchuna = rez.first;
                       }
                      
                   }
               }

               int min = 0;
               try
               {
                   if (value_versh.Count == 0)
                   {
                        throw new main_vershuna_exeption();
                   }
                   else
                   {
                       min = value_versh.Min();
                   }
               }
               catch (main_vershuna_exeption ex)
               {

                   MessageBox.Show(ex.ToString());
                   vudilena_verchuna = rez.first;
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.ToString());
               }
             
               for (int i = 0; i < id_rebra.Count; i++)
               {

                   if (value_versh[i] == min)
                   {
                       rez.rebra[id_rebra[i]].rebro.Stroke = redBrush;
                       vudilena_verchuna = nomer_vershunu[i];
                
                   }
               }
             

               if (vudilena_verchuna == rez.first)
               {break;}
               
           }
          
    
       }
        protected void blueRectangle_MouseDown(object sender, RoutedEventArgs e) 
        {
         

        }

        void add_vershuna()//створення вершини
        {
            int StrokeThickness_ = 0;
            Color color_ = Colors.Blue;
            
             blueRectangle = new Ellipse()
            {
                
                Height = 28,
                Width = 28,
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = StrokeThickness_,
                
                Fill = new RadialGradientBrush()
                {
                    RadiusX = 1,
                    RadiusY = 1,
                    GradientOrigin = new Point(0.7, 0.3),
                    GradientStops = new GradientStopCollection()
                        {
                             new GradientStop() {Color = Colors.White, Offset = 0},
                             new GradientStop() {Color = color_, Offset = 1},
                              new GradientStop() {Color = Colors.Aqua, Offset = 1}
                        }
                }
                
            };
            
            blueRectangle.Margin = new Thickness(x-15, y-15, 0, 0);
            blueRectangle.MouseDown += new MouseButtonEventHandler(blueRectangle_MouseDown);
            blueRectangle.MouseRightButtonDown += new MouseButtonEventHandler(add_redbrechko);
            blueRectangle.Name = "n" + id.ToString();
             
            lb_vershunu = new Label()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                Width = 32,
                Height = 28
            };
            lb_vershunu.Name = "l" + id.ToString();
            lb_vershunu.Uid = id.ToString();
            lb_vershunu.Margin = new Thickness(x-15, y-15, 0, 0);
            blueRectangle.ToolTip = blueRectangle.Name;
            lb_vershunu.MouseDown += new MouseButtonEventHandler(lb_vershunu_MouseDown);

        }
    }
}

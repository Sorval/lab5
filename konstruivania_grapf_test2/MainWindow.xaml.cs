using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.Common;
///

namespace konstruivania_grapf_test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///  

    public partial class MainWindow : Window
    {
      
        main_control final; //
        save_to_db save;
        public MainWindow()
        {
            InitializeComponent(); //ініціалізація форми
            final = new main_control();//створення екземпляра класу управління
          
        }
        

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void main_vindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)//обробка двойного щелчка на формі, створення дочірніх елементів форми
        {
            if (final.editing == true) 
            {
                Point currPos = e.GetPosition(grid_main_window);
                int x = (int)currPos.X;
                int y = (int)currPos.Y;
                final.add_vershuna(x, y);
                grid_main_window.Children.Add(final.elipsu[final.elipsu.Count - 1].blueRectangle);
                comboBox1.Items.Add(final.elipsu[final.elipsu.Count - 1].blueRectangle.Name);
                comboBox1.SelectedIndex = final.elipsu.Count - 1;
            }
        }
        

        private void main_window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void main_window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        public void add_rebr()
        {
            
        }

        private void button1_Click(object sender, RoutedEventArgs e) //метод поєднання вершин (створеня ребра), створення дочірніх елементів форми
        {
            bool no_select_verchuna = false;
            for (int ia = 0; ia < final.elipsu.Count; ia++)
            {
                if (final.elipsu[ia].blueRectangle.StrokeThickness == 6)
                {
                    no_select_verchuna = true;
                }
            }
            if (no_select_verchuna)
            {
                grid_main_window.Children.Add(final.rebra[final.rebra.Count - 1].rebro);
                Grid.SetZIndex(final.rebra[final.rebra.Count - 1].rebro, -1);
                final.elipsu[final.rebra[final.rebra.Count - 1].from].blueRectangle.StrokeThickness = 1;
                final.elipsu[final.rebra[final.rebra.Count - 1].to].blueRectangle.StrokeThickness = 1;
                final.enable_conect = true;
            }
            else
            {
                MessageBox.Show("Щоб використати цей елемент управлління потрібно щоб було виділено 2 вершини");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e) //запуск знаходження найкоротшого шляху
        {
            bool no_select_verchuna = true;
            for (int ia = 0; ia < final.elipsu.Count; ia++) 
            {
                if (final.elipsu[ia].blueRectangle.StrokeThickness ==4)
                {
                    no_select_verchuna = false;
                }
            }

            if (comboBox1.Items.Count==0)
            {
                MessageBox.Show("Перш ніж шукати найкоротший шлях потрібно створити мінімум 1 вершину","Eror",MessageBoxButton.OK,MessageBoxImage.Error);

            }
            else if(no_select_verchuna==false)
            {
                MessageBox.Show("Перш ніж шукати найкоротший шлях освободіть поле від виділених вершин");
            }

             else if(final.editing==true)
            {
                MessageBox.Show("Перш ніж шукати найкоротший шлях потрібно вийти з режиму редагування");
             }
            else
            {

                final.first = comboBox1.SelectedIndex;
                final.deikstra_run();
                for (int i = 0; i < final.elipsu.Count;i++ )
                {
                grid_main_window.Children.Add(final.elipsu[i].lb_vershunu);
                }
                button1.IsEnabled = false;
                button4.IsEnabled=false;
                button2.IsEnabled = false;

            }

        }

        private void button5_Click(object sender, RoutedEventArgs e)//очищення вікна для створення графів, активація кнопок загрузки, створення ребер
        {
            button2.IsEnabled = true;
            button1.IsEnabled = true;
            button4.IsEnabled = true;
            grid_main_window.Children.RemoveRange(0,grid_main_window.Children.Count);
            final = null;
            final = new main_control();
            comboBox1.Items.Clear();
          
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void checkBox1_Click(object sender, RoutedEventArgs e) //включення виключення режиму редагування
        {
            if (checkBox1.IsChecked==true)
            {
                groupBox2.IsEnabled=true;
                final.editing = true;
            }
            else
            {
                groupBox2.IsEnabled = false ;
                final.editing = false;
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)//збереження графа у файл
        {
            save = new save_to_db();
            save.x = final.elipsu.Count;
            save.x_reber = final.rebra.Count;
            save.save_to_DB();
            //vnosum vershunu
            for (int ui = 0; ui < final.elipsu.Count; ui++)
            {
                save.Ax[ui] = final.elipsu[ui].x;
                save.Ay[ui] = final.elipsu[ui].y;
            }
            //vnosum rebra
            for (int ui = 0; ui < final.rebra.Count; ui++)
            {
                save.from[ui] = final.rebra[ui].from;
                save.to[ui] = final.rebra[ui].to;
            }

                Nullable<bool> rez;
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Deikstra_graph (.volfar)|*.volfar";
            sf.FileName = "Deikstra_graph";
            rez = sf.ShowDialog();
            if (rez == true)
            {
                FileStream fils = new FileStream(sf.FileName, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fils, save);
                fils.Close();
            }
            

            
            
        }   

        private void button4_Click(object sender, RoutedEventArgs e) //загрузка графа з файла
        {
            

            Nullable<bool> rez;
            OpenFileDialog sf = new OpenFileDialog();
            sf.Filter = "Deikstra_graph (.volfar)|*.volfar";
            sf.FileName = "Deikstra_graph";
            rez = sf.ShowDialog();
            try
            {
                if (rez == true)
                {
                    FileStream fils = new FileStream(sf.FileName, FileMode.Open, FileAccess.ReadWrite);
                    BinaryFormatter bf = new BinaryFormatter();
                    save = (save_to_db)bf.Deserialize(fils);
                    fils.Close();
                }
           
            //clear all
            grid_main_window.Children.RemoveRange(0, grid_main_window.Children.Count);
            final = null;
            final = new main_control();
            comboBox1.Items.Clear();
            //vostanovlenia vershun
            for (int opi = 0; opi < save.x; opi++)
            {
                final.add_vershuna(save.Ax[opi], save.Ay[opi]);
                grid_main_window.Children.Add(final.elipsu[final.elipsu.Count - 1].blueRectangle);
                comboBox1.Items.Add(final.elipsu[final.elipsu.Count - 1].blueRectangle.Name);
            }
            comboBox1.SelectedIndex = 0;
            //vostanovlenia reber
            for (int opi = 0; opi < save.x_reber; opi++)
            {
                final.from = save.from[opi];
                final.to = save.to[opi];
                final.add_rebro();
                grid_main_window.Children.Add(final.rebra[final.rebra.Count - 1].rebro);
                Grid.SetZIndex(final.rebra[final.rebra.Count - 1].rebro, -1);
                final.elipsu[final.rebra[final.rebra.Count - 1].from].blueRectangle.StrokeThickness = 1;
                final.elipsu[final.rebra[final.rebra.Count - 1].to].blueRectangle.StrokeThickness = 1;
                final.enable_conect = true;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btn_exit_MouseEnter(object sender, MouseEventArgs e)
        {
          
        }

        private void btn_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btn_exit_MouseDoubleClick(object sender, MouseButtonEventArgs e)//завершення роботи програми
        {
            Close();
        }


    }
}

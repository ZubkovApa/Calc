using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement e1 in Buttons.Children)
            {
                if (e1 is Button)
                {
                    ((Button)e1).Click += ButtonClick;
                }
            }
        }

        public double m = 0;

        private void ButtonClick(Object sender, RoutedEventArgs e)
        {
            try
            {
                string textButton = ((Button)e.OriginalSource).Content.ToString();

                if (textButton == "C") { text.Clear(); }

                else if (textButton == "Mc") { m = 0; }

                else if (textButton == "Mr") { text.Text = m.ToString(); }

                else if (textButton == "Ms") { m = Double.Parse(text.Text); }

                else if (textButton == "M+") { text.Text = (int.Parse(text.Text) + m).ToString(); }

                else if (textButton == "M-") { text.Text = (int.Parse(text.Text) - m).ToString(); }

                else if (textButton == "🠔") { text.Text = text.Text.Remove(text.Text.Length-1, 1).ToString(); }

                else if (textButton == "CE")
                {
                    for (int i = 0; i < text.Text.Length; i++)
                    {
                        if (!char.IsLetter(text.Text[text.Text.Length - i - 1])) 
                        { 
                            text.Text = text.Text.Remove(text.Text.Length - i, i);
                            break;
                        }
                    }
                }

                else if (textButton == "x²") { text.Text = (int.Parse(text.Text) * int.Parse(text.Text)).ToString(); }

                else if (textButton == "√") { text.Text = Math.Pow(Double.Parse(text.Text), 0.5).ToString(); }

                else if (textButton == "1/x") { text.Text = Math.Pow(Double.Parse(text.Text), -1).ToString(); }

                else if (textButton == "=") { text.Text = new DataTable().Compute(text.Text, null).ToString().Replace(',','.'); }

                else if (textButton == "±")
                {
                    for (int i = 0; i < text.Text.Length; i++)
                    {
                        if (text.Text[text.Text.Length - i - 1] == '-') 
                        { 
                            text.Text = text.Text.Insert(text.Text.Length - i, "+").Remove(text.Text.Length - i - 1, 1);
                            break;
                        }
                        else if (text.Text[text.Text.Length - i - 1] == '+')
                        {
                            text.Text = text.Text.Insert(text.Text.Length - i, "-").Remove(text.Text.Length - i - 1, 1);
                            break;
                        }
                    } 
                }

                else text.Text += textButton;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}

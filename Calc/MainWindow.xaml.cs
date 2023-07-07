using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Calc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            bool a = true;
            InitializeComponent();
            foreach (UIElement e1 in Buttons.Children)
            {
                check(Mc); 
                check(Mr);
                text.IsReadOnly = true;
                if (e1 is Button button)
                {
                    button.Click += ButtonClick;
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

                else if (textButton == "Mc") { m = 0; check(Mc); check(Mr); }

                else if (textButton == "Mr") { text.Text = m.ToString(); check(Mc); check(Mr); }

                else if (textButton == "Ms") { m = Double.Parse(text.Text = "\n" + new DataTable().Compute(text.Text, null).ToString().Replace(',', '.')); check(Mc); check(Mr); }

                else if (textButton == "M+") { text.Text = (double.Parse(text.Text) + m).ToString(); }

                else if (textButton == "M-") { text.Text = (double.Parse(text.Text) - m).ToString(); }

                else if (textButton == "🠔") { text.Text = text.Text.Remove(text.Text.Length-1, 1).ToString(); }

                else if (textButton == "CE")
                {
                    for (int i = 0; i < text.Text.Length; i++)
                    {
                        if (!char.IsNumber(text.Text[text.Text.Length - i - 1])) { text.Text = text.Text.Remove(text.Text.Length - i, i); break; }
                    }
                }

                else if (textButton == "x²") { text.Text = Math.Pow(Double.Parse((text.Text).Replace('.', ',')), 2).ToString().Replace(',', '.'); }

                else if (textButton == "√") {
                    if (Double.Parse((text.Text).Replace('.', ',')) < 0){
                        text.Text = "Less Than Zero\n(c)The Weekend";
                    }
                    else { text.Text = Math.Pow(Double.Parse((text.Text).Replace('.', ',')), 0.5).ToString().Replace(',', '.'); }
                }

                else if (textButton == "1/x") { text.Text = Math.Pow(Double.Parse((text.Text).Replace('.', ',')), -1).ToString().Replace(',', '.'); }

                else if (textButton == "=") { text.Text = new DataTable().Compute(text.Text, null).ToString().Replace(',','.'); }

                else if (textButton == "±")
                {
                    bool check = false;
                    for (int i = 0; i < text.Text.Length; i++)
                    {
                        if (!Char.IsNumber(text.Text[i])) { check = true; break; }
                    } 
                    if (check)
                    {
                        for (int i = 0; i < text.Text.Length; i++)
                        {
                            if (text.Text[text.Text.Length - i - 1] == '-') { text.Text = text.Text.Insert(text.Text.Length - i, "+").Remove(text.Text.Length - i - 1, 1); break; }
                            else if (text.Text[text.Text.Length - i - 1] == '+') { text.Text = text.Text.Insert(text.Text.Length - i, "-").Remove(text.Text.Length - i - 1, 1); break; }
                        }
                    }
                    else { text.Text = text.Text.Insert(0, "-"); }
                }

                else text.Text += textButton;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        void check(Button M)
        {
            if (m == 0) { M.IsEnabled = false; }
            else { M.IsEnabled = true; }
        }
    }
}

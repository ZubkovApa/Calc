using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Calc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement e1 in Buttons.Children)
            {
                check(one); 
                check(two);
                text.Text = "";
                text.IsReadOnly = true;
                allButtons = new List<Button>(34) { one, two, three, four, five, six, seven, eight, nine, ten , eleven, twelve, thirteen, fourteen, fifteen, sixteen, seventeen, eighteen, nineteen, twentyone, twentytwo, twentythree, twentyfour, twentyfive, twentysix, twentyseven, twentyeight, twentynine, thirty, thirtyone, thirtytwo, thirtythree, thirtyfour};
                if (e1 is Button button)
                {
                    button.Click += ButtonClick;
                }
            }
        }

        public List<Button> allButtons;
        public double m = 0;
        public bool blk = false;

        private void ButtonClick(Object sender, RoutedEventArgs e)
        {
            try
            {
                string textButton = ((Button)e.OriginalSource).Content.ToString();

                if (textButton == "C") { text.Clear(); }

                else if (textButton == "Mc") { m = 0; check(one); check(two); }

                else if (textButton == "Mr") { text.Text = m.ToString(); check(one); check(two); }

                else if (textButton == "Ms") { m = Double.Parse(text.Text = "\n" + new DataTable().Compute(text.Text, null).ToString().Replace(',', '.')); check(one); check(two); }

                else if (textButton == "M+") { text.Text = (double.Parse(text.Text) + m).ToString(); }

                else if (textButton == "M-") { text.Text = (double.Parse(text.Text) - m).ToString(); }

                else if (textButton == "🠔") { text.Text = text.Text.Remove(text.Text.Length-1, 1).ToString(); }

                else if (textButton == "CE")
                {
                    bool check = false;
                    for (int i = 0;  i < text.Text.Length; i++)
                    {
                        if (!Char.IsNumber(text.Text[i])) { check = true; }
                    }
                    if (check)
                    {
                        for (int i = 0; i < text.Text.Length; i++)
                        {
                            if (!char.IsNumber(text.Text[text.Text.Length - i - 1])) { text.Text = text.Text.Remove(text.Text.Length - i, i); break; }
                        }
                    }
                    else { text.Text = ""; }
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

                else if (textButton == "OFF") { Application.Current.Shutdown(); }

                else if (textButton == "BLK") 
                { 
                    if (blk == true) { blk = false;  block(); }
                    else { blk = true; block(); }
                }

                else text.Text += textButton;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        void block()
        {
            foreach (Button button in allButtons) { if (blk == true) { button.IsEnabled = false; blk = true;} else { button.IsEnabled = true; blk = false; check(one); check(two); } }
        }


        void check(Button M)
        {
            if (m == 0) { M.IsEnabled = false; }
            else { M.IsEnabled = true; }
        }
    }
}

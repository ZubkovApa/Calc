﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                text.Text = "\n";
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

                if (textButton == "C") { text.Clear(); text.Text += "\n"; }

                else if (textButton == "Mc") { m = 0; check(one); check(two); }

                else if (textButton == "Mr") { text.Text = "\n" + m.ToString(); check(one); check(two); }

                else if (textButton == "Ms") { m = Double.Parse(text.Text = "\n" + new DataTable().Compute(text.Text, null).ToString().Replace(',', '.')); check(one); check(two); }

                else if (textButton == "M+") { text.Text = "\n" + (double.Parse(text.Text) + m).ToString(); }

                else if (textButton == "M-") { text.Text = "\n" + (double.Parse(text.Text) - m).ToString(); }

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
                    else { text.Text = "\n"; }
                }

                else if (textButton == "x²") { text.Text = "\n" + Math.Pow(Double.Parse((text.Text).Replace('.', ',')), 2).ToString().Replace(',', '.'); }

                else if (textButton == "√") {
                    if (Double.Parse((text.Text).Replace('.', ',')) < 0) {
                        text.Text = "\nLess Than Zero\n(c)The Weekend";
                    }
                    else { text.Text = "\n" + Math.Pow(Double.Parse((text.Text).Replace('.', ',')), 0.5).ToString().Replace(',', '.'); }
                }

                else if (textButton == "1/x") { text.Text = "\n" + Math.Pow(Double.Parse((text.Text).Replace('.', ',')), -1).ToString().Replace(',', '.'); }

                else if (textButton == "=") { text.Text = "\n" + new DataTable().Compute(text.Text, null).ToString().Replace(',','.'); }

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

                else if (textButton == "%") {
                    text.Text = text.Text.Replace("%","").Replace("\n","").Insert(0,"@");
                    char[] a = Array.Empty<char>(); char[] b= Array.Empty<char>();
                    int c = 0; int c1 = 0; int c2 = 0;
                    for (int i = 1; i < text.Text.Length; i++)
                    {
                        if (Char.IsNumber(text.Text[text.Text.Length - i])) { Array.Resize(ref a, c1 + 1); a[c1]= text.Text[text.Text.Length - i]; c1++; }
                        else { c = i; break; }
                    }
                    for (int i = c + 1; i < text.Text.Length + 3; i++)
                    {
                        if (Char.IsNumber(text.Text[text.Text.Length - i])) { Array.Resize(ref b, c2 + 1); b[c2] = text.Text[text.Text.Length - i]; c2++; }
                        else { break; }
                    }
                    string str1 =""; string str2 = "";
                    Array.Reverse(a); Array.Reverse(b);
                    for (int i = 0; i < a.Length; i++) { str1 += a[i].ToString(); }
                    for (int i = 0; i < b.Length; i++) { str2 += b[i].ToString(); }
                    double f1 = (Double.Parse(str1)); double f2 = (Double.Parse(str2));
                    text.Text = "\n" + text.Text.Remove(text.Text.Length - c + 1, c - 1).Replace("@","");
                    text.Text = text.Text.Insert(text.Text.Length - c + 3, (f1 / 100 * f2).ToString());
                }

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

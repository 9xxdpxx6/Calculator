using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        string leftOp = "";
        string operation = "";
        string rightOp = "";

        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += ButtonClick;
                }
            }
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            string s = (string)((Button)e.OriginalSource).Content;
            if (s != "=")
            {
                textBlock.Text += s;
            }
            bool result = double.TryParse(s, out double num);
            if (result == true)
            {
                if (rightOp == "")
                {
                    if (operation == "")
                    {
                        leftOp += s;
                    }
                    else
                    {
                        rightOp += s;
                    }
                }
                else
                {
                    if (operation == "")
                    {
                        rightOp = "";
                        leftOp = s;
                        textBlock.Text = leftOp;
                    }
                    else
                    {
                        rightOp += s;
                    }
                }
            }
            else
            {
                if (s == "=" && rightOp == "")
                {
                    rightOp = leftOp;
                    textBlock.Text = rightOp;
                    operation = "";
                }
                else if (s == "=" && leftOp == "")
                {
                    if (operation == "-")
                    {
                        leftOp = "-" + rightOp;
                        rightOp = leftOp;
                        textBlock.Text = rightOp;
                        operation = "";
                    }
                    else
                    {
                        leftOp = rightOp;
                        textBlock.Text = rightOp;
                        operation = "";
                    }
                }
                else if (s == "=")
                {
                    UpdateRightOp();
                    textBlock.Text = rightOp;
                    operation = "";
                }
                else if (s == "СБРОС")
                {
                    rightOp = "";
                    leftOp = "";
                    rightOp = "";
                    operation = "";
                    textBlock.Text = "";
                    addition.IsEnabled = true;
                    subtraction.IsEnabled = true;
                    multiplication.IsEnabled = true;
                    division.IsEnabled = true;
                    equals.IsEnabled = true;
                }
                else if (rightOp == "ошибка! Деление на 0")
                {
                    addition.IsEnabled = false;
                    subtraction.IsEnabled = false;
                    multiplication.IsEnabled = false;
                    division.IsEnabled = false;
                    equals.IsEnabled = false;
                }
                else
                {
                    char[] last_symbols = textBlock.Text.ToCharArray();
                    if (last_symbols.Length > 1 && !int.TryParse(last_symbols[last_symbols.Length-1]+"", out _) && !int.TryParse(last_symbols[last_symbols.Length - 2] + "", out _))
                    {
                        operation = s;
                        textBlock.Text = textBlock.Text.Substring(0, textBlock.Text.Length - 2) + operation;
                        return;
                    }
                    if (rightOp != "" && rightOp != "ошибка! Деление на 0")
                    {
                        if (operation == "-")
                        {
                            leftOp = "-" + rightOp;
                            rightOp = leftOp;
                            operation = "";
                        }
                        else
                        {
                            leftOp = rightOp;
                            operation = "";
                        }
                        UpdateRightOp();
                        leftOp = rightOp;
                        rightOp = "";
                    }
                    operation = s;
                }
            }
        }
        private void UpdateRightOp()
        {
            double firstOp = double.Parse(leftOp);
            double lastOp = double.Parse(rightOp);
            switch (operation)
            {
                case "+":
                    rightOp = (firstOp + lastOp).ToString();
                    break;
                case "-":
                    rightOp = (firstOp - lastOp).ToString();
                    break;
                case "*":
                    rightOp = (firstOp * lastOp).ToString();
                    break;
                case "/":
                    if(lastOp != 0)
                    {
                        rightOp = Math.Round(firstOp / lastOp, 4).ToString();
                        break;
                    }
                    else
                    {
                        rightOp = "ошибка! Деление на 0";
                        break;
                    }
            }
        }
    }
}

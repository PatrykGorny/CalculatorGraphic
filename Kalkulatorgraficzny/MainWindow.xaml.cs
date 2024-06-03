using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
//@author  Patryk Górny
/// @version 1.0, 03.06.2024 
namespace Kalkulatorgraficzny
{
    public partial class MainWindow : MetroWindow
    {
        private string _currentOperation;
        private double _firstNumber;
        private bool _isOperationPressed;
        private bool _isCleared;

        public MainWindow()
        {
            InitializeComponent();
            DisplayTextBox.Text = "0";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            string buttonText = button.Content.ToString();
            if (double.TryParse(buttonText, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
            {
                if (_isOperationPressed || _isCleared)
                {
                    DisplayTextBox.Text = "";
                    _isOperationPressed = false;
                    _isCleared = false;
                }

                if (DisplayTextBox.Text == "0")
                {
                    DisplayTextBox.Text = "";
                }

                DisplayTextBox.Text += buttonText;
            }
            else if (buttonText == ".")
            {
                if (!_isCleared && !DisplayTextBox.Text.Contains("."))
                {
                    DisplayTextBox.Text += ".";
                }
            }
            else
            {
                switch (buttonText)
                {
                    case "C":
                        DisplayTextBox.Text = "0";
                        _firstNumber = 0;
                        _currentOperation = string.Empty;
                        _isCleared = true;
                        break;

                    case "=":
                        if (double.TryParse(DisplayTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double secondNumber))
                        {
                            switch (_currentOperation)
                            {
                                case "+":
                                    DisplayTextBox.Text = (_firstNumber + secondNumber).ToString(CultureInfo.InvariantCulture);
                                    break;
                                case "-":
                                    DisplayTextBox.Text = (_firstNumber - secondNumber).ToString(CultureInfo.InvariantCulture);
                                    break;
                                case "*":
                                    DisplayTextBox.Text = (_firstNumber * secondNumber).ToString(CultureInfo.InvariantCulture);
                                    break;
                                case "/":
                                    if (secondNumber != 0)
                                    {
                                        DisplayTextBox.Text = (_firstNumber / secondNumber).ToString(CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        DisplayTextBox.Text = "Error";
                                    }
                                    break;
                            }
                        }
                        _isOperationPressed = false;
                        _isCleared = true;
                        break;

                    default:
                        _isOperationPressed = true;
                        if (double.TryParse(DisplayTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedNumber))
                        {
                            _firstNumber = parsedNumber;
                        }
                        _currentOperation = buttonText;
                        break;
                }
            }
        }
    }
}

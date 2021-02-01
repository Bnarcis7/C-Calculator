using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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


namespace NewCalculatorApplication
{
   
    public partial class MainWindow : Window
    {
        decimal number1 = 0;
        decimal number2 = 0;
        string defaultValue = "0";
        string operation = string.Empty;
        private double memory;
        private bool memoryFlag;
        bool isDigitGroupingOn = false;
        bool isDotOn = false;



        public MainWindow()
        {
            InitializeComponent();
            btnMC.IsEnabled = false;
            btnMR.IsEnabled = false;
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("Main.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
        }

        //Minimize to System tray
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnValues_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var buttonContentValue = Convert.ToInt32(button.Content);
            changeValue(buttonContentValue);
          
           
        }

        private void changeValue(int newValue)
        {
            if ((operation == "" || memoryFlag == true) && isDotOn == false)
            {
                number1 = (number1 * 10) + newValue;
                changeTextValue(number1.ToString());
            }
            else if (isDotOn == true)
            {
                if (operation == string.Empty && !number1.ToString().Contains('.'))
                {
                    number1 = Convert.ToDecimal(number1.ToString() + "." + newValue.ToString());
                    changeTextValue(number1.ToString());
                }
                else if (operation == string.Empty && number1.ToString().Contains('.'))
                {
                    number1 = Convert.ToDecimal(number1.ToString() + newValue.ToString());
                    changeTextValue(number1.ToString());
                }
                else if (operation != string.Empty && !number2.ToString().Contains('.'))
                {
                    number2 = Convert.ToDecimal(number2.ToString() + "." + newValue.ToString());
                    changeTextValue(number2.ToString());

                }
                else if (operation != string.Empty && number2.ToString().Contains('.'))
                {
                    number2 = Convert.ToDecimal(number2.ToString() + newValue.ToString());
                    changeTextValue(number2.ToString());
                }



            }
            else
            {
                number2 = (number2 * 10) + newValue;
                changeTextValue(number2.ToString());

            }


        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            addOperation();

        }

        private void addOperation()
        {
            operation = "+";

            if (isDotOn == true)
            {
                isDotOn = false;
            }

        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            equalsOperation();
        }

        private void equalsOperation()
        {
            switch (operation)
            {
                case "+":
                    changeTextValue((number1 + number2).ToString());
                    number1 = number1 + number2;
                    number2 = 0;
                    operation = string.Empty;
                    break;

                case "-":
                    changeTextValue((number1 - number2).ToString());
                    number1 = number1 - number2;
                    number2 = 0;
                    operation = string.Empty;
                    break;

                case "*":
                    changeTextValue((number1 * number2).ToString());
                    number1 = number1 * number2;
                    number2 = 0;
                    operation = string.Empty;
                    break;

                case "/":
                    changeTextValue((number1 / number2).ToString());
                    number1 = number1 / number2;
                    number2 = 0;
                    operation = string.Empty;
                    break;

                case "%":

                    break;

                default:
                    break;
            }

        }



        private void btnDot_Click(object sender, RoutedEventArgs e)
        {
            dotOperation();
        }

        private void dotOperation()
        {

            isDotOn = true;

            char decimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            if (!getTextValue().Contains(decimalSeparator))
            {
                changeTextValue(getTextValue() + decimalSeparator);

            }

        }

        private void btnPosNeg_Click(object sender, RoutedEventArgs e)
        {

            double v = Double.Parse(getTextValue());
            changeTextValue((-v).ToString());
            number1=-number1;

        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            minusOperation();
        }

        private void minusOperation()
        {

            operation = "-";
            if (isDotOn == true)
            {
                isDotOn = false;
            }

        }

        private void btnMult_Click(object sender, RoutedEventArgs e)
        {
            multiplyOperation();            
        }

        private void multiplyOperation()
        {

            operation = "*";
            if (isDotOn == true)
            {
                isDotOn = false;
            }

        }

        private void btnDiv_Click(object sender, RoutedEventArgs e)
        {
            divisionOperation();

        }

        private void divisionOperation()
        {

            operation = "/";

            if (isDotOn == true)
            {
                isDotOn = false;
            }

        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
            operation = "sqrt";
            changeTextValue(((decimal)Math.Sqrt((double)number1)).ToString());
            number1 = (decimal)(Math.Sqrt((double)number1));
            number2 = 0;

           


        }

        private void btnPowTwo_Click(object sender, RoutedEventArgs e)
        {
            operation = "x^2";
            changeTextValue((number1 * number1).ToString());
            number1 = number1 * number1;
            number2 = 0;

           

        }

        private void btnDivX_Click(object sender, RoutedEventArgs e)
        {
            operation = "1/x";
            changeTextValue(((decimal)(1 / number1)).ToString());
            number1 = (decimal)(1 / number1);
            number2 = 0;
           

        }

        private void btnBackSpace_Click(object sender, RoutedEventArgs e)
        {

            backspaceOperation();

        }

        private void backspaceOperation()
        {

            changeTextValue(getTextValue().Remove(getTextValue().Length - 1));
            if (getTextValue().Length == 0)
                changeTextValue("0");
            number1 = number2;
            number2 = 0;

        }



        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            number1 = 0;
            number2 = 0;
            operation = "";
            changeTextValue(defaultValue);

            if (isDotOn == true)
            {
                isDotOn = false;
            }

        }

        private void btnCE_Click(object sender, RoutedEventArgs e)
        {
            ceOperation();
        }

        private void ceOperation()
        {

            if (operation == "")
            {
                number1 = 0;

            }
            else
            {
                number2 = 0;
            }
            changeTextValue(defaultValue);

            if (isDotOn == true)
            {
                isDotOn = false;
            }

        }



        private void btnPercentage_Click(object sender, RoutedEventArgs e)
        {
            percentageOperation();

        }

        private void percentageOperation()
        {

            if (number1 != 0 && number2 == 0 && operation == string.Empty)
            {
                changeTextValue("0");
                number1 = 0;

            }
           
            if (number1 != 0 && number2 == 0 && operation != string.Empty)
            {
                changeTextValue("0");
                number1 = 0;

            }
            if (number1 != 0 && number2 != 0 && operation != string.Empty)
            {
                number2 = number1 * (number2 / 100);
                changeTextValue(number2.ToString());
            }

            if (isDotOn == true)
            {
                isDotOn = false;
            }

        }

        private void btnMC_Click(object sender, RoutedEventArgs e)
        {
            changeTextValue("0");
            memory = 0;
            btnMC.IsEnabled = false;
            btnMR.IsEnabled = false;
            number1 = 0;



        }

        //Memory Read
        private void btnMR_Click(object sender, RoutedEventArgs e)
        {
            changeTextValue(memory.ToString());
            memoryFlag = true;
        }

        //Memory Plus
        private void btnMPlus_Click(object sender, RoutedEventArgs e)
        {
            memory += Double.Parse(getTextValue());

        }

        //Memory Minus
        private void btnMMinus_Click(object sender, RoutedEventArgs e)
        {
            memory -= Double.Parse(getTextValue());
        }

        //Memory Save
        private void btnMs_Click(object sender, RoutedEventArgs e)
        {
            memory = Double.Parse(getTextValue());
            btnMC.IsEnabled = true;
            btnMR.IsEnabled = true;
            memoryFlag = true;

        }

        private void bntAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow objAboutWindow = new AboutWindow();
            this.Visibility = Visibility.Hidden;
            objAboutWindow.Show();


        }

        private void btnCut_Click(object sender, RoutedEventArgs e)
        {
            cutOperation();

        }

        private void cutOperation()
        {

            Clipboard.SetText(getTextValue());

            changeTextValue("0");

        }



        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            copyOperation();

        }

        private void copyOperation()
        {

            Clipboard.SetText(getTextValue());

        }


        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            pasteOperation();   
        }

        private void pasteOperation()
        {

            string pasteContent = Clipboard.GetText();
            bool allDigits = pasteContent.All(char.IsDigit);
            if (allDigits == true)
            {
                changeTextValue(string.Empty);
                number1 = 0;
                changeTextValue(getTextValue().Insert(DisplayTextBox.SelectionStart, pasteContent));
                number1 = Convert.ToDecimal(pasteContent);
            }

        }

        private void btnDigitGrouping_Click(object sender, RoutedEventArgs e)
        {
           
            if(isDigitGroupingOn==true)
            {
                DisplayTextBox.Text = getTextValue();
                isDigitGroupingOn = false;

            }
            else
            {
                isDigitGroupingOn = true;
                applyDigitGrouping();
            }
            

        }

        private void applyDigitGrouping()
        {
            decimal v = decimal.Parse(DisplayTextBox.Text);
            int pos = 0;
            char groupSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator);
            if (DisplayTextBox.Text.Contains(groupSeparator))
            {
                pos = DisplayTextBox.Text.Length - DisplayTextBox.Text.IndexOf(groupSeparator);
                if (pos == 1)
                    return;
                string formatStr = "{0:N" + (pos - 1) + "}";
                DisplayTextBox.Text = string.Format(formatStr, v);
            }
            else
            {
                DisplayTextBox.Text = AddGroupSeparators(v.ToString());
            }
        }

        private void changeTextValue(string newValue)
        {
            DisplayTextBox.Text = newValue;
            if (isDigitGroupingOn == true)
            {
                applyDigitGrouping();
            }

        }
        private string getTextValue()
        {
            string groupSeparator = Convert.ToString(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator);

            if (isDigitGroupingOn == true)
            {
                return DisplayTextBox.Text.Replace(groupSeparator, string.Empty);
            }
            else
            {
                return DisplayTextBox.Text;
            }

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.NumPad0)
            {
                changeValue(0);
                e.Handled = true;
            }

            if (e.Key == Key.NumPad1)
            {
                changeValue(1);
                e.Handled = true;
            }

            if (e.Key == Key.NumPad2)
            {
                changeValue(2);
                e.Handled = true;
            }

            if (e.Key == Key.NumPad3)
            {
                changeValue(3);
                e.Handled = true;
            }

            if (e.Key == Key.NumPad4)
            {
                changeValue(4);
                e.Handled = true;
            }

            if (e.Key == Key.NumPad5)
            {
                changeValue(5);
                e.Handled = true;
            }

            if (e.Key == Key.NumPad6)
            {
                changeValue(6);
                e.Handled = true;
            }

            if (e.Key == Key.NumPad7)
            {
                changeValue(7);
                e.Handled = true;
            }

            if (e.Key == Key.NumPad8)
            {
                changeValue(8);
                e.Handled = true;
            }

            if (e.Key == Key.NumPad9)
            {
                changeValue(9);
                e.Handled = true;
            }

            if (e.Key == Key.Decimal)
            {
                dotOperation();
                e.Handled = true;
            }

            if (e.Key == Key.Enter)
            {
                equalsOperation();
                e.Handled = true;
            }

            if (e.Key == Key.OemPlus)
            {
                equalsOperation();
                e.Handled = true;
            }

            if (e.Key == Key.Add)
            {
                addOperation();
                e.Handled = true;
            }

            if (e.Key == Key.OemMinus)
            {
                minusOperation();
                e.Handled = true;
            }

            if (e.Key == Key.Subtract)
            {
                minusOperation();
                e.Handled = true;
            }


            if (e.Key == Key.Multiply)
            {
                multiplyOperation();
                e.Handled = true;
            }

            if (e.Key == Key.Divide)
            {
                divisionOperation();
                e.Handled = true;
            }

            if (e.Key == Key.Back)
            {
                backspaceOperation();
                e.Handled = true;
            }


            if (e.Key == Key.Delete)
            {
                ceOperation();
                e.Handled = true;
            }

            if (e.Key == Key.OemPlus && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                addOperation();
                e.Handled = true;
            }

            if (e.Key == Key.D8 && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                multiplyOperation();
                e.Handled = true;
            }

            if (e.Key == Key.D5 && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                percentageOperation();
                e.Handled = true;
            }


            if (e.Key == Key.OemMinus && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                minusOperation();
                e.Handled = true;
            }


            if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                copyOperation();
                e.Handled = true;
            }
            if (e.Key == Key.X && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                cutOperation();
                e.Handled = true;
            }

            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                pasteOperation();
                e.Handled = true;
            }

        }
        public static string AddGroupSeparators(string number)
        {
            int[] sizes = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSizes;
            int pos = number.LastIndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            if (pos == -1) pos = number.Length;
            int sizeIndex = 0;
            while (sizes[sizeIndex] > 0 && pos > sizes[sizeIndex])
            {
                pos -= sizes[sizeIndex];
                number = number.Insert(pos, CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator);
                if (sizeIndex < sizes.Length - 1) sizeIndex++;
            }
            return number;
        }

    }

}

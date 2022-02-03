using BaseLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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


namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculate calculate = new Calculate();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = calculate;
        }

        private void button_Number_Click(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Trace.WriteLine("sneed");
            Button btn = (Button)sender;
            #pragma warning disable CS8604 // Possible null reference argument.
            int value = int.Parse(btn.Content.ToString());
            #pragma warning restore CS8604 // Possible null reference argument.

            calculate.setNumber(value);
        }

        private void button_Clear_Click(object sender, RoutedEventArgs e)
        {
            calculate.clearNumbers();
            calculate.result = 0;
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            calculate.add = true;
        }

        private void button_Equal_Click(object sender, RoutedEventArgs e)
        {
            calculate.calculateNumbers();
        }

        private void button_Subtract_Click(object sender, RoutedEventArgs e)
        {
            calculate.subtract = true;
        }
    }

    public class Calculate : baseViewModel
    {

        private int _numOne = 0;
        private int _numTwo = 0;
        private int _result = 0;

        public bool add = false;
        public bool subtract = false;

        // Methods
        public int numOne
        {
            get { return _numOne; }
            set {
                if (_numOne != value)
                {
                    _numOne = value;
                    OnPropertyChanged(nameof(numOne));
                }
            }
        }

        public int numTwo
        {
            get { return _numTwo; }
            set
            {
                if (_numTwo != value)
                {
                    _numTwo = value;
                    OnPropertyChanged(nameof(numTwo));
                }
            }
        }

        public int result
        {
            get { return _result; }
            set
            {
                if (_result != value)
                {
                    _result = value;
                    OnPropertyChanged(nameof(result));
                }
            }
        }

        public void AddNumbers()
        {
            if (numTwo == 0) result = numOne;
            else result = numOne + numTwo;

            clearNumbers();
            clearBools();
        }

        public void SubtractNumbers()
        {
            if (numTwo == 0) result = numOne;
            else result = numOne - numTwo;

            clearNumbers();
            clearBools();
        }

        public void calculateNumbers()
        {
            if(add)
            {
                AddNumbers();
            }
            else if (subtract)
            {
                SubtractNumbers();
            }
        }

        public void clearNumbers()
        {
            numOne = 0;
            numTwo = 0;
        }

        public void clearBools()
        {
            add = false;
            subtract = false;
        }

        public void setNumber(int number)
        {
            if (numOne == 0) numOne = number;
            else numTwo = number;
        }

        // END Methods

    }
}

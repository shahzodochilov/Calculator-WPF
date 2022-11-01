using System.Windows;
using System.Windows.Controls;

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool helper = false, helperRes = false, isoperator = false;
        public MainWindow()
        {
            InitializeComponent();
            this.MinWidth = this.Width;
            this.MinHeight = this.Height;
        }

        private void NumericButton_Click(object sender, RoutedEventArgs e)
        {
            isoperator = false;
            Button button = (Button)sender;

            if (ResultTbUI.Text == "0")
                ResultTbUI.Text = "";

            if (helper)
            {
                ResultTbUI.Text = "";
                helper = false;
            }

            if (helperRes)
            {
                BeforeResultTbUI.Text = "";
                helperRes = false;
            }

            if (button.Content.ToString() == ".")
            {
                if (ResultTbUI.Text.Contains('.'))
                    return;
                if (ResultTbUI.Text == "")
                    ResultTbUI.Text = "0";
            }
            ResultTbUI.Text += button.Content;

        }

        public void Add()
        {
            string str = BeforeResultTbUI.Text + ResultTbUI.Text + " " + "=";
            ResultTbUI.Text = (ExpressionEvaluator.ExpressionEvaluator.Evaluate(BeforeResultTbUI.Text + ResultTbUI.Text)).ToString();
            BeforeResultTbUI.Text = str;
            helper = true;
            helperRes = true;
            isoperator = false;
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            isoperator = false;
            Button button = (Button)sender;

            switch (button.Content.ToString())
            {
                case "C":
                    BeforeResultTbUI.Text = "";
                    ResultTbUI.Text = "0";
                    break;

                case "<-":
                    if (ResultTbUI.Text.Length > 1)
                        ResultTbUI.Text = ResultTbUI.Text[..^1];
                    else if (ResultTbUI.Text.Length == 1) ResultTbUI.Text = "0";
                    break;

                case "+/-":
                    if (ResultTbUI.Text != "0")
                        ResultTbUI.Text = ResultTbUI.Text[0] == '-' ? ResultTbUI.Text[1..] : "-" + ResultTbUI.Text;
                    break;

                case "=":
                    Add();                    
                    break;
                default:
                    break;
            }
        }

        private void MathematicalOperationsButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (isoperator)
            {
                BeforeResultTbUI.Text = BeforeResultTbUI.Text[..^1] + button.Content.ToString();
            }

            else
            { 
                 if(!helperRes)  Add();
                BeforeResultTbUI.Text = ResultTbUI.Text + button.Content.ToString();
                helper = true;
                helperRes = false;
                isoperator = true;      
            }
        }
    }
}

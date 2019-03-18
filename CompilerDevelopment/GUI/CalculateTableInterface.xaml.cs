using CompilerDevelopment.Graphics;
using CompilerDevelopment.Upstream_analysis.SyntaxAnalyzerForUpstreamAnalysis;
using System;
using System.Collections.Generic;
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

namespace CompilerDevelopment.GUI
{
    /// <summary>
    /// Логика взаимодействия для CalculateTableInterface.xaml
    /// </summary>
    public partial class CalculateTableInterface : UserControl
    {
        public CalculateTableInterface()
        {
            InitializeComponent();
            header.Text = "Calculate Table";
            PrintTableCalculate();
        }

        private void PrintTableCalculate()
        {
            var result = TableOfCalculationExpression.tableOfCalculationExpression.Select(r =>
            new
            {
                Step = r.Step,
                TokenStack = MakeStack(r.Stack),
                Polis = r.Polis
            }
            );

            dataGrid.ItemsSource = result;
        }

        static string MakeStack(string stack)
        {
            string[] words = stack.Split(new char[] { ' ' });
            StringBuilder sb = new StringBuilder(stack.Length);
            sb.Append("| ");
            for (int i = words.Count(); i-- != 0;)
            {
                sb.Append(words[i] + " | ");
            }
            return sb.ToString();
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TableOfAnalyzerInterface());
        }
    }
}

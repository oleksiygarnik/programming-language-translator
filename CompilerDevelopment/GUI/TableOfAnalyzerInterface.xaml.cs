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
    /// Логика взаимодействия для TableOfAnalyzerInterface.xaml
    /// </summary>
    public partial class TableOfAnalyzerInterface : UserControl
    {
        public TableOfAnalyzerInterface()
        {
            InitializeComponent();
            PrintTableCalculate();
        }
        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }

        private void Print()
        {
            var result = MPA.TableOfAnalyzer.tableOfAnalysis.Select(
                (n, i) =>
                new
                {
                    i = i,
                    InputToken = n.Value.InputToken.ToString(),
                    CurrentState = n.Value.CurrentState,
                    Stack = n.Value.FullStack,

                }
            );

            dataGrid.ItemsSource = result;
        }

        static string ReverseStringBuilder(string str)
        {
            string[] words = str.Split(new char[] { ' ' });
            StringBuilder sb = new StringBuilder(str.Length);
            for (int i = words.Count(); i-- != 0;)
                sb.Append(words[i].ToString() + "   ");
            return sb.ToString();
        }

        static string BuildQueue(List<MiniToken> tokenQueue)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tokenQueue.Count(); i++)
            {
                sb.Append(tokenQueue[i].Name + " ");
            }
            return sb.ToString();
        }

        private void PrintTable()
        {
            var result = TableOfUpstreamParsing.tableOfUpstreamParsing.Select(r =>
            new
            {
                Step = r.Step,
                TokenStack = ReverseStringBuilder(r.TokenStack),
                Sign = r.Sign,
                TokenQueue = BuildQueue(r.TokenQueue), 
                Polis = r.Polis
            }
            );

            dataGrid.ItemsSource = result;
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
    }
}

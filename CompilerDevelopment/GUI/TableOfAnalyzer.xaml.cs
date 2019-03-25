using CompilerDevelopment.DijkstraAlgorithmLab7;
using CompilerDevelopment.Graphics;
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
    /// Логика взаимодействия для TableOfAnalyzer.xaml
    /// </summary>
    public partial class TableOfAnalyzer : UserControl
    {
        public TableOfAnalyzer()
        {
            InitializeComponent();
            Print();
            //PrintTableOfLabel();
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

        private void PrintTableOfLabel()
        {
            var result = TableOfLabels.tableOfLabels.Select(
                (n, i) => new
                {
                    i = i,
                    LabelName = n.Value.Item1,
                    Value = n.Value.Item2
                }
                );

            dataGrid.ItemsSource = result;
        }
    }
}

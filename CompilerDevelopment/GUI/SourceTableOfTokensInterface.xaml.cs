using CompilerDevelopment.Entities;
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
    /// Логика взаимодействия для SourceTableOfTokensInterface.xaml
    /// </summary>
    public partial class SourceTableOfTokensInterface : UserControl
    {
        List<(int, (string, float))> list = new List<(int, (string, float))>();

        public SourceTableOfTokensInterface()
        {
            InitializeComponent();
            //Print();
            list123.ItemsSource = SourceTableOfTokens.SourceListOfTokens;
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }

        //private void PrintLab9()
        //{
        //    var result1 = list.Select(
        //        (n, i) =>
        //        new
        //        {
        //            Method = n.Item2.Item1,
        //            Percent = n.Item1.ToString(), 
        //            AverageTryCount = n.Item2.Item2
        //        }
        //        );

        //    result.ItemsSource = result1;
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    list.Clear();

        //    Lab9.TableOfIdentifiersLab9.TryCount.Clear();
        //    Lab9.TableOfIdentifiersLab9.tableOfIdentifiers.Clear();

        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 100, sizeOfTable: 1000, method: "Random", condition: "add");
        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 100, sizeOfTable: 1000, method: "Random");
        //    float numRandom10 = Lab9.TableOfIdentifiersLab9.CalculateAverageValue();
        //    list.Add((10, ("Random", numRandom10)));

        //    Lab9.TableOfIdentifiersLab9.TryCount.Clear();
        //    Lab9.TableOfIdentifiersLab9.tableOfIdentifiers.Clear();

        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 500, sizeOfTable: 1000, method: "Random", condition: "add");
        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 100, sizeOfTable: 1000, method: "Random");
        //    float numRandom50 = Lab9.TableOfIdentifiersLab9.CalculateAverageValue();
        //    list.Add((50, ("Random", numRandom50)));


        //    Lab9.TableOfIdentifiersLab9.TryCount.Clear();
        //    Lab9.TableOfIdentifiersLab9.tableOfIdentifiers.Clear();

        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 900, sizeOfTable: 1000, method: "Random", condition: "add");
        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 100, sizeOfTable: 1000, method: "Random");
        //    float numRandom90 = Lab9.TableOfIdentifiersLab9.CalculateAverageValue();
        //    list.Add((90, ("Random", numRandom90)));



        //    Lab9.TableOfIdentifiersLab9.TryCount.Clear();
        //    Lab9.TableOfIdentifiersLab9.tableOfIdentifiers.Clear();

        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 100, sizeOfTable: 1000, method: "Square", condition: "add");
        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 100, sizeOfTable: 1000, method: "Square");
        //    float numSquare10 = Lab9.TableOfIdentifiersLab9.CalculateAverageValue();
        //    list.Add((10, ("Square", numSquare10)));


        //    Lab9.TableOfIdentifiersLab9.TryCount.Clear();
        //    Lab9.TableOfIdentifiersLab9.tableOfIdentifiers.Clear();

        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 500, sizeOfTable: 1000, method: "Square", condition: "add");
        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 100, sizeOfTable: 1000, method: "Square");
        //    float numSquare50 = Lab9.TableOfIdentifiersLab9.CalculateAverageValue();
        //    list.Add((50, ("Square", numSquare50)));


        //    Lab9.TableOfIdentifiersLab9.TryCount.Clear();
        //    Lab9.TableOfIdentifiersLab9.tableOfIdentifiers.Clear();

        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 900, sizeOfTable: 1000, method: "Square", condition: "add");
        //    Lab9.TableOfIdentifiersLab9.RandomLoadTable(countOfElement: 100, sizeOfTable: 1000, method: "Square");
        //    float numSquare90 = Lab9.TableOfIdentifiersLab9.CalculateAverageValue();
        //    list.Add((90, ("Square", numSquare90)));


        //    PrintLab9();
        //}

        //private void Print()
        //{
        //    var result = MPA.TableOfAnalyzer.tableOfAnalysis.Select(
        //        (n, i) =>
        //        new
        //        {
        //            i = i,
        //            InputToken = n.Value.InputToken.ToString(),
        //            CurrentState = n.Value.CurrentState,
        //            Stack = n.Value.FullStack,

        //        }
        //    );

        //    dataGrid.ItemsSource = result;
        //}
    }
}

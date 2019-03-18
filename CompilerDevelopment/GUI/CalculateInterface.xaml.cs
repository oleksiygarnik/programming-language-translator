using CompilerDevelopment.Entities;
using CompilerDevelopment.Graphics;
using CompilerDevelopment.Upstream_analysis.SyntaxAnalyzerForUpstreamAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Логика взаимодействия для CalculateInterface.xaml
    /// </summary>
    public partial class CalculateInterface : UserControl
    {
        public CalculateInterface()
        {
            InitializeComponent();
            header.Text = "Таблица для заполнения идентификаторов";
            expression.Text = Storage.Expression;



            for (int i = 0; i < TableOfIdentifiers.IdentifierListOfTokens.Count; i++)
            {
             
                //Label lab = new Label();
                //lab.Content = TableOfIdentifiers.IdentifierListOfTokens[i].View;
                //TextBox tb = new TextBox();
                //tb.Name = TableOfIdentifiers.IdentifierListOfTokens[i].View;
                //stackPanel.Children.Add(lab);
                //stackPanel.Children.Add(tb);
               
            }
            UpdateMatrix(2);
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TableOfAnalyzerInterface());
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            DataView dv = (DataView)lol.ItemsSource;
            dt = dv.ToTable();
            Storage.valueForIdentifiers = new Dictionary<string, string>();
            for(int i = 0; i < TableOfIdentifiers.IdentifierListOfTokens.Count; i++)
            {
                var value = dt.Rows[0][i];
                Storage.valueForIdentifiers.Add(TableOfIdentifiers.IdentifierListOfTokens[i].View, value.ToString());
            }
            result.Text = TableOfCalculationExpression.CalculationOfExpression();
            // var value = dt.Rows[0][0];
            //var value1 = dt.Rows[0][1];
        }

        void UpdateMatrix(int size)
        {
            size = TableOfIdentifiers.IdentifierListOfTokens.Count();

            string[] elem = TableOfIdentifiers.IdentifierListOfTokens.Select(n => n.View).ToArray();

            var dt = new DataTable();
            for (var i = 0; i < size; i++)
            {
                dt.Columns.Add(new DataColumn(elem[i], typeof(string)));
            }

                var r = dt.NewRow();
                dt.Rows.Add(r);
            lol.ItemsSource = dt.DefaultView;
            lol.Margin = new Thickness(50, 50, 50, 50);
         
        }
    }
}

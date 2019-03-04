using CompilerDevelopment.Graphics;
using CompilerDevelopment.Upstream_analysis;
using System;
using System.Collections;
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
    /// Логика взаимодействия для TableOfTransitions.xaml
    /// </summary>
    public partial class TableOfTransitions : UserControl
    {
        public TableOfTransitions()
        {
            InitializeComponent();
            
            //Print();
            MatrixSize = Enumerable.Range(1, 10).ToArray();
            //DataContext = this;
            this.UpdateMatrix(2);
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }

        private void Print()
        {
            var res = TableOfRelations.tableOfRelation.SelectMany(n => n.Value.Select(m => m.Value.ToString()));
            dataGrid.ItemsSource = res;

        }

        public IList MatrixSize { get; private set; }
        public object Matrix { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var size = (int)e.AddedItems[0];
            this.UpdateMatrix(size);
        }

        //void UpdateMatrix(int size)
        //{
        //    size = Lab4.TableOfRelation.tableOfRelation.Count();
        //    string[] elem = Lab4.TableOfRelation.tableOfRelation.Select(n => n.Key).ToArray();
        //    string[] sign = Lab4.TableOfRelation.tableOfRelation.SelectMany(n => n.Value).Select(m => m.Value).ToArray();
        //    var dt = new DataTable();
        //    //DataColumn dc = new DataColumn(" ", typeof(string));
        //    //dt.Columns.Add(dc);
        //    for (var i = 0; i < size; i++)
        //        dt.Columns.Add(new DataColumn(elem[i], typeof(string)));
        //    for (var i = 0; i < size; i++)
        //    {
        //        var r = dt.NewRow();
        //        for (var c = 0; c < size; c++)
        //            r[c] = "hello";
        //        dt.Rows.Add(r);
        //    }
        //    this.Matrix = dt.DefaultView;
        //    PropertyChanged(this, new PropertyChangedEventArgs("Matrix"));

        //}

        void UpdateMatrix(int size)
        {
            size = TableOfRelations.tableOfRelation.Count();

            string[] elem = TableOfRelations.tableOfRelation.Select(n => n.Key).ToArray();

            var dt = new DataTable();
            DataColumn dc = new DataColumn(elem[0]+ 1, typeof(string));
            dt.Columns.Add(dc);
            for (var i = 0; i < size; i++)
            {
                string tmp = elem[i];
                if (elem[i] == "[" || elem[i] == "]" || elem[i] == "(" || elem[i] == ")")
                {
                    elem[i] = "skobka" + i ;
                }
                dt.Columns.Add(new DataColumn(elem[i], typeof(string)));
                elem[i] = tmp;
            }

            string[] sign = TableOfRelations.tableOfRelation.SelectMany(n => n.Value).Select(m => m.Value).ToArray();
            // string[] arr = new string[] { };
            Dictionary<string, string> dict = new Dictionary<string, string>();
          
            for (var i = 0; i < size; i++)
            {
                var r = dt.NewRow();
                //string tmp = elem[i];
                //if(elem[i]=="[" || elem[i]=="]" || elem[i] == "(" || elem[i] == ")")
                //{
                //    elem[i] = "skobka";
                //}
                if (elem[i] == "type")
                {

                }

                TableOfRelations.tableOfRelation.TryGetValue(elem[i], out dict);
                sign = dict.Select(s=>s.Value).ToArray();
                r[0] = elem[i];

                for (var c = 0; c < size; c++)
                {
                    r[c+1] = sign[c];
                }
               
                dt.Rows.Add(r);
            }
            lol.ItemsSource = dt.DefaultView;
            PropertyChanged(this, new PropertyChangedEventArgs("Matrix"));
        }
        //private void Print()
        //{
        //    var result = MPA.TableOfTransitions.tableOfTransitions.SelectMany(
        //        n => n.Value.dictionary.Skip(1).Select(m =>
        //        new
        //        {
        //            StateIndex = string.Empty,
        //            SymbolClass = m.Key.ToString(),
        //            NextStateIndex = m.Value.destination.ToString(),
        //            Stack = m.Value.stack.ToString(),
        //            SubProgramm = string.Empty
        //        }).Prepend(new
        //        {
        //            StateIndex = n.Key.ToString(),
        //            SymbolClass = n.Value.dictionary.First().Key.ToString(),
        //            NextStateIndex = n.Value.dictionary.First().Value.destination.ToString(),
        //            Stack = n.Value.dictionary.First().Value.stack.ToString(),
        //            SubProgramm = string.Empty
        //        }).Append(new
        //        {
        //            StateIndex = string.Empty,
        //            SymbolClass = string.Empty,
        //            NextStateIndex = string.Empty,
        //            Stack = string.Empty,
        //            SubProgramm = n.Value.Info
        //        })
        //    );

        //    dataGrid.ItemsSource = result;
        //}
    }
}

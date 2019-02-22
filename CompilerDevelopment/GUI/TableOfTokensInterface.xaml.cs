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
    /// Логика взаимодействия для TableOfTokensInterface.xaml
    /// </summary>
    public partial class TableOfTokensInterface : UserControl
    {
      
        
        public TableOfTokensInterface()
        {
            InitializeComponent();
            dictionary.ItemsSource = TableOfTokens.Tokens;
            //Load.PushDownStates();

            //Print();

        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }


        //private void Print()
        //{
        //    var result = TableOfTransitions.tableOfTransitions.SelectMany(
        //        n => n.Value.dictionary.Skip(1).Select(m =>
        //        new
        //        {
        //            StateIndex = string.Empty,
        //            SymbolClass = m.Key.ToString(),
        //            NextStateIndex = m.Value.destination.ToString(),
        //            Stack = m.Value.stack.ToString(),
        //            IsFinal = string.Empty
        //        }).Prepend(new
        //        {
        //            StateIndex = n.Key.ToString(),
        //            SymbolClass = n.Value.dictionary.First().Key.ToString(),
        //            NextStateIndex = n.Value.dictionary.First().Value.destination.ToString(),
        //            Stack = n.Value.dictionary.First().Value.stack.ToString(),
        //            IsFinal = string.Empty
        //        }).Append(new
        //        {
        //            StateIndex = string.Empty,
        //            SymbolClass = string.Empty,
        //            NextStateIndex = string.Empty,
        //            Stack = string.Empty,
        //            IsFinal = n.Value.isFinal.ToString()
        //        })
        //    );

        //    dataGrid.ItemsSource = result;
        //}

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

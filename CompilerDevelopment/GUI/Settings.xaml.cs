using CompilerDevelopment.Graphics;
using CompilerDevelopment.Upstream_analysis;
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
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {

        public static string SettingMethod = "MPA";
        public Settings()
        {
            InitializeComponent();
            //TableOfRelations.LoadFields();
            //TableOfRelations.LoadEquels();

            //TableOfRelations.LoadSupportTable();
            //TableOfRelations.LoadLessSign();
            //TableOfRelations.LoadMoreSign();
            //TableOfRelations.TwoNonTernminal();
            Print();
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            button1.BorderBrush = new SolidColorBrush(Colors.OrangeRed);
            button2.BorderBrush = new SolidColorBrush(Colors.LightBlue);
            button3.BorderBrush = new SolidColorBrush(Colors.LightBlue);
            SettingMethod = "Recursion";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            button2.BorderBrush = new SolidColorBrush(Colors.OrangeRed);
            button1.BorderBrush = new SolidColorBrush(Colors.LightBlue);
            button3.BorderBrush = new SolidColorBrush(Colors.LightBlue);
            SettingMethod = "MPA";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            button3.BorderBrush = new SolidColorBrush(Colors.OrangeRed);
            button1.BorderBrush = new SolidColorBrush(Colors.LightBlue);
            button2.BorderBrush = new SolidColorBrush(Colors.LightBlue);
            SettingMethod = "UpstreamParsing"; //передувань
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            Switcher.Switch(new MainMenu());
        }

        private void Print()
        {
            var result = MPA.TableOfTransitions.tableOfTransitions.SelectMany(
                n => n.Value.dictionary.Skip(1).Select(m =>
                new
                {
                    StateIndex = string.Empty,
                    SymbolClass = m.Key.ToString(),
                    NextStateIndex = m.Value.destination.ToString(),
                    Stack = m.Value.stack.ToString(),
                    SubProgramm = string.Empty
                }).Prepend(new
                {
                    StateIndex = n.Key.ToString(),
                    SymbolClass = n.Value.dictionary.First().Key.ToString(),
                    NextStateIndex = n.Value.dictionary.First().Value.destination.ToString(),
                    Stack = n.Value.dictionary.First().Value.stack.ToString(),
                    SubProgramm = string.Empty
                }).Append(new
                {
                    StateIndex = string.Empty,
                    SymbolClass = string.Empty,
                    NextStateIndex = string.Empty,
                    Stack = string.Empty,
                    SubProgramm = n.Value.Info
                })
            );

            dataGrid.ItemsSource = result;
        }

        //private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        //{
        //    SettingMethod = checkBox1.Content.ToString();
        //}

        //private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        //{
        //    SettingMethod = checkBox2.Content.ToString();
        //}

        //private void CheckBox1_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    SettingMethod = "";
        //}

        //private void CheckBox2_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    SettingMethod = "";
        //}
    }
}

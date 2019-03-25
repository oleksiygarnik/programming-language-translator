using CompilerDevelopment.Graphics;
using CompilerDevelopment.Upstream_analysis;
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
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
            MPA.TableOfTransitions.Loading();


            //Start Test 7 lab

          





            ///////////////////////////////////////End Lab7
            //Grammar.Loading();

            Grammar.LoadingFor6Lab();

           
            foreach (var item in TableOfRelations.tableOfRelation)
            {

            }
            
        }


        private void TryParse_Click(object sender, RoutedEventArgs e)
        {
            UserControl page = new ParsePage();
            Switcher.Switch(page);
        }

        private void TableOfTokens_Click(object sender, RoutedEventArgs e)
        {
            UserControl page = new TableOfTokensInterface();
            // page = new TableOfAnalyzer();

            Switcher.Switch(page);
        }

        private void TableOfConstantsAndIdentifier_Click(object sender, RoutedEventArgs e)
        {
            UserControl page = new TableOfConstantsAndIdentifier();
            Switcher.Switch(page);
        }

        private void SourceTableOfTokens_Click(object sender, RoutedEventArgs e)
        {
            UserControl page = new SourceTableOfTokensInterface();
            Switcher.Switch(page);
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            UserControl page = new Settings();
            Switcher.Switch(page);
        }
        private void TableTransitions_Click(object sender, RoutedEventArgs e)
        {
            //UserControl page = new TableOfTransitions();
            UserControl page = new TableOfAnalyzer();
            Switcher.Switch(page);
        }
        private void TableAnalyzer_Click(object sender, RoutedEventArgs e)
        {
            UserControl page = new TableOfAnalyzerInterface();
            Switcher.Switch(page);
        }
    }
}

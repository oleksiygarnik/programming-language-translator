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
    /// Логика взаимодействия для TableOfConstantsInterface.xaml
    /// </summary>
public partial class TableOfConstantsInterface : UserControl
    {
        public TableOfConstantsInterface()
        {
            
            InitializeComponent();
            list1.ItemsSource = TableOfConstants.ConstantListOfTokens;
            prev.IsEnabled = false;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            header.Text = "Таблица индентификаторов";
            list1.ItemsSource = TableOfIdentifiers.IdentifierListOfTokens;
            next.IsEnabled = false;
            prev.IsEnabled = true;
        }
        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            list1.ItemsSource = TableOfConstants.ConstantListOfTokens;
            header.Text = "Таблица констант";
            next.IsEnabled = true;
        }
        
        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}

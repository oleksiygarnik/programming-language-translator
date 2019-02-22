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
    /// Логика взаимодействия для TableOfConstantsAndIdentifier.xaml
    /// </summary>
    public partial class TableOfConstantsAndIdentifier : UserControl
    {
        public TableOfConstantsAndIdentifier()
        {
            InitializeComponent();
            //list1.ItemsSource = TableOfIdentifier.IdentifierListOfTokens;
            //list2.ItemsSource = TableOfConstants.ConstantListOfTokens;
            frame.Content = new TableOfConstantsInterface();
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}

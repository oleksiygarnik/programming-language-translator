using CompilerDevelopment.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace CompilerDevelopment.GUI
{
    /// <summary>
    /// Логика взаимодействия для ProgrammResult.xaml
    /// </summary>
    public partial class ProgrammResult : Window
    {
        public string TextBoxAtTheMoment { get; set; }
        public ProgrammResult(string result)
        {
            InitializeComponent();
           
            resultBox.Text = result;
            if (Storage.valueForIdentifiers.Count == 1)
            {
                //resultBox.Text += "Введите значение перемееной " + Storage.valueForIdentifiers.First().Key;
            }
            else
            {
                //resultBox.Text += "Введите значение переменным ";
                foreach (KeyValuePair<string, string> KeyValue in Storage.valueForIdentifiers)
                {
                    //resultBox.Text += KeyValue.Key + " ";
                }
            }
            TextBoxAtTheMoment = result;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Storage.End = true;
        }

        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
           
        } // end textChangedEventHandler

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            int j = 0;
            //float num;
           
            if (e.Key == Key.Enter)
            {
                string value = null;
                if (TextBoxAtTheMoment == string.Empty || TextBoxAtTheMoment == null)
                {
                    value = resultBox.Text;
                }
                else
                {
                    value = resultBox.Text.Replace(TextBoxAtTheMoment, "");
                }
                value = value.Trim();
                string[] words = value.Split(new char[] { ' ', '\n' });
                words = words.Where(x => x != "").ToArray();
                if (words.Count() == Storage.valueForIdentifiers.Keys.Count() || words.Count() >= Storage.valueForIdentifiers.Keys.Count())
                {
                    foreach (KeyValuePair<string, string> KeyValue in Storage.valueForIdentifiers)
                    {
                        if(TableOfIdentifiers.TokenIsContained(KeyValue.Key))
                        {
                            for(int i = 0; i < TableOfIdentifiers.IdentifierListOfTokens.Count(); i++)
                            {
                                if(KeyValue.Key == TableOfIdentifiers.IdentifierListOfTokens[i].View)
                                {
                                    if (float.TryParse(words[j++], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out float num)) // сделать точку
                                    {
                                        if (TableOfIdentifiers.IdentifierListOfTokens[i].Type == "int")
                                        {
                                            TableOfIdentifiers.IdentifierListOfTokens[i].Value = (int)num;
                                        }
                                        else if(TableOfIdentifiers.IdentifierListOfTokens[i].Type == "float")
                                        {
                                            TableOfIdentifiers.IdentifierListOfTokens[i].Value = num;
                                        }
                                    }
                                    else
                                    {
                                        Storage.ListOfErrors.Add("Введено неправильное значение");
                                        this.DialogResult = false;
                                    }
                                }
                            }
                        }
                    }
                    Storage.StringTemplate = resultBox.Text + "\n";
                    if (this.DialogResult != false)
                        this.DialogResult = true;
                    
                }


                

            }
       
        }

        private void ResultBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                if(TextBoxAtTheMoment == null)
                {
                    TextBoxAtTheMoment = string.Empty;
                }
                if (resultBox.Text.Count() == TextBoxAtTheMoment.Count())
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }
    }
}

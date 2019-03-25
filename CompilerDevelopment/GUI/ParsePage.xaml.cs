using CompilerDevelopment.DijkstraAlgorithmLab7;
using CompilerDevelopment.Entities;
using CompilerDevelopment.Graphics;
using CompilerDevelopment.RecursiveDescent;
using CompilerDevelopment.Upstream_analysis;
using CompilerDevelopment.Upstream_analysis.SyntaxAnalyzerForUpstreamAnalysis;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ParsePage.xaml
    /// </summary>
    public partial class ParsePage : UserControl
    {
        public ParsePage()
        {
            InitializeComponent();
            //string path = @"D:\ASM\testLabCompiler1.txt";
            //using (StreamReader sr = new StreamReader(path))
            //{
            //    TextCode.Text = sr.ReadToEnd();
            //}
            TextCode.Text = Storage.StringTemplate;
            //if (TextCode.Text == "")
            //{

            //    parse.IsEnabled = false;
            //}
            Storage.StringTemplate = TextCode.Text;
        }


        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
            Storage.StringTemplate = TextCode.Text;

            //SyntaxAnalyzer.errors.Clear();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TextCode.Text = "";
            ListError.Text = "";
            RecursiveDescent.SyntaxAnalyzer.errors.Clear();
            Storage.StringTemplate = TextCode.Text;

            // parse.IsEnabled = false;

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    TextCode.Text = File.ReadAllText(openFileDialog.FileName);
                    Storage.StringTemplate = TextCode.Text;
                }
            }
        }

        private void Compiler_Click(object sender, RoutedEventArgs e)
        {
            ListError.Text = "";
            MPA.TableOfAnalyzer.fullStack.Clear();
            MPA.TableOfAnalyzer.tableOfAnalysis.Clear();
            SourceTableOfTokens.SourceListOfTokens.Clear();
            TableOfConstants.ConstantListOfTokens.Clear();
            TableOfIdentifiers.IdentifierListOfTokens.Clear();


            PolisTableByDijkstraAlgo.polisTableByDijkstraAlgo.Clear();
            TableOfLabels.tableOfLabels.Clear();

            string path = @"D:\ASM\testLabCompiler1.txt";
            string code = TextCode.Text;
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(code);
            }
            if (code != "")
            {
                try
                {
                    SourceTableOfTokens.StartParse(path);
                    ListError.Text += "\n Программа успешно прошла лексический анализ";
                    // TableOfAnalyzer table1 = new TableOfAnalyzer();

                    switch (Settings.SettingMethod)
                    {
                        case "Recursion":
                            SyntaxAnalyzer.Analyze(0);

                            if (SyntaxAnalyzer.errors.Count > 0)
                            {
                                for (int i = 0; i < SyntaxAnalyzer.errors.Count; i++)
                                {
                                    ListError.Text += "\n" + SyntaxAnalyzer.errors[i];
                                }
                                SyntaxAnalyzer.errors.Clear();
                            }
                            else
                            {
                                Storage.SyntaxAnalyzer = true;
                                ListError.Text += "\n Программа успешно прошла синтаксический анализ";
                            }
                            break;
                        case "MPA":
                            
                            Storage.ListOfErrors.Clear();
                            MPA.TableOfAnalyzer.tableOfAnalysis.Clear();
                            string error = MPA.TableOfAnalyzer.PushInTable(9);
                            ListError.Text += "\n";
                            ListError.Text += error;


                            string result = null;

                            PolisTableByDijkstraAlgo.LoadingOperationsTable();
                            if (Storage.SyntaxAnalyzer)
                            {
                                PolisTableByDijkstraAlgo.LoadingPolisTableByDijkstraAlgorithm();

                                result = TableForExecutePolis.ExecutePolis();
                            }


                            

                            

                            if (!Storage.End)
                            {
                                ProgrammResult programmResult = new ProgrammResult(result);
                                programmResult.ShowDialog();
                            }
                            if(Storage.ListOfErrors.Count!=0)
                            {
                                string errors = null;
                                foreach(string str in Storage.ListOfErrors)
                                {
                                    errors += str + "\n";
                                }
                                ProgrammResult programmResult = new ProgrammResult(errors);
                                programmResult.ShowDialog();

                                
                            }
                            
                            //if(programmResult.ShowDialog() == true)
                            //{
                                //if (programmResult.Password == "12345678")
                                //    MessageBox.Show("Авторизация пройдена");
                                //else
                                //    MessageBox.Show("Неверный пароль");
                            //}
                            //else
                            //{
                                //MessageBox.Show("Авторизация не пройдена");
                            //}
                          
                            break;

                        case "UpstreamParsing":
                            TableOfUpstreamParsing.tableOfUpstreamParsing.Clear();
                            TableOfCalculationExpression.tableOfCalculationExpression.Clear();

                            //ListError.Text += "\n" + TableOfUpstreamParsing.Loading2();
                            ListError.Text += "\n" + TableOfUpstreamParsing.LoadingForLab6();
                            Storage.Expression = TextCode.Text;
                            //ListError.Text += "\n Result: " + TableOfCalculationExpression.CalculationOfExpression();

                            break;

                    }

                    //SyntaxAnalyzer.Analyze(0);

                    //if (SyntaxAnalyzer.errors.Count > 0)
                    //{
                    //    for (int i = 0; i < SyntaxAnalyzer.errors.Count; i++)
                    //    {
                    //        ListError.Text += "\n" + SyntaxAnalyzer.errors[i];
                    //    }
                    //    SyntaxAnalyzer.errors.Clear();
                    //}
                    //else
                    //{
                    //    ListError.Text += "\n Программа успешно прошла синтаксический анализ";
                    //}
                }
                catch (Exception ex)
                {
                    //TextCode.Text = ex.Message;
                    ListError.Text = ex.Message;
                }
            }
            else
            {
                TextCode.Text = "Введите программу!";
            }
            //if (SourceTableOfTokens.StartParse(path))
            //{
            //    TextCode.Text = "Программа успешно прошла лексический анализ";
            //}
            //else
            //{
            //    TextCode.Text = "Ошибка";
            //}
            Storage.StringTemplate = TextCode.Text;
        }


    }
}

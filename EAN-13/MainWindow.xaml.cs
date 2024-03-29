﻿using EAN_13.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace EAN_13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<int, List<int>> dictA = new Dictionary<int, List<int>>(10);
        Dictionary<int, List<int>> dictB = new Dictionary<int, List<int>>(10);
        Dictionary<int, List<int>> dictC = new Dictionary<int, List<int>>(10);
        Dictionary<int, List<string>> numGroup = new Dictionary<int, List<string>>(10);
        public MainWindow()
        {
            InitializeComponent();
            FillDict();
            Load("0000000000000");
            CbBasement.ItemsSource = App.DB.BarCode.ToList();
        }

        private Rectangle copyRec(Rectangle rec)
        {
            return new Rectangle()
            {
                Height = Math.Floor(rec.Height),
                Width = Math.Floor(rec.Width),
                Margin = new Thickness(rec.Margin.Left + rec.Width, 10, 0, 0),
                Fill = System.Windows.Media.Brushes.Black,
                SnapsToDevicePixels = true
            };
        }
        public void FillDict()
        {
            dictA.Add(0, new List<int> { 3, 2, 1, 1 });
            dictA.Add(1, new List<int> { 2, 2, 2, 1 });
            dictA.Add(2, new List<int> { 2, 1, 2, 2 });
            dictA.Add(3, new List<int> { 1, 4, 1, 1 });
            dictA.Add(4, new List<int> { 1, 1, 3, 2 });
            dictA.Add(5, new List<int> { 1, 2, 3, 1 });
            dictA.Add(6, new List<int> { 1, 1, 1, 4 });
            dictA.Add(7, new List<int> { 1, 3, 1, 2 });
            dictA.Add(8, new List<int> { 1, 2, 1, 3 });
            dictA.Add(9, new List<int> { 3, 1, 1, 2 });

            dictB.Add(0, new List<int> { 1, 1, 2, 3 });
            dictB.Add(1, new List<int> { 1, 2, 2, 2 });
            dictB.Add(2, new List<int> { 2, 2, 1, 2 });
            dictB.Add(3, new List<int> { 1, 1, 4, 1 });
            dictB.Add(4, new List<int> { 2, 3, 1, 1 });
            dictB.Add(5, new List<int> { 1, 3, 2, 1 });
            dictB.Add(6, new List<int> { 4, 1, 1, 1 });
            dictB.Add(7, new List<int> { 2, 1, 3, 1 });
            dictB.Add(8, new List<int> { 3, 1, 2, 1 });
            dictB.Add(9, new List<int> { 2, 1, 1, 3 });

            dictC.Add(0, new List<int> { 3, 2, 1, 1 });
            dictC.Add(1, new List<int> { 2, 2, 2, 1 });
            dictC.Add(2, new List<int> { 2, 1, 2, 2 });
            dictC.Add(3, new List<int> { 1, 4, 1, 1 });
            dictC.Add(4, new List<int> { 1, 1, 3, 2 });
            dictC.Add(5, new List<int> { 1, 2, 3, 1 });
            dictC.Add(6, new List<int> { 1, 1, 1, 4 });
            dictC.Add(7, new List<int> { 1, 3, 1, 2 });
            dictC.Add(8, new List<int> { 1, 2, 1, 3 });
            dictC.Add(9, new List<int> { 3, 1, 1, 2 });

            numGroup.Add(0, new List<string> {"A", "A", "A", "A", "A", "A"});
            numGroup.Add(1, new List<string> { "A", "A", "B", "A", "B", "B"});
            numGroup.Add(2, new List<string> { "A", "A", "B", "B", "A", "B"});
            numGroup.Add(3, new List<string> { "A", "A", "B", "B", "B", "A"});
            numGroup.Add(4, new List<string> { "A", "B", "A", "A", "B", "B"});
            numGroup.Add(5, new List<string> { "A", "B", "A", "A", "A", "B"});
            numGroup.Add(6, new List<string> { "A", "B", "B", "B", "A", "A"});
            numGroup.Add(7, new List<string> { "A", "B", "A", "B", "A", "B"});
            numGroup.Add(8, new List<string> { "A", "B", "A", "B", "B", "A"});
            numGroup.Add(9, new List<string> { "A", "B", "B", "A", "B", "A"});
        }

        public void Load(string num)
        {
            barcodeCan.Children.RemoveRange(0, barcodeCan.Children.Count - 1);
            char[] nums = num.Replace(" ", "").Replace(",", "").ToArray<char>();
            List<string> numGroupNum = numGroup[Convert.ToInt32(nums[0]) - 48];

            Rectangle rect = new Rectangle()
            {
                Height = 222.0,
                Width = 2.0,
                Margin = new Thickness(30, 10, 0, 0),
                Fill = System.Windows.Media.Brushes.Black,
                SnapsToDevicePixels = true
            };

            Rectangle rect2 = new Rectangle()
            {
                Height = 222.0,
                Width = 2.0,
                Margin = new Thickness(33, 10, 0, 0),
                Fill = System.Windows.Media.Brushes.White,
                SnapsToDevicePixels = true
            };

            Rectangle rect1_2 = new Rectangle()
            {
                Height = 222.0,
                Width = 4.0,
                Margin = new Thickness(36, 10, 0, 0),
                Fill = System.Windows.Media.Brushes.Black,
                SnapsToDevicePixels = true
            };

            Label lb1 = new Label()
            {
                Content = nums[0],
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(8, 207, 0, 0)
            };

            barcodeCan.Children.Add(lb1);
            barcodeCan.Children.Add(rect);
            barcodeCan.Children.Add(rect2);
            barcodeCan.Children.Add(rect1_2);

            Rectangle localRec = rect1_2;
                for (int i = 0; i < 6; i++)
            {
                Rectangle rec1 = new Rectangle()
                {
                    Height = 207.0,
                    Width = (numGroupNum[i] == "A") ? 3.0 * dictA[Convert.ToInt32(nums[i + 1]) - 48][0] : 3.0 * dictB[Convert.ToInt32(nums[i + 1]) - 48][0],
                    Margin = new Thickness(localRec.Margin.Left + localRec.Width, 10, 0, 0),
                    Fill = System.Windows.Media.Brushes.White,
                    SnapsToDevicePixels = true
                };

                Rectangle rec2 = new Rectangle()
                {
                    Height = 207.0,
                    Width = (numGroupNum[i] == "A") ? 3.0 * dictA[Convert.ToInt32(nums[i + 1]) - 48][1] : 3.0 * dictB[Convert.ToInt32(nums[i + 1]) - 48][1],
                    Margin = new Thickness(rec1.Margin.Left + rec1.Width, 10, 0, 0),
                    Fill = System.Windows.Media.Brushes.Black,
                    SnapsToDevicePixels = true
                };
                Rectangle rec3 = new Rectangle()
                {
                    Height = 207.0,
                    Width = (numGroupNum[i] == "A") ? 3.0 * dictA[Convert.ToInt32(nums[i + 1]) - 48][2] : 3.0 * dictB[Convert.ToInt32(nums[i + 1]) - 48][2],
                    Margin = new Thickness(rec2.Margin.Left + rec2.Width, 10, 0, 0),
                    Fill = System.Windows.Media.Brushes.White,
                    SnapsToDevicePixels = true
                };
                Rectangle rec4 = new Rectangle()
                {
                    Height = 207.0,
                    Width = (numGroupNum[i] == "A") ? 3.0 * dictA[Convert.ToInt32(nums[i + 1]) - 48][3] : 3.0 * dictB[Convert.ToInt32(nums[i + 1]) - 48][3],
                    Margin = new Thickness(rec3.Margin.Left + rec3.Width, 10, 0, 0),
                    Fill = System.Windows.Media.Brushes.Black,
                    SnapsToDevicePixels = true
                };
                Label lbl = new Label()
                {
                    Content = nums[i + 1],
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(rec1.Margin.Left, 207, 0, 0)
                };
                localRec = rec4;
                barcodeCan.Children.Add(lbl);
                barcodeCan.Children.Add(rec1);
                barcodeCan.Children.Add(rec2);
                barcodeCan.Children.Add(rec3);
                barcodeCan.Children.Add(rec4);
            }
            Rectangle rect3 = copyRec(localRec);
            rect3.Margin = new Thickness(rect3.Margin.Left + 6.0, 10, 0, 0);
            rect3.Width = 4.0;
            rect3.Height = 222.0;

            Rectangle rect4 = copyRec(rect3);
            rect3.Margin = new Thickness(rect4.Margin.Left + 6.0, 10, 0, 0);
            rect3.Width = 4.0;

            barcodeCan.Children.Add(rect3);
            barcodeCan.Children.Add(rect4);

            localRec = copyRec(rect4);
            for (int i = 7; i< 13; i++)
            {
                Rectangle rec1 = new Rectangle()
                {
                    Height = 207.0,
                    Width = 3.0 * dictC[Convert.ToInt32(nums[i]) - 48][0],
                    Margin = new Thickness(localRec.Margin.Left + localRec.Width, 10, 0, 0),
                    Fill = System.Windows.Media.Brushes.Black,
                    SnapsToDevicePixels = true
                };
                Rectangle rec2 = new Rectangle()
                {
                    Height = 207.0,
                    Width = 3.0 * dictC[Convert.ToInt32(nums[i]) - 48][1],
                    Margin = new Thickness(rec1.Margin.Left + rec1.Width, 10, 0, 0),
                    Fill = System.Windows.Media.Brushes.White,
                    SnapsToDevicePixels = true
                };
                Rectangle rec3 = new Rectangle()
                {
                    Height = 207.0,
                    Width = 3.0 * dictC[Convert.ToInt32(nums[i]) - 48][2],
                    Margin = new Thickness(rec2.Margin.Left + rec2.Width, 10, 0, 0),
                    Fill = System.Windows.Media.Brushes.Black,
                    SnapsToDevicePixels = true
                };
                Rectangle rec4 = new Rectangle()
                {
                    Height = 207.0,
                    Width = 3.0 * dictC[Convert.ToInt32(nums[i]) - 48][3],
                    Margin = new Thickness(rec3.Margin.Left + rec3.Width, 10, 0, 0),
                    Fill = System.Windows.Media.Brushes.White,
                    SnapsToDevicePixels = true
                };
                Label lbl = new Label()
                {
                    Content = nums[i],
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(rec1.Margin.Left, 207, 0, 0)
                };
                localRec = rec4;
                barcodeCan.Children.Add(lbl);
                barcodeCan.Children.Add(rec1);
                barcodeCan.Children.Add(rec2);
                barcodeCan.Children.Add(rec3);
                barcodeCan.Children.Add(rec4);
            }
            Rectangle rect5 = copyRec(localRec);
            rect5.Width = 4.0;
            rect5.Height = 222.0;

            Rectangle rect6 = copyRec(rect5);
            rect6.Width = 4.0;
            rect6.Margin = new Thickness(rect6.Margin.Left + 3.0, 10, 0, 0);

            barcodeCan.Children.Add(rect5);
            barcodeCan.Children.Add(rect6);
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
            if (txtBox.Text.Contains(" "))
            {
                txtBox.Text = txtBox.Text.Replace(" ", "");
                txtBox.SelectionStart = txtBox.Text.Length;
            }
            
        }

        private void BarCodeGenerate_Click(object sender, RoutedEventArgs e)
        {
            int buff = 0;
            int debuff = 0;
            int res = 0;
            string itog = "";
            if (txtBox.Text.Length == 13)
            {
                try
                {
                    for (int i = 1; i < 12; i += 2)
                    {
                        buff += int.Parse(txtBox.Text.Substring(i, 1));
                    }
                    buff *= 3;
                    for (int i = 0; i < 12; i += 2)
                    {
                        debuff += int.Parse(txtBox.Text.Substring(i, 1));
                    }
                    res = buff + debuff;
                    if (res.ToString().Length == 3)
                    {
                        itog = res.ToString().Remove(0, 2);
                        
                    }
                    if (res.ToString().Length == 2)
                    {
                        itog = res.ToString().Remove(0, 1);
                    }
                    if(res.ToString().Length == 1)
                    {
                        itog = res.ToString();
                    }
                    res = 10 - int.Parse(itog);
                    
                    txtBox.Text = $"{txtBox.Text.Substring(0,12) + res.ToString()}";
                     
                    Load($"{txtBox.Text}");
                }
                catch
                {

                }
            }
            else
            {
                if (txtBox.Text.Length > 13)
                {
                    MessageBox.Show($"Штрих-код слишком большой, уберите {txtBox.Text.Length-13} значений");
                    return;
                }
                else
                {
                    MessageBox.Show($"Введите еще {13 - txtBox.Text.Length} значений");
                    return;
                }
            }
        }

        private void CbBasement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtBox.Text = (CbBasement.SelectedItem as BarCode).ValueOfCode.ToString();
        }
    }
}

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
using System.Windows.Shapes;

namespace TobiiTest
{
    /// <summary>
    /// Setkey.xaml 的交互逻辑
    /// </summary>
    public partial class Preference : Window
    {
        public Preferences prefs;

        public Preference(Preferences pr)
        {
            InitializeComponent();
            InitComboBoxes();
            prefs = pr;
            if (prefs == null)
            {
                throw new ArgumentException("prefs are null");
            }
            
            ssSizeCB.SelectedIndex = ssSizeCB.Items.IndexOf(prefs.Get("sssize"));
            if (ssSizeCB.SelectedItem.ToString().Equals("Custom"))
            {
                sizeXTB.IsEnabled = true;
                sizeXTB.Text = prefs.Get("screenx");
                sizeYTB.IsEnabled = true;
                sizeXTB.Text = prefs.Get("screeny");
            }
            else
            {
                sizeXTB.IsEnabled = false;
                sizeXTB.Text = "";
                sizeYTB.IsEnabled = false;
                sizeYTB.Text = "";
            }
            translatorCB.SelectedItem = translatorCB.Items.IndexOf(prefs.Get("translator"));

            translatorCB.SelectionChanged += TranslatorCB_SelectionChanged;
            ssSizeCB.SelectionChanged += SsSizeCB_SelectionChanged;
            sizeXTB.TextChanged += SizeXTB_TextChanged;
            sizeYTB.TextChanged += SizeYTB_TextChanged;

            setKey.SelectedIndex = setKey.Items.IndexOf(prefs.Get("key"));

            magnify.IsEnabled = true;
            magnify.Text = prefs.Get("magnifyFactor");
        }

        private void InitComboBoxes()
        {
            ssSizeCB.Items.Clear();
            ssSizeCB.Items.Add("Small");
            ssSizeCB.Items.Add("Medium");
            ssSizeCB.Items.Add("Large");
            ssSizeCB.Items.Add("Custom");
            translatorCB.Items.Clear();
            translatorCB.Items.Add("Google");
            translatorCB.Items.Add("Microsoft");
            setKey.Items.Clear();
            setKey.Items.Add("Left Ctrl");
            setKey.Items.Add("Left Shift");
            setKey.Items.Add("Right Ctrl");
            setKey.Items.Add("Right Shift");
        }

        private void TranslatorCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            prefs.Update("translator", translatorCB.SelectedItem.ToString());
            Console.WriteLine("Current translator: " + prefs.Get("translator"));
        }

        private void SsSizeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            prefs.Update("sssize", ssSizeCB.SelectedItem.ToString());
            if (ssSizeCB.SelectedItem.ToString().Equals("Custom"))
            {
                sizeXTB.IsEnabled = true;
                sizeXTB.Text = prefs.Get("screenx");
                sizeYTB.IsEnabled = true;
                sizeXTB.Text = prefs.Get("screeny");
            }
            else
            {
                sizeXTB.IsEnabled = false;
                sizeXTB.Text = "";
                sizeYTB.IsEnabled = false;
                sizeYTB.Text = "";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void SizeXTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            prefs.Update("screenx", sizeXTB.Text);
        }

        private void SizeYTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            prefs.Update("screeny", sizeYTB.Text);
        }

        private void MagnifyFactor_TextChanged(object sender, TextChangedEventArgs e)
        {
            prefs.Update("magnifyFactor", magnify.Text);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

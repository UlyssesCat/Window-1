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
using Microsoft.International.Converters.PinYinConverter;
using System.Collections.ObjectModel;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using DotNetSpeech;

namespace chineseChar
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox2.Text.Trim().Length == 0) return;
            char one_char = textBox2.Text.Trim().ToCharArray()[0];
            int ch_int = (int)one_char;
            string str_char_int = string.Format("{0}", ch_int);
            if(ch_int>127)
            {
                ChineseChar chineseChar = new ChineseChar(one_char);
                ReadOnlyCollection<string> pinyin = chineseChar.Pinyins;
                string pin_str = "";
                foreach (string pin in pinyin) pin_str += pin + "\r\n";
                textBox1.Text = "";
                textBox1.Text = pin_str;
            }
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textBox1.Text = ChineseConverter.Convert(textBox2.Text.Trim(), ChineseConversionDirection.TraditionalToSimplified);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            textBox1.Text = ChineseConverter.Convert(textBox2.Text.Trim(), ChineseConversionDirection.SimplifiedToTraditional);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SpeechVoiceSpeakFlags sp = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            SpVoice voice = new SpVoice();
            voice.Speak(textBox2.Text.Trim(), sp);
        }
    }
}

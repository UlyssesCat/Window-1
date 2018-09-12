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
using System.Windows.Forms;
using System.IO;


namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            folderBrowserDialog1 = new FolderBrowserDialog();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("zzz");
            
        }

        private void clickableEllipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("click a ellipse");
        }

        FolderBrowserDialog folderBrowserDialog1;
        String folder_path = "";
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            if(folderBrowserDialog1.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                folder_path = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        //查找所有文件
        public static string[] folder_files;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(Directory.Exists(folder_path))
            {
                folder_files = Directory.GetFiles(folder_path, textbox1.Text, SearchOption.AllDirectories);
                listBox1.Items.Clear();
                int selected_index = 0;
                foreach(string folder_file in folder_files)
                {
                    selected_index = listBox1.Items.Add(folder_file);
                    listBox1.SelectedIndex = selected_index;
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int sel_index = listBox2.SelectedIndex;
            string sel_str = listBox2.SelectedItem.ToString();
            if(sel_index>0)
            {
                listBox2.Items[sel_index] = listBox2.Items[sel_index - 1];
                listBox2.Items[sel_index - 1] = sel_str;
                listBox2.SelectedIndex = sel_index;
                listBox2.SelectedIndex = sel_index-1;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            int sel_index = listBox2.SelectedIndex;
            string sel_str = listBox2.SelectedItem.ToString();
            if (sel_index < folder_files.Length - 1)
            {
                listBox2.Items[sel_index] = listBox2.Items[sel_index + 1];
                listBox2.Items[sel_index + 1] = sel_str;
                listBox2.SelectedIndex = sel_index;
                listBox2.SelectedIndex = sel_index + 1;
            }
        }


        //目标文件名
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        public static string dest_file;
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            saveFileDialog1.Title = "选择要合并后的文件";
            saveFileDialog1.InitialDirectory = System.Environment.SpecialFolder.DesktopDirectory.ToString();
            saveFileDialog1.OverwritePrompt = false;
            if(saveFileDialog1.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                dest_file = saveFileDialog1.FileName;
                label2.Content = dest_file;
            }

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (File.Exists(dest_file))
            {
                File.Delete(dest_file);
            }
            FileStream fs_dest = new FileStream(dest_file, FileMode.CreateNew, FileAccess.Write);
            byte[] DataBuffer = new byte[100000];
            byte[] file_name_buf;
            FileStream fs_sourse = null;
            int read_len;
            FileInfo fi_a = null;
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                fi_a = new FileInfo(listBox2.Items[i].ToString());
                if (checkBox2.IsChecked == true)
                {
                    file_name_buf = Encoding.Default.GetBytes(fi_a.Name);
                    fs_dest.Write(file_name_buf, 0, file_name_buf.Length);
                }
                if (checkBox1.IsChecked == true)
                {
                    fs_dest.WriteByte((byte)13);
                    fs_dest.WriteByte((byte)10);
                }
                fs_sourse = new FileStream(fi_a.FullName, FileMode.Open, FileAccess.Read);
                read_len = fs_sourse.Read(DataBuffer, 0, 100000);
                while (read_len > 0)
                {
                    fs_dest.Write(DataBuffer, 0, read_len);
                    read_len = fs_sourse.Read(DataBuffer, 0, 100000);
                }
                fs_dest.WriteByte((byte)13);
                fs_dest.WriteByte((byte)10);
                fs_sourse.Close();
            }
            fs_sourse.Dispose();
            fs_dest.Flush();
            fs_dest.Close();
            fs_dest.Dispose();
            if (checkBox3.IsChecked == true)
            {
                System.Diagnostics.Process.Start(dest_file);
            }
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string sel_str = listBox1.Items[i].ToString();
                for (int j = 0; j < listBox2.Items.Count; j++)
                {
                    if (sel_str == listBox2.Items[j].ToString())
                    {
                        return;
                    }
                }
                listBox2.Items.Add(sel_str);
            }


        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string s = listBox2.SelectedItem.ToString();
            listBox2.Items.Remove(s);
            
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                string s = listBox1.SelectedItem.ToString();
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    if (s == listBox2.Items[i].ToString()) return;
                }
                listBox2.Items.Add(s);
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            if(listBox2.Items.Count!=0&&listBox2.SelectedItem!=null)
            {
                System.Diagnostics.Process.Start(listBox2.SelectedItem.ToString());
            }
        }
    }
}

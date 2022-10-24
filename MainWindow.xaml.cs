using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace SaveBase64FileTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnSave.Click += BtnSave_Click;
            btnClear.Click += BtnClear_Click;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtBase64.Text = "";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == false)
                {
                    return;
                }

                string filePath = saveFileDialog.FileName;
                string base64File = txtBase64.Text;
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    MessageBox.Show("请选择文件保存路径");
                    return;
                }
                if (string.IsNullOrWhiteSpace(base64File))
                {
                    MessageBox.Show("请输入文件的base64内容");
                    return;
                }
                base64File = base64File.Replace("-", "+").Replace("_", "/");
                var base64 = Encoding.ASCII.GetBytes(base64File);
                var padding = base64.Length * 3 % 4;
                if (padding != 0)
                {
                    base64File = base64File.PadRight(base64File.Length + padding, '=');
                }
                byte[] bytes = Convert.FromBase64String(base64File);
                File.WriteAllBytes(filePath, bytes);
                MessageBox.Show("保存成功");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("转换异常");
                txtBase64.Text = ex.Message;
                return;
            }
        }
    }
}

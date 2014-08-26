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
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Windows.Media.Animation;

namespace ExcelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        DataAccess dataAccess;
        Storyboard storyShowEditor, storyHideEditor;
        Customer newCustomer = null;

        public MainWindow()
        {
            InitializeComponent();
            storyShowEditor = (Storyboard)this.Resources["storyShowEditorPanel"];
            storyHideEditor = (Storyboard)this.Resources["storyHideEditorPanel"];

            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dataAccess = new DataAccess();
            Sync();
        }

        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var customer = datagrid.SelectedItem;
            if (customer != null)
            {

            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            ED_WeChatID.Text = string.Empty;
            ED_CustomerName.Text = string.Empty;
            storyShowEditor.Begin();
        }

        private void editorSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ED_WeChatID.Text.Trim()) || string.IsNullOrEmpty(ED_CustomerName.Text.Trim()))
            {
                return;
            }

            string wechat_id = ED_WeChatID.Text.Trim();
            string customer_name = ED_CustomerName.Text.Trim();

            newCustomer = new Customer();
            newCustomer.WeChatID = wechat_id;
            newCustomer.CustomerName = customer_name;

            try
            {
                bool successful = dataAccess.InsertOrUpdateRowInExcelAsync(newCustomer).Result;

                if (successful)
                {
                    Sync();
                    storyHideEditor.Begin();
                }
                else
                {
                    MessageBox.Show("啊哦，保存失败喽！");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("啊哦，保存失败喽！重启程序后再试一次吧！");
            }
        }

        private void editorCancel_Click(object sender, RoutedEventArgs e)
        {
            storyHideEditor.Begin();
        }

        #region Data

        private void Sync()
        {
            datagrid.ItemsSource = dataAccess.GetDataFormExcelAsync().Result;
        }

        #endregion
    }
}

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
    public partial class MainWindow : System.Windows.Window
    {
        #region Property

        DataAccess dataAccess;
        Storyboard storyShowEditor, storyHideEditor;
        Customer newCustomer = null;

        #endregion

        #region Lifecycle

        public MainWindow()
        {
            InitializeComponent();
            //storyShowEditor = (Storyboard)this.Resources["storyShowEditorPanel"];
            //storyHideEditor = (Storyboard)this.Resources["storyHideEditorPanel"];

            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dataAccess = new DataAccess();
            Sync();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            ED_WeChatID.Text = string.Empty;
            ED_CustomerName.Text = string.Empty;
            storyShowEditor.Begin();
        }

        #endregion

        #region Editor Panel


        private async void editorSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ED_CustomerName.Text.Trim()))
            {
                return;
            }

            string customer_name = ED_CustomerName.Text.Trim();
            string email = ED_Email.Text.Trim();
            string phone = ED_Phone.Text.Trim();
            string wechat_id = ED_WeChatID.Text.Trim();
            string media_name = ED_MediaName.Text.Trim();
            string city = ED_City.Text.Trim();

            newCustomer = new Customer();
            newCustomer.CustomerName = customer_name;
            newCustomer.Email = email;
            newCustomer.Phone = phone;
            newCustomer.WeChatID = wechat_id;
            newCustomer.MediaName = media_name;
            newCustomer.City = city;
            newCustomer.SelectedStyle = "3";

            bool successful = false;

            try
            {
                await dataAccess.InsertOrUpdateRowInExcelAsync(newCustomer);
                successful = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("啊哦，保存失败喽！再试一次吧！");
            }

            if (successful)
            {
                Sync();
                //storyHideEditor.Begin();
            }
        }

        private void editorCancel_Click(object sender, RoutedEventArgs e)
        {
        }


        #endregion

        #region Data

        private void Sync()
        {
        }

        #endregion

        #region Style List

        private void StyleListItem_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((FrameworkElement)sender).Tag.ToString();
            switch (tag)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                default:
                    break;
            }
        }

        #endregion


    }
}

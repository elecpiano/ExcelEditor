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
using Microsoft.Expression.Interactivity.Core;

namespace ExcelEditor
{
    public partial class MainWindow : System.Windows.Window
    {
        #region Property

        DataAccess dataAccess;
        Customer newCustomer = null;
        Dictionary<string, InteriorStyle> interiorStyleList;
        InteriorStyle selectedStyle = null;

        #endregion

        #region Lifecycle

        public MainWindow()
        {
            InitializeComponent();
            //storyShowEditor = (Storyboard)this.Resources["storyShowEditorPanel"];
            //storyHideEditor = (Storyboard)this.Resources["storyHideEditorPanel"];

            this.Loaded += MainWindow_Loaded;
            InitData();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ExtendedVisualStateManager.GoToElementState(this.LayoutRoot as FrameworkElement, "VSSplash", true);
            dataAccess = new DataAccess();
            Sync();
        }

        #endregion

        #region Data

        private void Sync()
        {
        }

        private void InitData()
        {
            interiorStyleList = new Dictionary<string, InteriorStyle>();

            InteriorStyle item;

            //item 1
            item = new InteriorStyle();
            item.ID = "1";
            item.Title = "地中海式风格";
            item.Description = "地中海式风格地中海式风格地中海式风格地中海式风格地中海式风格地中海式风格地中海式风格地中海式风格地中海式风格";
            item.Images = new string[] { 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", };
            interiorStyleList.Add(item.ID, item);

            //item 2
            item = new InteriorStyle();
            item.ID = "2";
            item.Title = "法式风格";
            item.Description = "法式风格法式风格法式风格法式风格法式风格法式风格法式风格法式风格法式风格法式风格法式风格法式风格法式风格";
            item.Images = new string[] { 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", };
            interiorStyleList.Add(item.ID, item);

            //item 3
            item = new InteriorStyle();
            item.ID = "3";
            item.Title = "现代风格";
            item.Description = "现代风格现代风格现代风格现代风格现代风格";
            item.Images = new string[] { 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", };
            interiorStyleList.Add(item.ID, item);

            //item 4
            item = new InteriorStyle();
            item.ID = "4";
            item.Title = "新古典风格";
            item.Description = "新古典风格新古典风格新古典风格新古典风格新古典风格新古典风格新古典风格新古典风格新古典风格新古典风格新古典风格新古典风格新古典风格";
            item.Images = new string[] { 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", };
            interiorStyleList.Add(item.ID, item);

            //item 5
            item = new InteriorStyle();
            item.ID = "5";
            item.Title = "新亚洲风格";
            item.Description = "新亚洲风格新亚洲风格新亚洲风格新亚洲风格新亚洲风格新亚洲风格新亚洲风格";
            item.Images = new string[] { 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", 
                "/Assets/Style_1_1.jpg", };
            interiorStyleList.Add(item.ID, item);
        }

        private void ResetInputFields()
        {
            ED_CustomerName.Text = string.Empty;
            ED_Email.Text = string.Empty;
            ED_Phone.Text = string.Empty;
            ED_WeChatID.Text = string.Empty;
            ED_MediaName.Text = string.Empty;
            ED_City.Text = string.Empty;
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
            newCustomer.SelectedStyle = selectedStyle.ID;

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
                //Sync();
                ExtendedVisualStateManager.GoToElementState(this.LayoutRoot as FrameworkElement, "VSConfirm", true);
                ResetInputFields();
            }
        }

        private void editorCancel_Click(object sender, RoutedEventArgs e)
        {
            ExtendedVisualStateManager.GoToElementState(this.LayoutRoot as FrameworkElement, "VSDetail", true);
            ResetInputFields();
        }

        #endregion

        #region Style List

        private void StyleListItem_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((FrameworkElement)sender).Tag.ToString();
            if (!interiorStyleList.ContainsKey(tag))
            {
                return;
            }

            selectedStyle = interiorStyleList[tag];
            coverShow.SetDataSource(selectedStyle);
            ExtendedVisualStateManager.GoToElementState(this.LayoutRoot as FrameworkElement, "VSDetail", true);
        }

        #endregion

        #region Style Detail

        private void styleDetailOK(object sender, RoutedEventArgs e)
        {
            ExtendedVisualStateManager.GoToElementState(this.LayoutRoot as FrameworkElement, "VSEditor", true);
        }

        private void styleDetailCancel(object sender, RoutedEventArgs e)
        {
            ExtendedVisualStateManager.GoToElementState(this.LayoutRoot as FrameworkElement, "VSList", true);
        }

        #endregion

        #region Confirm Panel

        private void home_Click(object sender, RoutedEventArgs e)
        {
            ExtendedVisualStateManager.GoToElementState(this.LayoutRoot as FrameworkElement, "VSSplash", true);
        }

        #endregion

        #region Splash

        private void SplashGo_Click(object sender, RoutedEventArgs e)
        {
            ExtendedVisualStateManager.GoToElementState(this.LayoutRoot as FrameworkElement, "VSList", true);
        }

        #endregion

    }
}

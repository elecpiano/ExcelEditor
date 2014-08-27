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
            item.Description = "地中海风格的基础是明亮、简单、色彩丰富、带有民族性，具有十分鲜明的特色。通过一系列开放性和通透性的建筑装饰语言来表达地中海装修风格的自由精神内涵；同时，它通过取材天然来体现向往自然、亲近自然、感受自然的生活情趣，进而体现地中海风格的自然美感。";
            item.Images = new string[] { 
                "/Assets/Style1/Style_1_1.jpg", 
                "/Assets/Style1/Style_1_2.jpg", 
                "/Assets/Style1/Style_1_3.jpg", 
                "/Assets/Style1/Style_1_4.jpg", 
                "/Assets/Style1/Style_1_5.jpg", };
            interiorStyleList.Add(item.ID, item);

            //item 2
            item = new InteriorStyle();
            item.ID = "2";
            item.Title = "法式风格";
            item.Description = "法式风格整体弥漫着复古、奢华、自然主义的调性，开放式的空间结构、随处可见的花卉和绿色植物、雕刻精细的家具……所有的一切从整体上营造出一种田园之气，优雅、高贵、浪漫则是它的内在气质。建筑多采用对称造型，营造恢宏的气势，打造豪华舒适的居住空间。法式廊柱、雕花、线条等制作工艺无不透露出法式风格的精细考究。";
            item.Images = new string[] { 
                "/Assets/Style2/Style_2_1.jpg", 
                "/Assets/Style2/Style_2_2.jpg", 
                "/Assets/Style2/Style_2_3.jpg", 
                "/Assets/Style2/Style_2_4.jpg", 
                "/Assets/Style2/Style_2_5.jpg", };
            interiorStyleList.Add(item.ID, item);

            //item 3
            item = new InteriorStyle();
            item.ID = "3";
            item.Title = "现代风格";
            item.Description = "现代风格在造型方面多采用几何结构，在装饰与布置中最大限度的体现空间与家具的整体协调感，一般会打造色彩跳跃、简洁、实用、多功能的个性空间。在家具配置上，多采用白亮光系列的家具。独特的光泽使家具倍感时尚，具有舒适与美观并存的享受。在配饰上，以简洁的造型、完美的细节，营造出时尚前卫的感觉。";
            item.Images = new string[] { 
                "/Assets/Style3/Style_3_1.jpg", 
                "/Assets/Style3/Style_3_2.jpg", 
                "/Assets/Style3/Style_3_3.jpg", 
                "/Assets/Style3/Style_3_4.jpg", 
                "/Assets/Style3/Style_3_5.jpg", };
            interiorStyleList.Add(item.ID, item);

            //item 4
            item = new InteriorStyle();
            item.ID = "4";
            item.Title = "新古典风格";
            item.Description = "新古典风格十分注重装饰效果，多用室内陈设品来增强历史文化特色，同时用现代的手法和材质还原古典气质。该风格的设计从简单到繁杂、从整体到局部，精雕细琢，都给人种一丝不苟的印象，采用明亮、大方的色彩，使整个空间呈现出开放、宽容的大气之感，让人丝毫不显局促，高雅而和谐成为新古典风格的代名词。";
            item.Images = new string[] { 
                "/Assets/Style4/Style_4_1.jpg", 
                "/Assets/Style4/Style_4_2.jpg", 
                "/Assets/Style4/Style_4_3.jpg", 
                "/Assets/Style4/Style_4_4.jpg", 
                "/Assets/Style4/Style_4_5.jpg", };
            interiorStyleList.Add(item.ID, item);

            //item 5
            item = new InteriorStyle();
            item.ID = "5";
            item.Title = "新亚洲风格";
            item.Description = "新亚洲风格在设计上延续了明清时期家居配饰的理念，提炼了其中经典元素并加以简化和丰富，在家具形态上更加简洁清秀，同时又打破了传统空间布局中等级、尊卑等文化思想的束缚，空间配色上也更为轻松自然。以现代人的审美需求来打造富有传统韵味的事物，让传统艺术在当今社会得到合适的体现。";
            item.Images = new string[] { 
                "/Assets/Style5/Style_5_1.jpg", 
                "/Assets/Style5/Style_5_2.jpg", 
                "/Assets/Style5/Style_5_3.jpg", 
                "/Assets/Style5/Style_5_4.jpg", 
                "/Assets/Style5/Style_5_5.jpg", };
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
            styleDetailPanel.DataContext = selectedStyle;
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

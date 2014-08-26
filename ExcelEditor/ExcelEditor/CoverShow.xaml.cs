using Microsoft.Expression.Interactivity.Core;
using System.Windows;
using System.Windows.Controls;

namespace ExcelEditor
{
    public partial class CoverShow : UserControl
    {
        public CoverShow()
        {
            InitializeComponent();
        }

        public void SetDataSource(InteriorStyle interiorStyle)
        {
            if (interiorStyle.Images.Length < 5)
            {
                return;
            }

            img1_fix.DataContext = interiorStyle.Images[0];
            img2_fix.DataContext = interiorStyle.Images[1];
            img3_fix.DataContext = interiorStyle.Images[2];
            img4_fix.DataContext = interiorStyle.Images[3];
            img5_fix.DataContext = interiorStyle.Images[4];

            img1.DataContext = interiorStyle.Images[0];
            img2.DataContext = interiorStyle.Images[1];
            img3.DataContext = interiorStyle.Images[2];
            img4.DataContext = interiorStyle.Images[3];
            img5.DataContext = interiorStyle.Images[4];

            ExtendedVisualStateManager.GoToElementState(this.LayoutRoot as FrameworkElement, "VS1", false);

        }

        private void thumbnail_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string tag = ((FrameworkElement)sender).Tag.ToString();
            string vsName = "VS"+tag;
            ExtendedVisualStateManager.GoToElementState(this.LayoutRoot as FrameworkElement, vsName, true);
        }
    }
}

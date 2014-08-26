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
            if (interiorStyle.Images.Length < 6)
            {
                return;
            }
            img0.DataContext = interiorStyle.Images[0];
            img1.DataContext = interiorStyle.Images[1];
            img2.DataContext = interiorStyle.Images[2];
            img3.DataContext = interiorStyle.Images[3];
            img4.DataContext = interiorStyle.Images[4];
            img5.DataContext = interiorStyle.Images[5];

        }
    }
}

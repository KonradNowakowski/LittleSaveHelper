using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LittleSaveHelper.Windows
{
    public partial class DeleteListElement
    {
        public DeleteListElement()
        {
            InitializeComponent();
        }

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            List<TextBox> list = new List<TextBox>
            {
                Box1, Box2, Box3, Box4, Box5, Box6, Box6, Box7, Box8
            };
            
            for (int i = 0; i < 8; i++)
            {
                if (!string.IsNullOrEmpty(list[i].Text))
                    MainWindow.PublicIntList.Add(int.Parse(list[i].Text));
            }
            
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

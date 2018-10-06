using System.Windows;

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
            MainWindow.publicArray = new[]
            {
                int.Parse(Box1.Text), int.Parse(Box2.Text), int.Parse(Box3.Text), int.Parse(Box4.Text),
                int.Parse(Box5.Text), int.Parse(Box6.Text), int.Parse(Box7.Text), int.Parse(Box8.Text)
            };
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

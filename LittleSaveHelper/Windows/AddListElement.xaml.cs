using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using Ookii.Dialogs.Wpf;

namespace LittleSaveHelper.Windows
{
    public partial class AddListElement : Window
    {
        private string GameName, path, _path, time;
        public AddListElement()
        {
            InitializeComponent();
        }

        private void GetButton_OnClick(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();

            folderBrowserDialog.ShowDialog();

            _path = folderBrowserDialog.SelectedPath;
        }
        
        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            GameName = GameNameBox.Text;
            path = _path;
            time = time = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " +
                          DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            var a = new List<string> {GameName, path, time};
            MainWindow.publicList = a;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using Ookii.Dialogs.Wpf;

namespace LittleSaveHelper.Windows
{
    public partial class AddListElement : Window
    {
        // ReSharper disable once InconsistentNaming
        private string _gameName, path, _path, _time;
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
            _gameName = GameNameBox.Text;
            path = _path;
            _time = _time = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " +
                          DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            var a = new List<string> {_gameName, path, _time};
            MainWindow.PublicList = a;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

using System;
using System.Windows;
using System.Windows.Input;

namespace LittleSaveHelper
{
    /// <summary>
    /// Logic of xaml file
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            ItemList.Items.Add(new SaveInfo {GameName = "GTA 5", LastBackupTime = DateTime.Now});
            string a = DateTime.Now.Year.ToString();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void ItemList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private class SaveInfo
        {
            public string GameName { get; set; }
            public DateTime LastBackupTime { get; set; }
        }
    }
}
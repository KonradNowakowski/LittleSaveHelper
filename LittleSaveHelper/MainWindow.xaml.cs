using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using LittleSaveHelper.Windows;

namespace LittleSaveHelper
{
    /// <summary>
    /// Logic of xaml file
    /// </summary>
    public partial class MainWindow
    {
        public static int[] publicArray = null;
        public static List<string> publicList = new List<string>();
        private List<SaveInfo> list = new List<SaveInfo>();
        readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        const string fileName = "Data.dat";
        
        public MainWindow()
        {
            InitializeComponent();
            File.Create(Path.Combine(path, fileName));
            Directory.CreateDirectory(path);
            ItemList.ItemsSource = ReadFromFile();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addListElement = new AddListElement();
            addListElement.Show();

            // Adds content to private list and clears public
            list.Add(new SaveInfo {GameName = publicList[0], Path = publicList[1], LastBackupTime = publicList[2]});
            publicList.Clear();
            
            // Refreshes content of ViewList and saves list to file
            ItemList.Items.Refresh();
            SaveToFile(list);
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var deleteListElement = new DeleteListElement();
            deleteListElement.Show();

            // Deletes elements pointed by user
            foreach (int i in publicArray)
            {
                list.RemoveAt(i);
            }

            // Refreshes content of ViewList and saves list to file
            ItemList.Items.Refresh();
            SaveToFile(list);
        }

        /// <summary>
        /// Saves to file given list
        /// </summary>
        /// <param name="_list"></param>
        private void SaveToFile(List<SaveInfo> _list)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(path, "data.dat"), true))
            {
                foreach (var saveInfo in _list)
                {
                    sw.WriteLine(saveInfo.GameName + "@" + saveInfo.LastBackupTime + "@" + saveInfo.Path);
                }
            }
        }

        private List<SaveInfo> ReadFromFile()
        {
            if (!File.Exists(Path.Combine(path, fileName)))
                return null;
            
            var a = File.ReadAllLines(Path.Combine(path, fileName));
            if (a.Length == 0)
                return null;

            string[] temp = File.ReadAllLines(Path.Combine(path, fileName));
            string[] names = new string[temp.Length];
            string[] dates = new string[temp.Length];
            string[] pathes = new string[temp.Length];
            List<SaveInfo> tempList = new List<SaveInfo>();
            
            for (int i = 0; i < temp.Length; i++)
            {
                names[i] = temp[i].Split('@')[0];
                dates[i] = temp[i].Split('@')[1];
                pathes[i] = temp[i].Split('@')[2];
            }

            for (int i = 0; i < temp.Length; i++)
            {
                tempList[i] = new SaveInfo{GameName = names[i], LastBackupTime = dates[i], Path = pathes[i]};
            }

            return tempList;
        }

        private class SaveInfo
        {
            public string GameName { get; set; }
            public string LastBackupTime { get; set; }
            public string Path { get; set; }
        }
    }
}
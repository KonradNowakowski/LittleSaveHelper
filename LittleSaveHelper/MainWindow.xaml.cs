using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using LittleSaveHelper.Windows;

namespace LittleSaveHelper
{
    /// <summary>
    /// Logic of xaml file
    /// </summary>
    public partial class MainWindow
    {
        internal static int[] publicArray = null;
        internal static List<string> publicList = new List<string>(3);
        private List<SaveInfo> list = new List<SaveInfo>();
        private static readonly string path =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LittleSaveHelper");
        const string fileName = "Data.dat";
        
        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory(path);
            File.Create(Path.Combine(path, fileName)).Close();
            var temp = ReadFromFile();

            try
            {
                if (temp != null || temp.Count == 0)
                {
                    for (int i = 0; i < temp.Count; i++)
                    {
                        ItemList.Items.Add(new SaveInfo
                        {
                            GameName = temp[i].GameName,
                            LastBackupTime = temp[i].LastBackupTime
                        });
                    }
                }
            }
            catch (Exception) { }
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addListElement = new AddListElement();
            addListElement.ShowDialog();
            ReadFromFile(ref list);
            
            for (int i = 0; i < publicList.Count; i++)
            {
                if (publicList[i] == null || publicList[i] == "")
                {
                    return;
                }
            }
            
            // Adds content to private list and clears public
            List<SaveInfo> a = new List<SaveInfo> { new SaveInfo
            {
                GameName = publicList[0],
                LastBackupTime = publicList[2],
                Path = publicList[1]
            }};
            list.Add(a[0]);
            publicList.Clear();
            
            // Refreshes content of ViewList and saves list to file
            ItemList.ItemsSource = list;
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
            ItemList.ItemsSource = list;
            ItemList.Items.Refresh();
            SaveToFile(list);
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            ItemList.Items.Refresh();
        }

        /// <summary>
        /// Saves to file given list
        /// </summary>
        /// <param name="_list">List which is saved to file</param>
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

        private static List<SaveInfo> ReadFromFile()
        {
            if (!File.Exists(Path.Combine(path, fileName)))
                return null;
            
            var a = File.ReadAllLines(Path.Combine(path, fileName));
            if (a.Length == 0)
                return null;

            string[] temp = File.ReadAllLines(Path.Combine(path, fileName));
            string[] names = new string[temp.Length];
            string[] dates = new string[temp.Length];
            string[] patches = new string[temp.Length];
            List<SaveInfo> tempList = new List<SaveInfo>();
            
            for (int i = 0; i < temp.Length; i++)
            {
                names[i] = temp[i].Split('@')[0];
                dates[i] = temp[i].Split('@')[1];
                patches[i] = temp[i].Split('@')[2];
            }

            for (int i = 0; i < temp.Length; i++)
            {
                tempList[i] = new SaveInfo{GameName = names[i], LastBackupTime = dates[i], Path = patches[i]};
            }

            return tempList;
        }
        
        private static void ReadFromFile(ref List<SaveInfo> _list)
        {
            if (!File.Exists(Path.Combine(path, fileName)))
                return;
            
            var a = File.ReadAllLines(Path.Combine(path, fileName));
            if (a.Length == 0)
                return;

            string[] temp = File.ReadAllLines(Path.Combine(path, fileName));
            string[] names = new string[temp.Length];
            string[] dates = new string[temp.Length];
            string[] patches = new string[temp.Length];
            
            for (int i = 0; i < temp.Length; i++)
            {
                names[i] = temp[i].Split('@')[0];
                dates[i] = temp[i].Split('@')[1];
                patches[i] = temp[i].Split('@')[2];
            }

            for (int i = 0; i < temp.Length; i++)
            {
                _list[i] = new SaveInfo{GameName = names[i], LastBackupTime = dates[i], Path = patches[i]};
            }
        }

        private class SaveInfo
        {
            public string GameName { get; set; }
            public string LastBackupTime { get; set; }
            public string Path { get; set; }
        }
    }
}
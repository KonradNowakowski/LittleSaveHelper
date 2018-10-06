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
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        internal static List<int> PublicIntList = new List<int>();
        internal static List<string> PublicList = new List<string>(3);
        private static List<SaveInfo> _list;
        private static readonly string Path =
            System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LittleSaveHelper");
        private const string FileName = "Data.dat";
        
        public MainWindow()
        {
            InitializeComponent();
            
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
            
            if (!File.Exists(System.IO.Path.Combine(Path, FileName)))
                File.Create(System.IO.Path.Combine(Path, FileName)).Close();

            _list = ReadFromFile();
            
            ItemList.ItemsSource = _list;
            
            RefreshElements();
        }

        #region Add button event and method

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddElement();
        }

        private void AddElement()
        {
            var addListElement = new AddListElement();
            addListElement.ShowDialog();
            ReadFromFile();

            foreach (var t in PublicList)
                if (string.IsNullOrEmpty(t))
                    return;

            // Adds content to private list and clears public
            var a = new List<SaveInfo>
            {
                new SaveInfo
                {
                    Number = _list.Count,
                    GameName = PublicList[0],
                    LastBackupTime = PublicList[2],
                    Path = PublicList[1]
                }
            };
            _list.Add(a[0]);
            PublicList.Clear();

            // Refreshes content of ViewList and saves list to file
            ItemList.Items.Refresh();
            SaveToFile(_list);
        }

        #endregion

        #region Delete button event and method

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteElement(ref _list);
        }

        private void DeleteElement(ref List<SaveInfo> list)
        {
            var deleteListElement = new DeleteListElement();
            deleteListElement.ShowDialog();

            foreach (SaveInfo saveInfo in list)
                if (saveInfo == null || string.IsNullOrEmpty(saveInfo.GameName))
                    return;
            
            PublicIntList.Sort();
            var temp = new List<int>();
            
            foreach (var i in PublicIntList) temp.Add(i);

            // Deletes elements pointed by user
            //list.RemoveAll(r => temp.Any(a => a == r.Number));

            list.RemoveAll(r => temp.Any(a => a==r.Number));

            PublicIntList.Clear();
            temp.Clear();

            // Refreshes content of ViewList and saves list to file
            SaveToFile(list);
            RefreshElements();
            ReadFromFile();
        }

        #endregion

        #region Refresh button event and method

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshElements();
        }

        private void RefreshElements()
        {
            if (ItemList.Items.Count != 0)
            {
                ItemList.ItemsSource = null;
                ItemList.Items.Clear();
                _list = ReadFromFile();
                ItemList.ItemsSource = _list;
            }

            ItemList.Items.Refresh();
            SaveToFile(_list);
        }

        #endregion

        #region Save to file

        /// <summary>
        /// Saves to file given list
        /// </summary>
        /// <param name="list">List which is saved to file</param>
        private static void SaveToFile(IEnumerable<SaveInfo> list)
        {
            File.Delete(System.IO.Path.Combine(Path, FileName));
            File.Create(System.IO.Path.Combine(Path, FileName)).Close();
            
            using (var sw = new StreamWriter(System.IO.Path.Combine(Path, FileName), true))
            {
                foreach (var saveInfo in list)
                {
                    sw.WriteLine(saveInfo.Number + "@" + saveInfo.GameName + "@" + saveInfo.LastBackupTime + "@" + saveInfo.Path);
                }
            }
        }

        #endregion

        #region Read from file

        private static List<SaveInfo> ReadFromFile()
        {
            if (!File.Exists(System.IO.Path.Combine(Path, FileName)))
                return new List<SaveInfo>();
            
            if (File.ReadAllLines(System.IO.Path.Combine(Path, FileName)).Length == 0)
                return new List<SaveInfo>();

            var temp = File.ReadAllLines(System.IO.Path.Combine(Path, FileName));
            
            var numbers = new int[temp.Length];
            var gameNames = new string[temp.Length];
            var dates = new string[temp.Length];
            var patches = new string[temp.Length];
            var tempList = new List<SaveInfo>();
            
            for (var i = 0; i < temp.Length; i++)
            {
                numbers[i] = i;
                gameNames[i] = temp[i].Split('@')[1];
                dates[i] = temp[i].Split('@')[2];
                patches[i] = temp[i].Split('@')[3];
            }
            
            for (var i = 0; i < temp.Length; i++)
            {
                tempList.Add(new SaveInfo
                    { Path = patches[i], GameName = gameNames[i], LastBackupTime = dates[i], Number = numbers[i] });
            }

            return tempList;
        }

        #endregion

        private class SaveInfo
        {
            public int Number { get; set; }
            public string GameName { get; set; }
            public string LastBackupTime { get; set; }
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public string Path { get; set; }
        }
    }
}
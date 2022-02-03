using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BaseLibrary;

using System.Diagnostics; // temp

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string databaseName = "database.db";

        private List<ToDoRecord> Records;

        public MainWindow()
        {
            InitializeComponent();
            Records = new List<ToDoRecord>();
            GetRecords();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string title = ToDoTitle.Text;
            string desc = ToDoDetails.Text;

            if (!string.IsNullOrWhiteSpace(title)) {
                using (var connection = CommonFunctions.databaseOpenConnection(databaseName))
                {
                    string insertString = string.Format("INSERT INTO test(Title, Desc) VALUES ('{0}', '{1}')", title, desc);
                    var command = CommonFunctions.databaseCreateCommand(connection, insertString);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            } 
            else
            {
                Debug.WriteLine("empty strings");
            }

            GetRecords();
        }

        private void GetRecords()
        {
            Records = new List<ToDoRecord>();
            var connection = CommonFunctions.databaseOpenConnection(databaseName);

            var command = CommonFunctions.databaseCreateCommand(connection, "SELECT * FROM test");
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                //Debug.WriteLine(reader.GetString(1));
                var key = int.Parse(reader.GetString(0));
                var title = reader.GetString(1);
                var desc = reader.GetString(2);
                Records.Add(new ToDoRecord(key, title, desc));
            }

            connection.Close();

            // Update list
            toDoEntries.Items.Clear();

            foreach(var record in Records)
            {
                toDoEntries.Items.Add(record.ToString());
                Debug.WriteLine(record.ToString());
            }
        }

        private void toDoEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var buff = sender as ListBox;
            if (buff != null)
            {
                Debug.WriteLine(buff.SelectedItem);
            }
        }
    }

    public class ToDoRecord
    {
        public int Key;
        public string Title;
        public string Desc;

        public ToDoRecord(int key, string title, string desc)
        {
            this.Key   = key;
            this.Title = title;
            this.Desc  = desc;
        }

        public override string ToString()
        {
            return string.Format("Key: {0}\nTitle: {1}\nDesc: {2}", Key, Title, Desc);
        }
    }
}

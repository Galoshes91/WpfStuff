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

        private ToDoListClass toDoList;

        public MainWindow()
        {
            InitializeComponent();
            toDoList = new ToDoListClass();
            UpdateList();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            toDoList.SaveRecord(ToDoTitle, ToDoDetails);
            UpdateList();
        }

        private void toDoEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var buff = sender as ListBox;
            if (buff != null)
            {
                Debug.WriteLine(buff.SelectedItem);
            }
        }

        private void UpdateList()
        {
            toDoList.GetRecords(toDoEntries);
        }
    }

    public class ToDoListClass : baseViewModel
    {
        private readonly string databaseName = "database.db";
        private List<ToDoRecord> Records;

        public ToDoListClass()
        {
            Records = new List<ToDoRecord>();
        }

        public void SaveRecord(TextBox ToDoTitle, TextBox ToDoDetails)
        {
            string title = ToDoTitle.Text;
            string desc = ToDoDetails.Text;

            if (!string.IsNullOrWhiteSpace(title))
            {
                using (var connection = CommonFunctions.DatabaseOpenConnection(databaseName))
                {
                    string insertString = string.Format("INSERT INTO test(Title, Desc) VALUES ('{0}', '{1}')", title, desc);
                    var command = CommonFunctions.DatabaseCreateCommand(connection, insertString);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            else
            {
                Debug.WriteLine("empty strings");
            }

            ToDoTitle.Text = string.Empty;
            ToDoDetails.Text = string.Empty;
        }

        public void GetRecords(ListBox toDoEntries)
        {
            Records = new List<ToDoRecord>();
            var connection = CommonFunctions.DatabaseOpenConnection(databaseName);

            var command = CommonFunctions.DatabaseCreateCommand(connection, "SELECT * FROM test");
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

            foreach (var record in Records)
            {
                toDoEntries.Items.Add(record.ToString());
                Debug.WriteLine(record.ToString());
            }
        }
    }
}

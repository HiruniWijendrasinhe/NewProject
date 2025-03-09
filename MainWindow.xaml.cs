using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Diagnostics;
using System.Windows.Threading;
//using WpfApp6Base;

namespace NewProject
{
    public partial class DatahomeWindow : Window
    {
        public List<Tasks>? DatabaseUsers { get; set; }
        public Tasks Currenttask { get; private set; } 

        public DatahomeWindow()
        {
            InitializeComponent();

            Currenttask = new Tasks("Sample Task", DateTime.Now);
            this.DataContext = this;  

            Debug.WriteLine("MainWindow loaded");
            Readme(); 
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePicker.SelectedDate.HasValue)
            {
                DateTextBox.Text = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            }
        }


        private void Create()
        {
            using (Datacontextnew context1 = new Datacontextnew())
            {
                var taskname = NameTextBox.Text;

                if (string.IsNullOrEmpty(taskname))
                {
                    MessageBox.Show("Task name cannot be empty.");
                    return;
                }

                if (!DatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select a valid date.");
                    return;
                }
            

            DateTime date = DatePicker.SelectedDate.Value;
                Tasks newTask = new Tasks
                {
                    Taskname = taskname,
                    Datetime = date,
                    
                };

                context1.NewTask.Add(newTask);
                context1.SaveChanges();
                MessageBox.Show("Task successfully created!");
                Readme(); 
            }
        }

        private void Delete()
        {
            if (ItemList.SelectedItem == null)
            {
                MessageBox.Show("Please select a task to delete.");
                return;
            }

            Tasks? selectedTask = (Tasks)ItemList.SelectedItem;

            using (var context = new Datacontextnew())
            {
                var taskToDelete = context.NewTask.SingleOrDefault(x => x.Id == selectedTask.Id);
                if (taskToDelete != null)
                {
                    context.NewTask.Remove(taskToDelete);
                    context.SaveChanges();
                    MessageBox.Show("Task deleted successfully.");
                    Readme(); // Refresh list
                }
                else
                {
                    MessageBox.Show("Task not found in the database.");
                }
            }
        }

        private void Update()
        {
            if (ItemList.SelectedItem == null)
            {
                MessageBox.Show("Please select a task to update.");
                return;
            }

            Tasks? selectedTask = (Tasks)ItemList.SelectedItem;

            using (Datacontextnew context1 = new Datacontextnew())
            {
                Tasks? taskToUpdate = context1.NewTask.SingleOrDefault(x => x.Id == selectedTask.Id);
                if (taskToUpdate != null)
                {
                    taskToUpdate.Taskname = NameTextBox.Text;
                    if (DatePicker.SelectedDate.HasValue)
                    {
                        taskToUpdate.Datetime = DatePicker.SelectedDate.Value;
                    }
                    else
                    {
                        MessageBox.Show("Please select a valid date.");
                        return;
                    }

                    context1.SaveChanges();
                    MessageBox.Show("Task updated successfully!");
                    Readme();
                }
                else
                {
                    MessageBox.Show("Task not found.");
                }
            }
        }

        private void Readme()
        {
            using (Datacontextnew context1 = new Datacontextnew())
            {
                DatabaseUsers = context1.NewTask.ToList();
                ItemList.ItemsSource = null;
                ItemList.ItemsSource = DatabaseUsers;
            }
        }

        

        private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemList.SelectedItem != null)
            {
                Tasks selectedTask = (Tasks)ItemList.SelectedItem;
                NameTextBox.Text = selectedTask.Taskname;
                DatePicker.SelectedDate = selectedTask.Datetime;
            }
        }

        /*private void NavListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavListBox.SelectedItem is ListBoxItem selectedItem)
            

                switch (selectedItem.Content.ToString())
                {
                    case "Login":
                        new LoginWindow().Show();
                        break;
                   /* case "Home-To-DoList":
                        new ProfileWindow().Show();
                        break;
                    case "Work-To-DoList":
                        new SettingsWindow().Show();
                        break;
                    case "TimeAlert-To-DoList":
                        new SettingsWindow().Show();
                        break;*/
              //  }
           // }
        

        private void CreateButton_Click(object sender, RoutedEventArgs e) => Create();
        private void DeleteButton_Click(object sender, RoutedEventArgs e) => Delete();
        private void UpdateButton_Click(object sender, RoutedEventArgs e) => Update();
        private void ReadButton_Click(object sender, RoutedEventArgs e) => Readme();
    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewProject
{
    public class Tasks 
    {
        

        [Key]
        public int Id { get; set; }

        public string? Taskname { get; set; }
        public DateTime Datetime { get; set; }

       

        public Tasks() { }

        public Tasks(string taskname, DateTime datetime)
        {
            Taskname = taskname;
            Datetime = datetime;
           
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

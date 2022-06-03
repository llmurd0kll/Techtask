using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TodoAppp
{
    public class Task : INotifyPropertyChanged
    {
        private string title;
        private string task;
        private DateTime deadlinedate;

        public int Id { get; set; }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Description
        {
            get { return task; }
            set
            {
                task = value;
                OnPropertyChanged("Task");
            }
        }
        public DateTime DeadlineDate
        {
            get { return deadlinedate; }
            set
            {
                deadlinedate = value;
                OnPropertyChanged("DeadlineDate");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

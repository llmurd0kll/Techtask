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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity;


namespace TodoAppp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ApplicationContext db;
        public MainWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Tasks.Load();
            this.DataContext = db.Tasks.Local.ToBindingList(); //Выражение db.Task.Load() загружает данные из таблицы Task в локальный кэш контекста данных. И затем список загруженных объектов устанавливается в качестве контекста данных:
        }
        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow addTask = new TaskWindow(new Task());
            if (addTask.ShowDialog() == true)
            {
                Task task = addTask.Task;
                db.Tasks.Add(task);
                db.SaveChanges();
            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (tasksList.SelectedItem == null) return;
            // получаем выделенный объект
            Task task = tasksList.SelectedItem as Task;

            TaskWindow taskWindow = new TaskWindow(new Task
            {
                Id = task.Id,
                Description = task.Description,
                DeadlineDate = task.DeadlineDate,
                Title = task.Title
            });

            if (taskWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                task = db.Tasks.Find(taskWindow.Task.Id);
                if (task != null)
                {
                    task.Description = taskWindow.Task.Description;
                    task.Title = taskWindow.Task.Title;
                    task.DeadlineDate = taskWindow.Task.DeadlineDate;
                    db.Entry(task).State = EntityState.Modified; //После получения измененного объекта мы находим его в базе данных и устанавливаем у него состояние Modified, после чего сохраняем все изменения
                    db.SaveChanges();
                }
            }
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // если ни одного объекта не выделено, выходим
            if (tasksList.SelectedItem == null) return;
            // получаем выделенный объект
            Task task = tasksList.SelectedItem as Task;
            db.Tasks.Remove(task);
            db.SaveChanges();
        }
    }
}

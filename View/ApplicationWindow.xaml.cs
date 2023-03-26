using SchoolApplication.DbEntity;
using SchoolApplication.ViewModel;
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
using System.Windows.Shapes;

namespace SchoolApplication.View
{
    /// <summary>
    /// Логика взаимодействия для ApplicationWindow.xaml
    /// </summary>
    public partial class ApplicationWindow : Window
    {
        public ApplicationWindow(User user)
        {
            InitializeComponent();


            this.DataContext = new AppVM(user);
        }

        private void BtnAddData_Click(object sender, RoutedEventArgs e)
        {
            var AddWindows = new ChangeWindow(null);
            AddWindows.Show();
        }

        private void BtnEditData_Click(object sender, RoutedEventArgs e)
        {
            var AddWindows = new ChangeWindow((DataContext as AppVM).SelectedItem);
            AddWindows.Show();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as AppVM).DeleteSelectedItem();
        }
        public void Refresh()
        {
            (DataContext as AppVM).LoadData();
        }
    }
}

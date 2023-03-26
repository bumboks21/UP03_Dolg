using SchoolApplication.DbEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для ChangeWindow.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {
        private InfoSportsman _infoSportsman;
        public ChangeWindow(InfoSportsman infoSportsman)
        {
            InitializeComponent();
            
            foreach (var item in App.Current.Windows)
            {
                if (item is ApplicationWindow)
                {
                    this.Owner = item as Window;
                }
            }

            if (infoSportsman is null)
            {
                _infoSportsman = infoSportsman = new InfoSportsman();
            }
            else
            {
                _infoSportsman = infoSportsman;
            }
            this.DataContext = _infoSportsman;
        }


        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new SportSchoolEntities1())
            {
                try
                {

                    var validateRes = ValidateEntity();
                    if (validateRes.Length > 0)
                    {
                        MessageBox.Show(validateRes.ToString(), "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    db.InfoSportsman.AddOrUpdate(_infoSportsman);
                    db.SaveChanges();
                    MessageBox.Show("данные успешно сохранены", "успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    (Owner as ApplicationWindow)?.Refresh();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private StringBuilder ValidateEntity()
        {
            var errors = new StringBuilder();

            if (_infoSportsman != null)
            {
                if (string.IsNullOrEmpty(_infoSportsman.Name))
                {
                    errors.AppendLine("Поле Имя спортсмена не может быть пустым!");
                }
                if (string.IsNullOrEmpty(_infoSportsman.Surname))
                {
                    errors.AppendLine("Поле Фамилия спортсмена не может быть пустым!");
                }
                if (_infoSportsman.Age <= 0)
                { 
                    errors.AppendLine("Поле Возраст спортсмена не может быть пустым");
                }
                if (string.IsNullOrEmpty(_infoSportsman.HorseName))
                {
                    errors.AppendLine("Поле Имя лошади не может быть пустым!");
                }
            }
            return errors;
        }
    }
}


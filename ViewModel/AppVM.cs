using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SchoolApplication.DbEntity;

namespace SchoolApplication.ViewModel
{
    public class AppVM : BaseVM
    {
        /* private User _user;


         private string _name;
         private string _surname;
         private int _age;
        private string _horsename;

         public string Name
         {
             get => _name;
             set
             {
                 _name = value;
                 OnPropertyChanged(nameof(Name));
             }
         }

         public string Surname
         {
             get => _surname;
             set
             {
                 _surname = value;
                 OnPropertyChanged(nameof(Surname));
             }
         }

         public int Age
         {
             get => _age;
             set
             {
                 _age = value;
                 OnPropertyChanged(nameof(Age));
             }
         }

        public string HorseName
         {
             get => _horsename;
             set
             {
                 _horsename = value;
                 OnPropertyChanged(nameof(HorseName));
             }
         }

         public AppVM(User user)
         {

             Name = user.InfoSportsman.Name;
             Surname = user.InfoSportsman.Surname;
             Age = (int)user.InfoSportsman.Age;
             HorseName = user.InfoSportsman.HorseName;
         }

         private void LoadData()
         {
             using (var db = new SportSchoolEntities1())
             {
                 var result = db.User;

             }
         }
        */
        private InfoSportsman _selectedItem;
        private ObservableCollection<InfoSportsman> _infoSportsman;

        public ObservableCollection<InfoSportsman> InfoSportsman
        {
            get => _infoSportsman;
            set
            {
                _infoSportsman = value;
                OnPropertyChanged(nameof(InfoSportsman));
            }
        }

        public InfoSportsman SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public AppVM(User user)
        {
            InfoSportsman = new ObservableCollection<InfoSportsman>();

            LoadData();
        }

        public void LoadData()
        {
            if (InfoSportsman.Count != 0)
            {
                InfoSportsman.Clear();
            }

            var result = DbStorage.DB_s.InfoSportsman.ToList();
            result.ForEach(elem => InfoSportsman?.Add(elem));
        }

        public void DeleteSelectedItem()
        {
            if (!(SelectedItem is null))
            {
                using (var db = new SportSchoolEntities1())
                {
                    var result = MessageBox.Show("Вы действительно хотите удалить этот элемент?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var ItemForDelete = db.InfoSportsman.Where(elem => elem.Id == SelectedItem.Id).FirstOrDefault();
                            db.InfoSportsman.Remove(ItemForDelete);
                            db.SaveChanges();
                            LoadData();
                            MessageBox.Show("Данные успешно удалены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Удаление завершено успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }
    }
}

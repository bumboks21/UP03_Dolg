using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SchoolApplication.DbEntity;
using SchoolApplication.View;

namespace SchoolApplication.ViewModel
{
    public class AuthVM : BaseVM
    {
        private string _buttonDesc = "Войти";

        private User _user;

        private string _login;
        private string _password;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string BtnDesc
        {
            get => _buttonDesc;
            set
            {
                _buttonDesc = value;
                OnPropertyChanged(nameof(BtnDesc));
            }
        }

        private async Task<bool> Authorize(string login, string password)
        {
            try
            {

                var result = await DbStorage.DB_s.User.FirstOrDefaultAsync(user => user.Login == login && user.Password == password);

                _user = result;

                if (result != null)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Необработанное исключение", MessageBoxButton.OK, MessageBoxImage.Stop);

                return false;
            }

        }

        public async void AuthInApp()
        {
            BtnDesc = "Подождите...";

            if (await Authorize(Login, Password))
            {
                var appWindow = new ApplicationWindow(_user);

                appWindow.Show();

                foreach (var item in App.Current.Windows)
                {
                    if (item is MainWindow)
                    {
                        (item as Window)?.Hide();
                    }
                }

                BtnDesc = "Войти";

                return;
            }

            MessageBox.Show("Неверный логин или пароль", "Авторизация" ,MessageBoxButton.OK, MessageBoxImage.Error);

            BtnDesc = "Войти";
        }
    }
}

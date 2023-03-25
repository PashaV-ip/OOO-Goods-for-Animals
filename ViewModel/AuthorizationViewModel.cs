using OOO_Goods_for_Animals.DbEntity;
using OOO_Goods_for_Animals.View.Windows;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOO_Goods_for_Animals.ViewModel
{
    public class AuthorizationViewModel : BaseViewModel
    {
        private User _user;

        private Visibility _loadScreen = Visibility.Hidden;
        private string _login = "pixil59@gmail.com";
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
        public Visibility LoadScreen
        {
            get => _loadScreen;
            set
            {
                _loadScreen = value;
                OnPropertyChanged(nameof(LoadScreen));
            }
        }

        private async Task<bool> Validation(string login, string password)
        {
            try
            {

                var result = await DbStorage.DB_s.User.FirstOrDefaultAsync(user => user.UserLogin == login &&
                        user.UserPassword == password);

                _user = result;

                if (result != null)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Необработанное исключение",
                        MessageBoxButton.OK, MessageBoxImage.Stop);

                return false;
            }
        }

        public async void Authentication()
        {
            LoadScreen = Visibility.Visible;

            if (await Validation(Login, Password))
            {
                var applicationWindow = new ApplicationWindow(_user);

                applicationWindow.Show();

                foreach (var item in App.Current.Windows)
                {
                    if (item is MainWindow)
                    {
                        (item as Window)?.Hide();
                    }
                }
                LoadScreen = Visibility.Hidden;

                return;
            }

            MessageBox.Show("Неверный логин или пароль", "Авторизация",
                    MessageBoxButton.OK, MessageBoxImage.Error);

            LoadScreen = Visibility.Hidden;
        }
    }
}

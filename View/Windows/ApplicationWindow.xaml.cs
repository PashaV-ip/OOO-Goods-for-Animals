using OOO_Goods_for_Animals.DbEntity;
using OOO_Goods_for_Animals.ViewModel;
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

namespace OOO_Goods_for_Animals.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для ApplicationWindow.xaml
    /// </summary>
    public partial class ApplicationWindow : Window
    {
        public ApplicationWindow(User user)
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel(user);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (Window window in App.Current.Windows)
                window.Close();
        }

        private void ButtonOpenAdminPanel_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ApplicationViewModel).OpenAdminPanel();
        }
    }
}

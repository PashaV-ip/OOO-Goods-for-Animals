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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOO_Goods_for_Animals
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new AuthorizationViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name.ToString())
            {
                case "buttonEnter":
                    (DataContext as AuthorizationViewModel).Password = pswUserPassword.Password.ToString();
                    (DataContext as AuthorizationViewModel).Authentication();
                    break;
                case "buttonExit":
                    Close();
                    break;
            }
        }
    }
}

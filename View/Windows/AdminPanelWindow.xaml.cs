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
    /// Логика взаимодействия для AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow(User user)
        {
            InitializeComponent();

            DataContext = new AdminPanelViewModel(user);
            foreach (var item in App.Current.Windows)
            {
                if (item is MainWindow)
                {
                    this.Owner = item as Window;
                }
            }
        }

        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            switch((sender as Button).Content)
            {
                case "Добавить / Обновить":
                    (DataContext as AdminPanelViewModel).AddProductDB();
                    break;
                case "Удалить":
                    (DataContext as AdminPanelViewModel).DeleteSelectItem();
                    break;
                case "Снять выделение":
                    ProductTable.SelectedIndex = -1;
                    break;
            }
            
        }
    }
}

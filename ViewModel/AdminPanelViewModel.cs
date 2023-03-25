using OOO_Goods_for_Animals.DbEntity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace OOO_Goods_for_Animals.ViewModel
{
    public class AdminPanelViewModel : BaseViewModel
    {
        /*
         У меня почему-то обновление данных работает криво, обычные значения меняет без проблем, а вот с выпадающими списками беда.
        Он как бы меняет в бд значения, а вот отрисовывать в программе это не хочет.
         */
        private ObservableCollection<Product> _products;
        private ObservableCollection<string> _manufacturers;
        private ObservableCollection<string> _providers;
        private ObservableCollection<string> _categorys;
        private ObservableCollection<string> _names;
        private User _user;
        private Product _addProduct;

        private Product _selectProduct;


        
        public ObservableCollection<string> Names
        {
            get => _names;
            set
            {
                _names = value;
                OnPropertyChanged(nameof(Names));
            }
        }
        public ObservableCollection<string> Categorys
        {
            get => _categorys;
            set
            {
                _categorys = value;
                OnPropertyChanged(nameof(Categorys));
            }
        }
        public ObservableCollection<string> Providers
        {
            get => _providers;
            set
            {
                _providers = value;
                OnPropertyChanged(nameof(Providers));
            }
        }

        public ObservableCollection<string> Manufacturers
        {
            get => _manufacturers;
            set
            {
                _manufacturers = value;
                OnPropertyChanged(nameof(Manufacturers));
            }
        }

        public Product AddProduct
        {
            get => _addProduct;
            set
            {
                _addProduct = value;
                OnPropertyChanged(nameof(AddProduct));
            }
        }

        public Product SelectProduct
        {
            get => _selectProduct;
            set
            {
                _selectProduct = value;
                if (_selectProduct != null)
                {
                    _selectProduct.NameID -= 1;              
                    _selectProduct.ManufacturerID -= 1;
                    _selectProduct.ProviderItemID -= 1;          //Данные конструкции в остальной части программы необходимы
                    _selectProduct.CategoryItemID -= 1;          //для сооблюдения индексирования (Я их мог вынести в отдельный метод)
                    AddProduct = _selectProduct;                 //но пока оставил так.
                }
                else
                {
                    AddProduct = new Product();
                    AddProduct.ManufacturerID = 0;
                    AddProduct.ProviderItemID = 0;
                    AddProduct.CategoryItemID = 0;
                    OnPropertyChanged(nameof(AddProduct));
                }
                OnPropertyChanged(nameof(SelectProduct));
            }
        }
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public void LoadData()
        {
            if (Products.Count > 0)
            {
                Products.Clear();
            }
            var productsList = DbStorage.DB_s.Product.ToList();
            productsList.ForEach(element => Products?.Add(element));

            
        }

        private void LoadComboBoxData()
        {
            if (Manufacturers.Count > 0)
            {
                Manufacturers.Clear();
            }
            var manufacturersList = DbStorage.DB_s.ManufacturerItem.ToList();
            manufacturersList.ForEach(element => Manufacturers?.Add(element.Name));

            if (Providers.Count > 0)
            {
                Providers.Clear();
            }
            var providersList = DbStorage.DB_s.ProviderItem.ToList();
            providersList.ForEach(element => Providers?.Add(element.Name));

            if (Categorys.Count > 0)
            {
                Categorys.Clear();
            }
            var categorysList = DbStorage.DB_s.CategoryItem.ToList();
            categorysList.ForEach(element => Categorys?.Add(element.Name));

            if (Names.Count > 0)
            {
                Names.Clear();
            }
            var namesList = DbStorage.DB_s.NameItem.ToList();
            namesList.ForEach(element => Names?.Add(element.Name));
        }

        public void AddProductDB()
        {
            using (var db = new TradeEntities())
            {
                try
                {
                    var validateResult = ValidateEntity();

                    if (validateResult.Length > 0)
                    {
                        MessageBox.Show(validateResult.ToString() + "Ошибка в AddOrEditTableProductDB", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    AddProduct.NameID += 1;
                    AddProduct.ManufacturerID += 1;
                    AddProduct.ProviderItemID += 1;
                    AddProduct.CategoryItemID += 1;

                    db.Product.AddOrUpdate(AddProduct);

                    db.SaveChanges();

                    MessageBox.Show("Данные успешно сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                    
                    AddProduct = new Product();
                    AddProduct.ManufacturerID = 0;
                    AddProduct.ProviderItemID = 0;
                    AddProduct.CategoryItemID = 0;
                    OnPropertyChanged(nameof(AddProduct));
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "Ошибка в TRY CATCHE", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        public void DeleteSelectItem()
        {
            if (!(SelectProduct is null))
            {
                using (var db = new TradeEntities())
                {

                    var result = MessageBox.Show("Вы действительно хотите удалить выбранный Товар?" +
                        "Это действие невозможно отменить.", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var entityForDelete = db.Product.Where(elem => elem.ArticleNumber == SelectProduct.ArticleNumber).FirstOrDefault();

                            db.Product.Remove(entityForDelete);

                            db.SaveChanges();

                            LoadData();

                            MessageBox.Show("Данные успешно удалены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }

                }
            }
        }

        private StringBuilder ValidateEntity()
        {
            var errors = new StringBuilder();

            if (_addProduct != null)
            {
                if (string.IsNullOrEmpty(AddProduct.ArticleNumber))
                {
                    errors.AppendLine("Поле Артикул не может быть пустым!");
                }
                if(!double.TryParse(AddProduct.Price.ToString(), out double price))
                {
                    errors.AppendLine("Поле Цена должно содержать только число!");
                }
                if (!int.TryParse(AddProduct.MaxDiscount.ToString(), out int maxDiscount))
                {
                    errors.AppendLine("Поле MAX скидка должно содержать только целое число!");
                }
                if (!int.TryParse(AddProduct.CurrentDiscount.ToString(), out int currentDiscount))
                {
                    errors.AppendLine("Поле Скидка должно содержать только целое число!");
                }
                if (!int.TryParse(AddProduct.CountProductStock.ToString(), out int countProductStock))
                {
                    errors.AppendLine("Поле Количество должно содержать только целое число!");
                }
            }

            return errors;
        }


        public AdminPanelViewModel(User user)
        {
            Products = new ObservableCollection<Product>();
            Manufacturers = new ObservableCollection<string>();
            Providers = new ObservableCollection<string>();
            Categorys = new ObservableCollection<string>();
            Names = new ObservableCollection<string>();
            User = user;
            AddProduct = new Product();
            AddProduct.ManufacturerID = 0;
            AddProduct.ProviderItemID = 0;
            AddProduct.CategoryItemID = 0;
            SelectProduct = new Product();
            LoadData();
            LoadComboBoxData();
        }
    }
}

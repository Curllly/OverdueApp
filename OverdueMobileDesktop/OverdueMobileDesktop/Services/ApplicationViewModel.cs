using OverdueMobileDesktop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OverdueMobileDesktop.Services
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        bool initialized = false;   // Проверка на начальную инициализацию
        Product selectedProduct;    // Выбранный продукт
        private bool isBusy;        // Проверка на загрузку с сервера

        public ObservableCollection<Product> Products {get; set;}
        ProductService productService = new ProductService();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateProductCommand { get; protected set; }
        public ICommand DeleteProductCommand { get; protected set; }
        public ICommand SaveProductCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }

        public INavigation Navigation { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }
        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        public ApplicationViewModel() 
        {
            Products = new ObservableCollection<Product>();
            IsBusy = false;
            CreateProductCommand = new Command(CreateProduct);
            DeleteProductCommand = new Command(DeleteProduct);
            SaveProductCommand = new Command(SaveProduct);
            BackCommand = new Command(Back);
        }
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                if (selectedProduct != value)
                {
                    Product tempProduct = new Product()
                    {
                        Id = value.Id,
                        PlaceId = value.PlaceId,
                        Title = value.Title,
                        BestBefore = value.BestBefore,
                        Manufactured = value.Manufactured,
                        Count = value.Count
                    };
                    selectedProduct = value;
                    OnPropertyChanged("SelectedProduct");
                    Navigation.PushAsync(new ProductPage(tempProduct, this));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private async void CreateProduct()
        {
            await Navigation.PushAsync(new ProductPage(new Product(), this));
        }
        private void Back()
        {
            Navigation.PopAsync();
            selectedProduct = null;
        }
        public async Task GetProducts()
        {
            if (initialized == true) return;
            IsBusy = true;
            IEnumerable<Product> products = await productService.Get();

            // очищаем список
            //Friends.Clear();
            while (Products.Any())
                Products.RemoveAt(Products.Count - 1);

            // добавляем загруженные данные
            foreach (Product f in products)
                Products.Add(f);
            IsBusy = false;
            initialized = true;
        }
        private async void SaveProduct(object productObject)
        {
            Product product = productObject as Product;

            if (product != null) 
            {
                IsBusy = true;
                // редактирование
                if (product.Id > 0)
                {
                    Product updatedProduct = await productService.Update(product);
                    // заменяем объект в списке на новый
                    if (updatedProduct != null)
                    {
                        int pos = Products.IndexOf(updatedProduct);
                        Products.RemoveAt(pos);
                        Products.Insert(pos, updatedProduct);
                    }
                }
                // добавление
                else
                {
                    Product addedProduct = await productService.Add(product);
                    if (addedProduct != null)
                        Products.Add(addedProduct);
                }
                IsBusy = false;
            }
            Back();
        }
        private async void DeleteProduct(object productObject)
        {
            Product product = productObject as Product;
            if (product != null)
            {
                IsBusy = true;
                Product deletedProduct = await productService.Delete(product.Id);
                if (deletedProduct != null)
                {
                    Products.Remove(deletedProduct);
                }
                IsBusy = false;
            }
            Back();
        }
    }
}

using OverdueMobileDesktop.Models;
using OverdueMobileDesktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OverdueMobileDesktop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductPage : ContentPage
	{
        public Product Model { get; private set; }
		public ProductService productService = new ProductService();
        public ApplicationViewModel ViewModel { get; private set; }
        public int pos;
        public string url;
        public ProductPage (Product model, ApplicationViewModel viewModel)
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent ();
			Model = model;
			ViewModel = viewModel;
			this.BindingContext = ViewModel;

            bestbefore.MinimumDate = manufactured.Date;
            placePicker.SelectedIndex = model.PlaceId - 1;
            pos = viewModel.Products.IndexOf(viewModel.SelectedProduct);
            if (model.Id == 0)
            {
                deleteBtn.IsVisible = false;
            }
            url = ProductService.GetUrl();
		}

		private async void SaveProduct(object sender, EventArgs e)
		{
            try
            {
                // Редактирование
			    if (Model.Id > 0)
			    {
                    const string Url = "http://10.0.2.2:5068/Products";

                    Product product = new Product()
				    {
					    Id = Model.Id,
					    Title = title.Text,
					    PlaceId = placePicker.SelectedIndex + 1,
					    Manufactured = manufactured.Date,
					    BestBefore = bestbefore.Date,
					    Count = int.Parse(count.Text)
				    };

				    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    var response = await client.PutAsync
                    (
                    url,
                    new StringContent(JsonSerializer.Serialize(product),
                        Encoding.UTF8,
                        "application/json")
                    );
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        await Navigation.PopAsync();

                    Product updatedProduct = product;
				    if (updatedProduct != null)
				    {
					    ViewModel.Products.RemoveAt(pos);
					    ViewModel.Products.Insert(pos, updatedProduct);
				    }
                    await Navigation.PopAsync();
                }
			    // Добавление
			    else
			    {
                    const string Url = "http://10.0.2.2:5068/Products";

                    Product product = new Product()
                    {
                        Id = Model.Id,
                        Title = title.Text,
                        PlaceId = placePicker.SelectedIndex + 1,
                        Manufactured = manufactured.Date,
                        BestBefore = bestbefore.Date,
                        Count = int.Parse(count.Text)
                    };

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

				    var response = await client.PostAsync(url, new StringContent(JsonSerializer.Serialize(product),
                        Encoding.UTF8,
                        "application/json")
                    );
				    if (response.StatusCode != System.Net.HttpStatusCode.OK)
					    await Navigation.PopAsync();

				    ViewModel.Products.Add(product);
                    await Navigation.PopAsync();
                }
            }
            catch
            {
                await DisplayAlert("Ошибка", "Поля заполнены неверно или отсутствует подключение к серверу", "Ок");
            }
		}

        private async void DeleteProduct(object sender, EventArgs e)
        {
            const string Url = "http://10.0.2.2:5068/Products";
            if (Model.Id > 0)
			{
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.DeleteAsync(url + $"?id={Model.Id}");

				if (response.StatusCode != System.Net.HttpStatusCode.OK)
					await Navigation.PopAsync();

				ViewModel.Products.RemoveAt(pos);
            }
			await Navigation.PopAsync();
        }
    }
}
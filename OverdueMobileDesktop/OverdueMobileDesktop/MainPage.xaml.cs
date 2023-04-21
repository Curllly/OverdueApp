using OverdueMobileDesktop.Models;
using OverdueMobileDesktop.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OverdueMobileDesktop
{
    public partial class MainPage : ContentPage
    {
        ApplicationViewModel viewModel;
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            viewModel = new ApplicationViewModel()
            {
                Navigation = this.Navigation
            };
            BindingContext = viewModel;
            productsList.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            try
            {
                await viewModel.GetProducts();
                productsList.SelectedItem = null;
            }
            catch
            {
                await DisplayAlert("Соединение недоступно", "В настоящий момент сервер недоступен. " +
                "Повторите попытку позже.\n" +
                "Но вы всё ещё можете воспользоваться калькулятором :)", "OK");
                await Navigation.PushAsync(new CalculatorPage());
            }
            base.OnAppearing();
        }

        private async void goToCalculatorPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalculatorPage());
        }

        private void ShowTutorial(object sender, EventArgs e)
        {
            DisplayAlert("Обучение", "Статусы:\n" +
                "Белый - ожидание\n" +
                "Жёлтый - списать сегодня\n" +
                "Красный - срочно списать\n", "OK");
        }
    }
}
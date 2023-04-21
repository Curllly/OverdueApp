using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OverdueMobileDesktop
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalculatorPage : ContentPage
	{
		public CalculatorPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent ();
		}

        private async void goToListPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private void Calculate(object sender, EventArgs e)
        {
            result.IsVisible = true;
            try
            {
                DateTime manufactured = Emanufactured.Date;
                int value = int.Parse(Ebestbefore.Text);

                if (day.IsChecked) 
                {
                    result.Text = manufactured.AddDays(value).ToString("dd.MM.yyyy");
                    if (DateTime.Now > manufactured.AddDays(value))
                    {
                        result.Text += "\nПродукт просрочен";
                    }
                }
                else if (month.IsChecked)
                {
                    result.Text = manufactured.AddMonths(value).ToString("dd.MM.yyyy");
                    if (DateTime.Now > manufactured.AddMonths(value))
                    {
                        result.Text += "\nПродукт просрочен";
                    }
                }
                else if (year.IsChecked)
                {
                    result.Text = manufactured.AddYears(value).ToString("dd.MM.yyyy");
                    if (DateTime.Now > manufactured.AddYears(value))
                    {
                        result.Text += "\nПродукт просрочен";
                    }
                }
            }
            catch
            {
                DisplayAlert("Ошибка", "Поля заполнены неверно", "OK");
            }
        }
    }
}
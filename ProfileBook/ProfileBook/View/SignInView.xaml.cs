using ProfileBook.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInView : ContentPage
    {
        public SignInView()
        {
            InitializeComponent();
        }

        private async void CreateUser(object sender, EventArgs e)
        {
            User users = new User();
            SignUpView sign = new SignUpView();
            sign.BindingContext = users;
            await Navigation.PushAsync(sign);
        }


        void ExecuteNavigateCommand()
        {

        }
    }
}
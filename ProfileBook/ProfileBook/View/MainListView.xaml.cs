
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListView : ContentPage
    {
        public MainListView()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            usersList.ItemsSource = App.Database.GetItems();
            base.OnAppearing();
        }
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Registrate selectedFriend = (Registrate)e.SelectedItem;
            //MainListView mainPage = new MainListView();
            //mainPage.BindingContext = usersList;
            //await Navigation.PushAsync(mainPage);
        }
    }
}
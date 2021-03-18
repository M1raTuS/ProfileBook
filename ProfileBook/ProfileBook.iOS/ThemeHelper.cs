using ProfileBook;
using ProfileBook.Enum;
using ProfileBook.Interface;
using ProfileBook.Resources.Style;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProfileBook.iOS.ThemeHelper))]
namespace ProfileBook.iOS
{
    public class ThemeHelper : IAppTheme
    {
        public void SetAppTheme(Theme theme)
        {
            SetTheme(theme);
        }

        void SetTheme(Theme mode)
        {

            if (mode == Theme.Dark)
            {
                if (App.AppTheme == Theme.Dark)
                    return;

                App.Current.Resources = new DarkTheme();
            }
            else
            {
                if (App.AppTheme != Theme.Dark)
                    return;
                App.Current.Resources = new LightTheme();
            }
            App.AppTheme = mode;
        }

    }
}
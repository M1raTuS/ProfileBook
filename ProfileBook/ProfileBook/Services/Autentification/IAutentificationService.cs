namespace ProfileBook.Services.Autentification
{
    public interface IAutentificationService
    {
        bool SignIn(string Login, string Password);
        bool CheckLogin(string login);
    }
}

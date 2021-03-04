namespace ProfileBook.Services.Autorization
{
    public interface IAutorizationService
    {
        bool IsAutorized { get; }
        void Unauthorize();
        bool SignIn(string Login, string Password);
        int GetCurrentId { get; }
        int GetCurrentUserId();

    }
}

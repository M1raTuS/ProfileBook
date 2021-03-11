namespace ProfileBook.Services.Autorization
{
    public interface IAutorizationService
    {
        bool IsAutorized { get; set; }
        void Unauthorize();
        bool SignIn(string Login, string Password);
        int GetCurrentId { get; set; }
        int GetCurrentUserId();


    }
}

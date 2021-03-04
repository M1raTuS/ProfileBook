namespace ProfileBook.Services.Autorization
{
    public interface IAutorizationService
    {
        bool IsAutorized { get; }
        void Unauthorize();
        int GetCurrentUserId();
    }
}

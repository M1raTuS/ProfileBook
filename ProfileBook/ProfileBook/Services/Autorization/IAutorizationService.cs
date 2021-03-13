namespace ProfileBook.Services.Autorization
{
    public interface IAutorizationService
    {
        bool IsAutorized { get; set; }
        void Unauthorize();
        int GetCurrentId { get; set; }
        int GetCurrentUserId();
    }
}

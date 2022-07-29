namespace BookLibraryManagerBL.Auth
{
    public interface ITokenGenerator
    {
        string GenerateToken(string userName, string role);
    }
}
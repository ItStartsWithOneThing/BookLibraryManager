namespace BookLibraryManagerBL.Services.EncryptionService
{
    public interface IEncryptionService
    {
        string DecryptString(string cipherText);
        string EncryptString(string plainText);
    }
}
namespace BusinessSolutionsLayer.Services
{
    public interface ICrytpoService
    {
        string GetHash(string toHash);

        string GenerateSalt(int keyLength);
    }
}

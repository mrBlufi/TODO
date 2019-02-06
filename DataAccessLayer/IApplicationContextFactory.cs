namespace DataAccessLayer
{
    public interface IApplicationContextFactory
    {
        ApplicationContext GetContext();
    }
}
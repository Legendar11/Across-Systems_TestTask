namespace DatabaseRepository.Factories.ContextFactory
{
    public interface IRepositoryContextFactory
    {
        RepositoryContext CreateDbContext(string connectionString);
    }
}

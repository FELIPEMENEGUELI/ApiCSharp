
namespace MP.ApiDotNet6.Domain.Repositories
{
    public interface IUnitOffWork : IDisposable
    {
        Task BeginTransaction();
        Task Commit();
        Task RollBack();
        
    }
}

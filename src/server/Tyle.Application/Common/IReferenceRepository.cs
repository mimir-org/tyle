namespace Tyle.Application.Common;

public interface IReferenceRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(int id);
    Task<T> Create(T reference);
    Task Delete(int id);
}
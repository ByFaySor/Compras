using System.Linq.Expressions;

namespace Compra.Repositories;

public interface IGenericRepository<T, TId>
{
    Task<T> Insert(T entity);
    Task<T?> GetById(TId id);
    Task<IEnumerable<T>> GetAll();
    Task Update(T entity);
    Task Delete(T entity);
    /// <summary>
    /// This method takes <see cref="Expression{Func}"/> any expression. This method perform exist operation for condition. In additional returns <see cref="bool"/>
    /// </summary>
    /// <typeparam name="T">
    /// Type of entity
    /// </typeparam>
    /// <param name="anyExpression">
    /// Any expression <see cref="Expression{Func}"/>
    /// </param>
    /// <returns>
    /// Returns <see cref="bool"/>
    /// </returns>
    bool Any(Expression<Func<T, bool>> anyExpression);
}

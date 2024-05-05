namespace Compras.Services;

using Compras.Models;
using Compras.Models.DTOs;
using Compras.Repositories;

public class ShoppingService
{
    private readonly IShoppingRepository _repository;

    public ShoppingService(IShoppingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Shopping>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Shopping?> GetById(long id) => await _repository.GetById(id);

    public async Task<Shopping> Create(ShoppingCreateRequest entity)
    {
        var createEntity = new Shopping
        {
            Name = entity.Name,
            Price = entity.Price,
        };

        return await _repository.Insert(createEntity);
    }

    public async Task<Shopping?> Update(long id, ShoppingUpdateRequest entity)
    {
        var oldEntity = await _repository.GetById(id);

        if (oldEntity is not null)
        {
            oldEntity.Name = entity.Name;
            oldEntity.Price = entity.Price;

            await _repository.Update(oldEntity);
        }

        return oldEntity;
    }

    public async Task<Shopping?> Delete(long id)
    {
        var entity = await _repository.GetById(id);

        if (entity is not null)
        {
            await _repository.Delete(entity);
        }

        return entity;
    }

    public bool Any(long id) => _repository.Any(e => e.Id == id);
}

using AutoMapper;

using Compras.Models;
using Compras.Models.DTOs;

namespace compras.Mappers;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Shopping, ShoppingGetResponse>();
    }
}
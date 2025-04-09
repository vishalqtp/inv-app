using AutoMapper;
using InventoryAPI.Models;
using InventoryAPI.DTOs;

namespace InventoryAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping between InventoryItem model and InventoryItemDTO
            CreateMap<InventoryItem, InventoryItemDTO>().ReverseMap();
        }
    }
}

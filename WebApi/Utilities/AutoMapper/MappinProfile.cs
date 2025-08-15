using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappinProfile:Profile
    {
        public MappinProfile()
        {
            CreateMap<BookDtoForUpdate, Book>();
        }
    }
}

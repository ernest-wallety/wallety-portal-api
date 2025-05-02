using AutoMapper;
using Wallety.Portal.Application.Dto.User;
using Wallety.Portal.Application.Response;
using Wallety.Portal.Application.Response.Customer;
using Wallety.Portal.Application.Response.General;
using Wallety.Portal.Application.Response.Menu;
using Wallety.Portal.Application.Response.User;
using Wallety.Portal.Core.Entity;
using Wallety.Portal.Core.Entity.Customer;
using Wallety.Portal.Core.Entity.Menu;
using Wallety.Portal.Core.Entity.User;
using Wallety.Portal.Core.Requests.Common;
using Wallety.Portal.Core.Requests.User;
using Wallety.Portal.Core.Results;
using Wallety.Portal.Core.Specs;

namespace Wallety.Portal.Application.Mapper
{
    public class ConfigureMappingProfile : Profile
    {
        public ConfigureMappingProfile()
        {
            // Common
            CreateMap<UpdateResponse, UpdateRecordResult>().ReverseMap();
            CreateMap<CreateResponse, CreateRecordResult>().ReverseMap();
            CreateMap<DeleteResponse, DeleteRecordResult>().ReverseMap();



            // Lookups
            CreateMap<LookupModel, LookupResponse>().ReverseMap();
            CreateMap<DataList<LookupModel>, DataList<LookupResponse>>().ReverseMap();

            // Users
            CreateMap<UserEntity, UserResponse>().ReverseMap();
            CreateMap<UserRoleEntity, UserRoleResponse>().ReverseMap();
            CreateMap<Pagination<UserEntity>, Pagination<UserResponse>>().ReverseMap();

            CreateMap<PasswordResetDTO, PasswordResetModel>().ReverseMap();
            CreateMap<UserRoleUpdateDTO, UserRoleUpdateModel>().ReverseMap();


            // Menu
            CreateMap<MenuEntity, MenuResponse>().ReverseMap();
            CreateMap<DataList<MenuEntity>, DataList<MenuResponse>>().ReverseMap();

            CreateMap<MenuItemEntity, MenuItemResponse>().ReverseMap();
            CreateMap<DataList<MenuItemEntity>, DataList<MenuItemResponse>>().ReverseMap();

            // Transaction History
            CreateMap<TransactionHistoryEntity, TransactionHistoryResponse>().ReverseMap();
            CreateMap<DataList<TransactionHistoryEntity>, DataList<TransactionHistoryResponse>>().ReverseMap();
            CreateMap<Pagination<TransactionHistoryEntity>, Pagination<TransactionHistoryResponse>>().ReverseMap();


            // Customer
            CreateMap<Pagination<CustomerEntity>, Pagination<CustomerResponse>>().ReverseMap();
            CreateMap<CustomerEntity, CustomerResponse>().ReverseMap();

            CreateMap<Pagination<CustomerVerifyEntity>, Pagination<CustomerVerifyResponse>>().ReverseMap();
            CreateMap<CustomerVerifyEntity, CustomerVerifyResponse>().ReverseMap();

        }
    }
}

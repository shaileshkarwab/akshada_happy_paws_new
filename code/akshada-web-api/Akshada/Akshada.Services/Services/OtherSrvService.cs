using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Akshada.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class OtherSrvService : IOtherSrvService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public OtherSrvService(IUnitOfWork unitOfWork, IMapper mapper) { 
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public PagedList<DTO_OtherServicesOffered> GetAll(DTO_FilterAndPaging filterAndPaging)
        {
            var queryResult = this.unitOfWork.OtherServiceRepository.GetAllWithInclude(includeProperties: "Customer,Pet,ServiceOfferedUser,OtherServiceSystem").ApplyEqualityFilters(filterAndPaging.EqualityFilters);
            var pagedList = PagedList<OtherServicesOffered>.ToPagedList(queryResult,filterAndPaging.PageParameter.PageNumber,filterAndPaging.PageParameter.PageSize);
            var dtoList = this.mapper.Map<List<DTO_OtherServicesOffered>>(pagedList);
            return new PagedList<DTO_OtherServicesOffered>(dtoList,pagedList.Count(), pagedList.CurrentPage , pagedList.PageSize);
        }
    }
}

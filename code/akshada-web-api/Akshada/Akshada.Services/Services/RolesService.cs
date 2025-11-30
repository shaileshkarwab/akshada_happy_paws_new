using Akshada.DTO.Models;
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
    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public RolesService(IUnitOfWork unitOfWork,IMapper mapper) { 
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public List<DTO_LookUp> GetAllRoles()
        {
            var roles = this.unitOfWork.RolesRepository.Find(c => c.Status == true).OrderBy(c => c.RoleName).ToList();
            var response = this.mapper.Map<List<DTO_LookUp>>(roles);
            return response;
        }
    }
}

using Akshada.API.AuthFilter;
using Akshada.DTO.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Akshada.DTO.Enums.EnumHelper;

namespace Akshada.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(BasicAuthorization))]
    public class EnumController : BaseController
    {
        [HttpGet("get-enum/{typeOfEnum}")]
        public IActionResult GetEnums([FromRoute]Int32 typeOfEnum) {
            var list = new EnumList();
            switch(typeOfEnum)
            {
                case 1:
                    list = EnumHelper.EnumToJson<SystemParameterEnum>();
                    break;
                case 2:
                    list = EnumHelper.EnumToJson<PetWalkingSttus>();
                    break;
            }
            return SuccessResponse(list.EnumNamesValues);
        }
    }
}

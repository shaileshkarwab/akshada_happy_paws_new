using Akshada.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Akshada.API.Extensions
{
    public static class PaginationExtensions
    {
        public static void AddPaginationHeader<T>(this ControllerBase controller, PagedList<T> pagedResponse)
        {
            if (pagedResponse == null) return;

            var metadata = new
            {
                pagedResponse.TotalCount,
                pagedResponse.PageSize,
                pagedResponse.CurrentPage,
                pagedResponse.TotalPages,
                pagedResponse.HasNext,
                pagedResponse.HasPrevious
            };

            controller.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        }
    }
}

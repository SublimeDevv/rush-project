using Rush.Domain.DTO;
using System.Linq.Expressions;
using Rush.Domain.Common.ViewModels.Util;

namespace Rush.Application.Interfaces.Base
{
    public interface IServiceBase<T, TDto> where T : class where TDto : BaseDTO
    {
        Task<ResponseHelper> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        
        Task<ResponseHelper> InsertAsync(T entity);
        Task<ResponseHelper> UpdateAsync(T entity);
        Task<ResponseHelper> GetById(Expression<Func<T, bool>>? filter = null);
        Task<ResponseHelper> RemoveAsync(T entity);
        Task<ResponseHelper> RemoveAsync(Guid Id);
        Task<TDto> ConvertToDto(T entity);
        Task<T> ConvertToEntity(TDto dto);
        Task<List<TDto>> ConvertToDto(List<T> entity);
        Task<List<T>> ConvertToEntity(List<TDto> dto);
    }
}

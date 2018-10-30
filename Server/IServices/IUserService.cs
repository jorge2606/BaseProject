using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using server.Dto;
using server.ServiceResult;

namespace server.IServices
{

    public interface IUserService
    {
        Task<ServiceResult<UserDto>> Authenticate(string username, string password);
        Task UpdateAsync(MofidyUserCommingFromClientDto user);
        Task<ServiceResult<string>> CreateAsync(createUserDto user);
        void Delete(Guid id);
        Task<ServiceResult<UserDto>> Register(SaveUserDto model);
        Task UpdateUserRole(Guid idUser, Guid id);
        Task<ServiceResult<string>> ForgotPassword(ForgotPasswordDto model);
        Task<ServiceResult<string>> ResetPassword(ResetPassword model);
    }
    
}

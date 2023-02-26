using VKStore.ViewModels.Common;
using VKStore.ViewModels.System.Roles;

namespace VKStore.ApiIntergration
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}

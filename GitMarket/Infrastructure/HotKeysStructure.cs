using GitMarket.Domain.Models.APIResponseRequest;
using GitMarket.Domain.Models.TitiModels.ProductsModel;
using GitMarket.Infrastructure.APIs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitMarket.Infrastructure
{
    public static class HotKeysStructure
    {
        public static List<HotKeyModel?>? HotKeysDictionary;
        public static async Task<List<HotKeyModel?>> GetHotKeys()
        {
            return new List<HotKeyModel?>(await APIRequests.GetFromAPIAsync<HotKeyModel?>(
                    new RequestModelGet
                    {
                        parameter = "get",
                        shopId = Setts.Default.ShopId,
                        staffId = Setts.Default.StaffId,
                        page = 1,
                        pageSize = 1000,
                        data = null
                    }, "FC_HOTKEYS"));
        }
    }
}

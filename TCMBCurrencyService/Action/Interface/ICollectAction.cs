using System.Collections.Generic;
using TCMBCurrencyService.Model;

namespace TCMBCurrencyService.Action.Interface
{
    public interface ICollectAction
    {
        List<Currency> GetList();
    }
}
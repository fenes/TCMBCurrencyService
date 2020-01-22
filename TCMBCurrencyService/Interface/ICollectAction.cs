using System.Collections.Generic;
using TCMBCurrencyService.Model;

namespace TCMBCurrencyService.Interface
{
    public interface ICollectAction
    {
        string Export(ExportType exportType);
        List<Currency> GetList();
        Currency GetFirst();
    }
}
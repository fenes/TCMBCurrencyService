using System;
using System.Collections.Generic;
using TCMBCurrencyService.Model;

namespace TCMBCurrencyService.Interface
{
    public interface ICollectAction
    {
        List<Currency> GetList();
        Currency GetFirst();
    }
}
using System;
using TCMBCurrencyService.Model;

namespace TCMBCurrencyService.Interface
{
    public interface IFilterOperation
    {
        IFilterOperationAfter Filter(Func<Currency, bool> expression);
    }


    public interface IFilterOperationAfter : ICollectAction, IOrderOperation
    {
    }
}
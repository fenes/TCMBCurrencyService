using System;
using TCMBCurrencyService.Model;

namespace TCMBCurrencyService.Interface
{
    public interface IOrderOperation
    {
        IOrderOperationAfter Order(Func<Currency, object> expression, SortOrder sortOrder);
    }

    public interface IOrderOperationAfter : ICollectAction, IFilterOperation
    {
    }
}
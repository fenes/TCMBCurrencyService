using System;

namespace TCMBCurrencyService.Action.Interface
{
    public interface IAction
    {
        IActionAfter Initialize(DateTime? date = null);
    }

    public interface IActionAfter : IFilterOperation, IOrderOperation, ICollectAction
    {
    }
}
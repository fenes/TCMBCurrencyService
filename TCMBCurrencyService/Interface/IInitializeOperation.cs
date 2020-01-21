using System;

namespace TCMBCurrencyService.Interface
{
    public interface IInitializeOperation
    {
        IInitializeOperationAfter Initialize(DateTime? date = null);
    }

    public interface IInitializeOperationAfter : IFilterOperation, IOrderOperation, ICollectAction
    {
    }
}
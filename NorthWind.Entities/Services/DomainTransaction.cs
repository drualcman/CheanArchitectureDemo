using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NorthWind.Entities.Services
{
    public class DomainTransaction : IDomainTransaction
    {

        TransactionScope TransactionScope;

        public void BeginTransaction()
        {
            TransactionScope = new TransactionScope(
                TransactionScopeOption.Required, 
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted
                }, TransactionScopeAsyncFlowOption.Enabled);
        }

        public void CommitTransaction()
        {
            TransactionScope.Complete();
            TransactionScope.Dispose();
        }

        public void RollbackTransaction()
        {
            Dispose();
        }
        public void Dispose()
        {
            TransactionScope?.Dispose();
        }
    }
}

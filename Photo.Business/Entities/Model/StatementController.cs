using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;

namespace Photo.Business.Entities.Model
{
    public class StatementController : ControllerBase<StatementInfo, int>
    {
        #region Singleton

        public static StatementController Instance { get; private set; }

        #endregion


        #region Constructor

        static StatementController()
        {
            Instance = new StatementController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<StatementInfo>> GetAllFunc => DataProviderManager.Provider.GetAllStatements;

		public static int Save(StatementInfo statement)
		{
			IDbTransaction transaction = DataProviderManager.Provider.NewDataTransaction;
			IDbConnection connection = transaction.Connection;
			int statementId = 0;
			try
			{
				statementId = DataProviderManager.Provider.SaveStatement(statement, transaction);				
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
			finally
			{
				connection.Close();
			}
			return statementId;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;

namespace Photo.Business.Entities.Model
{
    public class ConversationController : ControllerBase<ConversationInfo, long>
    {
        #region Singleton

        public static ConversationController Instance { get; private set; }

        #endregion


        #region Constructor

        static ConversationController()
        {
            Instance = new ConversationController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<ConversationInfo>> GetAllFunc => DataProviderManager.Provider.GetAllConversations;

        public ConversationInfo GetByID(long id)
        {
            return DataProviderManager.Provider.GetByConversationID(id);
        }

        public IEnumerable<ConversationInfo> GetConversationsByBookingID(long bookingID)
        {
            return DataProviderManager.Provider.GetConversationsByBookingID(bookingID);
        }

        public static IEnumerable<SearchConversationInfo> Search(
            int pageSize,
            int pageNumber,
            int pageMaximumCount,
            string sortBy,
            string sortOrder,
            string systemReference,
            string customerName,
            string statuses,
            string email,
            DateTime? bookedStartDate,
            DateTime? bookedEndDate,
            int? productId,
            int? dealerId,
            string voucherCode,
            int? artistID)
        {
            return DataProviderManager.Provider.SearchConversations(
                false,
                pageSize,
                pageNumber,
                pageMaximumCount,
                sortBy,
                sortOrder,
                systemReference,
                customerName,
                statuses,
                email,
                bookedStartDate,
                bookedEndDate,
                productId,
                dealerId,
                voucherCode,
                artistID);
        }

        public static long Save(ConversationInfo conversation)
        {
            IDbTransaction transaction = DataProviderManager.Provider.NewDataTransaction;
            IDbConnection connection = transaction.Connection;
            long conversationId = 0;
            try
            {
                if (conversation.ID != 0)
                    conversationId = DataProviderManager.Provider.UpdateConversation(conversation, transaction);
                else
                    conversationId = DataProviderManager.Provider.SaveConversation(conversation, transaction);

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
            return conversationId;
        }
        
    }
}

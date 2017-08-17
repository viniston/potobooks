using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Photo.Business.DataProvider;
using Photo.Business.Entities.Configuration;
using Photo.Business.Entities.Model;
using Photo.Business.Entities.Model.Common.Currency;
using Photo.Business.Entities.Security;
using Photo.Utility.Dapper;
using Microsoft.ApplicationBlocks.Data;
using Photo.Business.Entities.Album;

namespace Photo.DataAccess.SQLDataProvider {
    /// <summary>
    /// SQL Data Provider class for the data access layer
    /// </summary>
    public class PhotoSqlDataProvider : DataProvider {
        #region Priavte properties

        private static string DataConnectionString => ConfigurationManager.AppSettings["PhotoConnectionString"];

        private static IDbConnection NewDataConnection {
            get {
                var connection = new SqlConnection(DataConnectionString);
                connection.Open();

                return connection;
            }
        }

        #endregion


        #region Public override

        public override void Initialize(string name, NameValueCollection config) {
            config["connectionString"] = ConfigurationManager.AppSettings["PhotoConnectionString"];
            base.Initialize(name, config);
        }

        #endregion


        #region Public properties

        public override IDbTransaction NewDataTransaction => NewDataConnection.BeginTransaction();

        #endregion


        #region Security


        #region Roles and Actions

        public override IEnumerable<UserActionInfo> GetAllUserActions(DateTime? lastUpdated) {
            using (var connection = NewDataConnection) {
                return connection.Query<UserActionInfo>("[dbo].[GetAllUserActions]", null,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<RoleInfo> GetAllRoles(DateTime? lastUpdated) {
            using (var connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<RoleInfo>("[dbo].[GetAllRoles]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<RoleInfo> GetRolesByUserID(Guid userId) {
            using (var connection = NewDataConnection) {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userId);

                return connection.Query<RoleInfo>("[dbo].[GetRolesByUserID]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        #endregion


        #region User

        /// <summary>
        /// Method to create new or update existing User
        /// </summary>
        /// <param name="user">UserInfo</param>
        /// <param name="transaction">IDbTransaction</param>
        public override void SaveUser(UserInfo user, IDbTransaction transaction) {
            object[] _params = { user.ID, user.FirstNameEN, user.LastNameEN, user.AdditionalDetails };

            var result = transaction != null
                ? SqlHelper.ExecuteNonQuery((SqlTransaction)transaction, "UserSave", _params)
                : SqlHelper.ExecuteNonQuery(DataConnectionString, "UserSave", _params);

            if (result < 1)
                throw new Exception("Database operation was not completed successfully");
        }

        /// <summary>
        /// Method to get User details
        /// </summary>
        /// <param name="userId">Guid</param>
        /// <returns>IDataReader</returns>
        public override List<UserInfo> UserGetByID(Guid userId) {
            using (var connection = NewDataConnection) {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", userId);

                return
                    connection.Query<UserInfo>("[dbo].[UserGetByID]", parameters,
                        commandType: CommandType.StoredProcedure).ToList();
            }
        }

        /// <summary>
        /// Returns user based on the user name
        /// </summary>
        /// <param name="userName">string</param>
        /// <returns>IDataReader</returns>
        public override List<UserInfo> UserGetByUserName(string userName) {
            using (var connection = NewDataConnection) {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", userName);

                return
                    connection.Query<UserInfo>("[dbo].[UserGetByUserName]", parameters,
                        commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public override List<UserInfo> UserSelectAllByUserType() {
            using (var connection = NewDataConnection) {
                var parameters = new DynamicParameters();
                return
                    connection.Query<UserInfo>("[dbo].[UserSelectAllByUserType]", parameters,
                        commandType: CommandType.StoredProcedure).ToList();
            }
        }

        #endregion


        #endregion


        #region Configuration

        public override IEnumerable<ConfigurationInfo> GetAllConfigurations(DateTime? lastUpdated) {
            using (var connection = NewDataConnection) {
                var parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<ConfigurationInfo>("[dbo].[GetAllConfigurations]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        #endregion


        #region Entities

        public override IEnumerable<CurrencyInfo> GetAllCurrencies(DateTime? lastUpdated) {
            using (var connection = NewDataConnection) {
                var parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<CurrencyInfo>("[dbo].[CurrencySelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<ArtistBookingPaymentInfo> GetArtistBookingPaymentDetails(int artistId) {
            using (var connection = NewDataConnection) {
                var parameters = new DynamicParameters();
                parameters.Add("@ArtistID", artistId);

                return connection.Query<ArtistBookingPaymentInfo>("[dbo].[GetArtistBookingPaymentDetails]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<CategoryInfo> GetAllCategories(DateTime? lastUpdated) {
            using (var connection = NewDataConnection) {
                var parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<CategoryInfo>("[dbo].[CategorySelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<VoucherCodeInfo> GetAllVoucherCodes(DateTime? lastUpdated) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<VoucherCodeInfo>("[dbo].[VoucherCodeSelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<ProductCategoryInfo> GetAllProductCategories(DateTime? lastUpdated) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<ProductCategoryInfo>("[dbo].[ProductCategorySelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<DealerInfo> GetAllDealers(DateTime? lastUpdated) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<DealerInfo>("[dbo].[DealerSelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<ArtistInfo> GetAllArtists(DateTime? lastUpdated) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<ArtistInfo>("[dbo].[ArtistSelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<ImageInfo> GetAllImages(DateTime? lastUpdated) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<ImageInfo>("[dbo].[ImageSelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<AlbumImageInfo> GetAllAlbumImages(long albumId) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AlbumID", albumId);

                return connection.Query<AlbumImageInfo>("[dbo].[GetAllAlbumImages]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<ArtistFareInfo> GetAllArtistFares(DateTime? lastUpdated) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<ArtistFareInfo>("[dbo].[ArtistFareSelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<ProductInfo> GetAllProducts(DateTime? lastUpdated) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<ProductInfo>("[dbo].[ProductSelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<BookingInfo> GetAllBookings(DateTime? lastUpdated) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<BookingInfo>("[dbo].[BookingSelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<ConversationInfo> GetAllConversations(DateTime? lastUpdated) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<ConversationInfo>("[dbo].[ConversationSelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override ConversationInfo GetByConversationID(long id) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", id);

                return
                    connection.Query<ConversationInfo>("[dbo].[GetByConversationID]", parameters,
                        commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public override IEnumerable<ConversationInfo> GetConversationsByBookingID(long bookingId) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BookingID", bookingId);

                return connection.Query<ConversationInfo>("[dbo].[GetConversationsByBookingID]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override long UpdateConversation(ConversationInfo conversation, IDbTransaction transaction) {
            object[] _params =
            {
                conversation.ID,
                conversation.BookingID,
                conversation.IsCustomerQuery,
                conversation.UpdatedBy,
                conversation.Text,
                conversation.IsActive
            };
            var result = SqlHelper.ExecuteNonQuery((SqlTransaction)transaction, "ConversationUpdate", _params);

            if (result < 1)
                throw new Exception("Database operation was not completed successfully");

            return conversation.ID;
        }

        public override long SaveConversation(ConversationInfo conversation, IDbTransaction transaction) {
            var sqlParam = new DynamicParameters();
            sqlParam.Add("@BookingID", conversation.BookingID);
            sqlParam.Add("@IsCustomerQuery", conversation.IsCustomerQuery);
            sqlParam.Add("@Resolved", conversation.Resolved);
            sqlParam.Add("@UpdatedBy", conversation.UpdatedBy);
            sqlParam.Add("@Text", conversation.Text);

            sqlParam.Add("@IdentityReturn", dbType: DbType.Int64, direction: ParameterDirection.Output);

            transaction.Connection.Execute("[dbo].[ConversationInsert]", sqlParam, transaction, null,
                CommandType.StoredProcedure);

            return sqlParam.Get<long>("IdentityReturn");
        }

        public override IEnumerable<StatementInfo> GetAllStatements(DateTime? lastUpdated) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<StatementInfo>("[dbo].[StatementSelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<PaymentInfo> GetAllPayments(DateTime? lastUpdated) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Updated", lastUpdated);

                return connection.Query<PaymentInfo>("[dbo].[PaymentSelectAll]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<BookingInfo> GetBookingsByEmail(string email) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Email", email);

                return connection.Query<BookingInfo>("[dbo].[GetBookingsByEmail]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<BookingInfo> GetByVoucherCode(string voucherCode) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@VoucherCode", voucherCode);

                return connection.Query<BookingInfo>("[dbo].[GetBookingByVoucherCode]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<VoucherCodeInfo> CheckVoucherCodeByCodeAndDealer(string voucherCode, int dealerId) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@VoucherCode", voucherCode);
                parameters.Add("@DealerID", dealerId);

                return connection.Query<VoucherCodeInfo>("[dbo].[CheckVoucherCodeByCodeAndDealer]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override bool RedeemVoucherCode(long voucherId, string securityCode) {
            object[] _params =
            {
                voucherId,
                securityCode
            };
            var result = SqlHelper.ExecuteNonQuery(DataConnectionString, "RedeemVoucherCode", _params);

            return result == 1;
        }

        public override IEnumerable<BookingInfo> GetBySystemReference(string systemReference) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@SystemReference", systemReference);

                return connection.Query<BookingInfo>("[dbo].[GetBookingBySystemReference]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override long UpdateArtist(ArtistInfo artist) {
            var sqlParam = new DynamicParameters();
            sqlParam.Add("@ID", artist.ID);
            sqlParam.Add("@Email", artist.Email);
            sqlParam.Add("@Name", artist.Name);
            sqlParam.Add("@Token", artist.Token);
            sqlParam.Add("@Updated", artist.Updated);

            using (IDbConnection connection = NewDataConnection) {
                connection.Execute("[dbo].[ArtistUpdate]", sqlParam, null, null,
                    CommandType.StoredProcedure);
            }
            return artist.ID;
        }

        public override IEnumerable<ArtistInfo> GetArtistByToken(string token) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Token", token);

                return connection.Query<ArtistInfo>("[dbo].[GetArtistByToken]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<BookingInfo> GetBookingsByArtist(string email) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Email", email);

                return connection.Query<BookingInfo>("[dbo].[GetBookingsByArtist]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<BookingInfo> GetByID(long id) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", id);

                return connection.Query<BookingInfo>("[dbo].[GetBookingByID]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override long SavePayment(PaymentInfo payment, IDbTransaction transaction) {
            var sqlParam = new DynamicParameters();
            sqlParam.Add("@BookingID", payment.BookingID);
            sqlParam.Add("@Description", payment.Description);
            sqlParam.Add("@Amount", payment.Amount);
            sqlParam.Add("@ProcessingCost");
            sqlParam.Add("@AmountCurrencyID", payment.AmountObject.CurrencyID);
            sqlParam.Add("@PaymentMethodID", payment.PaymentMethodID);
            sqlParam.Add("@PaymentTypeID");
            sqlParam.Add("@PaymentStatusID", payment.PaymentStatusID);
            sqlParam.Add("@ProviderProfileID");
            sqlParam.Add("@ProviderReference", payment.ProviderReference);
            sqlParam.Add("@ProviderReply", payment.ProviderReply);
            sqlParam.Add("@IPAddress");
            sqlParam.Add("@ForwardedForIP");
            sqlParam.Add("@PaymentDate");
            sqlParam.Add("@CardNumberPrefix");
            sqlParam.Add("@CardNumberPostfix");

            sqlParam.Add("@IdentityReturn", dbType: DbType.Int64, direction: ParameterDirection.Output);
            transaction.Connection.Execute("[dbo].[PaymentInsert]", sqlParam, transaction, null,
                CommandType.StoredProcedure);

            return sqlParam.Get<long>("IdentityReturn");
        }

        public override int SaveStatement(StatementInfo statement, IDbTransaction transaction) {
            var sqlParam = new DynamicParameters();
            sqlParam.Add("@Date", statement.Date);
            sqlParam.Add("@ArtistID", statement.ArtistID);
            sqlParam.Add("@Amount", statement.Amount);
            sqlParam.Add("@BookingIDS", statement.BookingIDS);
            sqlParam.Add("@Remark", statement.Remark);
            sqlParam.Add("@UpdatedBy", statement.UpdatedBy);

            sqlParam.Add("@IdentityReturn", dbType: DbType.Int32, direction: ParameterDirection.Output);

            transaction.Connection.Execute("[dbo].[StatementInsert]", sqlParam, transaction, null,
                CommandType.StoredProcedure);

            return sqlParam.Get<int>("IdentityReturn");
        }

        public override long SaveBooking(BookingInfo booking, IDbTransaction transaction) {
            var sqlParam = new DynamicParameters();
            sqlParam.Add("@PurchaseID", booking.PurchaseID);
            sqlParam.Add("@ProductCategoryID", booking.ProductCategoryID);
            sqlParam.Add("@Email", booking.Email);
            sqlParam.Add("@Remarks", booking.Remarks);
            sqlParam.Add("@DealerID", booking.DealerID);
            sqlParam.Add("@VoucherCode", booking.VoucherCode);
            sqlParam.Add("@StatusID", booking.StatusID);
            sqlParam.Add("@FirstName", booking.FirstName);
            sqlParam.Add("@LastName", booking.LastName);
            sqlParam.Add("@UpdatedBy", booking.UpdatedBy);

            sqlParam.Add("@IdentityReturn", dbType: DbType.Int64, direction: ParameterDirection.Output);

            transaction.Connection.Execute("[dbo].[BookingInsert]", sqlParam, transaction, null,
                CommandType.StoredProcedure);

            return sqlParam.Get<long>("IdentityReturn");
        }

        public override long SaveImage(ImageInfo image, IDbTransaction transaction) {
            var sqlParam = new DynamicParameters();
            sqlParam.Add("@Path", image.Path);
            sqlParam.Add("@TypeID", image.TypeID);
            sqlParam.Add("@BookingID", image.BookingID);
            sqlParam.Add("@IdentityReturn", dbType: DbType.Int64, direction: ParameterDirection.Output);

            if (transaction != null)
                transaction.Connection.Execute("[dbo].[ImageInsert]", sqlParam, transaction, null,
                    CommandType.StoredProcedure);
            else {
                using (IDbConnection connection = NewDataConnection) {
                    connection.Execute("[dbo].[ImageInsert]", sqlParam, null, null, CommandType.StoredProcedure);
                }
            }
            return sqlParam.Get<long>("IdentityReturn");
        }

        public override long UpdateBooking(BookingInfo booking, IDbTransaction transaction) {
            object[] _params =
            {
                booking.ID,
                booking.SystemReference,
                booking.StatusID,
                booking.ArtistID,
                booking.UpdatedBy,
                booking.IsPaidToArtist,
                booking.IsActive
            };
            var result = SqlHelper.ExecuteNonQuery(DataConnectionString, "BookingUpdate", _params);

            if (result < 1)
                throw new Exception("Database operation was not completed successfully");

            return booking.ID;
        }

        public override long UpdatePayment(PaymentInfo payment, IDbTransaction transaction) {
            object[] _params =
            {
                payment.ID,
                payment.PaymentStatusID
            };
            var result = SqlHelper.ExecuteNonQuery((SqlTransaction)transaction, "PaymentUpdate", _params);

            if (result < 1)
                throw new Exception("Database operation was not completed successfully");

            return payment.ID;
        }

        public override bool DeleteImageByID(long imageId) {
            int result;
            object[] _params =
            {
                imageId
            };
            using (IDbConnection connection = NewDataConnection) {
                result = SqlHelper.ExecuteNonQuery((SqlConnection)connection, "DeleteImageByID", _params);
            }

            return result >= 1;
        }

        public override IEnumerable<SearchBookingInfo> SearchBookings(
            bool countMode,
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
            int? productCategoryId,
            int? dealerId,
            string voucherCode,
            int? artistId) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@IsCountMode", countMode);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageMaximumCount", pageMaximumCount);
                parameters.Add("@SortBy", sortBy);
                parameters.Add("@SortOrder", sortOrder);
                parameters.Add("@SystemReference", systemReference);
                parameters.Add("@CustomerName", customerName);
                parameters.Add("@Statuses", statuses);
                parameters.Add("@Email", email);
                parameters.Add("@BookedStartDate", bookedStartDate);
                parameters.Add("@BookedEndDate", bookedEndDate);
                parameters.Add("@ProductCategoryID", productCategoryId);
                parameters.Add("@DealerID", dealerId);
                parameters.Add("@VoucherCode", voucherCode);
                parameters.Add("@ArtistID", artistId);

                return connection.Query<SearchBookingInfo>("[dbo].[SearchBookings]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<SearchConversationInfo> SearchConversations(
            bool countMode,
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
            int? artistId) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@IsCountMode", countMode);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageMaximumCount", pageMaximumCount);
                parameters.Add("@SortBy", sortBy);
                parameters.Add("@SortOrder", sortOrder);
                parameters.Add("@SystemReference", systemReference);
                parameters.Add("@CustomerName", customerName);
                parameters.Add("@Statuses", statuses);
                parameters.Add("@Email", email);
                parameters.Add("@BookedStartDate", bookedStartDate);
                parameters.Add("@BookedEndDate", bookedEndDate);
                parameters.Add("@ProductCategoryID", productId);
                parameters.Add("@DealerID", dealerId);
                parameters.Add("@VoucherCode", voucherCode);
                parameters.Add("@ArtistID", artistId);

                return connection.Query<SearchConversationInfo>("[dbo].[SearchConversations]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override IEnumerable<ImageInfo> GetImageByBookingID(long id) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BookingID", id);

                return connection.Query<ImageInfo>("[dbo].[GetImageByBookingID]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override AlbumInfo GetAlbumByID(long id) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AlbumID", id);

                return connection.Query<AlbumInfo>("[dbo].[GetAlbumByID]", parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public override IEnumerable<AlbumInfo> GetAllAlbums()
        {
            using (IDbConnection connection = NewDataConnection)
            {
                return connection.Query<AlbumInfo>("[dbo].[GetAllAlbums]", null,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override AlbumImageInfo GetAlbumImageByID(long id) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", id);

                return connection.Query<AlbumImageInfo>("[dbo].[GetAlbumImageByID]", parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public override IEnumerable<AlbumImageInfo> GetIAlbumImageByAlbumID(long albumId) {
            using (IDbConnection connection = NewDataConnection) {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AlbumID", albumId);

                return connection.Query<AlbumImageInfo>("[dbo].[GetAllAlbumImageByAlbumID]", parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override long SaveAlbumImage(AlbumImageInfo image, IDbTransaction transaction) {
            var sqlParam = new DynamicParameters();
            sqlParam.Add("@AlbumImagePath", image.AlbumImagePath);
            sqlParam.Add("@AlbumImageName", image.AlbumImageName);
            sqlParam.Add("@AlbumImageDescription", image.AlbumImageDescription);
            sqlParam.Add("@ImageTypeId", image.ImageTypeId);
            sqlParam.Add("@UploadedBy", image.UploadedBy);
            sqlParam.Add("@IsActive", image.IsActive);
            sqlParam.Add("@StatusId", image.StatusId);
            sqlParam.Add("@IdentityReturn", dbType: DbType.Int64, direction: ParameterDirection.Output);

            if (transaction != null)
                transaction.Connection.Execute("[dbo].[AlbumImageInsert]", sqlParam, transaction, null,
                    CommandType.StoredProcedure);
            else {
                using (IDbConnection connection = NewDataConnection) {
                    connection.Execute("[dbo].[AlbumImageInsert]", sqlParam, null, null, CommandType.StoredProcedure);
                }
            }
            return sqlParam.Get<long>("IdentityReturn");
        }


        public override long SaveAlbum(AlbumInfo image, IDbTransaction transaction) {
            var sqlParam = new DynamicParameters();
            sqlParam.Add("@AlbumType", image.AlbumType);
            sqlParam.Add("@AlbumName", image.AlbumName);
            sqlParam.Add("@UploadedBy", image.UploadedBy);
            sqlParam.Add("@AlbumDescription", image.AlbumDescription);
            sqlParam.Add("@AlbumImagePath", image.AlbumImagePath);
            sqlParam.Add("@IsActive", image.IsActive);
            sqlParam.Add("@StatusId", image.StatusId);
            sqlParam.Add("@IdentityReturn", dbType: DbType.Int64, direction: ParameterDirection.Output);

            if (transaction != null)
                transaction.Connection.Execute("[dbo].[AlbumInsert]", sqlParam, transaction, null,
                    CommandType.StoredProcedure);
            else {
                using (IDbConnection connection = NewDataConnection) {
                    connection.Execute("[dbo].[AlbumInsert]", sqlParam, null, null, CommandType.StoredProcedure);
                }
            }
            return sqlParam.Get<long>("IdentityReturn");
        }

        #endregion
    }
}
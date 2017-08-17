using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Data;
using Photo.Business.Entities.Configuration;
using Photo.Business.Entities.Security;
using Photo.Business.Entities.Model;
using Photo.Business.Entities.Model.Common.Currency;

namespace Photo.Business.DataProvider
{
	/// <summary>
	/// An abstract class for the data access layer
	/// </summary>
	public abstract class DataProvider : ProviderBase
	{
		#region Transaction management

		public abstract IDbTransaction NewDataTransaction { get; }

		#endregion


		#region Security

		#region Roles and Actions

		public abstract IEnumerable<UserActionInfo> GetAllUserActions(DateTime? lastUpdated);

		public abstract IEnumerable<StatementInfo> GetAllStatements(DateTime? lastUpdated);

		public abstract IEnumerable<VoucherCodeInfo> GetAllVoucherCodes(DateTime? lastUpdated);
		
		public abstract IEnumerable<RoleInfo> GetAllRoles(DateTime? lastUpdated);

		public abstract int SaveStatement(StatementInfo statement, IDbTransaction transaction);

		public abstract IEnumerable<RoleInfo> GetRolesByUserID(Guid userId);

		public abstract IEnumerable<VoucherCodeInfo> CheckVoucherCodeByCodeAndDealer(string voucherCode, int dealerId);

		public abstract bool RedeemVoucherCode(long voucherId, string securityCode);

		public abstract IEnumerable<ArtistInfo> GetArtistByToken(string token);

		public abstract long UpdateArtist(ArtistInfo artist);

		public abstract ConversationInfo GetByConversationID(long id);

        public abstract IEnumerable<ConversationInfo> GetConversationsByBookingID(long bookingId);

        public abstract long UpdateConversation(ConversationInfo conversation, IDbTransaction transaction);

        public abstract long SaveConversation(ConversationInfo conversation, IDbTransaction transaction);

        #endregion


        #region User

        public abstract void SaveUser(UserInfo user, IDbTransaction transaction);

        public abstract IEnumerable<BookingInfo> GetBookingsByEmail(string email);

        public abstract long SaveBooking(BookingInfo booking, IDbTransaction transaction);

		public abstract bool DeleteImageByID(long imageId);

        public abstract long UpdateBooking(BookingInfo booking, IDbTransaction transaction);

        public abstract long SavePayment(PaymentInfo payment, IDbTransaction transaction);

        public abstract List<UserInfo> UserGetByID(Guid userId);

		public abstract List<UserInfo> UserGetByUserName(string userName);

        public abstract long UpdatePayment(PaymentInfo payment, IDbTransaction transaction);

        public abstract List<UserInfo> UserSelectAllByUserType();

        public abstract IEnumerable<ArtistFareInfo> GetAllArtistFares(DateTime? lastUpdated);

        #endregion


        #endregion


        #region Configuration

        public abstract IEnumerable<ConfigurationInfo> GetAllConfigurations(DateTime? lastUpdated);

		#endregion


		#region Entities

		public abstract IEnumerable<CurrencyInfo> GetAllCurrencies(DateTime? lastUpdated);

		public abstract IEnumerable<ArtistBookingPaymentInfo> GetArtistBookingPaymentDetails(int artistId);

		public abstract IEnumerable<CategoryInfo> GetAllCategories(DateTime? lastUpdated);

		public abstract IEnumerable<ProductCategoryInfo> GetAllProductCategories(DateTime? lastUpdated);

		public abstract IEnumerable<DealerInfo> GetAllDealers(DateTime? lastUpdated);

		public abstract IEnumerable<ArtistInfo> GetAllArtists(DateTime? lastUpdated);

		public abstract IEnumerable<ImageInfo> GetAllImages(DateTime? lastUpdated);
        
        public abstract IEnumerable<AlbumImageInfo> GetAllAlbumImages(long albumId);

        public abstract IEnumerable<ProductInfo> GetAllProducts(DateTime? lastUpdated);

        public abstract IEnumerable<BookingInfo> GetAllBookings(DateTime? lastUpdated);

        public abstract IEnumerable<ConversationInfo> GetAllConversations(DateTime? lastUpdated);

        public abstract IEnumerable<PaymentInfo> GetAllPayments(DateTime? lastUpdated);

        public abstract IEnumerable<BookingInfo> GetByVoucherCode(string vouherCode);

        public abstract IEnumerable<BookingInfo> GetBySystemReference(string systemReference);

		public abstract IEnumerable<BookingInfo> GetBookingsByArtist(string email);

		public abstract IEnumerable<SearchBookingInfo> SearchBookings(
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
                int? artistId);

        public abstract IEnumerable<SearchConversationInfo> SearchConversations(
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
                int? artistId);

        public abstract long SaveImage(ImageInfo image, IDbTransaction transation);

        public abstract IEnumerable<BookingInfo> GetByID(long id);

        public abstract IEnumerable<ImageInfo> GetImageByBookingID(long id);


	    public abstract IEnumerable<AlbumInfo> GetAllAlbums();

        public abstract AlbumInfo GetAlbumByID(long id);

        public abstract AlbumImageInfo GetAlbumImageByID(long id);

        public abstract IEnumerable<AlbumImageInfo> GetIAlbumImageByAlbumID(long id);

        public abstract long SaveAlbumImage(AlbumImageInfo image, IDbTransaction transation);

        public abstract long SaveAlbum(AlbumInfo album, IDbTransaction transation);


        #endregion
    }
}

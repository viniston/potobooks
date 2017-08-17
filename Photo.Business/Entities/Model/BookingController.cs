using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Photo.Business.Entities.Model
{
    public class BookingController : ControllerBase<BookingInfo, long>
    {
        #region Singleton

        public static BookingController Instance { get; private set; }

        #endregion


        #region Constructor

        static BookingController()
        {
            Instance = new BookingController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<BookingInfo>> GetAllFunc => DataProviderManager.Provider.GetAllBookings;

        public BookingInfo GetByID(long id)
        {
            return DataProviderManager.Provider.GetByID(id).FirstOrDefault();
        }

        public BookingInfo GetByVoucherCode(string voucherCode)
        {
            return DataProviderManager.Provider.GetByVoucherCode(voucherCode).FirstOrDefault();
        }

        public BookingInfo GetBySystemReference(string systemReference)
        {
            return DataProviderManager.Provider.GetBySystemReference(systemReference).FirstOrDefault();
        }

        public IEnumerable<BookingInfo> GetBookingsByEmail(string email)
        {
            return DataProviderManager.Provider.GetBookingsByEmail(email);
        }

		public IEnumerable<BookingInfo> GetBookingsByArtist(string email)
		{
			return DataProviderManager.Provider.GetBookingsByArtist(email);
		}

		public static IEnumerable<ArtistBookingPaymentInfo> GetArtistBookingPaymentDetails(int artistID)
		{
			return DataProviderManager.Provider.GetArtistBookingPaymentDetails(artistID);
		}

		public static IEnumerable<SearchBookingInfo> Search(
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
            return DataProviderManager.Provider.SearchBookings(
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

        public static long Save(BookingInfo booking, out long paymentId, out List<long> imageIds)
        {
            IDbTransaction transaction = DataProviderManager.Provider.NewDataTransaction;
            IDbConnection connection = transaction.Connection;
            long bookingId = 0;
            try
            {
                paymentId = 0;
                imageIds = new List<long>();

                if (booking.ID != 0)
                    bookingId = DataProviderManager.Provider.UpdateBooking(booking, transaction);
                else
                    bookingId = DataProviderManager.Provider.SaveBooking(booking, transaction);

                booking.SystemReference = "CK" + bookingId;

                foreach (PaymentInfo payment in booking.PaymentList)
                {
                    payment.BookingID = bookingId;
                    if (payment.ID != 0)
                    {
                        DataProviderManager.Provider.UpdatePayment(payment, transaction);
                        paymentId = payment.ID;
                    }
                    else
                        paymentId = DataProviderManager.Provider.SavePayment(payment, transaction);
                }

                foreach (ImageInfo image in booking.ImageList)
                {
                    image.BookingID = bookingId;
                    if (image.ID == 0)
                        imageIds.Add(DataProviderManager.Provider.SaveImage(image, transaction));
                }
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
            return bookingId;
        }
    }
}

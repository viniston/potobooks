using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo.Business.Entities.Model {
    public enum PaymentStatus {
        Requested = 0,
        Completed = 1,
        Failed = 2,
        Errors = 3,
        Canceled = 4,
        PendingSettlement = 5
    }

    public enum PaymentMethod {
        CreditCard = 0,
        Direct = 1,
        Cash = 2,
        Cheque = 3,
        NeBanking = 4,
        PayPal = 5
    }

    public enum ImageType {
        Original = 1,
        Draft = 2,
        Final = 3
    }

    public enum BookingStatus {
        New = 0,
        RepliedToCustomer = 1,
        ForwardedToArtist = 2,
        DraftReceivedFromArtist = 3,
        DraftSentToCS = 4,
        DraftFeedbackFromCstoArtist = 5,
        DraftCsApproved = 6,
        Completed = 7
    }

    public enum AlbumType
    {
        SoftCover = 1,
        HardCover
    }
}

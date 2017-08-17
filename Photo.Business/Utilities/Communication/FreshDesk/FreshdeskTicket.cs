using System.Collections.Generic;
using Photo.Business.Utilities.Communication;

namespace Photo.Business.Utilities.Communication
{
    /// <summary>
    /// Summary description for FreshdeskTicketFields
    /// </summary>
    public class FreshdeskTicket
    {
        #region Private Properties

        private string _name;
        private string _email;
        private string _contactNumber;
        private string _ckReferenceNumber;
        private string _url;
        private List<string> _ccEmails;
        private string _descriptionHTML;
        private string _subject;
        private FreshdeskTicketPriority _priority;
        private FreshdeskTicketStatus _status;
        private string _toEmail;
        private string _tripType;
        private FreshdeskTicketSourceInternal _sourceInternal;
        private FreshdeskTicketSource? _source;
        private string _ticketType;

        #endregion


        #region Public Properties

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// Contact number
        /// </summary>
        public string ContactNumber
        {
            get { return _contactNumber; }
            set { _contactNumber = value; }
        }

        /// <summary>
        /// System reference
        /// </summary>
        public string CKReferenceNumber
        {
            get { return _ckReferenceNumber; }
            set { _ckReferenceNumber = value; }
        }

        /// <summary>
        /// URL
        /// </summary>
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// CC Email
        /// </summary>
        public List<string> CcEmails
        {
            get { return _ccEmails; }
            set { _ccEmails = value; }
        }

        /// <summary>
        /// Mail HTML
        /// </summary>
        public string DescriptionHTML
        {
            get { return _descriptionHTML; }
            set { _descriptionHTML = value; }
        }

        /// <summary>
        /// Subject
        /// </summary>
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        /// <summary>
        /// Priority
        /// </summary>
        public FreshdeskTicketPriority Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        /// <summary>
        /// Status
        /// </summary>
        public FreshdeskTicketStatus Staus
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// To Email Address
        /// </summary>
        public string ToEmail
        {
            get { return _toEmail; }
            set { _toEmail = value; }
        }

        /// <summary>
        /// Trip type
        /// </summary>
        public string TripType
        {
            get { return _tripType; }
            set { _tripType = value; }
        }

        /// <summary>
        /// Source
        /// </summary>
        public FreshdeskTicketSourceInternal SourceInternal
        {
            get { return _sourceInternal; }
            set { _sourceInternal = value; }
        }

        /// <summary>
        /// Source
        /// </summary>
        public FreshdeskTicketSource? Source
        {
            get { return _source; }
            set { _source = value; }
        }

        /// <summary>
        /// Ticket type
        /// </summary>
        public string TicketType
        {
            get { return _ticketType; }
            set { _ticketType = value; }
        }

        #endregion
    }
}
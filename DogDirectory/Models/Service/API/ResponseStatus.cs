
namespace DogDirectory.Models.Service.API
{
    /// <summary>
    /// An object for storing a responses status
    /// </summary>
    public class ResponseStatus
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ResponseStatus()
        {
            SetStatus(Status.Unknown);
        }

        /// <summary>
        /// Sets response status
        /// </summary>
        /// <param name="status">Status of response</param>
        public ResponseStatus(Status status)
        {
            SetStatus(status);
        }

        #endregion

        #region Public Attributes

        /// <summary>
        /// Enumerable for the types of response statuses Warning: CriticallyInvalid type indicates that the response is unusabled
        /// </summary>
        public enum Status { Success, Error, Unknown, CriticallyInvalid }
        
        /// <summary>
        /// A string representation of the status
        /// </summary>
        public string StatusString { get { return _statusString; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the status
        /// </summary>
        /// <returns>This objects status</returns>
        public Status GetStatus()
        {
            return _status;
        }

        /// <summary>
        /// Sets this objects status and status string
        /// </summary>
        /// <param name="status">New status</param>
        public void SetStatus(Status status)
        {
            _status = status;

            switch (status)
            {
                case Status.Success:
                    _statusString = "success";
                    break;
                case Status.Error:
                    _statusString = "error";
                    break;
                case Status.CriticallyInvalid:
                    _statusString = "critically-invalid";
                    break;
                case Status.Unknown:
                default:
                    _statusString = "unknown";
                    break;
            }
        }

        #endregion

        #region Private Attributes

        /// <summary>
        /// Status string representation of this status
        /// </summary>
        private string _statusString;

        /// <summary>
        /// Status value of this object
        /// </summary>
        private Status _status;
        
        #endregion

    }
}
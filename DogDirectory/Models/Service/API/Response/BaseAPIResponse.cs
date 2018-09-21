using System;
using System.Net;

namespace DogDirectory.Models.Service.API.Response
{
    public class BaseApiResponse: IAPIResponse
    {
        /// <summary>
        /// Base Api response returned from api containing all data that must be present
        /// </summary>
        /// <param name="rawResponse"></param>
        /// <param name="responseBody"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        public BaseApiResponse(HttpWebResponse rawResponse, Object responseBody, ResponseStatus status, Object message)
        {
            _rawResponse = rawResponse;
            _rawResponseBody = responseBody;
            Status = status;
            Message = message;
        }

        /// <summary>
        /// Exact Http response returned by request
        /// </summary>
        public HttpWebResponse RawResponse { get { return _rawResponse; } }
        
        /// <summary>
        /// Response body content of the response
        /// </summary>
        public Object RawResponseBody { get { return _rawResponseBody; } }

        /// <summary>
        /// The status returned in the API response
        /// </summary>
        public ResponseStatus Status { get; set; }

        /// <summary>
        /// The raw message object returned by the API
        /// </summary>
        public Object Message { get; set; }

        /// <summary>
        /// Internal raw response
        /// </summary>
        private HttpWebResponse _rawResponse;

        /// <summary>
        /// Internal raw response body
        /// </summary>
        private Object _rawResponseBody;
    }
}
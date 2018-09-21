using System;
using System.Net;
using DogDirectory.Models.Data;

namespace DogDirectory.Models.Service.API.Response
{
    public class ImageAPIResponse : BaseApiResponse
    {
        /// <summary>
        /// Constructor used if an invalid response is returned by the api factory
        /// </summary>
        /// <param name="baseApiResponse">Base API Response</param>
        public ImageAPIResponse(BaseApiResponse baseApiResponse) : base(baseApiResponse.RawResponse, baseApiResponse.RawResponseBody, baseApiResponse.Status, baseApiResponse.Message)
        {
            if(baseApiResponse.Status.GetStatus() != ResponseStatus.Status.CriticallyInvalid)
                Status.SetStatus(ResponseStatus.Status.Error);

            _image = new Image("");
        }

        /// <summary>
        /// Main constructor used by the API Factory for creation
        /// </summary>
        /// <param name="rawResponse">Raw response object</param>
        /// <param name="responseBody">Response body taken from response object</param>
        /// <param name="status">Status of response</param>
        /// <param name="message">Message as returned by API</param>
        /// <param name="sourceString">Image source url</param>
        public ImageAPIResponse(HttpWebResponse rawResponse, Object responseBody, ResponseStatus status, Object message, string sourceString) : base(rawResponse, responseBody, status, message)
        {
            _image = new Image();
            _image.SourceString = sourceString;
        }

        /// <summary>
        /// Public Image to be returned
        /// </summary>
        public Image Image { get { return _image; } }


        /// <summary>
        /// Private Image to be used internally
        /// </summary>
        private Image _image;
    }
}
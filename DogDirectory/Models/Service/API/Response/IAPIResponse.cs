using System;
using System.Net;

namespace DogDirectory.Models.Service.API.Response
{
    public interface IAPIResponse
    {
        /// <summary>
        /// Raw response returned by web request
        /// </summary>
        HttpWebResponse RawResponse { get; }

        /// <summary>
        /// Response body of RawResponse
        /// </summary>
        Object RawResponseBody { get; }

        /// <summary>
        /// The status of response
        /// </summary>
        ResponseStatus Status { get; set; }

    }
}

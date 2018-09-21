using DogDirectory.Models.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace DogDirectory.Models.Service.API.Response
{
    public class BreedListAPIResponse : BaseApiResponse
    {

        /// <summary>
        /// Constructor used if an invalid response is returned by the api factory
        /// </summary>
        /// <param name="baseApiResponse">Base API Response</param>
        public BreedListAPIResponse(BaseApiResponse baseApiResponse) : base(baseApiResponse.RawResponse, baseApiResponse.RawResponseBody, baseApiResponse.Status, baseApiResponse.Message)
        {
            if (baseApiResponse.Status.GetStatus() != ResponseStatus.Status.CriticallyInvalid)
                Status.SetStatus(ResponseStatus.Status.Error);

            _breedList = new List<Breed>();
        }
        public BreedListAPIResponse(HttpWebResponse rawResponse, Object responseBody, ResponseStatus status, JObject message, List<Breed> breedList) : base(rawResponse, responseBody, status, message)
        {
            _breedList = breedList;
        }

        /// <summary>
        /// Public breed list to be returned
        /// </summary>
        public List<Breed> BreedList { get { return _breedList; } }

        /// <summary>
        /// Private breed list to be used internally
        /// </summary>
        private List<Breed> _breedList;

    }
}
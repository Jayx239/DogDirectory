using DogDirectory.Models.Service.API.Response;
using System;
using System.Net;

namespace DogDirectory.Models.Service.API
{
    /// <summary>
    /// Service for making api calls
    /// </summary>
    public class APIService
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public APIService()
        {
            _breedListUri = new Uri("https://dog.ceo/api/breeds/list/all");
            _breadRandomImageFormat = "https://dog.ceo/api/breed/{0}/images/random";
            _jsonContentTypeString = "application/json; charset=utf-8";
            _apiResponseFactory = APIResponseFactory.GetInstance();
        }

        /// <summary>
        /// The uri for retreiving the breedlist
        /// </summary>
        private Uri _breedListUri;

        /// <summary>
        /// The uri format string for retreiving random dog images
        /// </summary>
        private string _breadRandomImageFormat;

        /// <summary>
        /// The content encoding type for the API calls
        /// </summary>
        private string _jsonContentTypeString;

        /// <summary>
        /// The factory singleton for packaging api responses
        /// </summary>
        private APIResponseFactory _apiResponseFactory;

        /// <summary>
        /// Method for making api call to retreive a dog image given a breed
        /// </summary>
        /// <param name="breedName">Breed to find an image of</param>
        /// <returns>Image Api Response containing breed image url</returns>
        public ImageAPIResponse GetImage(string breedName)
        {
            Uri breadImageUri = new Uri(string.Format(_breadRandomImageFormat, breedName));
            WebRequest request = WebRequest.Create(breadImageUri);
            request.ContentType = _jsonContentTypeString;
            HttpWebResponse response = GetWebResponse(request);
            BaseApiResponse apiResponse = _apiResponseFactory.PackageAPIResponse(response);
            if (apiResponse is ImageAPIResponse)
                return (ImageAPIResponse)apiResponse;
            else
                return new ImageAPIResponse(apiResponse);
        }


        /// <summary>
        /// Returns a list of breeds from an API call
        /// </summary>
        /// <returns>The Api response containing the breed list</returns>
        public BreedListAPIResponse GetBreedList()
        {
            WebRequest request = WebRequest.Create(_breedListUri);
            request.ContentType = _jsonContentTypeString;
            HttpWebResponse response = GetWebResponse(request);
            BaseApiResponse apiResponse = _apiResponseFactory.PackageAPIResponse(response);
            if (apiResponse is BreedListAPIResponse)
                return (BreedListAPIResponse)apiResponse;
            else
                return new BreedListAPIResponse(apiResponse);
        }

        /// <summary>
        /// Safely attempts to get a web response from the request
        /// </summary>
        /// <param name="request">Web request to be made</param>
        /// <returns>Web request if successful, null otherwise</returns>
        private HttpWebResponse GetWebResponse(WebRequest request)
        {
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: Unable to get web response\n{0}", ex.StackTrace);
                return (HttpWebResponse) null;
            }
        }

    }
}
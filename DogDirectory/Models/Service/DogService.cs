using DogDirectory.Models.Service.API;
using DogDirectory.Models.Service.API.Response;
using System.Collections.Generic;
using static DogDirectory.Models.Service.API.ResponseStatus;
using DogDirectory.Models.Data;

namespace DogDirectory.Models.Service
{
    /// <summary>
    /// Service to be used by controllers to retreive Dog images and the Breed list
    /// </summary>
    public class DogService
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DogService()
        {
            _apiService = new APIService();
        }

        /// <summary>
        /// Api service used for making api calls
        /// </summary>
        private APIService _apiService;

        /// <summary>
        /// Method to get a random image for a certain breed
        /// </summary>
        /// <param name="breedName">Name of breed to get image for</param>
        /// <returns>Image containing breed image url</returns>
        public Image GetImage(string breedName)
        {
            ImageAPIResponse apiResponse = _apiService.GetImage(breedName);
            if (apiResponse.Status.GetStatus() == Status.Success)
                return apiResponse.Image;
            else
                return new Image();
        }

        /// <summary>
        /// Method to get a list of breeds
        /// </summary>
        /// <returns>A list of breeds</returns>
        public List<Breed> GetBreedList()
        {
            BreedListAPIResponse apiResponse = _apiService.GetBreedList();
            if (apiResponse.Status.GetStatus() == Status.Success)
                return apiResponse.BreedList;
            else
                return new List<Breed>();
        }
    }
}
using System.Collections.Generic;

namespace DogDirectory.Models.Data
{
    /// <summary>
    /// Contains breed information as returned by API
    /// </summary>
    public class Breed
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="breedName">Name of breed</param>
        public Breed(string breedName)
        {
            BreedName = breedName;
            Variations = new List<BreedVariation>();
        }

        /// <summary>
        /// Name of breed
        /// </summary>
        public string BreedName { get; set; }

        /// <summary>
        /// List of variations of this breed
        /// </summary>
        public List<BreedVariation> Variations { get; set; }

    }
}
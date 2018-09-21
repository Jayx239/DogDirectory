
namespace DogDirectory.Models.Data
{
    /// <summary>
    /// Containes the name of the breed variation
    /// </summary>
    public class BreedVariation
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="breedVariationName">Name of breed variation</param>
        public BreedVariation(string breedVariationName)
        {
            Name = breedVariationName;
        }

        /// <summary>
        /// Breed variation name
        /// </summary>
        public string Name { get; set; }
    }
}
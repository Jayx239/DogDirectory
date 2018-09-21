
namespace DogDirectory.Models.Data
{
    /// <summary>
    /// Image object for use in application
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Image()
        {
            SourceString = "";
        }

        /// <summary>
        /// Secondary constructor allowing for source string input
        /// </summary>
        /// <param name="sourceString"></param>
        public Image(string sourceString)
        {
            SourceString = sourceString;
        }

        /// <summary>
        /// The image source url string
        /// </summary>
        public string SourceString { get; set; }
    }
}
using DogDirectory.Models.Data;
using DogDirectory.Models.Service.API.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using static DogDirectory.Models.Service.API.ResponseStatus;

namespace DogDirectory.Models.Service.API
{
    /// <summary>
    /// Singleton clase for creating APIResponse Objects
    /// </summary>
    public class APIResponseFactory
    {
        #region Initialization
        
        /// <summary>
        /// Private constructor
        /// </summary>
        private APIResponseFactory()
        {

        }
        
        /// <summary>
        /// Method for retrieving an instance of this factory
        /// </summary>
        /// <returns></returns>
        public static APIResponseFactory GetInstance()
        {
            if (_apiResponseFactory == null)
            {
                _apiResponseFactory = new APIResponseFactory();
            }
            return _apiResponseFactory;
        }

        #endregion

        #region private attributes
        
        /// <summary>
        /// Singleton instance of factory
        /// </summary>
        private static APIResponseFactory _apiResponseFactory;

        #endregion

        #region Public Methods
        /// <summary>
        /// A Factory method for generating an API Response object
        /// </summary>
        /// <param name="response">Raw http response</param>
        /// <returns>A new API Response object</returns>
        public BaseApiResponse PackageAPIResponse(HttpWebResponse response)
        {
            if(response == null)
            {
                return new BaseApiResponse(response, new object(), new ResponseStatus(Status.CriticallyInvalid), new Object());
            }

            JObject responseBody = PackageResponseBody(response);
            ResponseStatus status = PackageResponseStatus(responseBody);
            Object message = PackageResponseMessage(responseBody);

            var path = response.ResponseUri.AbsolutePath;

            if (path.Contains("/api/breeds/list"))
            {
                return PackageBreedListResponse(response, responseBody, status, message);
            }
            else if (path.Contains("/api/breed/") && path.Contains("/images/"))
            {
                return PackageImageResponse(response, responseBody, status, message);
            }
            else
            {
                return new BaseApiResponse(response, responseBody, status, message);
            }

        }

        #endregion

        #region Private Methods
        #region API Response Packagers

        /// <summary>
        /// Extracts the status from the response body
        /// </summary>
        /// <param name="responseBody">The response body contents</param>
        /// <returns>A ResponseStatus Object containing the status of the request</returns>
        private ResponseStatus PackageResponseStatus(JObject responseBody)
        {
            ResponseStatus responseStatus = new ResponseStatus();

            if (responseBody == null)
                return responseStatus;

            try
            {
                Status status = Status.Unknown;
                if (responseBody.ContainsKey("status"))
                    status = GetStatusEnum(responseBody.GetValue("status").ToString());

                responseStatus.SetStatus(status);
                return responseStatus;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to package response status\n{0}", ex.StackTrace);
                return responseStatus;
            }
        }

        /// <summary>
        /// Method for generating a Status variable from a string
        /// </summary>
        /// <param name="inStatusString">String interpretation of status</param>
        /// <returns>Status enum value</returns>
        private Status GetStatusEnum(string inStatusString)
        {
            if (inStatusString == null)
            {
                return Status.Unknown;
            }

            string cleanStatusString = inStatusString.ToLower();
            switch (cleanStatusString)
            {
                case "success":
                    return Status.Success;
                case "error":
                    return Status.Error;
                case "unknown":
                default:
                    return Status.Unknown;
            }
        }
        /// <summary>
        /// Extracts the response message from the response body
        /// </summary>
        /// <param name="responseBody">The response body contents</param>
        /// <returns>An object containing the message from the response body</returns>
        private Object PackageResponseMessage(JObject responseBody)
        {
            Object message = new Object();

            if (responseBody == null)
                return message;
            try
            {
                if (responseBody.ContainsKey("message"))
                    message = responseBody.GetValue("message");

                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to package response message\n{0}", ex.StackTrace);
                return message;
            }
        }

        /// <summary>
        /// Extracts the body of an http response into a Json Object
        /// </summary>
        /// <param name="response">Raw http response</param>
        /// <returns>JObject containing contents of the response body</returns>
        private JObject PackageResponseBody(HttpWebResponse response)
        {
            try
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseBody = reader.ReadToEnd();

                JObject responseBodyObject = JObject.Parse(responseBody);
                return responseBodyObject;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to package response body\n{0}", ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Generates a new BreedListAPIResponse
        /// </summary>
        /// <param name="response">Raw http response</param>
        /// <param name="responseBody">Response Body Object</param>
        /// <param name="status">Response Status</param>
        /// <param name="message">Response Message</param>
        /// <returns>New BreedListAPIResponse</returns>
        private BreedListAPIResponse PackageBreedListResponse(HttpWebResponse response, Object responseBody, ResponseStatus status, Object message)
        {
            JObject messageValue = GetMessageAsJObject(message);
            List<Breed> breedList = GetBreedList(messageValue);
            return new BreedListAPIResponse(response, responseBody, status, messageValue, breedList);
        }

        /// <summary>
        /// Generates a new ImageAPIResponse
        /// </summary>
        /// <param name="response">Raw http response</param>
        /// <param name="responseBody">Response Body Object</param>
        /// <param name="status">Response Status</param>
        /// <param name="message">Response Message</param>
        /// <returns>New ImageAPIResponse</returns>
        private ImageAPIResponse PackageImageResponse(HttpWebResponse response, Object responseBody, ResponseStatus status, Object message)
        {
            JValue messageValue = GetMessageAsJValue(message);
            string sourceString = "";
            try
            {
                sourceString = messageValue.Value.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: Unable JValue to string\n{0}", ex.StackTrace);
                return new ImageAPIResponse(response, responseBody, status, message, sourceString);
            }

            return new ImageAPIResponse(response, responseBody, status, message, sourceString);
        }

        #endregion

        /// <summary>
        /// Gets the breed list from the raw API response message body
        /// </summary>
        /// <param name="breedJsonArray">The object containing the breed array</param>
        /// <returns>List of breeds</returns>
        private List<Breed> GetBreedList(JObject breedJsonArray)
        {
            List<Breed> breedList = new List<Breed>();

            try { 
                foreach (JProperty breedObject in breedJsonArray.Children())
                {
                    if (breedObject != null)
                    {
                        Breed nextBreed = new Breed(breedObject.Name);
                        JEnumerable<JToken> variations = breedObject.Children();
                        foreach (JValue variation in variations.Children())
                        {
                            nextBreed.Variations.Add(new BreedVariation(variation.ToString()));
                        }
                        breedList.Add(nextBreed);
                    }
                }
                return breedList;
            } catch(Exception ex)
            {
                Console.WriteLine("Error: Unable generate BreedList\n{0}", ex.StackTrace);
                return breedList;
            }
        }

        #region Json Helpers
        /// <summary>
        /// Method for converting message object to string
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>JValue representation of object</returns>
        private JValue GetMessageAsJValue(Object message)
        {
            try
            {
                JValue messageValue = (JValue)message;
                return messageValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable convert message to JValue\n{0}", ex.StackTrace);
                return new JValue("");
            }
        }

        /// <summary>
        /// Returns the input message as a JObject
        /// </summary>
        /// <param name="message">Response message</param>
        /// <returns></returns>
        private JObject GetMessageAsJObject(Object message)
        {
            try
            {
                return (JObject)message;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable convert message to JObject\n{0}", ex.StackTrace);
                return new JObject();
            }
        }
        #endregion

        #endregion
    }
}
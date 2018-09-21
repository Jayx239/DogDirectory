using System.Collections.Generic;
using System.Net;
using DogDirectory.Models.Data;
using DogDirectory.Models.Service;
using DogDirectory.Models.Service.API;
using DogDirectory.Models.Service.API.Response;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DogDirectory.Test
{
    [TestClass]
    public class APIServiceTest
    {

        #region Test APIFactory

        /// <summary>
        /// Method to test that if an invalid response is received that the APIFactory will gracefully handle it
        /// </summary>
        [TestMethod]
        public void TestInvalidResponse()
        {
            APIResponseFactory responseFactory = APIResponseFactory.GetInstance();
            WebRequest request = WebRequest.Create("http://stackoverflow.com");
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            IAPIResponse ApiResponse = responseFactory.PackageAPIResponse(response);

            Assert.IsNotNull(ApiResponse);
            Assert.IsTrue(ApiResponse.Status.GetStatus() == ResponseStatus.Status.Unknown);
            /* Null tests */
            //Assert.IsNotNull(response);
        }

        [TestMethod]
        public void TestNullResponse()
        {
            APIResponseFactory responseFactory = APIResponseFactory.GetInstance();
            HttpWebResponse response = (HttpWebResponse)null;
            IAPIResponse ApiResponse = responseFactory.PackageAPIResponse(response);

            Assert.IsNotNull(ApiResponse);
            Assert.IsTrue(ApiResponse.Status.GetStatus() == ResponseStatus.Status.CriticallyInvalid);
        }

        #endregion

        #region Test Dog Service
        [TestMethod]
        public void TestGetBreedList()
        {
            DogService dogService = new DogService();
            List<Breed> breedList = dogService.GetBreedList();

            /* Null tests */
            Assert.IsNotNull(breedList);

            Assert.IsTrue(breedList.Count > 0);
            Assert.IsTrue(breedList.Exists(b => b.Variations.Count > 0));

        }

        [TestMethod]
        public void TestGetImage()
        {
            DogService dogService = new DogService();
            Image response = dogService.GetImage("beagle");
            
            /* Null tests */
            Assert.IsNotNull(response);

            Assert.IsTrue(response.SourceString.Length > 0);
            Assert.IsTrue(response.SourceString.StartsWith("http"));
        }

        [TestMethod]
        public void TestGetImageFail()
        {
            DogService dogService = new DogService();
            Image response = dogService.GetImage("somedogthatdoesntexist");

            /* Null tests */
            Assert.IsNotNull(response);

            Assert.IsTrue(response.SourceString.Length == 0);
        }

        #endregion
    }
}

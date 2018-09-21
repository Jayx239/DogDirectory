###Dog Directory
####Created by Jason Gallagher
###Goals
#### Scalable Maintainable Solution
The intention of this project was to create a web app with a scalable, maintainable solution. To make the solution scalable, I made sure to create a base API response that contains at minimum what you expect to see is returned by the API, a status and a message. This allows for further API responses to be added in the future by simply extending this class and adding the create logic to the API Response Factory.



#### Seperation of Concerns
I also wanted to make the code easy to use for a team that has various coding abilities. For example, if you do not know how to make a web request, stick to updating the dog service for data access, and use API level functionality that somebody else develops. If you only know how to write controllers to return data, just use the dog service that somebody else develops to get the data you need and return it. This seperation of concerns helps a team work together without having to know how the rest of the system works.

####Comments on the Dog API
One thing that I would have done differently with the Api is to make it simply return the data you need. If there was an issue retreiving this data, the Api should simply return an http error by setting the status code in the header. This would reduce the need for the status field and eliminate the headache of adding boilerplate code to get what you need. This would also make the Api more developer friendly for a restful implementation.

My solution abstracts this boiler plate code into the Api service layer, so that at the controller and UI layer you do not need to worry about this.

For an example of how the Dog Api could work, see the API2GetDogList and API2GetDogImage controller actions in the home controller. For the restful front end see Better API tab of the website.

###Unit Tests
I only got around to writing a few unit tests for the API Factory and Dog service in the 3 night window that I spent making this project. I tried to test the functionality that I thought would cause the code to break, and tests to ensure the functionality that is expected. With more time I would write more thorough tests to hit all possible paths of execution.
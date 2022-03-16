# ConsumeBenchLabsProject
This project is a .Net Core 3.1 console application.  This app connects to a REST API and calculates running daily balances, prints them to the console, and includes unit test.

![console_output](https://user-images.githubusercontent.com/6993716/158532439-60ae6ce0-2b28-4634-ba89-27275c66c19e.PNG)

![json](https://user-images.githubusercontent.com/6993716/158532489-a0516fc2-549f-4054-b8b0-04ccbb89f361.PNG)

## Installation

To run app, go to the Release directory and run ConsumeBenchLabs.exe.  To read and modify code, download repository to an IDE like Visual Studio.

## Considerations

Scalability: To output the report, the method to collect running balanaces must loop through a list filled with data. The code works well because the datasaet to pull from is small.  If the dataset is larger, a more scalable solution is to create a dictionary for a faster look up time (dictionary key: dates, values: balance sums).    The list requires a time complexity of O(n) to loop through.  If a dictionary is used, the time complexity is O(1) to look up data.

Complexity: To output the report, the Daily Transaction Report class inherits from a report interface.  Although not required for this project, the interface allows the app to be extensible and makes the code easier to maintain.  If a new report is required, the developer can create a new concrete report class, inherit from the report interface and implement new code in the new class.  This will ensure the current report class does not need to change and new features will not break working code.

Goal: In the future, the code can be refactored to use the page's total count value to recieve the correct amount of data.  Currently, the app has to call the API multiple times to ensure a complete set of data is being pulled. This stratgy works well for a small set of data.  If a slow network speed or a larger dataset input needs to be considered, a better strategy would be to use the total count value.  For example, create another API that returns the  dataset with just one call.

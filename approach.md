# Plan:
	- File type input field and a button on page
	- When button is clicked send file to server
	- Convert/parse/deserialize Xml to model
	- Send model back to frontend
	- Display model properties on page.
	
##### Should I send file to server? We could just process everything on the frontend.
	Well the task is to use C# and ASP.Net, if I only use javascript/typesript for everything that defeats the purpose.

##### How should I send the file to the server and receive the parsed model back?
	Ajax or standard http request? Lets try and avoid javascript for this task, use standard form submit.
---
### 1) Add WebOrder model.
### 2) Add frontend form to upload file and display table to display contents.
---
##### Should I create a seperate API project or controller, where we can upload a file, convert file content to model, maybe even save to DB.
	Seems overly complicated for this simple task.
---
### 3) Add controller action to accept file and convert it into model.
---
##### Should I save file on server?
	Don't really see a reason, let's not do that. Just process it immediately.

##### How and where should I calculate the extra data (Price average, Total) necessary for the display?
	Adding them as readonly properties to the model should be fine.
	Actually, the model should just contain the bare minimum, and I should create a ViewModel and a mapper to map in between.
---
### 4) Add ViewModel.
### 5) Add Mapper.
### 6) Inject and use mapper in homecontroller.
---
##### My "Average" calculation gives 379.99 instead of 462.21 that is in the task description.
	I checked it by dividing the total price 3039.92 with 8 which is the total quantity, and it should return 379.99.

##### I have to find the correct culture to display the Total and Average numbers properly.
	Found it, its the danish culture. Should I set it to default on the whole application? Or just on these 2 properties specifically?
	I decided to use the culture globally, I think that's closer to a real world scenario.
---
### 7) Change culture globally.
---
##### Should I add responsive UI?
	I don't see why not, should be very easy with how small this application is.
---
### 8) Add responsive UI.
---
##### Should I set up minifying and bundleing?
	There is not much css and javascript in this application, don't really see a huge benefit in doing it.

##### Should I add validation?
	- Yes there should be form validation both client side and server side.
	- Just like I did with mapping, I am not going to use an external nuget package, rather I'll make my own validators.
---
### 9) Add backend validation.
### 10) Add frontend validation.
---
##### Should I add unit tests?
	- Yes, we can test the mappers and validators.
---
### 11) Add unit tests for WebOrderMapper.
### 12) Add WebOrderValidatorTests.
### 13) Add FileValidatorTests.
---

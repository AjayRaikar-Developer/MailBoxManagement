# Mail Box Management
This project helps to fetch the mailbox data from any given provider like outlook.

It contains two major projects -
- **MailManagement :** Its a **_library_** which manages all the functions like fetching mailbox & search a mailbox with search filters.
- **WebAPI :** Its an **_API Project_** which has all the Methods calling this mail library with provision of choosing the provider. 

# How to use the project ..?!
- Navigate over to appsettings.json file in WebAPI Project and put your **Outlook MailBox's - EmailAddress & Password** in the json object **Out_Look**. Replace this **YOUR_EMAIL_ADDRESS** with actual emailaddress  and **YOUR_PASSWORD** with the actual password.
- Then run the **WebAPI Project** using Visual Studio or anyother prefered C# IDE. 
- It will automatically load a browser window and **_Fetch All MailBox Messages_**
- To explore other GET API methods change the URI in the browser with the method names from the **Controller**
- You can find other API methods in Controller folder of the **WebAPI Project**

# License
[MIT License](License.md)

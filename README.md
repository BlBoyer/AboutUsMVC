Introduction:  
  
This app was made to prove and practice my skills with asp.net mvc projects. It allows users to create a public profile with some basic 'about me'-type information.  
  
Site layout:  
  
Home page  
  -Login - allows a user to log-in  
  -Lookup - allows a search to made on a user  
  -Manage profiles - allows an admin to moderate user content  
User page  
  -Displays user profile and content  
Lookup  
  -Allows search to be performed by username, displays user page- without ability to edit  
  
Usage:  
  -To create an account use the button on the homepage, enter your details, the business setion is not required.  
  -Once finished, wait until redirected to your personal page, otherwise errors may occur.  
  -You can edit content by clicking the edit icons on the page.  
  -To delete an account the password must be given to someone with administrator access of the app.  
  -To manage accounts, you must login with the correct password. From this page accounts may be edited or deleted.  
  
Functionality:  
  -When a new user is created, the username and password are used to create a unique key that is used to authenticate a user.  
  -On successful login a static variable is created to authenticate the session.  
  
Special Notes:  
  -Special care should be taken when creating accounts as to wait for all processes to complete an not close the browser during actions, or undeletable data will be entered into the database.  
  -To delete an account the account password is required, therefore, the password must be given to someone with administrator access in order to complete.  

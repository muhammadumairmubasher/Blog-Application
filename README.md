# Blog Application with ASP.net MVC

In this simple Blog Application, there are two types of users:

-	Admin User
-	Regular Users

Admin can add, delete, and update all the users. Simple users can only view
others posts while can create an account and can edit/update/delete posts written by themselves (like we all do on facebook/Instagram). They can also update their profile.

---
## Description

When your program starts, it should provide users with options of Login and Signup. When a user is successfully logged in Home page should be shown to him. All the posts written by all the users in your database should be displayed on Home page as shown in the figure.

![image](https://user-images.githubusercontent.com/59522109/145226395-8918aa16-6eee-4b6c-9e23-48e27bd9a377.png)

Note that On Home screen there is a button “Create Post” on the top of posts. When user click on it the following screen should be displayed.

![image](https://user-images.githubusercontent.com/59522109/145226459-4f781f64-2ee4-4eb3-a0b1-ddd3e44befaf.png) 

After Creating a Post when user clicks on “Post” button , The data should be saved in the database and then redirect user to home screen where new post written by you along with all the previous posts are displayed. In the current Example I am logged in as Saira. Now if Saira wants to edit her post. She’ll click on her post Title and the following screen should be displayed. 

![image](https://user-images.githubusercontent.com/59522109/145226506-354cf40d-0ef5-4b63-a933-e266b083c981.png)

When she clicks on delete button, the post should be deleted. 
When she clicks on Update, following screen should be displayed. After making changes when clicks on “Post” button, the post should be updated. 

![image](https://user-images.githubusercontent.com/59522109/145226603-3aa83cff-01e4-4f12-a38e-a4e6fcd0728f.png)

And the home screen will be displayed and the post should be updated there as well. 

![image](https://user-images.githubusercontent.com/59522109/145226644-3ee349bf-fee4-44ec-9692-9ac67d6ed8ef.png)

If she clicks on other people’s Posts, she will be provided with a screen where she can only read their post in detail but cannot edit/delete it as shown below. Only the author of the post should delete the post. 

![image](https://user-images.githubusercontent.com/59522109/145228100-88dd86a2-22df-40af-ba9c-ca3d7af8d223.png)



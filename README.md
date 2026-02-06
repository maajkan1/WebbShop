<h1 align="center"> Webshop </h1>

<h3 align="center"> 🚧Under construction🚧 </h3>

This project is for my final exam in the Entity framework class.

I decided to do a backend service with API that uses Code First database modelling, and also made it into an API to be able to test the stuffs.
The frontend is purely AI, and focused on implementing all my endpoints to show how they work.
I used a Service directory with interface, so you can implement if you want a Console app, API etc.

This project is under construction and is no where finished.

## How to install the project.

1) First download Docker Desktop: https://www.docker.com/products/docker-desktop/
2) Download this repo as a ZIP or clone it.
3) Use your PCs terminal (the one that comes with the operative system)
4) Navigate into the project (Tip: write cd, then drag the file path into the terminal for quicker access.)
5) Inside the terminal write ```docker compose up --build``` This will install the container, can take quite some time.
6) When finished and you see: ![Docker complete](/Images/docker-complete.png)  its completed! You can now use the app.

## How to navigate the project

You have two "websites" you can check, either the Swagger with all endpoints or the "whole" fullstack application.

For Swagger: http://localhost:5000/swagger/index.html  

For WebShop: http://localhost:5173/

The docker contains seed-data that fills the database. This means you get two users to choose from
but we will test the admin user:

Username: ```admin```  
password: ```Admin123!```

As of now there is no working admin dashboard, but will be implemented in the future.

## Product description:
The project is just a simple backend with a AI implemented Frontend.
The goal was to build an app, console app or anything that used Code First database managment.
I decided to go with a backend, and got some time over so i used AI to implement a Frontend to check all endpoints.

The login-system just uses a simple JWT token that gets generated when you login and saves in Localstorage in the browser.
The password is encrypted and hashed with BCrypt, and stores the password as the hash in the database.
I could have gone with the built in soultion in .NET but decided to just create a lightweight and simplisitc system.

There is no working "order" payment, so when you click "Till kassan" it just creates the order.


## Api Endpoints:

![Endpoints](/Images/endpoints.png)


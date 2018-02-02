# CheckOutChallenge
At the beginning this challenge seems to be implemented straight forward but I added some interesting features to render this challenge a bit more interesting. One of this challenge in this project was the **security layer**, althouth it is a basic implementation.

All projects are built with .NET Core 2.x.

## Challenge Definition

### Part 1:

Your company has decided to create a new line of business.  As a start to this effort, they’ve come to you to help develop a prototype.  It is expected that this prototype will be part of a beta test with some actual customers, and if successful, it is likely that the prototype will be expanded into a full product.

Your part of the prototype will be to develop a Web API that will be used by customers to manage a basket of items. The business describes the basic workflow is as follows:

> This API will allow our users to set up and manage an order of items.  The API will allow users to add and remove items and change the quantity of the items they want.  They should also be able to simply clear out all items from their order and start again.

The functionality to complete the purchase of the items will be handled separately and will be written by a different team once this prototype is complete.  

For the purpose of this exercise, you can assume there’s an existing data storage solution that the API will use, so you can either create stubs for that functionality or simply hold data in memory.

Feel free to make any assumptions whenever you are not certain about the requirements, but make sure your assumptions are made clear either through the design or additional documentation.

### Part 2

Create a client library that makes use of the API endpoints created in Part 1.  The purpose of this code to provide authors of client applications a simple framework to use in their applications.

If we decide to bring you in for further discussions, you should be prepared to explain and defend any coding and design decisions you make as a part of this exercise.

**All code should be written in C# and target the .NET framework library version 4.5 or higher, or .NET core.  Please check all code into a publicly accessible repository on GitHub and send us a link to your repository.**

## Implementation
The solution has different projects:

- Basket.DAL
- Basket.WebApi
- Basket.Library

For tests:
- Basket.WebApi.Test
- Basket.ConsoleApp

### Basket.DAL
DAL (Data Layer) cointains **enums** and **models** that I'm using across projects. 

Models are mainly divided in two folders: **Requests** and **Responses**. Those folders cointain models for the webapi but also I'm using this project in the **Basket.Library** and in tests.

### Basket.WebApi
This is the main project. Under **Controllers** folder, there are 3 controllers:

- _Basket_ exposes methods to manage a basket (add/remove item, delete an item or empty your basket)
- _Product_ returns a product list in the case of this project from a predefined list
- _Token_ is responsible to authenticate the user and returns a token

#### Security layer with JWT
JSON Web Token (JWT) is an open standard ([RFC 7519](https://tools.ietf.org/html/rfc7519)) that defines a compact and self-contained way for securely transmitting information between parties as a JSON object. **Authentication** is the most common scenario for using JWT. Once the user is logged in, each subsequent request will include the JWT, allowing the user to access routes, services, and resources that are permitted with that token.

In this project a client can receive a JWT token if the username and password are valid. The function does a very easy check: if the username is equal to the password, a token is generated. 

All requests to the webapis must have a JWT token. If not, the application returns an **401 UNAUTHORIZED**.

Please read this file before begin:

This is simple app that should met few criteria:
- have web service with database connection using Entity Framework
- have UI interfese as WinForms application
- be as simple as possible 
- some of things will be implement later, just to be aware that this is not final concept.

App should be able to add/edit/remove tasks (in application as cases). There are 2 types of users (user and manager). User see his own cases and manager can see his own and user related to him/her.

Not implemented / wrongly implemented:
- EntityFramework db modeel -> at this moment is as simple as possible without any forein keys, relations, indexes ets. It is as simple as it is possible to have working demo.
- HttpClient in UI application -> we should not use HttpClient at all. Even in "using" statement HttpClient didn't realese all his alocation and will fill all sockets and make server dead. Here we will need to implement IHttpClientFactory. More details https://cezarywalenciuk.pl/blog/programing/ihttpclientfactory-na-problem-z-httpclient
- Login implemened in same page -> should be as popup or other window
- Report -> at this moment there is no report -> should be separete window, propably will be save text file to disc
- Resize UI -> should be done, maybe will be done in future
- put Unit of Work design patter to existing Repositories

What is done:
- DI -> whole aplication use Inversion of control with interfaces
- current (latest) net core with c# 8 and EF core
- strategy pattern when sending user/password on network (we don't want to send user/password as a clear text)
- encrypting password in database (don't store clear text passwords in database)
- some sample data
- application that can be used and tested as it is (it's simple, but we can show this to customer and talk what he/she would like to change)
- sample unit tests for business logic (later it would be nice to add integration tests)
- 


Test users:
login: user
password: user 

Test managers:
login: manager
password: manager
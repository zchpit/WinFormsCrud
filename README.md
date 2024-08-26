Please read this file before begin:

This is simple app that should met few criteria:
- have web service with database connection using Entity Framework
- have UI interfese as WinForms application
- be as simple as possible 
- some of things will be implement later, just to be aware that this is not final concept.

Business conditions:
- App should have separated server side (rest api service) and UI (winforms)
- App should be able to add/edit/remove tasks (in application as cases). 
- There are 2 types of users (user and manager). 
- User see his own cases and manager can see his own and user related to him/her. 
- Manager should be able to generate report based on stored procedure.
- user should be able to see cases if there are baypassed to him (but don't need to implement baypassed mechanism) -> this is done with many to many relations on UserCase entity

Not implemented / wrongly implemented /fix later:
- Login implemened in same page -> should be as popup or other window
- Resize UI -> should be done, maybe will be done in future
- report is done as simple as possible with Json file 

What is done:
- DI -> whole aplication use Inversion of control with interfaces
- current (latest) net core with c# 8 and EF core
- strategy pattern when sending user/password on network (we don't want to send user/password as a clear text)
- encrypting password in database (don't store clear text passwords in database)
- inserted some sample data 
- application that can be used and tested as it is (it's simple, but we can show this to customer and talk what he/she would like to change)
- sample unit tests and integration test for API
- Report -> execute stored procedure and generate json file with report data -> simples possible solution but it works
- instead of using HttpRequest that have errors inside (don't realese sockets) I have used Flurl.Http

Test users:
login: test
password: test 

Test managers:
login: manager
password: manager
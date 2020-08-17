# Moneybox Money Withdrawal

The solution contains a .NET core library (Moneybox.App) which is structured into the following 3 folders:

* Domain - this contains the domain models for a user and an account, and a notification service.
* Features - this contains two operations, one which is implemented (transfer money) and another which isn't (withdraw money)
* DataAccess - this contains a repository for retrieving and saving an account (and the nested user it belongs to)

## The task

The task is to implement a money withdrawal in the WithdrawMoney.Execute(...) method in the features folder. For consistency, the logic should be the same as the TransferMoney.Execute(...) method i.e. notifications for low funds and exceptions where the operation is not possible. 

As part of this process however, you should look to refactor some of the code in the TransferMoney.Execute(...) method into the domain models, and make these models less susceptible to misuse. We're looking to make our domain models rich in behaviour and much more than just plain old objects, however we don't want any data persistance operations (i.e. data access repositories) to bleed into our domain. This should simplify the task of implementing WithdrawMoney.Execute(...).

## Guidelines

* You should spend no more than 1 hour on this task, although there is no time limit
* You should fork or copy this repository into your own public repository (Github, BitBucket etc.) before you do your work
* Your solution must compile and run first time
* You should not alter the notification service or the the account repository interfaces
* You may add unit/integration tests using a test framework (and/or mocking framework) of your choice
* You may edit this README.md if you want to give more details around your work (e.g. why you have done something a particular way, or anything else you would look to do but didn't have time)

Once you have completed test, zip up your solution, excluding any build artifacts to reduce the size, and email it back to our recruitment team.

Good luck!

_________________________________________________

Additional notes:

- Changed code style to that of private fields being in the format of -> '_privateField'
- Added summary comments for public methods and properties
- Refactored some existing variable names to be more detailed and less abstract in meaning.
- Added checks for null accounts, empty guid ids of accounts.
- For the purposes of this task, I've assumed that all amounts in question are in Pounds Sterling.
- Left certain properties as constants as opposed to configurable variables for the purposes of this task. For example, it would be better to have say the 'PayInLimit' be confgurable for any given account, depending on a user's needs.
- For this task, I've simply printed a console line stated for the notification system, as opposed to implementing sending an email to a given user.
- Simply used a dictionary to hold the accounts in the account repository, as opposed to a more data persisted approach with a database.
- Added unit tests to test different cases where certain exceptions are expected, in addition to successful feature test scenarios.
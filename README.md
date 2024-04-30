# Account Management

There are three web APIs
- AccountManagement
- CustomerService
- AccountService

__AccountManagement__ is the application interface. It uses __CustomerService__ to handle customers and __AccountService__ to handle savings accounts. __CustomerService__ and __AccountService__ have their own seperate SQLite databases, which they are interacting with through EF Core. The APIs are connected with REST API endpoints. The time was not spend on decoupling them with e.g. a message queue.

There is an unsolved bug where __AccountManagement__ is not returning the last x number of transactions. This seems to be related to how the one-to-many models are updated with EF Core.

The API tests for __AccountManagement__ are not implemented. There are a few tests that was added in the beginning, but they can be disregarded as they are not adjusted and are failing.

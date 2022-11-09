# Job Candidate Management
## Project Documentation
### Features: 
#### 1. Architecture:
For the architecture I first introduced repository pattern which provides an abstraction
to the data access layer. After that I introduced a business logic layer to encapsulate
any internal logic and decouple controllers from needing to implement any business
logic. This means the controller class will communicate with the business logic layer
and business layer will communicate with the data access layer which will handle
persistence to the csv file. Both the business logic layer and data access layer are
implemented to utilize the full power of ASP.NET Core dependency injection
framework.

#### 2. REST API:
For the REST API, I have implemented one endpoint which is responsible for
creating a new candidate and/or updating an existing candidate in the storage. It
returns an “Ok” status code if it successfully creates or updates the data. It can also
return a “BadRequest” status code for invalid request.

#### 3. Caching:
To implement caching, I have used a Concurrent Dictionary as an in-memory cache.
Concurrent Dictionary provides thread-safe collection class to store key/value pairs.
It internally uses locking to provide a thread-safe class, so no need to explicitly
declare lock object and handles concurrency. I have also used a timer and set an
interval of ten seconds after which data will be written from the in-memory cache to
the csv file. The time interval can be adjusted.

#### 4. Logging:
For logging I have implemented a third-party provider - Serilog. Using serilog, logs
will be written to the console, file and database.

#### 5. Exception Handling:
I have implemented global exception handling to handle exceptions by writing a
custom middleware.

### List of ways to improve the application:
1. As the application grows, and once the database is used for storage it will be
good to implement token-based authentication and authorization.

### Assumptions:
1. It is assumed that EmailAddress to be the unique primary key as stated in the
requirement specification document.
2. It is assumed that TimeInterval is of type TimeSpan.

### Time Spent:
I have work for two hours per day for three days to implement the project.

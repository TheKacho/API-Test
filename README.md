# API Test

This is the midterm repo for Code Louisville's QA 2 cohort

## Part 1: Postman API Test
### Link: 
Choose a public API and create a postman collection to test the API. The postman collection should contain at least
15 tests, which in turn should cover all of the following criteria:

- [x] 1. Test at least 3 different HTTP methods.
- [x] 2. Include at least one test for an endpoint that uses query string parameters.
- [x] 3. Include at least one test for an endpoint that requires a request body. 
- [x] 4. Include at least one negative test.

All your Postman tests should contain tests (in the Tests tab) that verify the test
passed. At a minimum, they should verify the correct expected status code was
returned. When finished, export your Postman collection as .json and add it to
a GitHub repo.


## Part 2: Automated API Tests with C#

Choose a public API and create automated tests for it in C#. 
You may use any Nuget packages, unit testing frameworks, or assertion libraries you choose, but your solution must be in C#. 
Create as many automated tests as required in order to complete all of the following criteria: 

- [x] 1. Test at least 3 different HTTP methods. 
  - Tested `GET`, `POST`, `PUT`, `PATCH`, and `DELETE` for JSON Placeholder API 
  - Base URL: `https://jsonplaceholder.typicode.com/`
- [x] 2. Include at least one test for an endpoint that uses query string parameters.
  - `Test JSONPlaceholder_GET_Params()` uses query params on `/comments?postId=1`
- [x] 3. Include at least one test for an endpoint that requires a request body.
  - `Test JSONPlaceholder_POST()` sends a request body for a new Post 
- [x] 4. Include at least one negative test.
  - `Test JSONPlaceholder_Negative_Invalid_Method()` uses incorrect `DELETE` method on `/posts` endpoint

Build your code and run your tests. All tests should run and all assertions pass in order to receive credit. 
If your code requires any setup in order to build or run your tests, include a readme file with instructions to build and run your code. 
Check your completed tests into a GitHub repository. 



## Part 3: Performance Testing with JMeter

Using a performance testing tool of your choice, create a performance test that simulates 1500
users accessing a website of your choice over a 30-second period. Create a table or graph that
displays the results of your tests. Write a 3-5 sentence paragraph explaining the results of your
tests and whether or not the results indicate that the website performs well. Add your graph or
table, paragraph explanation, and any code you wrote for your performance tests to a GitHub
repository.
# Data

## Purpose

This directory houses logic for communicating with external resources,
and offers abstractions which makes it easier to consume APIs in a structured manner.

**_Tip: To apply global logic for api requests you can use 'interceptors'. Existing interceptors can be found in ./api/interceptors_**

## Rules

- When communicating with external APIs inside react components use the hooks exposed inside ./queries
- Each endpoint's actions should have its own file and object which declare the api path and actions available, see: ./api

## Further reading

You can read more about the underlying libraries used for API communication below:

- https://axios-http.com/
- https://react-query-v3.tanstack.com/

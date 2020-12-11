# Frameworkless Basic Web Application


This project is a .NET Core solution to the [Frameworkless Basic Web Application Kata](https://github.com/MYOB-Technology/General_Developer/blob/master/katas/kata-frameworkless-basic-web-application/kata-frameworkless-basic-web-application.md) as well as the accompanying [enhancements](https://github.com/MYOB-Technology/General_Developer/blob/master/katas/kata-frameworkless-basic-web-application/kata-frameworkless-basic-web-application-enhancements.md). 

### URL
The project can be accessed through this URL: [Samaas Frameworkless App](https://samaa-frameworkless-app.svc.platform.myobdev.com/)

## Endpoints
The application supports the endpoints that are listed below. You can use an HTTP client such as [Postman](https://www.postman.com/), to make requests. Alternatively you can make requests with `curl`. Hitting the end point with your browser will not work, seeing as you have to pass in an API token through the header. 

### GET /

Returns a greeting, the names of all users, and the date/time on the server.

```
200 OK

"Hi DefaultBob 12/11/2020 05:36:46"
```

### GET /users

Returns all the users without the greeting and time/date.

```
200 OK

"DefaultBob, Sue, Mary"
```

### POST

Adds a new user to the list

```
201 Created
```

```
409 Conflict

This username already exists.
```

### PUT /{Samaa}

Updates the existing user name from the old one (defined in the end point) to the new one (defined in the body of the request).

```
200 OK
```

If the old username doesn't exist it will still create the new one.
```
201 Created
```
If the new username already exists it will cause a conflict, and won't add the name to the list.

```
409 Conflict

```

### DELETE /{Samaa}

Deletes the existing user `{Samaa}`. The super user, which in this case is DefaultBob can't be deleted as per the katas requirements.

```
200 OK
```

```
403 Forbidden

The power user cannot be deleted.
```

```
404 Not Found

This username cannot be found.
```

## Built With
* [GitHub](https://github.com/) - Code repository
* [Buildkite](https://buildkite.com/) - Continuous integration pipeline
* [Docker](https://www.docker.com/) - Containerization
* [AWS](https://https://aws.amazon.com/) - Specifically CloudFormation(CF) and Elastic Container Registry(ECR)
* [Jupiter](https://get.jupiter.myob.com/) - Myob's managed infrastructure platform
# Race Analysis

This is a simple application that aims to show a simple, yet consistent, project using **Clean Architecture**, **Domain Driven Design** and **SOLID Principles**. We also create a simple web frontend to show some result. In this way we created a total decoupled Backend and Frontend, which nowadays is a great practice.

The App consist in generate a report on a given Race. The data input is a template file .txt in which each line correspond to a Race Event containing some useful information. This file can be found on the PresenterWeb Folder (That Folder is the Start of the application)

## Using The App

The Race Analysis App is deployed on the cloud. You can access the app on the following links

- App (FrontEnd with api calls): <https://raceanalysis.now.sh/>

- App Api (Azure): <https://raceanalysis.azurewebsites.net/api/races/1> [Offline]

## Running The App on local environment

The Web Api (Backend) for the applications was made wih C#/ASP.NET Core. To publish the api locally run the following command on the root folder:

```bash
dotnet run --project PresenterWeb
```

You can test the endpoints using a rest API software of your choice or even via console through the **curl**. Example:

```bash
curl https://localhost:5000/api/races/1
```

The Frontend of the application was made with Angular 8. To publish the frontend locally run the following command inside of the PresentationWeb folder

```bash
npm install
ng serve
```

That's it! You have now the complete app running locally

## Project Structure

```
RaceAnalysis
├── PresentationWeb    # Client App (UI) made with Angular.
├── PresenterWeb       # Rest api framework project.
├── Application        # UseCases project.
├── Domain             # Module with all the business logic, entity (domain) models.
├── Repository         # All persistance related stuff goes here.
```

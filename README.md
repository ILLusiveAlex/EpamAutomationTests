# EPAM Automation Tests with SpecFlow

This project demonstrates the implementation of automated tests using Selenium WebDriver along with SpecFlow for Behavioral Driven Development (BDD).

## Project Structure

- **Features**: Contains Gherkin feature files that describe test scenarios in a human-readable format.
- **StepDefinitions**: Contains step definition classes that map Gherkin steps to code.
- **Pages**: Contains Page Object classes that encapsulate page elements and actions.
- **Core**: Contains core functionality like browser factory, logger, and configuration.
- **Hooks**: Contains SpecFlow hooks for setup and teardown.

## Test Scenarios

### Services Navigation Test

This test validates the navigation to the Services section of the EPAM website:

1. Navigate to https://www.epam.com/
2. Click on the "Services" link in the main navigation menu
3. Select a specific service category: "Generative AI" or "Responsible AI"
4. Validate that the page contains the correct title
5. Validate that the section 'Our Related Expertise' is displayed on the page

## Running Tests

To run the tests, you can use Visual Studio's Test Explorer or run the following command:

```
dotnet test
```

## Dependencies

- Selenium WebDriver
- SpecFlow
- MSTest
- NLog

## Setup

1. Clone the repository
2. Restore NuGet packages
3. Build the solution
4. Run the tests
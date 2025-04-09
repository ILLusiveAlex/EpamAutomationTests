Feature: EPAM Services Navigation
    As a user
    I want to navigate to specific service categories on the EPAM website
    So that I can learn more about EPAM's services

    @ServicesNavigation
    Scenario Outline: Validate Navigation to Different Service Categories
        Given I am on the EPAM homepage
        When I hover over the "Services" link in the main navigation menu
        And I select "<serviceCategory>" from the dropdown
        Then I should see the correct page title
        And I should see the "Our Related Expertise" section on the page

        Examples:
            | serviceCategory |
            | Generative AI   |
            | Responsible AI  | 
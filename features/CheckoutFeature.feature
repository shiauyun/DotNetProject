Feature: Checkout System

    In order to get the total from API
    As an API user
    I want to get the total by the order

    Scenario: One order
    Given I have taken the order from customer with following order "{'OrderID': 1,'Starters':4,'Mains':4,'Drinks':4}"
    When I post to checkout API with order details
    Then the final total should be 58.4
    And response code should return "200"

    Scenario Outline: Update order
    Given I have taken the initial order from customer with following order <OrderID> <InitialStarter> <InitialMain> <InitialDrink>
    When I post to checkout API with order details
    And I updated the same order <Starter> <Main> <Drink> to checkout
    Then the initial total should be <InitialTotal>
    And the final total should be <Total>
    And response code should return "200"

    Examples:
    | OrderID | InitialStarter | InitialMain | InitialDrink | Starter | Main | Drink | InitialTotal | Total |
    | 2       | 1              | 2           | 0            | 0       | 2    | 0     | 19.8         | 35.2  |
    | 3       | 4              | 4           | 4            | -1      | -1   | -1    | 58.4         | 43.8  |
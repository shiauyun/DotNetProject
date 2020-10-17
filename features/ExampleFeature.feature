Feature: Checkout System

    In order to get the total from API
    As an API user
    I want to get the total by the order

    Scenario: One time order
    Given I have entered the orders from customer
    When I press checkout
    Then the result should be the total of all orders
    And response code should be returned 200 OK

    Scenario: Update to add more orders
    Given I have entered the orders from customer
    When I press checkout
    And I added more orders
    And I press checkout
    Then the result should be the total of updated orders
    And response code should be returned '200'

    Scenario: Update to cancel orders
    Given I have entered the orders from customer
    When I press checkout
    And I cancelled some orders
    And I press checkout
    Then the result should be the total of updated orders
    And response code should be returned '200'
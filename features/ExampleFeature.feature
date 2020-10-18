Feature: Checkout System

    In order to get the total from API
    As an API user
    I want to get the total by the order

    Scenario: One time order
    Given I have taken the orders from customer
    When I post to checkout API with following order details "{'OrderID': '1','Starters':'4','Mains':'4','Drinks':'4'}"
    Then the total should be "58.4"
    And response code should be returned "200"

    Scenario: One order
    Given I have taken the order from customer with following order "{'OrderID': '1','Starters':'4','Mains':'4','Drinks':'4'}"
    When I post to checkout API with order details
    Then the total should be "58.4"
    And response code should be returned "200"

    Scenario Outline: Update order
    Given I have taken the order from customer with following order "{'OrderID': '2','Starters':'1','Mains':'2'}"
    When I post to checkout API with order details
    And I updated the same order <Starters> <Mains> <Drinks> to checkout
    Then the total should be <Total>
    And response code should be returned "200"

    Example: order
    | Starters | Mains | Drinks | Total |
    | 0        | 2     | 0      | 35.2  |
    | -1       | -1    | -1     | 43.8  |


    # Scenario Outline: Update orders
    # Given I have entered the orders from customer
    # When I post to checkout API with following order details "{'OrderID': '2','Starters':'1','Mains':'2'}"
    # And I post to checkout API with following updated order details "{'OrderID': '2','Mains':'2'}"
    # Then the total should be "58.4"
    # And response code should be returned "200"

    # Example: 
    # | OrderID | Mains |
    # | 2       | 2     |
    # | 3       | 3     |


    # Scenario: Update to cancel orders
    # Given I have entered the orders from customer
    # When I press checkout
    # And I cancelled some orders
    # And I press checkout
    # Then the result should be the total of updated orders
    # And response code should be returned '200'
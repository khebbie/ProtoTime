Feature: Stamping a date
  In order to format dates in a more programmer-friendly way
  the FormatLike
  formats a date given a human-readable example.

  @date
  Scenario Outline: Formatting dates by example
    Given the date September 8, 2011
    When I stamp the example "<example>"
    Then I produce "<output>"


    Examples:
      | example                  | output                       |
      | January                  | September                    |
      | Jan                      | Sep                          |
      | Jan 1                    | Sep  8                       |
      | Jan 31                   | Sep 08                       |
      | Jan 31                   | Sep 08                       |
      | Jan 1, 1999              | Sep  8, 2011                 |
      | Monday                   | Thursday                     |
      | Tue, Jan 1               | Thu, Sep  8                  |
      | Tuesday, January 1, 1999 | Thursday, September  8, 2011 |
      | 31/1999                  | 09/2011                      |
      | 01/31                    | 09/08                        |
      | 01/99                    | 09/11                        |
      | 01/31/1999               | 09/08/2011                   |
      | 12/31/99                 | 09/08/11                     |
      | 31/12                    | 08/09                        |
      | 31/12/99                 | 08/09/11                     |
      | 31-Jan-1999              | 08-Sep-2011                  |
      | 1999-12-31               | 2011-09-08                   |
      | DOB: 12-31-1999          | DOB: 09-08-2011              |
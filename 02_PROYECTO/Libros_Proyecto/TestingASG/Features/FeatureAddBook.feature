Feature: AddBook
  In order to add a new book
  As a user
  I want to fill the book details and save it

  Scenario Outline: Add a book
    Given Quiero agregar un libro en la sección de libros
    When rellene los campos de "<nombre>", "<añoPublicacion>", "<isbn>", "<editorial>", "<stock>", "<generoLiterario>", "<autor>"
    Then compruebo que el libro se agregó en la ventana principal /books

    Examples:
      | nombre           | añoPublicacion | isbn        | editorial | stock | generoLiterario | autor              |
      | Dune             | 1965           | 9780441013593 | Ace Books | 10    | Ciencia Ficcion | Edgar Allan Poe            |
      | Les Miserables   | 1862           | 9780451419439 | Penguin   | 5     | Romance         | Victor Hugo        |



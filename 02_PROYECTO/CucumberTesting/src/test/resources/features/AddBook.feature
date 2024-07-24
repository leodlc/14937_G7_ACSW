Feature: Agregar nuevo libro

  Scenario: Validar que un libro sea agregado correctamente
    Given Quiero agregar un nuevo libro
    When Relleno los campos, nombre: "<nombre>", isbn: "<isbn>", editorial: "<editorial>", precio: "<precio>", autor: "<autor>", año: "<año>", genero: "<genero>"
    Then El libro "<nombre>" debe ser agregado correctamente

    Examples:
      | nombre                  | isbn              | editorial  | precio | autor                    | año  | genero   |
      | El señor de los anillos | 978-84-450-7379-1 | Minotauro  | 20     | J.R.R. Tolkien           | 1954 | Aventura |
      | Harry Potter            | 978-84-450-7379-2 | Salamandra | 15     | J.K. Rowling             | 1997 | Suspenso |
      | El principito           | 978-84-450-7379-3 |            | 10     | Antoine de Saint-Exupéry | 1943 | Aventura |
      |                         | 978-84-450-7379-1 | Minotauro  | 20     | J.R.R. Tolkien           | 1954 | Aventura |
      | Mi libro                |                   | Minotauro  | 20     | J.R.R. Tolkien           | 1954 | Drama    |
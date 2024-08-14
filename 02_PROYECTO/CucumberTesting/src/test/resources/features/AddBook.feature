Feature: Agregar nuevo libro

  Scenario: Validar que un libro sea agregado correctamente
    Given Quiero agregar un nuevo libro
    When Relleno los campos, nombre: "<nombre>", isbn: "<isbn>", editorial: "<editorial>", precio: "<precio>", autor: "<autor>", año: "<año>", genero: "<genero>"
    Then El libro "<nombre>" debe ser agregado correctamente

    Examples:
      | nombre                  | isbn              | editorial  | precio | autor          | año  | genero   |
      | El señor de los anillos | 978-84-450-7379-1 | Minotauro  | 20     | sinautor       | 1954 | Aventura |
      | Harry Potter            | 978-84-450-7379-2 | Salamandra | 15     | Joanne Rowling | 1997 | Suspenso |
      | 1984                    | 978-84-450-7379-3 | Test       | 10     | George Orwell  | 1943 | Aventura |
Feature: Agregar nuevo libro

  Scenario: Validar que un libro sea agregado correctamente
    Given Quiero agregar un nuevo libro
    When Relleno los campos, nombre: "<nombre>", isbn: "<isbn>", editorial: "<editorial>", precio: "<precio>", autor: "<autor>", año: "<año>", genero: "<genero>"
    Then El libro "<nombre>" debe ser agregado correctamente

    Examples:
      | nombre                  | isbn              | editorial  | precio | autor          | año  | genero   |
      | El señor de los anillos | 978-84-450-7379-1 | Minotauro  | 20     | John Ronald    | 1954 | Aventura |
      | Harry Potter            | 978-84-450-7379-2 | Salamandra | 15     | Joanne Rowling | 1997 | Suspenso |

  Scenario: Validar que un libro no sea agregado correctamente
    Given Quiero agregar un nuevo libro
    When Relleno los campos, nombre: "<nombre>", isbn: "<isbn>", editorial: "<editorial>", precio: "<precio>", autor: "<autor>", año: "<año>", genero: "<genero>"
    Then El libro "<nombre>" debe ser agregado correctamente

    Examples:
      | nombre       | isbn              | editorial  | precio | autor       | año  | genero   |
      |              | 978-84-450-7379-1 | Minotauro  | 20     | John Ronald | 1954 | Aventura |
      | Harry Potter |                   | Salamandra | 15     | asdfghjk    | 1997 | Suspenso |
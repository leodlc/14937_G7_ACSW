Feature: Editar libro

  Scenario: Validar que un libro sea editado correctamente
    Given Quiero editar un libro
    When Creo un nuevo libro con nombre "<nombre>"
    When Edito el nombre del libro "<nombre>" con el nuevo nombre "<nuevo_nombre>"
    Then El libro debe ser editado correctamente

    Examples:
      | nombre | nuevo_nombre  |
      | test   |               |
      | test2  | nuevo nombre2 |
      | test3  | nuevo nombre3 |
Feature: Editar autor

  Scenario: Validar que un autor sea editado correctamente
    Given Quiero editar un autor
    When Creo un nuevo autor con nombre "<nombre>" y apellido "<apellido>"
    When Edito el nombre del autor "<nombre>" con el nuevo nombre "<nuevo_nombre>"
    Then El autor debe ser editado correctamente

    Examples:
      | nombre | apellido | nuevo_nombre |
      | Juan   | Perez    | Pedro        |
      | Maria  | Lopez    |              |
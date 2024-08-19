Feature: Editar autor

  Scenario: Validar que un autor sea editado correctamente
    Given Quiero editar un autor
    When Creo un nuevo autor con nombre "<nombre>" y apellido "<apellido>"
    When Edito el nombre del autor "<nombre>" con el nuevo nombre "<nuevo_nombre>"
    Then El autor debe ser editado correctamente

    Examples:
      | nombre | apellido | nuevo_nombre |
      | Juan   | Andres   | Pedro        |
      | Maria  | Lopez    | Paul         |

  Scenario: Validar que un autor no sea editado correctamente
    Given Quiero editar un autor
    When Creo un nuevo autor con nombre "<nombre>" y apellido "<apellido>"
    When Edito el nombre del autor "<nombre>" con el nuevo nombre "<nuevo_nombre>"
    Then El autor debe ser editado correctamente

    Examples:
      | nombre | apellido | nuevo_nombre |
      | Juan   |          | Pedro        |
      | Maria  | Lopez    |              |
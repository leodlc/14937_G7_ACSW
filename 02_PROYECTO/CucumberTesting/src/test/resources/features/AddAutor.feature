Feature: Agregar nuevo autor

  Scenario: Validar que un autor sea agregado correctamente
    Given Quiero agregar un nuevo autor
    When Relleno los campos, nombre: "<nombre>", apellido: "<apellido>", pseudónimo: "<pseudonimo>"
    Then El autor "<nombre>" "<apellido>" debe ser agregado correctamente

    Examples:
      | nombre | apellido | pseudonimo |
      | John   | Doe      | jdoe       |
      | Paul   | García   | Gabo       |

  Scenario: Validar que un autor no sea agregado correctamente
    Given Quiero agregar un nuevo autor
    When Relleno los campos, nombre: "<nombre>", apellido: "<apellido>", pseudónimo: "<pseudonimo>"
    Then El autor "<nombre>" "<apellido>" debe ser agregado correctamente

    Examples:
      | nombre | apellido | pseudonimo |
      | John   |          | jdoe       |
      | Paul   | García   | Gabo       |
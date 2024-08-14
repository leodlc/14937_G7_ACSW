Feature: Agregar nuevo autor

  Scenario: Validar que un autor sea agregado correctamente
    Given Quiero agregar un nuevo autor
    When Relleno los campos, nombre: "<nombre>", apellido: "<apellido>", pseudónimo: "<pseudonimo>"
    Then El autor "<nombre>" "<apellido>" debe ser agregado correctamente

    Examples:
      | nombre  | apellido | pseudonimo |
      | J.R.R.  | Tolkien  |            |
      |         | García   | Gabo       |
      | Antoine | de Saint | Exupéry    |
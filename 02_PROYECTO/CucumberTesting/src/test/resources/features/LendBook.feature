Feature: Préstamo de libro

  Scenario: Validar que un libro se pueda prestar correctamente
    Given Quiero registrar el prestamo de un libro
    When Selecciono el libro en la pagina de inicio para prestar
    When Ingreso los datos cedula: "<cedula>" y correo: "<correo>"
    Then El libro debe aparecer en la lista de prestamos

    Examples:
      | cedula     | correo         |
      | 1726744293 | test@gmail.com |
      | 1726744293 | test@gmail.com |

  Scenario: Validar que un libro no se pueda prestar correctamente
    Given Quiero registrar el prestamo de un libro
    When Selecciono el libro en la pagina de inicio para prestar
    When Ingreso los datos cedula: "<cedula>" y correo: "<correo>"
    Then El libro debe aparecer en la lista de prestamos

    Examples:
      | cedula     | correo         |
      | 1726744293 | test@gmail.com |
      | 1726744293 |                |
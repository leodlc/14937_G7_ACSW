Feature: Listar Últimos Préstamos

  Scenario: Validar que los últimos préstamos se registren correctamente
    Given He registrado algunos préstamos recientes
    When Consulto el historial de préstamos con el libro: <titulo>, autor: <autor>, cédula: <cedula>, email: <email>, fecha: <fecha>, estado: <estado>
    Then Debo ver los últimos préstamos registrados con los detalles correctos

    Examples:
      | titulo                                   | autor          | cedula     | email                   | fecha               | estado                       |
      | Harry Potter y el prisionero de Azkabhan | Joanne Rowling | 1726744293 | abc@test.com            | 14/08/2024 0:18:16  | El libro ya ha sido devuelto |
      | Brave New World                          | Aldous Huxley  | 1726744300 | huxley@example.com      | 12/08/2024 14:20:30 | El libro no ha sido devuelto |
      | Fahrenheit 451                           | Ray Bradbury   | 1726744311 | raybradbury@example.com | 11/08/2024 09:15:00 | El libro ya ha sido devuelto |
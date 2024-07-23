using System;
using System.Text.RegularExpressions;

namespace Presentacion_Web.Utils
{
    public static class Validations
    {
        public static void VerificaIdentificacion(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
            {
                throw new ArgumentException("La identificación no puede estar vacía.");
            }

            if (identificacion.Length < 10)
            {
                throw new ArgumentException("La identificación debe tener al menos 10 caracteres.");
            }

            if (!Regex.IsMatch(identificacion, "^[a-zA-Z0-9]+$"))
            {
                throw new ArgumentException("La identificación solo puede contener caracteres alfanuméricos.");
            }

            char[] valced = identificacion.Trim().ToCharArray();
            int provincia = int.Parse(valced[0].ToString() + valced[1].ToString());

            if (provincia < 1 || provincia > 31)
            {
                throw new ArgumentException("La provincia en la identificación no es válida.");
            }

            if (int.Parse(valced[2].ToString()) < 6)
            {
                if (!VerificaCedula(valced))
                {
                    throw new ArgumentException("La cédula no es válida.");
                }
            }
            else if (int.Parse(valced[2].ToString()) == 6 || int.Parse(valced[2].ToString()) == 9)
            {
                if (valced.Length != 13)
                {
                    throw new ArgumentException("El RUC debe tener 13 caracteres.");
                }
            }
            else
            {
                throw new ArgumentException("El tercer dígito de la identificación no es válido.");
            }
        }

        private static bool VerificaCedula(char[] validarCedula)
        {
            int aux = 0, par = 0, impar = 0, verifi;

            for (int i = 0; i < 9; i += 2)
            {
                aux = 2 * int.Parse(validarCedula[i].ToString());
                if (aux > 9) aux -= 9;
                par += aux;
            }

            for (int i = 1; i < 9; i += 2)
            {
                impar += int.Parse(validarCedula[i].ToString());
            }

            aux = par + impar;
            verifi = aux % 10 != 0 ? 10 - (aux % 10) : 0;

            return verifi == int.Parse(validarCedula[9].ToString());
        }

        public static void ValidaNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre) || !Regex.IsMatch(nombre, @"^[a-zA-Z]+$"))
            {
                throw new ArgumentException("El nombre solo puede contener letras.");
            }
        }

        public static void ValidaApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido) || !Regex.IsMatch(apellido, @"^[a-zA-Z]+$"))
            {
                throw new ArgumentException("El apellido solo puede contener letras.");
            }
        }

        public static void ValidaFecha(DateTime fecha)
        {
            if (fecha > DateTime.Now)
            {
                throw new ArgumentException("La fecha de nacimiento no puede ser en el futuro.");
            }
        }

        public static void ValidaTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono) || !Regex.IsMatch(telefono, @"^\d{10}$"))
            {
                throw new ArgumentException("El teléfono debe contener exactamente 10 dígitos numéricos.");
            }
        }

        public static void ValidaDireccion(string direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion) || !Regex.IsMatch(direccion, @"^[a-zA-Z0-9\s\-]+$"))
            {
                throw new ArgumentException("La dirección solo puede contener letras, números, espacios y guiones.");
            }
        }

        public static void VerificaInputStr(string texto)
        {
            if (Regex.IsMatch(texto, @"[;'\-\""\*/]+"))
            {
                throw new ArgumentException("El texto contiene caracteres especiales no permitidos.");
            }
        }
    }
}

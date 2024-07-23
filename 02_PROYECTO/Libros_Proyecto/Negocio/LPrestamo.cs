using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LPrestamo
    {
        public PRESTAMO Create(PRESTAMO prestamo)
        {
            PRESTAMO result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                PRESTAMO prestamoTemp = r.Retrieve<PRESTAMO>(p => p.IDCLIENTE == prestamo.IDCLIENTE && p.IDLIBRO == prestamo.IDLIBRO && p.FECHAFINPREST == null);
                if (prestamoTemp == null)
                {
                    result = r.Create(prestamo);
                }
                else
                {
                    // Caso negativo, el préstamo ya existe o no se ha devuelto
                }
            }
            return result;
        }


        // Al parecer no existe un id para el prestamo asi que esta pendiente esta funcion
        public PRESTAMO RetrieveById(int idCliente, int idLibro)
        {
            PRESTAMO result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Retrieve<PRESTAMO>(p => p.IDCLIENTE == idCliente && p.IDLIBRO == idLibro);
            }
            return result;
        }

        public bool Update(PRESTAMO prestamo, int idCliente, int idLibro)
        {
            bool result = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                PRESTAMO prestamoTemp = r.Retrieve<PRESTAMO>(p => p.IDCLIENTE == idCliente && p.IDLIBRO == idLibro);
                if (prestamoTemp != null)
                {
                    if (prestamo.IDCLIENTE != -1)
                    {
                        prestamoTemp.IDCLIENTE = prestamo.IDCLIENTE;
                    }
                    if(prestamo.IDLIBRO != -1)
                    {
                        prestamoTemp.IDLIBRO = prestamo.IDLIBRO;
                    }

                    if (prestamo.FECHAINIPREST.HasValue)
                    {
                        prestamoTemp.FECHAINIPREST = prestamo.FECHAINIPREST.Value;
                    }
                    if (prestamo.FECHAFINPREST.HasValue)
                    {
                        prestamoTemp.FECHAFINPREST = prestamo.FECHAFINPREST.Value;
                    }
                    if (!string.IsNullOrEmpty(prestamo.DESCRPREST))
                    {
                        prestamoTemp.DESCRPREST = prestamo.DESCRPREST;
                    }
                    if (prestamo.ESTADOPREST == true)
                    {
                        prestamoTemp.ESTADOPREST = true;

                    }
                    else
                    {
                        prestamoTemp.ESTADOPREST = false;
                    }
                    result = r.Update(prestamoTemp);
                }
                else
                {
                    // Caso negativo, el préstamo no existe
                }
            }
            return result;
        }

        public bool Delete(int idCliente, int idLibro)
        {
            bool result = false;
            var prestamoTemp = RetrieveById(idCliente, idLibro);
            if (prestamoTemp != null)
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    result = r.Delete(prestamoTemp);
                }
            }
            else
            {
                // Caso negativo, el préstamo no existe
            }
            return result;
        }

        public List<PRESTAMO> RetrieveAll()
        {
            List<PRESTAMO> result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Filter<PRESTAMO>(p => true); // Obtener todos los préstamos
            }
            return result;
        }

        public List<PRESTAMO> RetrieveByClient(int id)
        {
            List<PRESTAMO> result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Filter<PRESTAMO>(p => p.IDCLIENTE==id); // Obtener prestamos por id del Cliente
            }
            return result;
        }

        public List<PRESTAMO> RetrieveByBook(int id)
        {
            List<PRESTAMO> result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Filter<PRESTAMO>(p => p.IDLIBRO == id); // Obtener prestamos por id del Cliente
            }
            return result;
        }
    }
}

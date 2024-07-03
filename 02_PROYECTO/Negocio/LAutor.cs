using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LAutor
    {
        public AUTOR Create(AUTOR autor)
        {
            AUTOR result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                AUTOR autorTemp = r.Retrieve<AUTOR>(a => a.NOMBREAUTOR == autor.NOMBREAUTOR && a.APELLIDOAUTOR == autor.APELLIDOAUTOR);
                if (autorTemp == null)
                {
                    result = r.Create(autor);
                }
                else
                {
                    // Caso negativo, el autor ya existe
                }
            }
            return result;
        }

        public AUTOR RetrieveById(int id)
        {
            AUTOR result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Retrieve<AUTOR>(a => a.IDAUTOR == id);
            }
            return result;
        }
        /* public bool Update(CLIENTE cliente, int id)
        {
            bool result = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                CLIENTE clienteTemp = r.Retrieve<CLIENTE>(c => c.IDCLIENTE == id);
                if (clienteTemp != null)
                {
                    // Solo actualiza los campos que no son nulos
                    if (!string.IsNullOrEmpty(cliente.CEDULACLIENTE))
                    {
                        clienteTemp.CEDULACLIENTE = cliente.CEDULACLIENTE;
                    }

                    if (cliente.FECHANACCLIENTE.HasValue)
                    {
                        clienteTemp.FECHANACCLIENTE = cliente.FECHANACCLIENTE.Value;
                    }

                    if (!string.IsNullOrEmpty(cliente.APELLIDOCLIENTE))
                    {
                        clienteTemp.APELLIDOCLIENTE = cliente.APELLIDOCLIENTE;
                    }

                    if (!string.IsNullOrEmpty(cliente.NOMBRECLIENTE))
                    {
                        clienteTemp.NOMBRECLIENTE = cliente.NOMBRECLIENTE;
                    }

                    if (!string.IsNullOrEmpty(cliente.DIRECCLIENTE))
                    {
                        clienteTemp.DIRECCLIENTE = cliente.DIRECCLIENTE;
                    }

                    if (!string.IsNullOrEmpty(cliente.TELEFONOCLIENTE))
                    {
                        clienteTemp.TELEFONOCLIENTE = cliente.TELEFONOCLIENTE;
                    }

                    if (cliente.ESTADOCLIENTE==true)
                    {
                        // El valor es verdadero (true)
                        clienteTemp.ESTADOCLIENTE = true;
                    }
                    else
                    {
                        // El valor es falso (false)
                        clienteTemp.ESTADOCLIENTE = false;
                    }


                    result = r.Update(clienteTemp);
                }
                else
                {
                    // Caso negativo, el cliente no existe
                }
            }
            return result;

        }
*/
        public bool Update(AUTOR autor, int id)
        {
            bool result = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                AUTOR autorTemp = r.Retrieve<AUTOR>(a => a.IDAUTOR == id);
                if (autorTemp != null)
                {
                    if (!string.IsNullOrEmpty(autor.NOMBREAUTOR))
                    {
                        autorTemp.NOMBREAUTOR = autor.NOMBREAUTOR;
                    }

                    if (!string.IsNullOrEmpty(autor.APELLIDOAUTOR))
                    {
                        autorTemp.APELLIDOAUTOR = autor.APELLIDOAUTOR;
                    }

                    if (!string.IsNullOrEmpty(autor.NACIONALIDADAUTOR))
                    {
                        autorTemp.NACIONALIDADAUTOR = autor.NACIONALIDADAUTOR;
                    }

                    if (!string.IsNullOrEmpty(autor.BIOGRAFIAAUTOR))
                    {
                        autorTemp.BIOGRAFIAAUTOR = autor.BIOGRAFIAAUTOR;
                    }
                    result = r.Update(autorTemp);
                }
                else
                {
                    // Caso negativo, el autor no existe
                }
            }
            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            var autorTemp = RetrieveById(id);
            if (autorTemp != null)
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    result = r.Delete(autorTemp);
                }
            }
            else
            {
                // Caso negativo, el autor no existe
            }
            return result;
        }

        public List<AUTOR> RetrieveAll()
        {
            List<AUTOR> result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Filter<AUTOR>(a => true); // Obtener todos los autores
            }
            return result;
        }
    }
}

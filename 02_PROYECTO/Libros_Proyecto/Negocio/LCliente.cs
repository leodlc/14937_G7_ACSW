using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LCliente
    {
        public CLIENTE Create(CLIENTE cliente)
        {
            CLIENTE result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                CLIENTE clienteTemp = r.Retrieve<CLIENTE>(c => c.CEDULACLIENTE == cliente.CEDULACLIENTE);
                if (clienteTemp == null)
                {
                    result = r.Create(cliente);
                }
                else
                {
                    // Caso negativo, el cliente ya existe
                }
            }
            return result;
        }

        public CLIENTE RetrieveById(int id)
        {
            CLIENTE result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Retrieve<CLIENTE>(c => c.IDCLIENTE == id);
            }
            return result;
        }

        public bool Update(CLIENTE cliente, int id)
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

        public bool Delete(int id)
        {
            bool result = false;
            var clienteTemp = RetrieveById(id);
            if (clienteTemp != null)
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    result = r.Delete(clienteTemp);
                }
            }
            else
            {
                // Caso negativo, el cliente no existe
            }
            return result;
        }

        public List<CLIENTE> RetrieveAll()
        {
            List<CLIENTE> result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Filter<CLIENTE>(c => true); // Obtener todos los clientes
            }
            return result;
        }
    }

}

using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //AGREGAR DATOS
                //AddClient();
                //AddGenreL();
                //AddAutor();
                //AddBook();

                //AddPrestamo();


                // Cambiar el ID según sea necesario

                //RetrieveAndUpdateClient(1); 
                //RetrieveAndUpdateGenreL(1); 
                //RetrieveAndUpdateAutor(1); 
                //RetrieveAndUpdateBook(1);
                //

                //RetrieveAndUpdatePrestamoByClientId(3, true);

                //RetrieveAndUpdatePrestamoDesc(3, "Descripción actualizada");

                //OBTENER INFORMACIÓN (Cambiar el id según sea necesario)

                //Console.WriteLine("****Obtener info por ID****");
                //GetBookById(1);
                //GetAutorById(1);
                //GetGenreLById(1);
                //GetClientById(1);

                GetPrestamoByClientId(3);


                //OBTENER INFORMACIÓN (GET ALL)

                //Console.WriteLine("****Obtener todos los registros de cada entidad****");
                //GetAllClients();
                //GetAllAutors();
                //GetAllBooks();
                //GetAllGenresL();

                GetAllPrestamos();

                //ELIMINAR POR ID

                //DeleteClient(1);
                //DeleteBook(3);
                






            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }

        // CREAR DATOS

        static void AddClient()
        {
            var cl = new CLIENTE();
            //CAMBIAR DE DATOS PARA PROBAR
            cl.CEDULACLIENTE = "1724788953";
            cl.NOMBRECLIENTE = "Nathaly";
            cl.APELLIDOCLIENTE = "Chicaiza";
            cl.FECHANACCLIENTE = new DateTime(2000, 4, 14); // Asignar la fecha correctamente
            cl.TELEFONOCLIENTE = "0983805697";
            cl.DIRECCLIENTE = "Conocoto";
            cl.ESTADOCLIENTE = true;

            try
            {
                Validations.VerificaIdentificacion(cl.CEDULACLIENTE);
                Validations.ValidaNombre(cl.NOMBRECLIENTE);
                Validations.ValidaApellido(cl.APELLIDOCLIENTE);
                Validations.ValidaFecha(cl.FECHANACCLIENTE.Value);
                Validations.ValidaTelefono(cl.TELEFONOCLIENTE);
                Validations.ValidaDireccion(cl.DIRECCLIENTE);

                using (var r = RepositoryFactory.CreateRepository())
                {
                    r.Create(cl);
                }

                Console.WriteLine("Id cliente {0}", cl.IDCLIENTE);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Error en la validación de los datos del cliente: {ex.Message}");
            }



        }

        static void AddGenreL()
        {
            var genl = new GENEROLITERARIO();
            genl.NOMBREGL = "Romance";
            genl.DESCRIPGL = "Libros con temática de romance o amoríos";
            try
            {
                Validations.VerificaInputStr(genl.NOMBREGL);
                Validations.VerificaInputStr(genl.DESCRIPGL);



                using (var r = RepositoryFactory.CreateRepository())
                {
                    r.Create(genl);
                }

                Console.WriteLine("Id del género literario {0}", genl.IDGL);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Error en la validación de los datos del gémero del libro: {ex.Message}");
            }

        }
        static void AddAutor()
        {
            var atr = new AUTOR();
            atr.NOMBREAUTOR = "Victor";
            atr.APELLIDOAUTOR = "Hugo";
            atr.NACIONALIDADAUTOR = "Francés";
            atr.BIOGRAFIAAUTOR = "Poeta, dramaturgo y novelista romántico francés, considerado como uno de los más importantes en lengua francesa";

            try
            {
                
                Validations.ValidaNombre(atr.NOMBREAUTOR);
                Validations.ValidaApellido(atr.NOMBREAUTOR);
                Validations.VerificaInputStr(atr.NACIONALIDADAUTOR);
                Validations.VerificaInputStr(atr.BIOGRAFIAAUTOR);
                

                using (var r = RepositoryFactory.CreateRepository())
                {
                    r.Create(atr);
                }

                Console.WriteLine("Id autor {0}", atr.IDAUTOR);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Error en la validación de los datos del autor: {ex.Message}");
            }
        }
        static void AddBook()
        {
            var lb = new LIBRO();
            lb.IDAUTOR = 2;
            lb.IDGL = 2;
            lb.NOMBRELIBRO = "Los miserables";
            lb.ANIOPUBLIBRO = new DateTime(1818, 1, 1);
            lb.IMGLIBRO = "test2.jpg";
            lb.ISBNLIBRO = "7894-5678-1425-457";
            lb.ESTADOLIBRO = true;
            lb.EDITORIALLIBRO = "Santillana";
            lb.STOCKLIBRO = 5;

            try
            {

                Validations.VerificaInputStr(lb.NOMBRELIBRO);
                Validations.ValidaFecha(lb.ANIOPUBLIBRO.Value);
                Validations.VerificaInputStr(lb.IMGLIBRO);
                Validations.VerificaInputStr(lb.EDITORIALLIBRO);



                using (var r = RepositoryFactory.CreateRepository())
                {
                    r.Create(lb);
                }

                Console.WriteLine("Id libro {0}", lb.IDLIBRO);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Error en la validación de los datos del libro: {ex.Message}");
            }



        }
        static void AddPrestamo()
        {
            var prest = new PRESTAMO();
            prest.IDCLIENTE = 3;
            prest.IDLIBRO = 2;
            prest.FECHAINIPREST = DateTime.Now;
            prest.FECHAFINPREST = new DateTime(2024,6,12);
            prest.DESCRPREST = "Prestamo de libro Los miserables a Nathaly Chicaiza";
            try
            {

                
                Validations.VerificaInputStr(prest.DESCRPREST);



                using (var r = RepositoryFactory.CreateRepository())
                {
                    r.Create(prest);
                }

                Console.WriteLine("Id prestamo creado: {0}");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Error en la validación de los datos del préstamo: {ex.Message}");
            }

        }

        //FUNCIONES PARA OBTENER LA INFORMACIÓN

        static void GetClientById(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var cliente = r.Retrieve<CLIENTE>(c => c.IDCLIENTE == id);
                if (cliente != null)
                {
                    Console.WriteLine($"Cliente encontrado: {cliente.NOMBRECLIENTE} {cliente.APELLIDOCLIENTE}, Teléfono: {cliente.TELEFONOCLIENTE}, Dirección: {cliente.DIRECCLIENTE}");
                }
                else
                {
                    Console.WriteLine("Cliente no encontrado.");
                }
            }
        }

        static void GetAllClients()
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var clientes = r.Filter<CLIENTE>(c => true);
                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"Cliente: {cliente.NOMBRECLIENTE} {cliente.APELLIDOCLIENTE}, Teléfono: {cliente.TELEFONOCLIENTE}, Dirección: {cliente.DIRECCLIENTE}");
                }
            }
        }

        static void GetGenreLById(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var genero = r.Retrieve<GENEROLITERARIO>(g => g.IDGL == id);
                if (genero != null)
                {
                    Console.WriteLine($"Género Literario encontrado: {genero.NOMBREGL}, Descripción: {genero.DESCRIPGL}");
                }
                else
                {
                    Console.WriteLine("Género Literario no encontrado.");
                }
            }
        }

        static void GetAllGenresL()
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var generos = r.Filter<GENEROLITERARIO>(g => true);
                foreach (var genero in generos)
                {
                    Console.WriteLine($"Género Literario: {genero.NOMBREGL}, Descripción: {genero.DESCRIPGL}");
                }
            }
        }

        static void GetAutorById(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var autor = r.Retrieve<AUTOR>(a => a.IDAUTOR == id);
                if (autor != null)
                {
                    Console.WriteLine($"Autor encontrado: {autor.NOMBREAUTOR} {autor.APELLIDOAUTOR}, Nacionalidad: {autor.NACIONALIDADAUTOR}, Biografía: {autor.BIOGRAFIAAUTOR}");
                }
                else
                {
                    Console.WriteLine("Autor no encontrado.");
                }
            }
        }

        static void GetAllAutors()
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var autores = r.Filter<AUTOR>(a => true);
                foreach (var autor in autores)
                {
                    Console.WriteLine($"Autor: {autor.NOMBREAUTOR} {autor.APELLIDOAUTOR}, Nacionalidad: {autor.NACIONALIDADAUTOR}, Biografía: {autor.BIOGRAFIAAUTOR}");
                }
            }
        }

        static void GetBookById(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var libro = r.Retrieve<LIBRO>(l => l.IDLIBRO == id);
                if (libro != null)
                {
                    Console.WriteLine($"Libro encontrado: {libro.NOMBRELIBRO}, Año de Publicación: {libro.ANIOPUBLIBRO}, ISBN: {libro.ISBNLIBRO}, Editorial: {libro.EDITORIALLIBRO}");
                }
                else
                {
                    Console.WriteLine("Libro no encontrado.");
                }
            }
        }

        static void GetAllBooks()
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var libros = r.Filter<LIBRO>(l => true);
                foreach (var libro in libros)
                {
                    Console.WriteLine($"Libro: {libro.NOMBRELIBRO}, Año de Publicación: {libro.ANIOPUBLIBRO}, ISBN: {libro.ISBNLIBRO}, Editorial: {libro.EDITORIALLIBRO}");
                }
            }
        }



        static void GetAllPrestamos()
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var prestamos = r.Filter<PRESTAMO>(p => true);
                foreach (var prestamo in prestamos)
                {
                    Console.WriteLine($"Prestamo: {prestamo.DESCRPREST}, Cliente: {prestamo.CLIENTE.NOMBRECLIENTE}, Libro: {prestamo.LIBRO.NOMBRELIBRO}");
                }
            }
        }

        static void GetPrestamoByClientId(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var prestamo = r.Retrieve<PRESTAMO>(p => p.IDCLIENTE == id);
                if (prestamo != null)
                {
                    Console.WriteLine($"Prestamo encontrado: {prestamo.DESCRPREST}, Cliente: {prestamo.CLIENTE.NOMBRECLIENTE}, Libro: {prestamo.LIBRO.NOMBRELIBRO}");
                }
                else
                {
                    Console.WriteLine("Prestamo no encontrado");
                }
            }
        }

        // FUNCIONES DE ACTUALIZACIÓN

        static void RetrieveAndUpdateClient(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var client = r.Retrieve<CLIENTE>(c => c.IDCLIENTE == id);
                if (client != null)
                {
                    Console.WriteLine($"Cliente encontrado: {client.NOMBRECLIENTE} {client.APELLIDOCLIENTE}");
                    client.NOMBRECLIENTE = "NActualizado";
                    r.Update(client);
                    Console.WriteLine("Cliente actualizado.");
                }
                else
                {
                    Console.WriteLine("Cliente no encontrado.");
                }
            }
        }

        static void RetrieveAndUpdateGenreL(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var genre = r.Retrieve<GENEROLITERARIO>(g => g.IDGL == id);
                if (genre != null)
                {
                    Console.WriteLine($"Género literario encontrado: {genre.NOMBREGL}");
                    genre.NOMBREGL = "GLActualizado";
                    r.Update(genre);
                    Console.WriteLine("Género literario actualizado.");
                }
                else
                {
                    Console.WriteLine("Género literario no encontrado.");
                }
            }
        }

        static void RetrieveAndUpdateAutor(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var autor = r.Retrieve<AUTOR>(a => a.IDAUTOR == id);
                if (autor != null)
                {
                    Console.WriteLine($"Autor encontrado: {autor.NOMBREAUTOR} {autor.APELLIDOAUTOR}");
                    autor.NOMBREAUTOR = "AtActualizado";
                    r.Update(autor);
                    Console.WriteLine("Autor actualizado.");
                }
                else
                {
                    Console.WriteLine("Autor no encontrado.");
                }
            }
        }

        static void RetrieveAndUpdateBook(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var book = r.Retrieve<LIBRO>(b => b.IDLIBRO == id);
                if (book != null)
                {
                    Console.WriteLine($"Libro encontrado: {book.NOMBRELIBRO}");
                    book.NOMBRELIBRO = "LActualizado";
                    r.Update(book);
                    Console.WriteLine("Libro actualizado.");
                }
                else
                {
                    Console.WriteLine("Libro no encontrado.");
                }
            }
        }

        static void RetrieveAndUpdatePrestamoByClientId(int idCliente, bool estado)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var prestamos = r.Filter<PRESTAMO>(p => p.IDCLIENTE == idCliente);
                foreach (var prestamo in prestamos)
                {
                    prestamo.ESTADOPREST = estado;
                    r.Update(prestamo);
                    Console.WriteLine($"Estado del préstamo actualizado: ID Cliente: {idCliente}, Estado: {estado}");
                }
            }
        }
        static void RetrieveAndUpdatePrestamoDesc(int idCliente, string nuevaDescripcion)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var prestamos = r.Filter<PRESTAMO>(p => p.IDCLIENTE == idCliente);
                foreach (var prestamo in prestamos)
                {
                    prestamo.DESCRPREST = nuevaDescripcion;
                    r.Update(prestamo);
                    Console.WriteLine($"Descripción del préstamo actualizada: ID Cliente: {idCliente}, Nueva Descripción: {nuevaDescripcion}");
                }
            }
        }


        //FUNCIONES DELETE

        static void DeleteClient(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var cliente = r.Retrieve<CLIENTE>(c => c.IDCLIENTE == id);
                if (cliente != null)
                {
                    r.Delete(cliente);
                    Console.WriteLine("Cliente eliminado.");
                }
                else
                {
                    Console.WriteLine("Cliente no encontrado.");
                }
            }
        }

        static void DeleteGenreL(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var genero = r.Retrieve<GENEROLITERARIO>(g => g.IDGL == id);
                if (genero != null)
                {
                    r.Delete(genero);
                    Console.WriteLine("Género Literario eliminado.");
                }
                else
                {
                    Console.WriteLine("Género Literario no encontrado.");
                }
            }
        }

        static void DeleteAutor(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var autor = r.Retrieve<AUTOR>(a => a.IDAUTOR == id);
                if (autor != null)
                {
                    r.Delete(autor);
                    Console.WriteLine("Autor eliminado.");
                }
                else
                {
                    Console.WriteLine("Autor no encontrado.");
                }
            }
        }

        static void DeleteBook(int id)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var libro = r.Retrieve<LIBRO>(l => l.IDLIBRO == id);
                if (libro != null)
                {
                    r.Delete(libro);
                    Console.WriteLine("Libro eliminado.");
                }
                else
                {
                    Console.WriteLine("Libro no encontrado.");
                }
            }
        }

        static void DeletePrestamo(int idCliente, int idLibro)
        {
            using (var r = RepositoryFactory.CreateRepository())
            {
                var prestamo = r.Retrieve<PRESTAMO>(p => p.IDCLIENTE == idCliente && p.IDLIBRO == idLibro);
                if (prestamo != null)
                {
                    r.Delete(prestamo);
                    Console.WriteLine($"Préstamo eliminado: ID Cliente: {idCliente}, ID Libro: {idLibro}");
                }
                else
                {
                    Console.WriteLine("Préstamo no encontrado.");
                }
            }
        }

    }
}

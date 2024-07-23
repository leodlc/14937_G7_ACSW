using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LLibro
    {
        public LIBRO Create(LIBRO libro)
        {
            LIBRO result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                LIBRO libroTemp = r.Retrieve<LIBRO>(l => l.ISBNLIBRO == libro.ISBNLIBRO);
                if (libroTemp == null)
                {
                    result = r.Create(libro);
                }
                else
                {
                }
            }
            return result;
        }

        public LIBRO RetrieveById(int id)
        {
            LIBRO result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Retrieve<LIBRO>(l => l.IDLIBRO == id);
            }
            return result;
        }

        public bool Update(LIBRO libro,int id)
        {
            bool result = false;
            using (var r = RepositoryFactory.CreateRepository())
            {
                LIBRO libroTemp = r.Retrieve<LIBRO>(l => l.IDLIBRO == id);
                if (libroTemp != null)
                {
                    if (!string.IsNullOrEmpty(libro.NOMBRELIBRO))
                    {
                        libroTemp.NOMBRELIBRO = libro.NOMBRELIBRO;
                    }

                    if (libro.ANIOPUBLIBRO.HasValue)
                    {
                        libroTemp.ANIOPUBLIBRO = libro.ANIOPUBLIBRO.Value;
                    }
                    if (!string.IsNullOrEmpty(libro.IMGLIBRO))
                    {
                        libroTemp.IMGLIBRO = libro.IMGLIBRO;
                    }
                    if (!string.IsNullOrEmpty(libro.ISBNLIBRO))
                    {
                        libroTemp.ISBNLIBRO = libro.ISBNLIBRO;
                    }
                    if (!string.IsNullOrEmpty(libro.EDITORIALLIBRO))
                    {
                        libroTemp.EDITORIALLIBRO = libro.EDITORIALLIBRO;
                    }
                    if (libro.IDAUTOR != -1)
                    {
                        libroTemp.IDAUTOR = libro.IDAUTOR;
                    }

                    if (libro.IDGL !=-1)
                    {
                        libroTemp.IDGL = libro.IDGL;
                    }
                    if (libro.STOCKLIBRO != -1)
                    {
                        libroTemp.STOCKLIBRO = libro.STOCKLIBRO;
                    }

                    if (libro.ESTADOLIBRO == true)
                    {
                        // El valor es verdadero (true)
                        libroTemp.ESTADOLIBRO = true;
                    }

                    else
                    {
                        // El valor es falso (false)
                        libroTemp.ESTADOLIBRO = false;
                    }

                    result = r.Update(libroTemp);
                }
                else
                {
                    // Caso negativo, el libro no existe
                }
            }
            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            var libroTemp = RetrieveById(id);
            if (libroTemp != null)
            {
                using (var r = RepositoryFactory.CreateRepository())
                {
                    result = r.Delete(libroTemp);
                }
            }
            else
            {
                // Caso negativo, el libro no existe
            }
            return result;
        }

        public List<LIBRO> RetrieveAll()
        {
            List<LIBRO> result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                result = r.Filter<LIBRO>(l => true); // Obtener todos los libros
            }
            return result;
        }
    }

}

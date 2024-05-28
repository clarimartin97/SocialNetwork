using System.Net.NetworkInformation;
using LogicaNegocio;

namespace InterfazUsuario
{
    public class Program
    {
        private static Sistema unSistema = new Sistema();

        static void Main()
        {
            int opcion;

            do
            {
                MostrarMenu();
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        AltaMiembro();
                        break;
                    case 2:
                        ListarPublicacionesPorEmail();
                        break;
                    case 3:
                        ListarPostsConComentariosPorEmail();
                        break;
                    case 4:
                        ListarPostsEntreFechas();
                        break;
                    case 5:
                        ObtenerMiembrosConMasPublicaciones();
                        break;
                    case 6:
                        Console.Write("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opcion no valida. Por favor, elegi una opción valida.");
                        break;
                }

                Console.WriteLine();
            } while (opcion != 6);
        }

        static void MostrarMenu()
        {
            Console.WriteLine("-------------------- MENU -------------------");
            Console.WriteLine(" 1. Alta de miembro");
            Console.WriteLine(" 2. Listar publicaciones por email");
            Console.WriteLine(" 3. Listar posts con comentarios por email");
            Console.WriteLine(" 4. Listar posts entre fechas");
            Console.WriteLine(" 5. Obtener miembros con mas publicaciones");
            Console.WriteLine(" 6. Salir");
            Console.WriteLine("---------------------------------------------\n");
        }

        static int LeerOpcion()
        {
            try
            {
                Console.Write("Ingrese una opcion: ");
                return int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        static void AltaMiembro()
        {
            try
            {
                Console.WriteLine("\n-------------- ALTA DE MIEMBRO --------------");
                Console.Write("Email: ");
                string email = Console.ReadLine() ?? "";
                email = email.ToLower();

                List<Miembro> miembros = unSistema.GetMiembros();
                if (!unSistema.ValidarEmail(email))
                {
                    Console.WriteLine("\n-------------------------------------");
                    Console.WriteLine(" ERROR: El email ya está registrado.");
                    Console.WriteLine("-------------------------------------");
                }
                else
                {
                    Console.Write("Cedula: ");
                    string cedula = Console.ReadLine() ?? "";
                    Console.Write("Nombre: ");
                    string nombre = Console.ReadLine() ?? "";
                    Console.Write("Apellido: ");
                    string apellido = Console.ReadLine() ?? "";
                    Console.Write("Fecha de nacimiento (dd/mm/aaaa): ");
                    DateTime fechaNacimiento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                    if (nombre.Trim() != "" && apellido.Trim() != "" && fechaNacimiento > DateTime.MinValue && fechaNacimiento < DateTime.Now && cedula.Trim() != "")
                    {
                        Miembro miembro = new Miembro(email, cedula, nombre, apellido, fechaNacimiento, false);
                        unSistema.SetUsuario(miembro);
                        Console.WriteLine("\n------------------------------");
                        Console.WriteLine(" Miembro agregado con exito.");
                        Console.WriteLine("------------------------------\n");
                    }
                    else
                    {
                        Console.WriteLine("\n--------------------------");
                        Console.WriteLine(" ERROR: Datos invalidos.");
                        Console.WriteLine("--------------------------\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ListarPublicacionesPorEmail()
        {
            try
            {
                Console.Write("\nIngrese el email del miembro: ");
                string email = Console.ReadLine() ?? "";
                Console.WriteLine();
                email = email.ToLower();

                Miembro? miembro = unSistema.GetMiembroPorEmail(email);

                if (miembro == null)
                {
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine(" ERROR: No se encontro un miembro con ese email.");
                    Console.WriteLine("-------------------------------------------------");
                }
                else
                {
                    List<Post> postsDeMiembro = unSistema.GetPostsPorAutor(miembro);
                    List<Comentario> comentariosDeMiembro = unSistema.GetComentariosPorAutor(miembro);

                    if (postsDeMiembro.Count > 0 || comentariosDeMiembro.Count > 0)
                    {
                        Console.WriteLine($"PUBLICACIONES DE: {miembro.Email}:\n");
                    }

                    if (postsDeMiembro.Count > 0)
                    {
                        Console.WriteLine($"------------------- POSTS -------------------\n");

                        foreach (Post post in postsDeMiembro)
                        {
                            Console.WriteLine("---------------------------------------------");
                            Console.WriteLine($" Fecha: {post.Fecha}");
                            Console.WriteLine($" Contenido: {post.Contenido}");
                            Console.WriteLine($" Autor: {post.Autor.Email}");
                            Console.WriteLine($" Estado: {post.TipoPost}");
                            Console.WriteLine("---------------------------------------------\n");
                        }
                    }
                    if (comentariosDeMiembro.Count > 0)
                    {
                        Console.WriteLine($"---------------- COMENTARIOS ----------------\n");

                        foreach (Comentario comentario in comentariosDeMiembro)
                        {
                            Console.WriteLine("---------------------------------------------");
                            Console.WriteLine($" Fecha: {comentario.Fecha}");
                            Console.WriteLine($" Contenido: {comentario.Contenido}");
                            Console.WriteLine($" Autor: {comentario.Autor.Email}");
                            Console.WriteLine($" Estado: {comentario.TipoComentario}");
                            Console.WriteLine("---------------------------------------------\n");
                        }
                    }
                    if (postsDeMiembro.Count == 0 && comentariosDeMiembro.Count == 0)
                    {
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.WriteLine($" No se encontraron publicaciones de: {miembro.Email}.");
                        Console.WriteLine("-------------------------------------------------------------------\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ListarPostsConComentariosPorEmail() //3 -> Listar los posts con comentarios de un miembro
        {
            try
            {
                Console.Write("\nIngrese el email del miembro: ");
                string email = Console.ReadLine() ?? "";
                Console.WriteLine();
                email = email.ToLower();

                Miembro? miembro = unSistema.GetMiembroPorEmail(email);

                if (miembro == null)
                {
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine(" ERROR: No se encontró un miembro con ese email.");
                    Console.WriteLine("-------------------------------------------------");
                }
                else
                {
                    List<Post> postsDeMiembro = unSistema.GetPostsComentadosPorMiembro(miembro);

                    if (postsDeMiembro.Count > 0)
                    {
                        Console.WriteLine($"Posts con comentarios de: {miembro.Email}:\n");
                        Console.WriteLine($"------------------- POSTS -------------------\n");

                        foreach (Post post in postsDeMiembro)
                        {
                            Console.WriteLine($" Fecha: {post.Fecha}");
                            Console.WriteLine($" Contenido: {post.Contenido}");
                            Console.WriteLine($" Autor: {post.Autor.Email}");
                            Console.WriteLine($" Estado: {post.TipoPost}\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"-----------------------------------------------------------------");
                        Console.WriteLine($" No se encontraron posts con comentarios de: {miembro.Email}.");
                        Console.WriteLine($"-----------------------------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ListarPostsEntreFechas() //4
        {
            try
            {
                Console.Write("\nIngrese la primera fecha (formato dd/mm/yyyy): ");
                DateTime fechaInicio = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                if (fechaInicio < DateTime.MinValue || fechaInicio > DateTime.Now)
                {
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine(" ERROR: La fecha ingresada no es valida.");
                    Console.WriteLine("-----------------------------------------");
                    return;
                }

                Console.Write("Ingrese la segunda fecha (formato dd/mm/yyyy): ");
                DateTime fechaFin = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                Console.WriteLine();

                if (fechaFin < DateTime.MinValue || fechaFin > DateTime.Now)
                {
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine(" ERROR: La segunda fecha ingresada no es valida.");
                    Console.WriteLine("-------------------------------------------------");
                }
                else if (fechaFin < fechaInicio)
                {
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine(" ERROR: La segunda fecha ingresada es menor a la primera.");
                    Console.WriteLine("----------------------------------------------------------");
                }
                else
                {
                    List<Post> postsEntreFechas = unSistema.GetPostsEntreFechas(fechaInicio, fechaFin);

                    if (postsEntreFechas.Count == 0)
                    {
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine(" No se encontraron posts entre las fechas dadas.");
                        Console.WriteLine("-------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine($"Posts entre {fechaInicio} y {fechaFin}:\n");
                        foreach (Post post in postsEntreFechas)
                        {
                            Console.WriteLine("---------------------------------------------");
                            Console.WriteLine($" Fecha: {post.Fecha}");
                            Console.WriteLine($" Contenido: {post.Contenido}");
                            Console.WriteLine($" Autor: {post.Autor.Email}");
                            Console.WriteLine($" Estado: {post.TipoPost}");
                            Console.WriteLine("---------------------------------------------\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ObtenerMiembrosConMasPublicaciones()
        {
            try
            {
                Console.WriteLine("Top miembros con más publicaciones:\n");

                List<Miembro> miembros = unSistema.GetMiembros();

                miembros.Sort();

                int maxPublicaciones = miembros[0].Publicaciones.Count;

                while (miembros[0].Publicaciones.Count == maxPublicaciones)
                {
                    Console.WriteLine($" Email: {miembros[0].Email}");
                    Console.WriteLine($" Cantidad de publicaciones: {miembros[0].Publicaciones.Count}\n");
                    miembros.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

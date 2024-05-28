namespace LogicaNegocio
{
    public class Sistema
    {
        public void Precargas()
        {
            PrecargaMiembros();
            PrecargaAdministradores();
            PrecargaPosts();
            PrecargaComentarios();
            PrecargaReacciones();
            PrecargaSolicitudesDeAmistad();
        }

        public Sistema()
        {
            Precargas();
        }

        /*LISTS*/

        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Publicacion> _publicaciones = new List<Publicacion>();
        private List<Reaccion> _reacciones = new List<Reaccion>();
        public List<Usuario> Usuarios { get { return _usuarios; } }
        public List<Publicacion> Publicaciones { get { return _publicaciones; } }
        public List<Reaccion> Reacciones { get { return _reacciones; } }

        /*GETS*/

        //retorna una lista de usuarios de solo miembros
        public List<Miembro> GetMiembros()
        {
            List<Miembro> miembros = new List<Miembro>();
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario is Miembro)
                {
                    miembros.Add((Miembro)usuario);
                }
            }
            return miembros;
        }
        //retorna una lista de usuarios de solo administradores
        public List<Administrador> GetAdministradores()
        {
            List<Administrador> administradores = new List<Administrador>();
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario is Administrador)
                {
                    administradores.Add((Administrador)usuario);
                }
            }
            return administradores;
        }
        //retorna una lista de publicaciones de solo posts
        public List<Post> GetPosts()
        {
            List<Post> posts = new List<Post>();
            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion is Post)
                {
                    posts.Add((Post)publicacion);
                }
            }
            return posts;
        }
        //retorna una lista de publicaciones de solo comentarios
        public List<Comentario> GetComentarios()
        {
            List<Comentario> comentarios = new List<Comentario>();
            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion is Comentario)
                {
                    comentarios.Add((Comentario)publicacion);
                }
            }
            return comentarios;
        }
        //retorna una lista de posts de un usuario
        public List<Post> GetPostsPorAutor(Miembro autor)
        {
            List<Post> posts = new List<Post>();
            foreach (Publicacion publicacion in autor.Publicaciones)
            {
                if (publicacion is Post)
                {
                    posts.Add((Post)publicacion);
                }
            }
            return posts;
        }
        //retorna una lista de comentarios de un usuario
        public List<Comentario> GetComentariosPorAutor(Miembro autor)
        {
            List<Comentario> comentarios = new List<Comentario>();
            foreach (Publicacion publicacion in autor.Publicaciones)
            {
                if (publicacion is Comentario)
                {
                    comentarios.Add((Comentario)publicacion);
                }
            }
            return comentarios;
        }
        //retorna miembro por email
        public Miembro? GetMiembroPorEmail(string email)
        {
            int i = 0;
            List<Miembro> miembros = GetMiembros();
            while (i < miembros.Count && miembros[i].Email != email)
            {
                i++;
            }
            return i < miembros.Count ? miembros[i] : null;
        }
        //retorna una lista de publicaciones de un usuario
        public List<Publicacion> GetPublicacionesMiembro(Miembro miembro)
        {
            return miembro.Publicaciones;
        }
        //retorna una lista de reacciones de una publicacion
        public List<Reaccion> GetReaccionesPublicacion(Publicacion publicacion)
        {
            return publicacion.Reacciones;
        }
        //retorna una lista con todos los posts a los que Miembro haya comentado
        public List<Post> GetPostsComentadosPorMiembro(Miembro miembro)
        {
            List<Post> posts = new List<Post>();
            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion is Post)
                {
                    foreach (Comentario comentario in ((Post)publicacion).Comentarios)
                    {
                        if (comentario.Autor == miembro)
                        {
                            posts.Add((Post)publicacion);
                        }
                    }
                }
            }
            return posts;
        }
        //retorna una lista de Posts entre dos fechas
        public List<Post> GetPostsEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Post> posts = new List<Post>();
            foreach (Publicacion publicacion in _publicaciones)
            {
                if (publicacion is Post && publicacion.Fecha >= fechaInicio && publicacion.Fecha <= fechaFin)
                {
                    posts.Add((Post)publicacion);
                }
            }
            return posts;
        }
        //retorna un post por su id
        public Post? GetPostPorId(int id)
        {
            int i = 0;
            while (i < _publicaciones.Count && _publicaciones[i].Id != id)
            {
                i++;
            }
            return i < _publicaciones.Count ? (Post)_publicaciones[i] : null;
        }


        /*SETS*/

        //setea un usuario
        public void SetUsuario(Usuario usuario)
        {
            _usuarios.Add(usuario);
        }
        //setea una publicacion
        public void SetPublicacion(Publicacion publicacion, Miembro miembro)
        {
            _publicaciones.Add(publicacion);
            miembro.Publicaciones.Add(publicacion);
        }
        //setea una reaccion
        public void SetReaccion(Reaccion reaccion)
        {
            _reacciones.Add(reaccion);
        }

        //setea comentario a un post
        public void ComentarPost(Post post, Comentario comentario, Miembro miembro)
        {
            post.Comentarios.Add(comentario);
            SetPublicacion(comentario, miembro);
        }
        //setea una reacciona un post
        public void SetReaccionPost(Post post, Reaccion reaccion, Miembro miembro)
        {
            post.AgregarReaccion(reaccion);
            miembro.Reacciones.Add(reaccion);
        }
        //setea una reaccion a un comentario
        public void SetReaccionComentario(Comentario comentario, Reaccion reaccion, Miembro miembro)
        {
            comentario.AgregarReaccion(reaccion);
            miembro.Reacciones.Add(reaccion);
        }
        //acepta la solicitud de amistad
        public void AceptarSolicitudDeAmistad(SolicitudDeAmistad solicitud, Miembro solicitado)
        {
            if (solicitado == solicitud.MiembroSolicitado && solicitud.EstadoSolicitudDeAmistad == TipoSolicitudDeAmistad.Pendiente)
            {
                solicitud.FechaRespuesta = DateTime.Now;
                solicitud.EstadoSolicitudDeAmistad = TipoSolicitudDeAmistad.Aceptada;
                solicitud.MiembroSolicitado.Amigos.Add(solicitud.MiembroSolicitante);
                solicitud.MiembroSolicitante.Amigos.Add(solicitud.MiembroSolicitado);
            }
        }
        //rechaza la solicitud de amistad
        public void RechazarSolicitudDeAmistad(SolicitudDeAmistad solicitud, Miembro solicitado)
        {
            if (solicitado == solicitud.MiembroSolicitado && solicitud.EstadoSolicitudDeAmistad == TipoSolicitudDeAmistad.Pendiente)
            {
                solicitud.FechaRespuesta = DateTime.Now;
                solicitud.EstadoSolicitudDeAmistad = TipoSolicitudDeAmistad.Rechazada;
            }
        }


        /*VALIDACIONES*/

        //valida que el email no este repetido
        public bool ValidarEmail(string email)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.Email == email)
                {
                    return false;
                }
            }
            return true;
        }

        /*PRECARGAS*/

        //precarga de miembros
        public void PrecargaMiembros()
        {
            Miembro miembro1 = new Miembro("juanparez@outlook.com", "13123124", "Juan", "Perez", new DateTime(1990, 1, 1), false);
            Miembro miembro2 = new Miembro("claramartin@outlook.com", "14421312", "Clara", "Martin", new DateTime(1997, 7, 31), false);
            Miembro miembro3 = new Miembro("nicolasamoroso@outlook.com", "31578621", "Nicolas", "Amoroso", new DateTime(2001, 10, 13), false);
            Miembro miembro4 = new Miembro("mariagonzalez@outlook.com", "36125235", "Maria", "Gonzalez", new DateTime(1995, 5, 5), false);
            Miembro miembro5 = new Miembro("luciagarcia@outlook.com", "24547658", "Lucia", "Garcia", new DateTime(1993, 12, 25), false);
            Miembro miembro6 = new Miembro("pedrorodriguez@outlook.com", "27546322", "Pedro", "Rodriguez", new DateTime(1992, 3, 17), false);
            Miembro miembro7 = new Miembro("joselopez@outlook.com", "95423827", "Jose", "Lopez", new DateTime(1991, 4, 18), false);
            Miembro miembro8 = new Miembro("federicogomez@outlook.com", "36524124", "Federico", "Gomez", new DateTime(1994, 6, 6), false);
            Miembro miembro9 = new Miembro("marianapereira@outlook.com", "12934934", "Mariana", "Pereira", new DateTime(1996, 8, 8), false);
            Miembro miembro10 = new Miembro("andreafernandez@outlook.com", "74596242", "Andrea", "Fernandez", new DateTime(1998, 9, 9), false);

            List<Miembro> miembros = new List<Miembro>(
                new Miembro[] { miembro1, miembro2, miembro3, miembro4, miembro5, miembro6, miembro7, miembro8, miembro9, miembro10 }
            );

            _usuarios.AddRange(miembros);
        }
        //precarga de administradores
        public void PrecargaAdministradores()
        {
            Administrador administrador1 = new Administrador("lilianapino@outlook.com", "123456789");

            List<Administrador> administradores = new List<Administrador>(
                new Administrador[] { administrador1 }
            );

            _usuarios.AddRange(administradores);
        }
        //precarga de posts
        public void PrecargaPosts()
        {
            TipoPublicacion publico = TipoPublicacion.Publico;
            TipoPublicacion privado = TipoPublicacion.Privado;

            Miembro? juan = GetMiembroPorEmail("juanparez@outlook.com");

            Miembro? nico = GetMiembroPorEmail("nicolasamoroso@outlook.com");
            Miembro? maria = GetMiembroPorEmail("mariagonzalez@outlook.com");
            Miembro? pedro = GetMiembroPorEmail("pedrorodriguez@outlook.com");
            Miembro? jose = GetMiembroPorEmail("joselopez@outlook.com");

            Post post1 = new Post(juan, new DateTime(2023, 9, 17), "Hola, soy Juan", "imagen1.png", publico);
            Post post2 = new Post(maria, new DateTime(2023, 9, 17), "Hola, soy Maria", "imagen2.png", privado);
            Post post3 = new Post(pedro, new DateTime(2023, 9, 17), "Hola, soy Pedro", "imagen3.png", privado);
            Post post4 = new Post(jose, new DateTime(2023, 9, 17), "Hola, soy Jose", "imagen4.png", publico);
            Post post5 = new Post(nico, new DateTime(2023, 9, 17), "Hola, soy Nico", "imagen5.png", privado);

            SetPublicacion(post1, juan);
            SetPublicacion(post2, maria);
            SetPublicacion(post3, pedro);
            SetPublicacion(post4, jose);
            SetPublicacion(post5, nico);
        }
        //precarga de comentarios
        public void PrecargaComentarios()
        {
            TipoPublicacion publico = TipoPublicacion.Publico;
            TipoPublicacion privado = TipoPublicacion.Privado;

            Miembro? juan = GetMiembroPorEmail("juanparez@outlook.com");
            Miembro? clara = GetMiembroPorEmail("claramartin@outlook.com");
            Miembro? nico = GetMiembroPorEmail("nicolasamoroso@outlook.com");
            Miembro? maria = GetMiembroPorEmail("mariagonzalez@outlook.com");
            Miembro? pedro = GetMiembroPorEmail("pedrorodriguez@outlook.com");
            Miembro? jose = GetMiembroPorEmail("joselopez@outlook.com");
            Miembro? andrea = GetMiembroPorEmail("andreafernandez@outlook.com");

            Post primerPostJuan = GetPostsPorAutor(juan)[0];
            Post primerPostMaria = GetPostsPorAutor(maria)[0];
            Post primerPostPedro = GetPostsPorAutor(pedro)[0];
            Post primerPostJose = GetPostsPorAutor(jose)[0];
            Post primerPostNico = GetPostsPorAutor(nico)[0];


            Comentario comentario1 = new Comentario(clara, new DateTime(2023, 9, 17), "buenas juan, soy clara", publico);
            Comentario comentario2 = new Comentario(nico, new DateTime(2023, 9, 17), "buenas juan, soy nico", publico);
            Comentario comentario3 = new Comentario(andrea, new DateTime(2023, 9, 17), "buenas juan, soy maria", publico);
            Comentario comentario4 = new Comentario(juan, new DateTime(2023, 9, 17), "buenas maria, soy juan", privado);
            Comentario comentario5 = new Comentario(nico, new DateTime(2023, 9, 17), "buenas maria, soy nico", publico);
            Comentario comentario6 = new Comentario(andrea, new DateTime(2023, 9, 17), "buenas maria, soy clara", privado);
            Comentario comentario7 = new Comentario(juan, new DateTime(2023, 9, 17), "buenas pedro, soy juan", publico);
            Comentario comentario8 = new Comentario(maria, new DateTime(2023, 9, 17), "buenas pedro, soy maria", privado);
            Comentario comentario9 = new Comentario(andrea, new DateTime(2023, 9, 17), "buenas pedro, soy clara", publico);
            Comentario comentario10 = new Comentario(juan, new DateTime(2023, 9, 17), "buenas jose, soy juan", publico);
            Comentario comentario11 = new Comentario(maria, new DateTime(2023, 9, 17), "buenas jose, soy maria", publico);
            Comentario comentario12 = new Comentario(andrea, new DateTime(2023, 9, 17), "buenas jose, soy clara", privado);
            Comentario comentario13 = new Comentario(juan, new DateTime(2023, 9, 17), "buenas nico, soy juan", privado);
            Comentario comentario14 = new Comentario(maria, new DateTime(2023, 9, 17), "buenas nico, soy maria", publico);
            Comentario comentario15 = new Comentario(andrea, new DateTime(2023, 9, 17), "buenas nico, soy clara", publico);

            ComentarPost(primerPostJuan, comentario1, clara);
            ComentarPost(primerPostJuan, comentario2, nico);
            ComentarPost(primerPostJuan, comentario3, andrea);
            ComentarPost(primerPostMaria, comentario4, juan);
            ComentarPost(primerPostMaria, comentario5, nico);
            ComentarPost(primerPostMaria, comentario6, andrea);
            ComentarPost(primerPostPedro, comentario7, juan);
            ComentarPost(primerPostPedro, comentario8, maria);
            ComentarPost(primerPostPedro, comentario9, andrea);
            ComentarPost(primerPostJose, comentario10, juan);
            ComentarPost(primerPostJose, comentario11, maria);
            ComentarPost(primerPostJose, comentario12, andrea);
            ComentarPost(primerPostNico, comentario13, juan);
            ComentarPost(primerPostNico, comentario14, maria);
            ComentarPost(primerPostNico, comentario15, andrea);

            Comentario comentario16 = new Comentario(andrea, new DateTime(2023, 9, 17), "Hola", privado);
            SetPublicacion(comentario16, andrea);
        }

        //precarga de reacciones
        public void PrecargaReacciones()
        {
            Miembro? juan = GetMiembroPorEmail("juanparez@outlook.com");
            Miembro? maria = GetMiembroPorEmail("mariagonzalez@outlook.com");

            Post primerPostJuan = GetPostsPorAutor(juan)[0];
            Post primerPostMaria = GetPostsPorAutor(maria)[0];

            Reaccion likePostJuan = new Reaccion(TipoReaccion.Like, primerPostJuan);
            Reaccion disliekPostJuan = new Reaccion(TipoReaccion.Dislike, primerPostJuan);
            Reaccion likePostMaria = new Reaccion(TipoReaccion.Like, primerPostMaria);
            Reaccion disliekPostMaria = new Reaccion(TipoReaccion.Dislike, primerPostMaria);

            Miembro? clara = GetMiembroPorEmail("claramartin@outlook.com");
            Miembro? nico = GetMiembroPorEmail("nicolasamoroso@outlook.com");
            Miembro? pedro = GetMiembroPorEmail("pedrorodriguez@outlook.com");
            Miembro? jose = GetMiembroPorEmail("joselopez@outlook.com");
            Miembro? andrea = GetMiembroPorEmail("andreafernandez@outlook.com");

            SetReaccionPost(primerPostJuan, likePostJuan, clara);
            SetReaccionPost(primerPostJuan, disliekPostJuan, nico);
            SetReaccionPost(primerPostMaria, likePostMaria, pedro);
            SetReaccionPost(primerPostMaria, disliekPostMaria, jose);
            SetReaccionPost(primerPostMaria, disliekPostMaria, andrea);
            SetReaccionPost(primerPostMaria, disliekPostMaria, clara);

            Comentario comentario1 = primerPostJuan.Comentarios[0];
            Comentario comentario2 = primerPostJuan.Comentarios[1];

            Reaccion likeComentario1 = new Reaccion(TipoReaccion.Like, primerPostJuan);
            Reaccion dislikeComentario1 = new Reaccion(TipoReaccion.Dislike, primerPostJuan);
            Reaccion likeComentario2 = new Reaccion(TipoReaccion.Like, primerPostMaria);
            Reaccion dislikeComentario2 = new Reaccion(TipoReaccion.Dislike, primerPostMaria);

            SetReaccionComentario(comentario1, likeComentario1, juan);
            SetReaccionComentario(comentario1, dislikeComentario1, clara);
            SetReaccionComentario(comentario2, likeComentario2, juan);
            SetReaccionComentario(comentario2, likeComentario2, clara);
            SetReaccionComentario(comentario2, likeComentario2, andrea);
            SetReaccionComentario(comentario2, dislikeComentario2, maria);
        }

        //precarga de solicitudes de amistad
        public void PrecargaSolicitudesDeAmistad()
        {
            Miembro juan = GetMiembroPorEmail("juanparez@outlook.com");
            Miembro clara = GetMiembroPorEmail("claramartin@outlook.com");

            SolicitudDeAmistad solicitudEntreJuanYClara = new SolicitudDeAmistad(juan, clara);

            AceptarSolicitudDeAmistad(solicitudEntreJuanYClara, juan);

            Miembro nico = GetMiembroPorEmail("nicolasamoroso@outlook.com");
            Miembro maria = GetMiembroPorEmail("mariagonzalez@outlook.com");

            new SolicitudDeAmistad(nico, maria);

            Miembro lucia = GetMiembroPorEmail("luciagarcia@outlook.com");
            Miembro pedro = GetMiembroPorEmail("pedrorodriguez@outlook.com");

            SolicitudDeAmistad solicitudEntreLuciaYPedro = new SolicitudDeAmistad(lucia, pedro);

            RechazarSolicitudDeAmistad(solicitudEntreLuciaYPedro, lucia);

            Miembro jose = GetMiembroPorEmail("joselopez@outlook.com");
            new SolicitudDeAmistad(lucia, jose);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Post : Publicacion
    {
        private string _imagen { get; set; }
        private TipoPublicacion _tipoPost { get; set; }
        private List<Comentario> _comentarios { get; set; }

        public Post(Miembro autor, DateTime fecha, string contenido, string imagen, TipoPublicacion tipoPost) : base(autor, fecha, contenido)
        {
            _imagen = imagen;
            _tipoPost = tipoPost;
            _comentarios = new List<Comentario>();
        }

        public List<Comentario> Comentarios { get => _comentarios; set => _comentarios = value; }
        public TipoPublicacion TipoPost { get => _tipoPost; set => _tipoPost = value; }
    }
}

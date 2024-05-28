using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Comentario : Publicacion
    {
        private TipoPublicacion _tipoComentario { get; set; }

        public Comentario(Miembro autor, DateTime fecha, string contenido, TipoPublicacion tipoComentario) : base(autor, fecha, contenido)
        {
            _tipoComentario = tipoComentario;
        }

        public TipoPublicacion TipoComentario { get => _tipoComentario; set => _tipoComentario = value; }
    }
}

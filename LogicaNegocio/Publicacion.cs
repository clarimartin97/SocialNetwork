using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public abstract class Publicacion
    {
        private int _id;
        private Miembro _autor { get; set; }
        private DateTime _fecha { get; set; }
        private string _contenido { get; set; }
        private List<Reaccion> _reacciones { get; set; }
        private static int s_ultimoId;

        public int Id { get { return _id; } }
        public string Contenido { get { return _contenido; } }
        public Usuario Autor { get { return _autor; } }
        public DateTime Fecha { get { return _fecha; } }
        public List<Reaccion> Reacciones { get { return _reacciones; } }

        public Publicacion(Miembro autor, DateTime fecha, string contenido)
        {
            _id = ++s_ultimoId;
            _autor = autor;
            _fecha = fecha;
            _contenido = contenido;
            _reacciones = new List<Reaccion>();
        }

        public void CalcularValorAcceptacion(Boolean tipo)
        {

        }
        public void AgregarReaccion(Reaccion reaccion)
        {
            _reacciones.Add(reaccion);
        }
    }
}

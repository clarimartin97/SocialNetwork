using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Interfaces;

namespace LogicaNegocio
{
    public class Miembro : Usuario, IComparable<Miembro>, IValidate
    {
        private List<Miembro> _amigos { get; set; }
        private string _nombre { get; set; }
        private string _apellido { get; set; }
        private DateTime _fechaDeNacimiento { get; set; }
        private bool _bloqueado { get; set; }
        private List<Publicacion> _publicaciones { get; set; }
        private List<SolicitudDeAmistad> _solicitudes { get; set; }
        private List<Reaccion> _reacciones { get; set; }

        public Miembro(string email, string contrasenia, string nombre, string apellido, DateTime fechaDeNacimiento, bool bloqueado) : base(email, contrasenia)
        {
            _nombre = nombre;
            _apellido = apellido;
            _fechaDeNacimiento = fechaDeNacimiento;
            _bloqueado = bloqueado;
            _amigos = new List<Miembro>();
            _publicaciones = new List<Publicacion>();
            _solicitudes = new List<SolicitudDeAmistad>();
            _reacciones = new List<Reaccion>();
        }

        public List<Publicacion> Publicaciones { get => _publicaciones; set => _publicaciones = value; }
        public List<Miembro> Amigos { get => _amigos; set => _amigos = value; }
        public List<SolicitudDeAmistad> Solicitudes { get => _solicitudes; set => _solicitudes = value; }
        public List<Reaccion> Reacciones { get => _reacciones; set => _reacciones = value; }

        public int CompareTo(Miembro? otroMiembro)
        {
            return Publicaciones.Count.CompareTo(otroMiembro?.Publicaciones.Count) * -1;
        }

        public void ValidarDatos()
        {
            base.ValidarDatos();
            if (string.IsNullOrEmpty(_nombre))
            {
                throw new Exception("El nombre no puede estar vacio");
            }
            if (string.IsNullOrEmpty(_apellido))
            {
                throw new Exception("El apellido no puede estar vacio");
            }
            if (_fechaDeNacimiento == DateTime.MinValue)
            {
                throw new Exception("La fecha de nacimiento no ha sido inicializada");
            }
        }
    }
}

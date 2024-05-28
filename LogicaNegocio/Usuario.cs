using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Interfaces;

namespace LogicaNegocio
{
    public abstract class Usuario : IValidate
    {
        private string _email { get; set; }
        private string _contrasenia { get; set; }

        public Usuario(string email, string contrasenia)
        {
            _email = email;
            _contrasenia = contrasenia;
        }

        public void ValidarDatos()
        {
            if (string.IsNullOrEmpty(_email))
            {
                throw new Exception("El email no puede estar vacio");
            }
            if (string.IsNullOrEmpty(_contrasenia))
            {
                throw new Exception("La contraseña no puede estar vacia");
            }
        }
        public string Email { get => _email; set => _email = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class Reaccion
    {
        private TipoReaccion _tipo { get; set; }
        private Post post { get; set; }
        public Reaccion(TipoReaccion tipo, Post post)
        {
            _tipo = tipo;
            this.post = post;
        }

        public TipoReaccion TipoReaccion { get => _tipo; set => _tipo = value; }
        public Post Post { get => post; set => post = value; }
    }
}

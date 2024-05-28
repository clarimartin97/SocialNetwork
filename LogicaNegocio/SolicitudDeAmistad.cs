using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
  public class SolicitudDeAmistad
  {
    private int _id;
    private Miembro _miembroSolicitado { get; set; }
    private Miembro _miembroSolicitante { get; set; }
    private DateTime _fechaSolicitud { get; set; }
    private DateTime _fechaRespuesta { get; set; }
    private TipoSolicitudDeAmistad _tipoSolicitudDeAmistad { get; set; }
    private static int s_ultimoId;

    public int Id { get { return _id; } }
    public Miembro MiembroSolicitado { get { return _miembroSolicitado; } }
    public Miembro MiembroSolicitante { get { return _miembroSolicitante; } }
    public DateTime FechaSolicitud { get { return _fechaSolicitud; } }
    public DateTime FechaRespuesta { get => _fechaRespuesta; set => _fechaRespuesta = value; }
    public TipoSolicitudDeAmistad EstadoSolicitudDeAmistad { get => _tipoSolicitudDeAmistad; set => _tipoSolicitudDeAmistad = value; }

    public SolicitudDeAmistad(Miembro miembroSolicitado, Miembro miembroSolicitante)
    {
      if (miembroSolicitado.Email != miembroSolicitante.Email)
      {
        _id = ++s_ultimoId;
        _miembroSolicitado = miembroSolicitado;
        _miembroSolicitante = miembroSolicitante;
        _fechaSolicitud = DateTime.Now;
        _fechaRespuesta = DateTime.MinValue;
        _tipoSolicitudDeAmistad = TipoSolicitudDeAmistad.Pendiente;
        miembroSolicitado.Solicitudes.Add(this);
        miembroSolicitante.Solicitudes.Add(this);
      }
      else
      {
        throw new Exception("No se puede enviar una solicitud de amistad a uno mismo");
      }
    }
  }
}

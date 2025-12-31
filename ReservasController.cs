using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabMaster.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity; // Necesario para User.Identity.GetUserId()

namespace LabMaster.Controllers
{
    [Authorize]
    public class ReservasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var misReservas = db.Reservas.Where(r => r.UsuarioID == userId).Include(r => r.Laboratorio).ToList();
            return View(misReservas);
        }

        public ActionResult SeleccionarCarrera()
        {
            return View(db.Carreras.ToList());
        }

        public ActionResult VerLaboratorios(int id)
        {
            var carrera = db.Carreras.Find(id);
            ViewBag.CarreraNombre = carrera.NombreCarrera;
            return View(db.Laboratorios.Where(l => l.CarreraID == id).ToList());
        }

        // Modificado: ahora admite weekStart para navegación semanal y pasa los horarios ya reservados.
        public ActionResult VerCalendario(int id, string weekStart = null)
        {
            var lab = db.Laboratorios.Find(id);
            if (lab == null) return HttpNotFound();

            ViewBag.LabNombre = lab.NombreLab;
            ViewBag.LabID = lab.LabID;

            // Determinar inicio de semana (lunes)
            DateTime start;
            if (!string.IsNullOrEmpty(weekStart) && DateTime.TryParse(weekStart, out start))
            {
                // parsed ok
            }
            else
            {
                var today = DateTime.Today;
                int diff = (today.DayOfWeek == DayOfWeek.Sunday) ? 6 : ((int)today.DayOfWeek - 1);
                start = today.AddDays(-diff); // lunes de la semana actual
            }

            ViewBag.WeekStartDate = start;
            ViewBag.WeekLabel = "Semana del " + start.ToString("dd/MM/yyyy");

            // Obtener los horarios ya reservados para este laboratorio
            // Nota: en el modelo actual HoraInicio guarda el valor "Dia | Hora" (ej. "Lunes | 07:00")
            var reservados = db.Reservas
                               .Where(r => r.LabID == id)
                               .Select(r => r.HoraInicio)
                               .ToList();

            ViewBag.Reservados = reservados;

            return View();
        }

        // GET: Reservas/ConfirmarReserva
        // Cambiamos 'int labId' por 'int? labId' para evitar el error de la imagen 5973a9
        public ActionResult ConfirmarReserva(int? labId, string horariosSeleccionados)
        {
            if (labId == null) return RedirectToAction("SeleccionarCarrera");

            var lab = db.Laboratorios.Find(labId);
            if (lab == null) return HttpNotFound();

            // Cargamos insumos filtrados por carrera
            ViewBag.Insumos = db.Insumos.Where(i => i.CarreraID == lab.CarreraID && i.Stock > 0).ToList();
            ViewBag.LabNombre = lab.NombreLab;
            ViewBag.HorariosParaMostrar = string.IsNullOrEmpty(horariosSeleccionados)
                                         ? new List<string>()
                                         : horariosSeleccionados.Split(',').ToList();

            return View(new Reserva
            {
                LabID = lab.LabID,
                HoraInicio = horariosSeleccionados,
                FechaReserva = DateTime.Now
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarReserva(Reserva reserva, int[] insumosSeleccionados)
        {
            // Limpiamos el error de UsuarioID porque lo asignaremos nosotros
            ModelState.Remove("UsuarioID");

            if (ModelState.IsValid)
            {
                try
                {
                    string currentUserId = User.Identity.GetUserId();
                    var listaHorarios = reserva.HoraInicio.Split(',');

                    foreach (var horario in listaHorarios)
                    {
                        db.Reservas.Add(new Reserva
                        {
                            LabID = reserva.LabID,
                            UsuarioID = currentUserId, // Se asigna aquí físicamente
                            Motivo = reserva.Motivo,
                            FechaReserva = DateTime.Now,
                            HoraInicio = horario.Trim()
                        });
                    }

                    // Descontar stock de insumos
                    if (insumosSeleccionados != null)
                    {
                        foreach (var id in insumosSeleccionados)
                        {
                            var ins = db.Insumos.Find(id);
                            if (ins != null) ins.Stock--;
                        }
                    }

                    db.SaveChanges(); // Si la migración está hecha, esto guarda en SQL
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error: " + ex.Message);
                }
            }
            // Si falla, recargar ViewBag.Insumos para evitar error
            var labRef = db.Laboratorios.Find(reserva?.LabID ?? 0);
            int carreraId = labRef != null ? labRef.CarreraID : 0;
            ViewBag.Insumos = (carreraId > 0)
                ? db.Insumos.Where(i => i.CarreraID == carreraId && i.Stock > 0).ToList()
                : new List<Insumo>();

            ViewBag.LabNombre = labRef?.NombreLab;
            ViewBag.LabID = labRef?.LabID;
            ViewBag.HorariosParaMostrar = !string.IsNullOrEmpty(reserva?.HoraInicio)
                                          ? reserva.HoraInicio.Split(',').ToList()
                                          : new List<string>();

            var modelo = reserva ?? new Reserva { LabID = labRef?.LabID ?? 0, FechaReserva = DateTime.Now };
            return View("ConfirmarReserva", modelo);
        }


    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamenFinalEnunciadoD.Models;
using System.Web.Services.Description;
using Microsoft.Ajax.Utilities;
using System.Data.Entity.Validation;

namespace ExamenFinalEnunciadoD.Controllers
{
    public class EmpleadoController : Controller
    {
        private dbEntities db = new dbEntities();

        // GET: Empleado
        public async Task<ActionResult> Index()
        {
            return View(await db.Empleado.ToListAsync());
        }

        // GET: Empleado/Details/5
        public async Task<ActionResult> Details()
        {
           
            return View();
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            ViewBag.Tipo_Asalariado = db.Tipo_Asalariado.ToList();
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Empleado_Id,Empleado_Numero,Empleado_Nombre_Completo,Empleado_CI")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleado.Add(empleado);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Tipo_Asalariado = db.Tipo_Asalariado.ToList();
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = await db.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }

            ViewBag.Tipo_Asalariado = db.Tipo_Asalariado.ToList();
            ViewBag.Dia_Trabajado = db.Dia_Trabajado.ToList();
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Empleado modelo, string operacion)
        {
            if (modelo == null)
            {
                modelo = new Empleado();
            }

            if (operacion == null)
            {
                if (AgregarDetalleEdit(modelo,modelo.Empleado_Id))
                {
                    return RedirectToAction("Index");
                }
            }
       
            if (operacion == "agregar-horario")
            {
                modelo.Empleado_Horario.Add(new Empleado_Horario());
            }
            else if (operacion.StartsWith("eliminar-horario-"))
            {
                EliminarPorIndiceHonorario(modelo, operacion);
            }

            ViewBag.Tipo_Asalariado = db.Tipo_Asalariado.ToList();
            ViewBag.Dia_Trabajado = db.Dia_Trabajado.ToList();
            return View(modelo);
        }
    
        private bool AgregarDetalleEdit(Empleado modelo,int Empleado_Id)
        {
            if (ModelState.IsValid)
            {

                try
                {




                    Empleado_Horario horario = new Empleado_Horario();
                    foreach (var item in modelo.Empleado_Horario)
                    {
        
                        horario.Empleado_Horario_Trabajado_Diario = item.Empleado_Horario_Trabajado_Diario;
                        horario.Empleado_Horario_Hora_Extra_50 = item.Empleado_Horario_Hora_Extra_50;
                        horario.Empleado_Horario_Hora_Extra_100 = item.Empleado_Horario_Hora_Extra_100;
                        horario.Dia_Trabajado_Id = item.Dia_Trabajado_Id;
                        horario.Empleado_Id = modelo.Empleado_Id;
                        horario.Ausente_S_N = false;

                        db.Empleado_Horario.Add(horario);
                        db.SaveChanges();
                    };



                
                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }


            }

            return false;
        }
        private static void EliminarPorIndiceHonorario(Empleado empleado, string operacion)
        {
            // se asume que en el parametro 'operacion' viene el index del detalle a eliminar.             string indexStr = operacion.Replace("eliminar-detalle-", "");
            // se asume que en el parametro 'operacion' viene el index del detalle a eliminar.
            string indexStr = operacion.Replace("eliminar-horario-", "");
            int index = 0;

            if (int.TryParse(indexStr, out index) && index >= 0 && index <empleado.Empleado_Horario.Count)
            {
                var item = empleado.Empleado_Horario.ToArray()[index];
                empleado.Empleado_Horario.Remove(item);
            }
        }
        // GET: Empleado/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = await db.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Empleado empleado = await db.Empleado.FindAsync(id);
            db.Empleado.Remove(empleado);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //public JsonResult SearchOptions(int id)
        //{

        //    var Tipo_Asalariado = db.Tipo_Asalariado.Select(s => new
        //    {
        //        id = s.Tipo_Asalariado_Id,
        //        text = s.Tipo_Asalariado_Nombre
        //    }).ToList();


        //    if (id != null )
        //    {
        //        Tipo_Asalariado = Tipo_Asalariado.Where(o => o.id == id).ToList();
        //    }

        //    return Json(Tipo_Asalariado, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult SearchOptions(string searchTerm)
        {
            // Lógica para realizar la búsqueda de opciones basada en el término de búsqueda (searchTerm)



            var Contrato = db.Empleado.Select(s => new
            {
                id = s.Empleado_Id,
                text = s.Empleado_Numero+"-"+s.Empleado_Nombre_Completo
            }).ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                Contrato = Contrato.Where(o => o.text.Contains(searchTerm)).ToList();
            }

            return Json(Contrato, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchOptionsDatos(int id)
        {
            // Lógica para realizar la búsqueda de opciones basada en el término de búsqueda (searchTerm)



            var Horarios = db.Empleado_Horario.Where(w => w.Empleado_Id == id).ToList();
            decimal? cantidad_hora_trabajada = 0;
            decimal? sueldo_por_hora = db.Configuracion_Salarial.Where(w => w.Tipo_Asalariado_Id == 1).Select(w => w.Configuracion_Salarial_Jornal_Por_Hora).Single();
         
            decimal? totalSalarioPorHora=0;
            decimal? totalNormal;
            decimal? HoraExtra = 0;
            decimal? sueldoExtra = 0;
            foreach(var item in Horarios)
            {
                HoraExtra += (item.Empleado_Horario_Hora_Extra_100 + item.Empleado_Horario_Hora_Extra_50);
                cantidad_hora_trabajada += item.Empleado_Horario_Trabajado_Diario;
            }
            if(cantidad_hora_trabajada <= 40)
            {
                totalSalarioPorHora = cantidad_hora_trabajada * sueldo_por_hora;
            }
            if (cantidad_hora_trabajada == 40)
            {
                HoraExtra =  cantidad_hora_trabajada - HoraExtra;
                sueldoExtra = HoraExtra * (sueldo_por_hora * 2);
            }
            decimal? sueldoNeto = sueldoExtra + totalSalarioPorHora;
            var result = new
            {
                Salario_Hora_Normal = totalSalarioPorHora,
                Salario_Extra = sueldoExtra,
                Sueldo_Neto = sueldoNeto

            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

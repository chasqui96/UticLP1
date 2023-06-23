using LP1TP.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LP1TP.Controllers
{
    public class EstudianteController : Controller
    {
        private dbLP1TPEntities db = new dbLP1TPEntities();

        // GET: Estudiante
        public ActionResult Index()
        {
            var estudiante = db.Estudiante.Include(e => e.Persona);
            return View(estudiante.ToList());
        }

        // GET: Estudiante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiante.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            ViewBag.Carreras = db.Carrera.ToList();
            ViewBag.Cursos = db.Curso.ToList();
            ViewBag.Ciudades = db.Ciudad.ToList();
            ViewBag.Pais = db.Pais.ToList();
            //ViewBag.Ciudad_Id = new SelectList(db.Ciudad, "Ciudad_Id", "Ciudad_Codigo");
            //ViewBag.Pais_Id = new SelectList(db.Pais, "Pais_Id", "Pais_Codigo");
            return View(estudiante);
        }

        // GET: Estudiante/Create
        public ActionResult Create()
        {

            ViewBag.Carreras = db.Carrera.ToList();
            ViewBag.Cursos = db.Curso.ToList();

            ViewBag.Ciudad_Id = new SelectList(db.Ciudad, "Ciudad_Id", "Ciudad_Codigo");
            ViewBag.Pais_Id = new SelectList(db.Pais, "Pais_Id", "Pais_Codigo");
            return View();
        }

        // POST: Estudiante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Estudiante modelo, string operacion, int Ciudad_Id, int Pais_Id)
        {




            if (modelo == null)
            {
                modelo = new Estudiante();
            }

            if (operacion == null)
            {
                if (CrearEstudiante(modelo, Ciudad_Id, Pais_Id))
                {
                    return RedirectToAction("Index");
                }
            }
            if (operacion == "agregar-curso")
            {
                modelo.EstudianteGrado.Add(new EstudianteGrado());
            }
            else if (operacion.StartsWith("eliminar-detalle-"))
            {
                EliminarPorIndice(modelo, operacion);
            }
            ViewBag.Carreras = db.Carrera.ToList();
            ViewBag.Cursos = db.Curso.ToList();
            ViewBag.Ciudad_Id = new SelectList(db.Ciudad, "Ciudad_Id", "Ciudad_Codigo");
            ViewBag.Pais_Id = new SelectList(db.Pais, "Pais_Id", "Pais_Codigo");
            return View(modelo);
        }

        // GET: Estudiante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiante.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            ViewBag.Carreras = db.Carrera.ToList();
            ViewBag.Cursos = db.Curso.ToList();
            ViewBag.Ciudades = db.Ciudad.ToList();
            ViewBag.Pais = db.Pais.ToList();
            return View(estudiante);
        }

        // POST: Estudiante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Estudiante estudiante, int Ciudad_Id, int Pais_Id)
        {
            if (ModelState.IsValid)

                try
                {
                    var estudiantes = db.Estudiante.Find(estudiante.Estudiante_Id);
                    var personas = db.Persona.Find(estudiante.Persona_Id);
                    var estudianteGradoId = db.EstudianteGrado.Where(w=>w.Estudiante_Id == estudiante.Estudiante_Id).Single();
                    var estudianteGrado = db.EstudianteGrado.Find(estudianteGradoId.ID);

                    personas.Nombre = estudiante.Persona.Nombre;
                    personas.Edad = estudiante.Persona.Edad;
                    personas.Correo = estudiante.Persona.Correo;
                    personas.Direccion = estudiante.Persona.Direccion;
                    personas.Ciudad_Id = Ciudad_Id;
                    personas.Pais_Id = Pais_Id;

                    foreach (var item in estudiante.EstudianteGrado)
                    {
                        estudianteGrado.Estudiante_Id = estudiantes.Estudiante_Id;
                        estudianteGrado.Carrera_Id = item.Carrera_Id;
                        estudianteGrado.Curso_Id = item.Curso_Id;
                        estudianteGrado.Anho = item.Anho;

                    };

                    db.SaveChanges();
                    return RedirectToAction("Index");
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
            ViewBag.Persona_Id = new SelectList(db.Persona, "Persona_Id", "Nombre", estudiante.Persona_Id);
            return View(estudiante);
        }

        // GET: Estudiante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiante.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estudiante estudiante = db.Estudiante.Find(id);
            var estudianteGradoId = db.EstudianteGrado.Where(w => w.Estudiante_Id == id).Single();
            var estudianteGrado = db.EstudianteGrado.Find(estudianteGradoId.ID);
            db.EstudianteGrado.Remove(estudianteGrado);
            db.Estudiante.Remove(estudiante);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private static void EliminarPorIndice(Estudiante estudiante, string operacion)
        {
            // se asume que en el parametro 'operacion' viene el index del detalle a eliminar.             string indexStr = operacion.Replace("eliminar-detalle-", "");
            // se asume que en el parametro 'operacion' viene el index del detalle a eliminar.
            string indexStr = operacion.Replace("eliminar-detalle-", "");
            int index = 0;

            if (int.TryParse(indexStr, out index) && index >= 0 && index < estudiante.EstudianteGrado.Count)
            {
                var item = estudiante.EstudianteGrado.ToArray()[index];
                estudiante.EstudianteGrado.Remove(item);
            }
        }
        private bool CrearEstudiante(Estudiante estudiante, int Ciudad_Id, int Pais_Id)
        {
            if (ModelState.IsValid)
            {

                try
                { 

                    Persona persona = new Persona();
                    persona.Nombre = estudiante.Persona.Nombre;
                    persona.Edad = estudiante.Persona.Edad;
                    persona.Direccion = estudiante.Persona.Direccion;
                    persona.Correo = estudiante.Persona.Correo;
                    persona.Ciudad_Id = Ciudad_Id;
                    persona.Pais_Id = Pais_Id;
                    db.Persona.Add(persona);

                    Estudiante estudiantes = new Estudiante();
                    estudiantes.Persona_Id = persona.Persona_Id;
                    estudiantes.NumeroEstudiante = db.Persona.OrderByDescending(N=>N.Persona_Id).Select(S=>S.Persona_Id).Take(1).Single()+1 ;
                    db.Estudiante.Add(estudiantes);

                    EstudianteGrado estudianteGrado = new EstudianteGrado();
                    foreach (var item in estudiante.EstudianteGrado)
                    {
                        estudianteGrado.Estudiante_Id = estudiantes.Estudiante_Id;
                        estudianteGrado.Carrera_Id = item.Carrera_Id;
                        estudianteGrado.Curso_Id = item.Curso_Id;
                        estudianteGrado.Anho = item.Anho;
                        db.EstudianteGrado.Add(estudianteGrado);

                    };



                    db.SaveChanges();
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

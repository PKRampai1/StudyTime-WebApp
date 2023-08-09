using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyTime.Data;
using StudyTime.Models;


namespace StudyTime.Controllers
{
    public class StudentModulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentModulesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public IActionResult NewWeek(int ID)
        {

            var module = _context.studentModules.FirstOrDefault(x => x.ID == ID);
            var modUpdate = new UpdateModuleViewModule()
            {
                moduleName = module.moduleName,
                moduleCode = module.moduleCode,
                moduleCredit = module.moduleCredit,
                moduleHours = module.moduleHours,
                SemesterStartDate = module.SemesterStartDate,
                semesterWeeks = module.semesterWeeks,


            };
            modUpdate.CalcTime();


            return View(modUpdate);
        }

        [Authorize]
        [HttpPost]
        public IActionResult NewWeek([Bind("ID,Email,moduleCode,moduleName,moduleCredit,semesterWeeks,moduleHours,moduleSelfStudy,SemesterStartDate")] UpdateModuleViewModule updateModuleViewModule)
        {

            var module = _context.studentModules.Find(updateModuleViewModule.ID);
            if (module != null)
            {

                //module.moduleCode = updateModuleViewModule.moduleCode;
                //module.moduleHours = updateModuleViewModule.moduleHours;
                //module.moduleCredit = updateModuleViewModule.moduleCredit;
                //module.moduleName = updateModuleViewModule.moduleName;
                //module.moduleSelfStudy = updateModuleViewModule.moduleSelfStudy;
                //module.semesterWeeks = updateModuleViewModule.semesterWeeks;
                //module.SemesterStartDate = updateModuleViewModule.SemesterStartDate;

                module.CalcTime();
                _context.SaveChanges();

                return RedirectToAction("Index");




            }
            return RedirectToAction("Index");

        }

        [Authorize]
        [HttpGet]
        public IActionResult Graph(int ID)
        {
            //Adapted from: C-sharp corner
            //Author:Thiago Vivas
            //Author Profile:https://www.c-sharpcorner.com/members/thiago-vivas
            //Date:26 April 2018
            //Link:https://www.c-sharpcorner.com/article/creating-charts-with-asp-net-core/

            if (ID == null || _context.studentModules == null)
            {
                return RedirectToAction("Index");
            }

            Random rnd = new Random();
            //list of department  
            var lstModel = new List<SimpleReportViewModel>();



            var module = _context.studentModules.FirstOrDefault(x => x.ID == ID);
            var modUpdate = new UpdateModuleViewModule()
            {
                moduleName = module.moduleName,
                moduleCode = module.moduleCode,
                moduleCredit = module.moduleCredit,
                moduleHours = module.moduleHours,
                SemesterStartDate = module.SemesterStartDate,
                semesterWeeks = module.semesterWeeks,
                moduleSelfStudy = module.moduleSelfStudy,

            };
            ////  modUpdate.CalcTime();
            int hoursStudied = modUpdate.moduleSelfStudy;

            modUpdate.CalcTime();

            int weeklyHours = modUpdate.moduleSelfStudy;
            int realHours = weeklyHours - hoursStudied;
            ////list of collumns  
            //var lstModel = new List<SimpleReportViewModel>();



            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = " Required Weekly hours",
                Quantity = weeklyHours
            });

            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Hours studied in current week",
                Quantity = realHours
            });

            return View(lstModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Graph([Bind("ID,Email,moduleCode,moduleName,moduleCredit,semesterWeeks,moduleHours,moduleSelfStudy,SemesterStartDate")] UpdateModuleViewModule updateModuleViewModule)
        {
            var module = _context.studentModules.Find(updateModuleViewModule.ID);
            if (module != null)
            {

                //module.moduleCode = updateModuleViewModule.moduleCode;
                //module.moduleHours = updateModuleViewModule.moduleHours;
                //module.moduleCredit = updateModuleViewModule.moduleCredit;
                //module.moduleName = updateModuleViewModule.moduleName;
                //module.moduleSelfStudy = updateModuleViewModule.moduleSelfStudy;
                //module.semesterWeeks = updateModuleViewModule.semesterWeeks;
                //module.SemesterStartDate = updateModuleViewModule.SemesterStartDate;

                //  module.CalcTime();
                //_context.SaveChanges();

                return RedirectToAction("Index");




            }
            return RedirectToAction("Index");

        }


        [Authorize]
        [HttpGet]
        public IActionResult view(int ID)
        {
            {
                if (ID == null || _context.studentModules == null)
                {
                    return NotFound();
                }

                var module = _context.studentModules.FirstOrDefault(x => x.ID == ID);
                var modUpdate = new UpdateModuleViewModule()
                {
                    moduleName = module.moduleName,
                    moduleCode = module.moduleCode,
                    moduleCredit = module.moduleCredit,
                    moduleHours = module.moduleHours,
                    SemesterStartDate = module.SemesterStartDate,
                    semesterWeeks = module.semesterWeeks,


                };
                //  modUpdate.CalcTime();


                return View(modUpdate);
            }

        }

        [Authorize]

        [HttpPost]
        public IActionResult view([Bind("ID,Email,moduleCode,moduleName,moduleCredit,semesterWeeks,moduleHours,moduleSelfStudy,SemesterStartDate")] UpdateModuleViewModule updateModuleViewModule)
        {

            var module = _context.studentModules.Find(updateModuleViewModule.ID);
            if (module != null)
            {

                //module.moduleCode = updateModuleViewModule.moduleCode;
                //module.moduleHours = updateModuleViewModule.moduleHours;
                //module.moduleCredit = updateModuleViewModule.moduleCredit;
                //module.moduleName = updateModuleViewModule.moduleName;
                //module.moduleSelfStudy = updateModuleViewModule.moduleSelfStudy;
                //module.semesterWeeks = updateModuleViewModule.semesterWeeks;
                //module.SemesterStartDate = updateModuleViewModule.SemesterStartDate;

                //module.CalcTime();

                int stud = module.moduleSelfStudy;

                module.moduleSelfStudy = stud - updateModuleViewModule.moduleSelfStudy;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [Authorize]
        // GET: StudentModules
        public async Task<IActionResult> Index()
        {
            return View(await _context.studentModules.ToListAsync());
        }

        [Authorize]
        // GET: StudentModules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.studentModules == null)
            {
                return NotFound();
            }

            var studentModule = await _context.studentModules
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentModule == null)
            {
                return NotFound();
            }

            return View(studentModule);
        }

        [Authorize]
        // GET: StudentModules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentModules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("moduleCode,moduleName,moduleCredit,semesterWeeks,moduleHours,SemesterStartDate")] StudentModule studentModule)
        {

            //Adapted from: Stackoverflow
            //Author:james
            //Author Profile:https://stackoverflow.com/users/3064389/james
            //Date:26 September 2016
            //Link:https://stackoverflow.com/questions/39693946/how-to-get-currently-logged-users-email-address-to-the-view-in-asp-net-c

            studentModule.Email = User.Identity.Name;
            studentModule.CalcTime();
            _context.Add(studentModule);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }


        [Authorize]
        // GET: StudentModules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.studentModules == null)
            {
                return NotFound();
            }

            var studentModule = await _context.studentModules.FindAsync(id);
            if (studentModule == null)
            {
                return NotFound();
            }
            return View(studentModule);
        }

        // POST: StudentModules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Email,moduleCode,moduleName,moduleCredit,semesterWeeks,moduleHours,moduleSelfStudy,SemesterStartDate")] StudentModule studentModule)
        {
            if (id != studentModule.ID)
            {
                return NotFound();
            }


            {
                try
                {
                    _context.Update(studentModule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentModuleExists(studentModule.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studentModule);
        }

        [Authorize]
        // GET: StudentModules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.studentModules == null)
            {
                return NotFound();
            }

            var studentModule = await _context.studentModules
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentModule == null)
            {
                return NotFound();
            }

            return View(studentModule);
        }

        [Authorize]
        // POST: StudentModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.studentModules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.studentModules'  is null.");
            }
            var studentModule = await _context.studentModules.FindAsync(id);
            if (studentModule != null)
            {
                _context.studentModules.Remove(studentModule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentModuleExists(int id)
        {
            return _context.studentModules.Any(e => e.ID == id);
        }
    }




}

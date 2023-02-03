using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tasks.Data;
using Tasks.Models;
using Tasks.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace Tasks.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;
        public EmployeeController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDbContext.Employee.ToListAsync();

            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

          public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest ) 
        {
            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),

            Name = addEmployeeRequest.Name,
            Address = addEmployeeRequest.Address,
            DepartmentId = addEmployeeRequest.DepartmentId

            };

            await mvcDemoDbContext.AddAsync(employee);
           await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        
        }


        [HttpGet]

            public async Task<IActionResult> View(Guid EmployeeId) 
        {
            return View();

            var employee = await mvcDemoDbContext.Employee.FisrtOrDefaultAsync(x => x.EmployeeId == EmployeeId);

            if (employee != null) 
            {



                var viewModel = new UpdateEmployeeModel()
                {
                    EmployeeId = employee.EmployeeId,

                    Name = employee.Name,
                    Address = employee.Address,
                    DepartmentId = employee.DepartmentId
                };
                return await Task.Run(() => View("View",viewModel));
            }
            return RedirectToAction("Index");
        }



        [HttpPost]

           public async Task<IActionResult> View(UpdateEmployeeModel model)
        {

            var employee = await mvcDemoDbContext.Employee.FindAsync(model.EmployeeId) ;

                if(employee != null)
            {
                employee.Name = model.Name;
                employee.Address = model.Address;
                employee.DepartmentId = model.DepartmentId;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> View(UpdateEmployeeModel model)
        {
            var employee = await mvcDemoDbContext.Employee.FindAsync(model.EmployeeId);

            if(employee != null)
            {

                mvcDemoDbContext.Employee.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}

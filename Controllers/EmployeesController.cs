using System.ComponentModel.DataAnnotations;
using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Controllers
{
public class EmployeesController : Controller
{
    private readonly MVCDemoDbContext mvcDemoDbContext;


    public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
    {
        this.mvcDemoDbContext = mvcDemoDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var employees=await mvcDemoDbContext.EmployeesDetails.ToListAsync();
        return View(employees);
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
    {
        var employee = new Employee()
        {
            Id = addEmployeeRequest.Id,
            Name=addEmployeeRequest.Name,
            Email=addEmployeeRequest.Email,
            Salary=addEmployeeRequest.Salary,
            Department=addEmployeeRequest.Department,
            DateOfBirth=addEmployeeRequest.DateOfBirth
        };
        await mvcDemoDbContext.EmployeesDetails.AddAsync(employee);
        await mvcDemoDbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> View(int id)
    {
        var employee=await mvcDemoDbContext.EmployeesDetails.FirstOrDefaultAsync(x => x.Id==id);
        if(employee!=null)
        {
             var viewModel = new UpdateEmployeeViewModel()
        {
            Id=employee.Id,
            Name=employee.Name,
            Email=employee.Email,
            Salary=employee.Salary,
            Department=employee.Department,
            DateOfBirth=employee.DateOfBirth

        };
        return await Task.Run(()=>View("View",viewModel));
        }

        return RedirectToAction("Index");
       
        
    }
    [HttpPost]
    public async Task<IActionResult> View(UpdateEmployeeViewModel model)
    {
        var employee=await mvcDemoDbContext.EmployeesDetails.FindAsync(model.Id);
        if(employee!=null)
        {
            employee.Name=model.Name;
            employee.Email=model.Email;
            employee.Salary=model.Salary;
            employee.DateOfBirth=model.DateOfBirth;
            employee.Department=model.Department;

            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");


    }
    [HttpPost]
    public async Task<IActionResult> Delete(UpdateEmployeeViewModel model) 
    {
        var employee=await mvcDemoDbContext.EmployeesDetails.FindAsync(model.Id);
        if(employee !=null)
        {
            mvcDemoDbContext.EmployeesDetails.Remove(employee); 
            await mvcDemoDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
                    return RedirectToAction("Index");

    }
}

}
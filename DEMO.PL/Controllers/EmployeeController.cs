using AutoMapper;
using DEMO.BLL.InterFaces;
using DEMO.DAL.Entity;
using DEMO.PL.Helper;
using DEMO.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEMO.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Departmentcontroller> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, 
            ILogger<Departmentcontroller> logger,IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;   
        }

        public IActionResult Index(string SearchValue = "")
        {
           // var employee = _unitOfWork.Employee_InterFace.GetAll();
           // return View(employee);
           IEnumerable<Employee> employees;
           IEnumerable<Employeeviewmodle> employeeviewmodles
                ;


            if(string.IsNullOrEmpty(SearchValue))
            {
                employees = _unitOfWork.Employee_InterFace.GetAll();
                employeeviewmodles  = _mapper.Map<IEnumerable<Employeeviewmodle>>(employees);         
            }
            else
            {
                employees = _unitOfWork.Employee_InterFace.Searsh(SearchValue);
                employeeviewmodles = _mapper.Map<IEnumerable<Employeeviewmodle>>(employees);

            }
            return View(employeeviewmodles);
        }


        #region create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Department = _unitOfWork.Department_Interface.GetAll();
            return View(new Employeeviewmodle());
        }
        [HttpPost]
        public IActionResult Create(Employeeviewmodle  employeeviewmodle)
        {
           // ModelState["Department"].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
               // manual mapping
/*                Employee employee = new Employee
                {
                    Name = employeeviewmodle.Name,
                    Email = employeeviewmodle.Email,
                    Address = employeeviewmodle.Address,
                    DepartmentId = employeeviewmodle.DepartmentId,
                    HireDate = employeeviewmodle.HireDate,
                    Salary = employeeviewmodle.Salary, 
                    IsActive = employeeviewmodle.IsActive,  
                };*/
                    
                
                
                var employee = _mapper.Map<Employee>(employeeviewmodle);
                employee.imgUrl = DecumenSetting.UploadFile(employeeviewmodle.Image, "Images");
                _unitOfWork.Employee_InterFace.Add(employee);
                _unitOfWork.complet();
                return RedirectToAction("Index");
            }
            ViewBag.Department = _unitOfWork.Department_Interface.GetAll();
            return View(employeeviewmodle);
        }
        #endregion


        #region Details
        public IActionResult Details(int id)
        {
            // Retrieve the employee with the specified id
            var employee = _unitOfWork.Employee_InterFace.GetId(id);

            if (employee == null)
            {
                // Return a 404 Not Found result if the employee is not found
                return NotFound();
            }

            return View(employee);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var employee = _unitOfWork.Employee_InterFace.GetId(id);
                if (employee == null)
                {
                    return NotFound(); // Return a 404 Not Found response if the department with the given ID is not found.
                }

                return View(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _unitOfWork.Employee_InterFace.Delete(employee);
                _unitOfWork.complet();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }


        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Department = _unitOfWork.Department_Interface.GetAll();

            try
            {
                var employee = _unitOfWork.Employee_InterFace.GetId(id);
                if (employee == null)
                {
                    return NotFound();
                }
                var employeeViewModel = _mapper.Map<Employeeviewmodle>(employee);
                return View(employeeViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Update(int id , Employeeviewmodle employeeviewmodle)
        {
            // ModelState["Department"].ValidationState = ModelValidationState.Valid;

            if (id != employeeviewmodle.Id)
            {
                return BadRequest();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var employee = _mapper.Map<Employee>(employeeviewmodle);
                    _unitOfWork.Employee_InterFace.Update(employee);
                    _unitOfWork.complet();
                    return RedirectToAction(nameof(Index));
                }
               _unitOfWork.complet();
                ViewBag.Department = _unitOfWork.Department_Interface.GetAll();
                return View(employeeviewmodle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }
        #endregion
    }
}

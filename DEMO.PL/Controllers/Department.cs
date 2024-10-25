using DEMO.BLL.InterFaces;
using DEMO.BLL.Repository;
using DEMO.DAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace DEMO.PL.Controllers
{
    public class Departmentcontroller : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Departmentcontroller> _logger;

        public Departmentcontroller(
           IUnitOfWork unitOfWork,
            ILogger<Departmentcontroller> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }



        public IActionResult Index()
        {
            var departments = _unitOfWork.Department_Interface.GetAll();
            return View(departments);
        }



        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Department());
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Department_Interface.Add(department);
                _unitOfWork.complet();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        #endregion



        #region Details
        public IActionResult Details(int id)
        {
            try
            {
                var department = _unitOfWork.Department_Interface.GetId(id);
                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");

            }
        }
        #endregion



        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var department = _unitOfWork.Department_Interface.GetId(id);
                if (department == null)
                {
                    return NotFound(); // Return a 404 Not Found response if the department with the given ID is not found.
                }

                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            try
            {
                _unitOfWork.Department_Interface.Delete(department);
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
            try
            {
                var department = _unitOfWork.Department_Interface.GetId(id);
                if (department == null)
                {
                    return NotFound();
                }

                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Update(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Department_Interface.Update(department);
                    _unitOfWork.complet();
                    return RedirectToAction(nameof(Index));
                }

                return View(department);
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

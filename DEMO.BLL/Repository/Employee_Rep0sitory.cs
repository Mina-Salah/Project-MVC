using DEMO.BLL.InterFaces;
using DEMO.DAL.Context;
using DEMO.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.BLL.Repository
{
    public class Employee_Rep0sitory:Genaric_Repository<Employee>,IEmployee_InterFace
    {
        private readonly Add_context _context;

        public Employee_Rep0sitory(Add_context context):base(context) 
        {
            _context = context;

        }

        public IEnumerable<Employee> GetEmployeesByDepartmentNAme(string departmentName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Searsh(string name)
        {
            var result = _context.Employees.Where(e =>
            e.Name.Trim().ToLower().Contains(name.Trim().ToLower()) ||
            e.Email.Trim().ToLower().Contains(name.Trim().ToLower()));
            return result;
        }

        //public int Add(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _context.Employees.Remove(employee);
        //    return _context.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAll() => _context.Employees.ToList();


        //public Employee GetEmployeeId(int id) => _context.Employees.FirstOrDefault(x => x.Id == id);

        //public int Update(Employee employee)
        //{
        //    _context.Employees.Update(employee);
        //    return _context.SaveChanges();

        //}
    }
}

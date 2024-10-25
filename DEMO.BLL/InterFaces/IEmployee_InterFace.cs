using DEMO.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.BLL.InterFaces
{
    public interface IEmployee_InterFace:IGenaric_Interface<Employee>
    {
        IEnumerable<Employee> GetEmployeesByDepartmentNAme(string departmentName);
        IEnumerable<Employee> Searsh(string name);
        //Employee GetEmployeeId(int id);
        //IEnumerable<Employee> GetAll();
        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);
    }
}

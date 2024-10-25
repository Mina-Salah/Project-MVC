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
    public class Department_Repository:Genaric_Repository<Department>, IDepartment_interface
    {
       // private readonly Add_context _context;

        public Department_Repository(Add_context context):base(context) 
        {
           // _context = context;

        }


        //public int Add(Department department)
        //{
        //    _context.Department.Add(department);
        //    return _context.SaveChanges();
        //}



        //public int Delete(Department department)
        //{
        //    _context.Department.Remove(department);
        //    return _context.SaveChanges();
        //}

        //public int Delete( int id)
        //{
        //    return _context.SaveChanges();
        //}

        //public IEnumerable<Department> GetAll()
        //    => _context.Department.ToList();


        //public Department GetDepartmentId(int id)
        //    => _context.Department.FirstOrDefault(x => x.Id == id);


        //public int Update(Department department)
        //{
        //    _context.Department.Update(department);
        //    return _context.SaveChanges();

        //}


    }
}

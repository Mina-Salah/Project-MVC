using DEMO.BLL.InterFaces;
using DEMO.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Add_context _context;

        public IDepartment_interface Department_Interface {  get;  set; }
        public IEmployee_InterFace Employee_InterFace {  get;  set; }
        public ICatigory_inteface Catigory_inteface { get; set; }
        public UnitOfWork(Add_context context)
        {
            _context = context;
            Department_Interface = new Department_Repository(context);
            Employee_InterFace = new Employee_Rep0sitory(context);
            Catigory_inteface = new Catigory_Repository (context); 
        }

        public int complet()
        {
            return _context.SaveChanges ();    
        }
    }
}

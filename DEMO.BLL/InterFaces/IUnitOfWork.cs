using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.BLL.InterFaces
{
    public interface IUnitOfWork
    {
        public IDepartment_interface Department_Interface { get; set; }
        public IEmployee_InterFace Employee_InterFace { get; set; }
        public ICatigory_inteface Catigory_inteface { get; set; }

        int complet();
    }
}

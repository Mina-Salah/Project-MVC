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
    public class Catigory_Repository : Genaric_Repository<Catigory>, ICatigory_inteface
    {
        public Catigory_Repository(Add_context context) : base(context)
        {
        }
    }
}

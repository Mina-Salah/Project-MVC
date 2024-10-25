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
    public class Genaric_Repository<T> : IGenaric_Interface<T> where T : BaseEntity
    {

        private readonly Add_context _context;

        public Genaric_Repository(Add_context context)
        {
            _context = context;

        }

        public void Add(T Entity)
        {
            _context.Set<T>().Add(Entity);
            //return _context.SaveChanges();
        }

        public void Delete(T Entity)
        {
            _context.Set<T>().Remove(Entity);
           // return _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
            =>_context.Set<T>().ToList();


        public T GetId(int id) =>
            _context.Set<T>().Find(id);


        public void Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
            //return _context.SaveChanges();  
        }
    }
}

namespace DEMO.BLL.InterFaces
{
    public interface IGenaric_Interface<T>
    {
        T GetId(int id);
        IEnumerable<T> GetAll();
        void Add(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
        
    }
}

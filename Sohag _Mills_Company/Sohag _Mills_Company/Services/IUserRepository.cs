namespace Sohag__Mills_Company.Services
{
    public  interface  IUserRepository<T>
    {
        void Add(T entity);
        void Delete(int Id);
        void Update(T entity);
        int Save();

    }
}

using AutoMapper;

namespace DotnetAPI.Data
{
    public class UserRepository: IUserRepository
    {
        DataContextEF _entityFramwork;
       
        public UserRepository(IConfiguration config)
        {
            _entityFramwork = new DataContextEF(config);
           
        }

        public bool SaveChanges()
        {
            return _entityFramwork.SaveChanges()>0;   

        }

        public void AddEntity<T>(T entityToAdd)
        {
            if(entityToAdd != null) {
                _entityFramwork.Add(entityToAdd);
            }
           
        }

        public void RemoveEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFramwork.Remove(entityToAdd);
            }

        }
    }
}

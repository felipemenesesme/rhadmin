using System.Threading.Tasks;
using Api.Helpers;
using Api.Models;

namespace Api.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        Task<PageList<Employee>> GetAllEmployeesAsync(PageParams pageParams);
        Employee[] GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
    }
}
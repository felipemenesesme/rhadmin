using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges()) > 0;
        }

        public async Task<PageList<Employee>> GetAllEmployeesAsync(PageParams pageParams)
        {
            IQueryable<Employee> query = _context.Employees;

            query = query.AsNoTracking().OrderBy(employee => employee.Id);
            
            if (!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(employee => employee.Nome.ToUpper().Contains(pageParams.Nome.ToUpper()));
            
            if (pageParams.Ativo != null)
                query = query.Where(employee => employee.Ativo == (pageParams.Ativo != 0));

            return await PageList<Employee>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Employee[] GetAllEmployees()
        {
            IQueryable<Employee> query = _context.Employees;

            query = query.AsNoTracking().OrderBy(employee => employee.Id);

            return query.ToArray();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            IQueryable<Employee> query = _context.Employees;

            query = query.AsNoTracking()
                         .OrderBy(employee => employee.Id)
                         .Where(employee => employee.Id == employeeId);

            return query.FirstOrDefault();
        }
    }
}
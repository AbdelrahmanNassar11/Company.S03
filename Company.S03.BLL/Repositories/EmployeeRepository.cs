using Company.S03.BLL.Interface;
using Company.S03.DAL.Data.Contexts;
using Company.S03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.S03.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _context;
        private DbContext? dbContext;

        //public EmployeeRepository(CompanyDbContext companyDbContext)
        //{
        //    _context = companyDbContext;
        //}
        //public IEnumerable<Employee> GetAll()
        //{
        //    return _context.Employees.ToList();
        //}
        //public Employee? Get(int id)
        //{
        //    return _context.Employees.Find(id);
        //}
        //public int Add(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    return _context.SaveChanges();
        //}
        //public int Update(Employee employee)
        //{
        //    _context.Employees.Update(employee);
        //    return _context.SaveChanges();
        //}
        //public int Delete(Employee employee)
        //{
        //    _context.Employees.Remove(employee);
        //    return _context.SaveChanges();
        //} 
        public EmployeeRepository(CompanyDbContext context) : base(context) => _context = context;

        //public EmployeeRepository(DbContext? dbContext)
        //{
        //    this.dbContext = dbContext;
        //}

        public CompanyDbContext Context { get; }

        public async Task<List<Employee>> GetByNameAsync(string name)
        {
            return await _context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower() == name.ToLower()).ToListAsync();
        }
    }
}

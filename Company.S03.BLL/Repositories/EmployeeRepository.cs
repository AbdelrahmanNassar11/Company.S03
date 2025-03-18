using Company.S03.BLL.Interface;
using Company.S03.DAL.Data.Contexts;
using Company.S03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.S03.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        //private readonly CompanyDbContext _context;
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
        public EmployeeRepository(CompanyDbContext context) : base(context)
        {

        }
    }
}

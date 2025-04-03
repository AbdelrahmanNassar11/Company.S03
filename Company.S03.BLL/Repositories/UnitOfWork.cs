using Company.S03.BLL.Interface;
using Company.S03.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.S03.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _dbContext;
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public UnitOfWork(CompanyDbContext dbContext)
        {
            _dbContext = dbContext;
            DepartmentRepository = new DepartmentRepository(_dbContext);
            EmployeeRepository = new EmployeeRepository(_dbContext);
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
             await _dbContext.DisposeAsync();
        }
    }
}

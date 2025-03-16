using Company.S03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.S03.BLL.Interface
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        //IEnumerable<Department> GetAll();
        //Department? Get(int id);
        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);
    }
}

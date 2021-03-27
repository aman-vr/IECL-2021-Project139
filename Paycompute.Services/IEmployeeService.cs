using Microsoft.AspNetCore.Mvc.Rendering;
using Paycompute.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paycompute.Services.Implementation
{
    public interface IEmployeeService
    {
        Task CreateAsync(Employee newEmployee);
        Task Delete(int employeeId);
        IEnumerable<Employee> GetAll();
        Employee GetById(int employeeId);
        Task UpdateAsync(Employee employee);
        Task UpdateAsync(int employeeId);
        decimal UnionFees(int id);
        decimal StudentLoanRepaymentAmount(int id, decimal totalAmount);
        IEnumerable<SelectListItem> GetAllEmployeesForPayroll();
    }
}
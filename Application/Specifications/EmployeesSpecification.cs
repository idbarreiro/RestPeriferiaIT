using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class EmployeesSpecification : Specification<Employee>
    {
        public EmployeesSpecification(int departmentId)
        {
            Query
                .Where(e => e.DepartmentId == departmentId)
                .OrderBy(e => e.Name);
        }
    }
}

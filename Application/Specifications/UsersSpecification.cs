using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public class UsersSpecification : Specification<User>
    {
        public UsersSpecification(string email)
        {
            Query
                .Where(u => u.Email == email)
                .OrderBy(u => u.Name);
        }
    }
}

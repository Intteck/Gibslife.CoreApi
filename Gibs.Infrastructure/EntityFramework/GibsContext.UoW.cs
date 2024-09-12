using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Infrastructure
{
    public partial class GibsContext : DbContext
    {
        public CipCodeNumberFactory GetCodeFactory(Branch? branch, Product? product)
        {
            if (branch == null)
                throw new ArgumentNullException(nameof(branch));

            return new CipCodeNumberFactory(this, branch, product);
        }
    }
}

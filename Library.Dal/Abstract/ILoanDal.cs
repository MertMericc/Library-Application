using Library.Core.Repository;
using Library.Dal.Concrete.Context;
using Library.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dal.Abstract
{
    public interface ILoanDal : IEntityRepository<Loan>
    {
    }
}

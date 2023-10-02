using Library.Core.Entity;
using Library.Core.Repository;
using Library.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dal.Abstract
{
    public interface IBookDal:IEntityRepository<Book>
    {
    }
}

using Library.Core.EntityFramework;
using Library.Dal.Abstract;
using Library.Dal.Concrete.Context;
using Library.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dal.Concrete.EntityFramework
{
    public class EfBookDal : EfEntityRepositoryBase<Book, LibraryDbContext>, IBookDal
    {
    }
}

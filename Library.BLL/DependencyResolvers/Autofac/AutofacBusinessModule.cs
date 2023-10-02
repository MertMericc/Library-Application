using Autofac;
using Library.BLL.Abstract;
using Library.BLL.Concrete;
using Library.Dal.Abstract;
using Library.Dal.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LibraryManager>().As<ILibraryService>();
            builder.RegisterType<EfBookDal>().As<IBookDal>();

            builder.RegisterType<EfLoanDal>().As<ILoanDal>();
        }
    }
}

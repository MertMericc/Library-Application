using Library.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entity.Concrete
{
    public class Loan:IEntity
    {
        public int LoanId { get; set; } // Ödünç Verme kimlik numarası
        public int BookId { get; set; } // Ödünç verilen kitabın kimlik numarası
        public DateTime LoanDate { get; set; } // Ödünç alınma tarihi
        public DateTime DueDate { get; set; } // Geri getirme tarihi
        public bool IsReturned { get; set; } // Kitap geri getirildi mi?
        public string UserName { get; set; }
    }

}

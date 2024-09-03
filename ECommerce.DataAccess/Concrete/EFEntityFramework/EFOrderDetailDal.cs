using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.DataAccess.Abstraction;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Concrete.EFEntityFramework
{
    public class EFOrderDetailDal : EFEntityRepositoryBase<OrderDetail, NorthwindContext>, IOrderDetailsDal
    {
        public EFOrderDetailDal(NorthwindContext context) : base(context)
        {
        }
    }
}

using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstraction;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IProductDal _productDal;
        private readonly IOrderDetailsDal _orderDetailsDal;
        public CategoryService(ICategoryDal categoryDal, IProductDal productDal, IOrderDetailsDal orderDetailsDal)
        {
            _productDal = productDal;
            _categoryDal = categoryDal;
            _orderDetailsDal = orderDetailsDal;
        }

        public async Task AddAsync(Category category)
        {
            await _categoryDal.Add(category);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _categoryDal.Get(c => c.CategoryId == id);
            var collection =await _productDal.GetList(p=> p.CategoryId == id);
            
            foreach (var prod in collection)
            {
                var orderDetailCollection = await _orderDetailsDal.GetList(o=>o.ProductId==prod.ProductId);
                await _orderDetailsDal.DeleteList(orderDetailCollection);
            }
            await _productDal.DeleteList(collection);
            await _categoryDal.Delete(item);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryDal.GetList();
        }
    }
}

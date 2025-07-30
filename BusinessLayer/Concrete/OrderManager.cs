using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDAL _orderDAL;
        public OrderManager(IOrderDAL orderDAL)
        {
            _orderDAL = orderDAL;
        }
        public void TDelete(Order entity)
        {
            _orderDAL.Delete(entity);
        }

        public List<Order> TGetAll()
        {
            return _orderDAL.GetAll();
        }

        public Order TGetById(int id)
        {
            return _orderDAL.GetById(id);
        }

        public void TInsert(Order entity)
        {
            _orderDAL.Insert(entity);
        }

        public void TUpdate(Order entity)
        {
            _orderDAL.Update(entity);
        }
    }
}

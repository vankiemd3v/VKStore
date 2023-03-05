
using VKStore.Data.EF;
using VKStore.Data.Entities;
using VKStore.Utilities.Exceptions;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VKStore.Application.Common;
using Azure.Core;
using VKStore.ViewModels.Catalog.ProductImages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using VKStore.ViewModels.Catalog.Slide;
using VKStore.ViewModels.Catalog.Orders;
using VKStore.Data.Enums;
using static VKStore.Ultilities.Constants.SystemConstants;

namespace VKStore.Application.Catalog.Orders
{
    public class OrderService : IOrderService
    {
        private readonly VKStoreDbContext _context;
        private readonly IStorageService _storageService;
        public OrderService(VKStoreDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<bool> CreateOrder(CreateOrderRequest request)
        {
            var order = new Order();
            order.ShipEmail = request.ShipEmail;
            order.ShipName = request.ShipName;
            order.ShipPhoneNumber = request.ShipPhoneNumber;
            order.ShipAddress = request.ShipAddress;
            order.OrderDate = DateTime.Now;
            order.Status = StatusOrder.Inprogess;
            order.TotalPayment = request.TotalPayment;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            int id = order.Id;
            var orderDetails = new List<OrderDetail>();
            foreach (var item in request.OrderDetails)
            {
                orderDetails.Add(new OrderDetail
                {
                    OrderId = id,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }
            foreach (var item in orderDetails)
            {
                _context.OrderDetails.Add(item);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OrderViewModel> Detail(int id)
        {
            var query = from od in _context.OrderDetails
                        join p in _context.Products on od.ProductId equals p.Id
                        where od.OrderId == id
                        select new { od, p };
            var orderDetail = new List<OrderDetailViewModel>();
            foreach (var item in query)
            {
                orderDetail.Add(new OrderDetailViewModel()
                {
                    OrderId = item.od.OrderId,
                    ProductId = item.od.ProductId,
                    Price = item.od.Price,
                    Quantity = item.od.Quantity,
                    ProductName = item.p.Name
                });
            }
            var data = await _context.Orders.Where(x=>x.Id == id).Select(x => new OrderViewModel()
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                ShipName = x.ShipName,
                ShipPhoneNumber = x.ShipPhoneNumber,
                ShipEmail = x.ShipEmail,
                ShipAddress = x.ShipAddress,
                TotalPayment = x.TotalPayment,
                Status = x.Status,
                OrderDetails = orderDetail
            }).SingleOrDefaultAsync();
            return data;
        }

        public async Task<PagedResult<OrderViewModel>> GetAllPaging(GetOrdersPagingRequest request)
        {
            var query = from o in _context.Orders select new { o };
            if (!string.IsNullOrEmpty(request.Keyword))
                query =  query.Where(x => x.o.ShipName.Contains(request.Keyword));
            var data = await query.OrderByDescending(x=>x.o.OrderDate).Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                            .Select(x => new OrderViewModel()
                            {
                                Id = x.o.Id,
                                OrderDate = x.o.OrderDate,
                                ShipName = x.o.ShipName,
                                ShipPhoneNumber = x.o.ShipPhoneNumber,
                                ShipEmail = x.o.ShipEmail,
                                ShipAddress = x.o.ShipAddress,
                                TotalPayment = x.o.TotalPayment,
                                Status = x.o.Status,
                            }).ToListAsync();

            // 4. Trả về kết quả
            var pagedResult = new PagedResult<OrderViewModel>()
            {
                TotalRecords = query.Count(),
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<bool> UpdateStatusOrder(UpdateStatusOrderRequest request)
        {
            if(request != null)
            {
                var order = await _context.Orders.Where(x => x.Id == request.Id).SingleOrDefaultAsync();
                order.Status = request.Status;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

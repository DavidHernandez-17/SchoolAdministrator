﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrator.Data;
using SchoolAdministrator.Enums;
using SchoolAdministrator.Helpers;

namespace SchoolAdministrator.Controllers
{
    public class DashboardController : Controller
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public DashboardController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.UsersCount = _context.Users.Count();
            ViewBag.ProductsCount = _context.Products.Count();
            ViewBag.NewOrdersCount = _context.Sales.Where(o => o.OrderStatus == OrderStatus.Nuevo).Count();
            ViewBag.ConfirmedOrdersCount = _context.Sales.Where(o => o.OrderStatus == OrderStatus.Confirmado).Count();

            return View(await _context.TemporalSales
                    .Include(u => u.User)
                    .Include(p => p.Product).ToListAsync());
        }
    }

}


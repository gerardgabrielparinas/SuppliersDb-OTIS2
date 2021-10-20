using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuppliersDb.Data;
using SuppliersDb.Models;
using Microsoft.EntityFrameworkCore;

namespace SuppliersDb.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var list = _context.Items.ToList();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Item record)
        {
            var item = new Item();
            item.CompanyName = record.CompanyName;
            item.Representative = record.Representative;
            item.Code = record.Code;
            item.Address = record.Address;
            item.DateAdded = DateTime.Now;
            item.Type = record.Type;

            _context.Items.Add(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var item = _context.Items.Where(i => i.SupplierId == id).SingleOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Item record)
        {
            var item = _context.Items.Where(i => i.SupplierId == id).SingleOrDefault();
            item.CompanyName = record.CompanyName;
            item.Representative = record.Representative;
            item.Code = record.Code;
            item.Address = record.Address;
            item.DateAdded = DateTime.Now;
            item.DateModified = DateTime.Now;
            item.Type = record.Type;

            _context.Items.Update(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var item = _context.Items.Where(i => i.SupplierId == id).SingleOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            _context.Items.Remove(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Search()
        {
            var displaydata = _context.Items.ToList();
            return View("Index", displaydata);
        }

        [HttpGet]
        public async Task<IActionResult> Search(String SupplierSearch)
        {
            ViewData["GetSupplierDetails"] = SupplierSearch;

            var supplierquery = from x in _context.Items select x;
            if (!String.IsNullOrEmpty(SupplierSearch))
            {
                supplierquery = supplierquery.Where(x => x.CompanyName.Contains(SupplierSearch) || x.Representative.Contains(SupplierSearch) || x.Code.Contains(SupplierSearch) || x.Address.Contains(SupplierSearch));
            }
            return View("Index", await supplierquery.AsNoTracking().ToListAsync());
        }
    }
}

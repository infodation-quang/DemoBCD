﻿using AutoMapper;
using Demo.EntityFramework.Entities;
using Demo.Service.Base;
using Demo.Service.Base.Dtos;
using Demo.Service.Base.Enums;
using Demo.Service.Base.Interfaces;
using Demo.UnitOfWork.interfaces;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.Service.Business.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AbcController : Controller
    {
        private readonly IRepository<Organization, Guid> _repository;
        private readonly IExcelManager _excelManager;
        private List<ExcelHeader>  products = new List<ExcelHeader>();

        public AbcController(
            IRepository<Organization, Guid> repository,
            IExcelManager excelManager)
        {
            _repository = repository;
            _excelManager = excelManager;
        }

        [HttpPost]
        public async Task A()
        {
            var p = typeof(Organization).Name;

            var obj = new Organization();

            products = new List<ExcelHeader>()
            {
                new ExcelHeader()
                {
                    Key = nameof(obj.CodeValue),
                    Value = "Code Value",
                    Type = ExcelType.Default
                },
                new ExcelHeader()
                {
                    Key = nameof(obj.Name),
                    Value = "Code Value",
                    Type = ExcelType.Default
                },
                new ExcelHeader()
                {
                    Key = nameof(obj.CreatedTime),
                    Value = "Code Value",
                    Type = ExcelType.DateTime
                }
            };
             
            var query = _repository.Query;

            var list2 = await query.OrderBy(o => o.Name).ToListAsync();

            _excelManager.ExportExcelDefault<Organization>(products, list2);
        }


        [HttpPost]
        public async Task Abc()
        {
            var query = _repository.Query;

            var list2 = await query.OrderBy(o => o.Name).ToListAsync();

            var searchPredicate = PredicateBuilder.New<Organization>();


            Expression<Func<Organization, bool>> expression = p => EF.Property<string>(p, "CreatedTime") == "3/11/2022 8:47:03 AM";

            query = query.OrderBy(o => EF.Property<string>(o, "Name"));
            //query = query.Where(expression);
            //query = query.Where(a => EF.Property<string>(a, "Name") == "Kế Toán");
            //query = query.Where(a => EF.Property<string>(a, "CodeValue") != "BV");
            //query = query.Where(a => EF.Property<string>(a, "CodeValue") != "NS");

            //searchPredicate = searchPredicate.Or(QueryableExtention.GetExpression<Organization>("Name", orValue.Operation, orValue.Value));

            //for (int i = 0; i < 2; i++)
            //{
            //    if(i == 0)
            //    {
            //        query = query.Where(w => w.Name.Contains("Kế Toán"));
            //    }
            //    else if(i == 1)
            //    {
            //        query = query.Or(w => w.CodeValue.Contains("KT"));
            //    }
            //}

            var list = await query.ToListAsync();
        }
       
    }
}
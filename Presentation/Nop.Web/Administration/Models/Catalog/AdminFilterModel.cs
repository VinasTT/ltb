using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Core.Domain.Catalog;
//NOP 3.82
namespace Nop.Admin.Models.Catalog
{
    public static class AdminFilterModel
    {

        public static string StocklessProducts { get { return "Stoksuz Ürünler"; } }
        public static string NewProducts { get { return "Yeni Ürünler"; } }
        public static string PicturelessProducts { get { return "Resimsiz Ürünler"; } }
    }
}
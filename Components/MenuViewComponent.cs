using AutoService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AutoService.Components
{
    public class MenuViewComponent:ViewComponent
    {
        private List<MenuItem> _menuItems = new List<MenuItem>
        {
            new MenuItem{Controller="Home", Action="Index", Text="Автосервис"},
            new MenuItem{Controller="Drivers", Action="Index", Text="Водители"},
            new MenuItem{Controller="Auto", Action="Index", Text="Автотранспорт"},
            new MenuItem{Controller="Inspection", Action="Index", Text="Тех.осмотр"},
            new MenuItem{Controller="Insurances", Action="Index", Text="Страховки"},
            new MenuItem{Controller="Servs", Action="Index", Text="Тех.обслуживание"},
        };
        public IViewComponentResult Invoke()
        {
            var controller = ViewContext.RouteData.Values["controller"];
            var page = ViewContext.RouteData.Values["page"];
            var area = ViewContext.RouteData.Values["area"];

            foreach(var item in _menuItems)
            {
                var _matchController = controller?.Equals(item.Controller) ?? false;
                var _matchArea = area?.Equals(item.Area) ?? false;
                if(_matchArea|_matchController)
                {
                    item.Active = "active";
                }
            }
            return View(_menuItems);
        }
    }
}

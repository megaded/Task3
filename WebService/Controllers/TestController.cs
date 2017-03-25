using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers
{
    public class TestController : ApiController
    {
        public HttpResponseMessage GetList(string SearchField)
        {
            if (SearchField == null)
                return Request.CreateResponse(HttpStatusCode.OK, new List<SettingViewModel>());
            var keys = SearchField.Split(' ');
            var model = new List<SettingViewModel>();
            foreach (var key in keys)
            {
                model.Add(new SettingViewModel()
                {
                    Key = key,
                    Value = "Значение"
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
}

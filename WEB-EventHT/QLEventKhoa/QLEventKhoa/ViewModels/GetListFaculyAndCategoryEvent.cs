using QLEventKhoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLEventKhoa.ViewModels
{
    public class GetListFaculyAndCategoryEvent
    {
        public List<tbKhoa> GetListFaculty { get; set; }
        public List<tbLoaiEvent> GetCategoryEvent { get; set; }
        public List<tbSuKien> GetListEvent { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLEventKhoa.Models;


namespace QLEventKhoa.ViewModels
{
    public class topevthamgia
    {
        public string evname { set; get; }
        public int evid { set; get; }
        public int thamgia { set; get; }
        public int tong { set; get; }
        public string mathamgia { set; get; }
        public string  trangthai { set; get; }

    }
    public class topclassjoin
    {
        public string classname { set; get; }
        public int classid { set; get; }
        public int evjoincount { set; get; }
        public string schoolyear { set; get; }
        public string status { set; get; }
        public string faculty { set; get; }

    }
    public class topstudent
    {
        public string sname { set; get; }
        public string scode { set; get; }
        public int sjoin { set; get; }
        public string sclass { set; get; }
        public string status { set; get; }      
    }
    public class topfaculty
    {
        public string fname { set; get; }
        public string fcode { set; get; }
        public int fevcount { set; get; }
        public string status { set; get; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------
using System.Web;
namespace asp_test.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        public string BookId { get; set; }
        public string Categories { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public Nullable<int> Pages { get; set; }
        public string ISBN_10 { get; set; }
        public string ISBN_13 { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string Edition { get; set; }
        public Nullable<int> Year { get; set; }
        public HttpPostedFileBase Cover { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
    }
}

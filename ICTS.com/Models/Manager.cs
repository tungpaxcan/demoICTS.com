//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ICTS.com.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Users { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> Birth { get; set; }
        public Nullable<int> Phone { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Sex { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public string CreateBy { get; set; }
        public string ModifileBy { get; set; }
        public Nullable<System.DateTime> ModifileDate { get; set; }
        public Nullable<bool> Role { get; set; }
        public Nullable<bool> StatusImage { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}

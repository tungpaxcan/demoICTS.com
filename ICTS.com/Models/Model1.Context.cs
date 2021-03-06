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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BlogAdd> BlogAdds { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DetailProduct> DetailProducts { get; set; }
        public virtual DbSet<DetailSolution> DetailSolutions { get; set; }
        public virtual DbSet<Download> Downloads { get; set; }
        public virtual DbSet<GmailNewsLetter> GmailNewsLetters { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<SEO> SEOs { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Solution> Solutions { get; set; }
    }
}

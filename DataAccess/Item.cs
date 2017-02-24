//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.PurchaseItems = new HashSet<PurchaseItem>();
            this.Stocks = new HashSet<Stock>();
        }
    
        public int ItemID { get; set; }
        public string Name { get; set; }
        public Nullable<int> GenericNameID { get; set; }
        public Nullable<int> ManufacturerID { get; set; }
        public Nullable<int> CategeoryID { get; set; }
        public int AlertQty { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public string BarCode { get; set; }
    
        public virtual Catagory Catagory { get; set; }
        public virtual GenericName GenericName { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}

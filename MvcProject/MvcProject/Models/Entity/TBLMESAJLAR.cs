//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcProject.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLMESAJLAR
    {
        public int ID { get; set; }
        public string GONDEREN { get; set; }
        public string ALICI { get; set; }
        public string BASLIK { get; set; }
        public string MESAJ { get; set; }
        public Nullable<System.DateTime> TARIH { get; set; }
        public Nullable<bool> DURUM { get; set; }
    }
}

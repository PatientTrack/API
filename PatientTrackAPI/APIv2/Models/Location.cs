//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIv2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Location
    {
        public int LocationID { get; set; }
        public int PatientID { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Nullable<System.DateTime> TimeLocated { get; set; }
    
        public virtual Patient Patient { get; set; }
    }
}

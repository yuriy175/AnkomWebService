//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ankor.Model.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SPODE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPODE()
        {
            this.Measures = new HashSet<Measure>();
            this.DeviceDriverSpodes = new HashSet<DeviceDriverSpode>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public double OrderID { get; set; }
        public string OBIS { get; set; }
        public string DescRU { get; set; }
        public string DescEN { get; set; }
        public string IIC_Atr { get; set; }
        public string MeterClasses { get; set; }
        public Nullable<int> OKEI_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Measure> Measures { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceDriverSpode> DeviceDriverSpodes { get; set; }

        public SPODE ParentSPODE { get; set; }
    }
}
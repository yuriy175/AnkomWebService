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
    
    public partial class DeviceModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeviceModification()
        {
            this.DeviceDrivers = new HashSet<DeviceDriver>();
            //this.DeviceSettings = new HashSet<DeviceSetting>();
        }
    
        public int id { get; set; }
        public Nullable<int> parentId { get; set; }
        public int brandID { get; set; }
        public Nullable<int> resourceTypeID { get; set; }
        public string modification { get; set; }
        public string WEB { get; set; }
        public byte[] image { get; set; }
        public Nullable<int> id_driverDflt { get; set; }
        public string comment { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceDriver> DeviceDrivers { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DeviceSetting> DeviceSettings { get; set; }
    }
}

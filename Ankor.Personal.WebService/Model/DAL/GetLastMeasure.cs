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
    
    public partial class GetLastMeasure
    {
        public int deviceID { get; set; }
        public int measureTypeID { get; set; }
        public System.DateTime measureDT { get; set; }
        public Nullable<double> value { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string OBIS { get; set; }
        public string DescRU { get; set; }
    }
}
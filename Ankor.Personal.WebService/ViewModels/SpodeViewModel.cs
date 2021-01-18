using Ankor.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.ViewModels
{
    public struct SpodeViewModel
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public string parentDesc { get; set; }
        public double OrderID { get; set; }
        public string OBIS { get; set; }
        public string DescRU { get; set; }
        public string DescEN { get; set; }
        public string IIC_Atr { get; set; }
        public string MeterClasses { get; set; }
        public int? OKEI_ID { get; set; }
        public string title { get; set; }

        public SpodeViewModel(SPODE spode)
        {
            ID = spode.ID;
            ParentID = spode.ParentID;
            parentDesc = spode.ParentSPODE?.DescRU;
            OrderID = spode.OrderID;
            OBIS = spode.OBIS;
            DescRU = spode.DescRU;
            DescEN = spode.DescEN;
            IIC_Atr = spode.IIC_Atr;
            MeterClasses = spode.MeterClasses;
            OKEI_ID = spode.OKEI_ID;
            title = "No OKEI Item";
        }

        public SpodeViewModel(udf_GetMeterSpodes_Result spode)
        {
            ID = spode.id_spodes;
            ParentID = spode.id_category;
            parentDesc = spode.categoryName;
            OrderID = 0;
            OBIS = spode.OBIS;
            DescRU = spode.spodesName;
            DescEN = string.Empty;
            IIC_Atr = string.Empty;
            MeterClasses = string.Empty;
            OKEI_ID = spode.id_OKEI;
            title = spode.okeiName;
        }
    }
}

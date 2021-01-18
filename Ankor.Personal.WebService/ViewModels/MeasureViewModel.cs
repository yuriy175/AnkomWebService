using Ankor.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.ViewModels
{
    public struct MeasureViewModel
    {
        public int deviceID { get; set; }
        public int? factoryID { get; set; }
        public int measureTypeID { get; set; }
        public DateTime? measureDT { get; set; }
        public double? value { get; set; }
        public int? ParentID { get; set; }
        public string pDescRU { get; set; }
        public string OBIS { get; set; }
        public string DescRU { get; set; }
        public byte? Status { get; set; }

        public MeasureViewModel(Measure measure)
        {
            deviceID = measure.deviceID;
            factoryID = measure.Device?.factoryID;
            measureTypeID = measure.measureTypeID;
            measureDT = measure.measureDT;
            value = measure.value;
            ParentID = measure.SPODE?.ParentID;
            pDescRU = measure.SPODE?.DescRU;
            OBIS = measure.SPODE?.OBIS;
            DescRU = measure.SPODE?.DescRU;
            Status = null;
        }

        public MeasureViewModel(int deviceId, usp_GetLastDeviceMeasures_Result measure)
        {
            deviceID = deviceId;
            factoryID = measure.factoryID;
            measureTypeID = measure.measureType;
            measureDT = measure.measureDT;
            value = measure.value;
            int.TryParse(measure.parentDesc.Split(new[] { '.' })?.FirstOrDefault() ?? "0", out int parentId);
            ParentID = parentId;
            pDescRU = measure.parentDesc;
            OBIS = string.Empty;
            DescRU = measure.DescRU;
            Status = measure.status;
        }

        public MeasureViewModel(int deviceId, usp_GetDeviceMeasure_Result measure)
        {
            deviceID = deviceId;
            factoryID = measure.factoryID;
            measureTypeID = measure.measureType;
            measureDT = measure.measureDT;
            value = measure.value;
            ParentID = null;
            pDescRU = string.Empty;
            OBIS = string.Empty;
            DescRU = string.Empty;
            Status = measure.status;
        }
        //
    }
}

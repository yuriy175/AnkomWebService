using Ankor.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.ViewModels
{
    public struct DeviceViewModel
    {        
        public int id_account { get; set; }
        public int id_device { get; set; }
        public DateTime? registerDT { get; set; }
        public DateTime? breakDT { get; set; }
        public int? breakReason { get; set; }
        public string comment { get; set; }
        public int factoryID { get; set; }
        public string IMEI { get; set; }
        public DateTime? setupDT { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? VerificationDate { get; set; }
        public DateTime? NextVerification { get; set; }
        public int? ParentID { get; set; }
        public int? brandID { get; set; }
        public string brandName { get; set; }
        public int? id_resourse { get; set; }
        public string resourseName { get; set; }
        public int? id_modification { get; set; }
        public string measureType { get; set; }
        public string fullModification { get; set; }
        public int? id_status { get; set; }
        public string deviceStatus { get; set; }

        public string image { get; set; }

        public DeviceViewModel(DeviceRegHistory history)
        {
            id_account = history.id_account;
            id_device = history.id_device;
            registerDT = history.registerDT;
            breakDT = history.breakDT;
            breakReason = history.breakReason;
            comment = history.comment;
            factoryID = history.Device.factoryID;
            IMEI = !history.Device.IMEI.HasValue? string.Empty : history.Device.IMEI.Value.ToString();
            setupDT = history.Device?.setupDT;
            ProductionDate = history.Device?.DeviceSetting?.ProductionDate;
            VerificationDate = history.Device?.DeviceSetting?.VerificationDate;
            NextVerification = history.Device?.DeviceSetting?.VerificationDate;
            ParentID = history.Device?.DeviceSetting?.ParentID;
            brandID = history.Device?.DeviceSetting?.DeviceModification?.brandID;
            brandName = history.Device?.DeviceSetting?.name;
            id_resourse = history.Device?.DeviceSetting?.DeviceModification?.resourceTypeID;
            resourseName = string.Empty;// "Электроэнергия";
            id_modification = history.Device?.DeviceSetting?.id_modification;
            measureType = history.Device?.DeviceSetting?.fullModification;
            fullModification = history.Device?.DeviceSetting?.fullModification;
            id_status = history.Device?.DeviceSetting?.id_status;
            deviceStatus = string.Empty;//  "В работе";
            
            image = "data:image/bmp;base64," + Convert.ToBase64String(history.Device?.DeviceSetting?.DeviceModification?.image);
        }
    }
}

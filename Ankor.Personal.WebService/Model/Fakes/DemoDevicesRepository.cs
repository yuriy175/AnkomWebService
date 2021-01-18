using Ankor.Personal.WebService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.Model.Demos
{
    /// <summary>
    /// devices repository for demo mode
    /// </summary>
    public class DemoDevicesRepository
    {
        private readonly IEnumerable<DeviceViewModel> _devices = new List<DeviceViewModel>
        {
            new DeviceViewModel{
                id_account = 1,
                id_device = 21279,
                registerDT = DateTime.Parse("10.01.2020 13:29:37"),
                breakDT = null,
                breakReason = null,
                comment = null,
                factoryID = 4091659,
                IMEI = "8121069367279567747",
                setupDT = DateTime.Parse("01.01.2020 0:00:00"),
                ProductionDate = DateTime.Parse("07.11.2019 0:00:00"),
                VerificationDate = null,
                NextVerification = null,
                ParentID = null,
                brandID = 1,
                brandName = "СПбЗИП",
                id_resourse = 1,
                resourseName = "Электроэнергия",
                id_modification = 25,
                measureType = "ЦЭ2727А.RF.Lora.ZIP",
                fullModification = "ЦЭ2727А.RF.Lora.ZIP",
                id_status = 1,
                deviceStatus = "В работе"
            },
            new DeviceViewModel{
                id_account = 1,
                id_device = 21280,
                registerDT = DateTime.Parse("20.01.2020 13:29:37"),
                breakDT = null,
                breakReason = null,
                comment = null,
                factoryID = 1601629,
                IMEI = "8121069367279567888",
                setupDT = DateTime.Parse("01.01.2020 0:00:00"),
                ProductionDate = DateTime.Parse("07.11.2019 0:00:00"),
                VerificationDate = null,
                NextVerification = null,
                ParentID = null,
                brandID = 1,
                brandName = "СПбЗИП2",
                id_resourse = 1,
                resourseName = "Электроэнергия",
                id_modification = 25,
                measureType = "ЦЭ2727А.RF.Lora.ZIP",
                fullModification = "ЦЭ2727А.RF.Lora.ZIP",
                id_status = 1,
                deviceStatus = "В работе"
            },
            new DeviceViewModel{
                id_account = 1,
                id_device = 21281,
                registerDT = DateTime.Parse("21.01.2020 13:29:37"),
                breakDT = null,
                breakReason = null,
                comment = null,
                factoryID = 4091657,
                IMEI = "8121069367279567743",
                setupDT = DateTime.Parse("01.01.2020 0:00:00"),
                ProductionDate = DateTime.Parse("07.11.2019 0:00:00"),
                VerificationDate = null,
                NextVerification = null,
                ParentID = null,
                brandID = 1,
                brandName = "СПбЗИП2",
                id_resourse = 8,
                resourseName = "Холодная вода",
                id_modification = 25,
                measureType = "ЦЭ2727А.RF.Lora.ZIP",
                fullModification = "ЦЭ2727А.RF.Lora.ZIP",
                id_status = 1,
                deviceStatus = "В работе"
            },

        };

        public IEnumerable<DeviceViewModel> Devices => _devices;
    }
}

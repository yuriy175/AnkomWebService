using Ankor.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ankor.Personal.WebService.ViewModels
{
    public struct EventsViewModel
    {
        /// <summary>
        /// Серийный номер счетчика
        /// </summary>
        public int FactoryID { get; set; }

        /// <summary>
        /// IMEI встроенного модема (DevEUI)
        /// </summary>
        public long? IMEI { get; set; }

        /// <summary>
        /// Код журнала событий
        /// </summary>
        public int? EventCode { get; set; }

        /// <summary>
        /// Дата и время создания/отправки объекта
        /// </summary>
        public DateTime LogDT { get; set; }

        /// <summary>
        /// Уникальный ключ, идентифицирующий приложение, выполняющее отправку
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// Список записей журнала событий прибора учета
        /// </summary>
        public IEnumerable<EventLogViewModel> Events { get; set; }

        /// <summary>
        /// Запись журнала событий
        /// </summary>
        public class EventLogViewModel
        {
            /// <summary>
            /// id события
            /// </summary>
            public long? Id { get; set; }

            /// <summary>
            /// Дата и время начала события
            /// </summary>
            public DateTime StartDT { get; set; }

            /// <summary>
            /// Дата и время завершения события
            /// </summary>
            public DateTime? StopDT { get; set; }

            /// <summary>
            /// Данные события. Парсинг данных д.б. реализован клиентом в зависимости от модификации ПУ
            /// !!!! Требуется реализовать общий подход с учетом требований СПОДЭС
            /// </summary>
            public byte[] Data { get; set; }

            /// <summary>
            /// Текстовое описание события. Парсинг данных д.б. реализован клиентом в зависимости от модификации ПУ
            /// </summary>
            public string Text { get; set; }

            /// <summary>
            /// shows if the event is notified
            /// </summary>
            public bool IsNotified { get; set; }
        }

        public EventsViewModel(Event evnt)
        {
            FactoryID = evnt.Device.factoryID;
            IMEI = evnt.Device.IMEI;
            EventCode = evnt.eventCode ?? evnt.nativeCode;
            LogDT = evnt.startDT;
            AppKey = "000000000000000000";
            Events = new[]
            {
                new EventLogViewModel
                {
                    Id = evnt.id,
                    StartDT = evnt.startDT,
                    StopDT = evnt.stopDT,
                    Data = evnt.value,
                    Text = evnt.description,
                    IsNotified = evnt.IsNotified,
                }
            };
        }
    }

    public struct EventsRequestViewModel
    {
        /// <summary>
        /// Серийный номерa счетчика
        /// </summary>
        public int[] FactoryIDs { get; set; }

        /// <summary>
        /// IMEI встроенного модема (DevEUI)
        /// </summary>
        public string[] Imeis { get; set; }

        /// <summary>
        /// request start date
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// request end date
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}

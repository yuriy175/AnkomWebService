using Ankor.Personal.WebService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ankor.Personal.WebService.ViewModels.EventsViewModel;

namespace Ankor.Personal.WebService.Model.Demos
{
    /// <summary>
    /// events repository for demo mode
    /// </summary>
    public class DemoEventsRepository
    {
        /*
        RESPONSE: {"Command":"AlarmObject","Data":"\"{\\\"FactoryID\\\":1601629,\\\"IMEI\\\":8121069367279567888,\\\"EventCode\\\":8,\\\"Events\\\":
        [{\\\"StartDT\\\":\\\"2020-06-01T10:30:15.0023679+03:00\\\",\\\"StopDT\\\":null,\\\"Data\\\":\\\"ze7i7uUg8uXx8u7i7uUg8e7h+/Lo5SA4\\\",\\\"Text\\\":\\\"Новое тестовое событие 8\\\"}]}\"","LogDT":"2020-06-01T10:30:15.0023679+03:00","AppKey":"000000000000000000"}

Ошибка выполнения команды

RESPONSE: {"Command":"AlarmObject","Data":"\"{\\\"FactoryID\\\":1601629,\\\"IMEI\\\":8121069367279567888,\\\"EventCode\\\":10,\\\"Events\\\":
[{\\\"StartDT\\\":\\\"2020-06-01T10:30:00.0053718+03:00\\\",\\\"StopDT\\\":null,\\\"Data\\\":\\\"ze7i7uUg8uXx8u7i7uUg8e7h+/Lo5SAxMA==\\\",\\\"Text\\\":\\\"Новое тестовое событие 10\\\"}]}\"","LogDT":"2020-06-01T10:30:00.0053718+03:00","AppKey":"000000000000000000"}

    Ошибка выполнения команды

    RESPONSE: {"Command":"AlarmObject","Data":"\"{\\\"FactoryID\\\":4091659,\\\"IMEI\\\":8121069367279567747,\\\"EventCode\\\":1,\\\"Events\\\":
    [{\\\"StartDT\\\":\\\"2020-06-01T10:29:45.0013675+03:00\\\",\\\"StopDT\\\":null,\\\"Data\\\":\\\"ze7i7uUg8uXx8u7i7uUg8e7h+/Lo5SAx\\\",\\\"Text\\\":\\\"Новое тестовое событие 1\\\"}]}\"","LogDT":"2020-06-01T10:29:45.0013675+03:00","AppKey":"000000000000000000"}

Ошибка выполнения команды

RESPONSE: {"Command":"AlarmObject","Data":"\"{\\\"FactoryID\\\":4091659,\\\"IMEI\\\":8121069367279567747,\\\"EventCode\\\":7,\\\"Events\\\":
[{\\\"StartDT\\\":\\\"2020-06-01T10:29:30.001367+03:00\\\",\\\"StopDT\\\":null,\\\"Data\\\":\\\"ze7i7uUg8uXx8u7i7uUg8e7h+/Lo5SA3\\\",\\\"Text\\\":\\\"Новое тестовое событие 7\\\"}]}\"","LogDT":"2020-06-01T10:29:30.001367+03:00","AppKey":"000000000000000000"}

Ошибка выполнения команды

RESPONSE: {"Command":"AlarmObject","Data":"\"{\\\"FactoryID\\\":1601629,\\\"IMEI\\\":8121069367279567888,\\\"EventCode\\\":4,\\\"Events\\\":
[{\\\"StartDT\\\":\\\"2020-06-01T10:29:15.0013659+03:00\\\",\\\"StopDT\\\":null,\\\"Data\\\":\\\"ze7i7uUg8uXx8u7i7uUg8e7h+/Lo5SA0\\\",\\\"Text\\\":\\\"Новое тестовое событие 4\\\"}]}\"","LogDT":"2020-06-01T10:29:15.0013659+03:00","AppKey":"000000000000000000"}

Ошибка выполнения команды

RESPONSE: {"Command":"AlarmObject","Data":"\"{\\\"FactoryID\\\":1601629,\\\"IMEI\\\":8121069367279567888,\\\"EventCode\\\":6,\\\"Events\\\":
[{\\\"StartDT\\\":\\\"2020-06-01T10:29:00.0013673+03:00\\\",\\\"StopDT\\\":null,\\\"Data\\\":\\\"ze7i7uUg8uXx8u7i7uUg8e7h+/Lo5SA2\\\",\\\"Text\\\":\\\"Новое тестовое событие 6\\\"}]}\"","LogDT":"2020-06-01T10:29:00.0013673+03:00","AppKey":"000000000000000000"}
*/
        private readonly IEnumerable<EventsViewModel> _events = new List<EventsViewModel>
        {
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 8,
                LogDT = DateTime.Parse("06.01.2020 10:30:15"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 8,
                    StartDT = DateTime.Parse("06.01.2020 10:30:15"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 8",
                        IsNotified = false,
                    }
                }
            },
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 10,
                LogDT = DateTime.Parse("06.01.2020 10:30:00"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 10,
                    StartDT = DateTime.Parse("06.01.2020 10:30:00"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 10",
                        IsNotified = true,
                    }
                }
            },
            new EventsViewModel{
                FactoryID = 4091659,
                IMEI  = 8121069367279567747,
                EventCode = 1,
                LogDT = DateTime.Parse("06.01.2020 10:29:45"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 1,
                    StartDT = DateTime.Parse("06.01.2020 10:29:45"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 1",
                        IsNotified = false,
                    }
                }
            },

            new EventsViewModel{
                FactoryID = 4091659,
                IMEI  = 8121069367279567747,
                EventCode = 6,
                LogDT = DateTime.Parse("06.01.2020 10:29:30"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 6,
                    StartDT = DateTime.Parse("06.01.2020 10:29:30"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 6",
                        IsNotified = true,
                    }
                }
            },
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 4,
                LogDT = DateTime.Parse("06.01.2020 10:29:15"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 4,
                    StartDT = DateTime.Parse("06.01.2020 10:29:15"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 4",
                        IsNotified = false,
                    }
                }
            },
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 7,
                LogDT = DateTime.Parse("06.01.2020 10:29:00"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 7,
                    StartDT = DateTime.Parse("06.01.2020 10:29:00"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 7",
                        IsNotified = false,
                    }
                }
            },
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 71,
                LogDT = DateTime.Parse("06.01.2020 10:29:00"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 71,
                    StartDT = DateTime.Parse("06.01.2020 10:29:00"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 71",
                        IsNotified = false,
                    }
                }
            },
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 72,
                LogDT = DateTime.Parse("06.01.2020 10:29:00"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 72,
                    StartDT = DateTime.Parse("06.01.2020 10:29:00"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 72",
                        IsNotified = false,
                    }
                }
            },
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 73,
                LogDT = DateTime.Parse("06.01.2020 10:29:00"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 73,
                    StartDT = DateTime.Parse("06.01.2020 10:29:00"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 73",
                        IsNotified = false,
                    }
                }
            }
            ,
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 74,
                LogDT = DateTime.Parse("06.01.2020 10:29:00"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 74,
                    StartDT = DateTime.Parse("06.01.2020 10:29:00"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 74",
                        IsNotified = false,
                    }
                }
            },
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 75,
                LogDT = DateTime.Parse("06.01.2020 10:29:00"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 75,
                    StartDT = DateTime.Parse("06.01.2020 10:29:00"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 75",
                        IsNotified = false,
                    }
                }
            },
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 76,
                LogDT = DateTime.Parse("06.01.2020 10:29:00"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 76,
                    StartDT = DateTime.Parse("06.01.2020 10:29:00"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 76",
                        IsNotified = false,
                    }
                }
            },
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 77,
                LogDT = DateTime.Parse("06.01.2020 10:29:00"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 77,
                    StartDT = DateTime.Parse("06.01.2020 10:29:00"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 77",
                        IsNotified = false,
                    }
                }
            },
            new EventsViewModel{
                FactoryID = 1601629,
                IMEI  = 8121069367279567888,
                EventCode = 78,
                LogDT = DateTime.Parse("06.01.2020 10:29:00"),
                AppKey = "000000000000000000",
                Events = new[]
                {
                    new EventLogViewModel
                    {
                        Id = 78,
                    StartDT = DateTime.Parse("06.01.2020 10:29:00"),
                    StopDT = null,
                    Data = new byte[]{ },
                        Text = "Новое тестовое событие 78",
                        IsNotified = false,
                    }
                }
            },
        };

        public IEnumerable<EventsViewModel> Events => _events;

        /// <summary>
        /// sets a user events read
        /// </summary>
        /// <param name="readIds">events ids</param>
        /// <returns>result</returns>
        public async Task<bool> SetEventsReadAsync(int[] readIds)
        {
            _events.ToList()
                .ForEach(e =>
                {
                    var evLog = e.Events.FirstOrDefault();
                    if (evLog != null && evLog.Id.HasValue && readIds.Contains((int)evLog.Id.Value))
                    {
                        evLog.IsNotified = true;
                    }
                });

            return true;
        }

        /// <summary>
        /// gets user new events count
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>new events count</returns>
        public async Task<int> GetNewEventsCountAsync(int userId)
        {
            return _events.SelectMany(e => e.Events).Count(e => !e.IsNotified);
        }
    }
}

using EventAgency.Core.Models;
using EventAgency.Core.Services;

namespace EventAgency.Tests
{
    /// <summary> </summary>
    [TestClass]
    public class EventBookingServiceTests
    {
        private IEventBookingService NewService() => new EventBookingService();

        [TestMethod]
        public void RegisterEvent_ValidEvent_AppearsInList()
        {
            var service = NewService();
            service.RegisterEvent(new AgencyEvent("ev1", "Конференция DotNet Day", DateTime.Today.AddDays(10), 100));

            var all = service.GetAllEvents();

            Assert.AreEqual(1, all.Count);
            Assert.AreEqual("ev1", all[0].Id);
        }

        [TestMethod]
        public void GetAllEvents_ReturnsAllRegisteredEvents()
        {
            var service = NewService();
            service.RegisterEvent(new AgencyEvent("ev1", "Свадьба Ивановых", DateTime.Today.AddDays(30), 80));
            service.RegisterEvent(new AgencyEvent("ev2", "Корпоратив ООО Ромашка", DateTime.Today.AddDays(5), 50));

            var all = service.GetAllEvents();

            Assert.AreEqual(2, all.Count);
        }

        [TestMethod]
        public void BookSeats_AvailableEvent_ReturnsBooking()
        {
            var service = NewService();
            service.RegisterEvent(new AgencyEvent("ev1", "Выпускной", DateTime.Today.AddDays(20), 100));

            var booking = service.BookSeats("ev1", "Анна Смирнова", 5);

            Assert.AreEqual("Анна Смирнова", booking.ClientName);
            Assert.AreEqual(5, booking.GuestCount);
        }

        [TestMethod]
        public void GetAvailableSeats_AfterBooking_DecreasesCorrectly()
        {
            var service = NewService();
            service.RegisterEvent(new AgencyEvent("ev1", "Форум", DateTime.Today.AddDays(15), 100));

            service.BookSeats("ev1", "Пётр", 30);
            service.BookSeats("ev1", "Ольга", 20);

            Assert.AreEqual(50, service.GetAvailableSeats("ev1"));
        }

        [TestMethod]
        public void GetBookingsForEvent_ReturnsOnlyThatEventBookings()
        {
            var service = NewService();
            service.RegisterEvent(new AgencyEvent("ev1", "Митап", DateTime.Today.AddDays(7), 40));
            service.RegisterEvent(new AgencyEvent("ev2", "Хакатон", DateTime.Today.AddDays(8), 40));
            service.BookSeats("ev1", "Иван", 2);
            service.BookSeats("ev2", "Мария", 3);

            var list = service.GetBookingsForEvent("ev1");

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Иван", list[0].ClientName);
        }

        [TestMethod]
        public void CancelBooking_ActiveBooking_FreesSeats()
        {
            var service = NewService();
            service.RegisterEvent(new AgencyEvent("ev1", "Презентация", DateTime.Today.AddDays(3), 20));
            var booking = service.BookSeats("ev1", "Сергей", 10);

            service.CancelBooking(booking.Id);

            Assert.AreEqual(20, service.GetAvailableSeats("ev1"));
        }
    }
}

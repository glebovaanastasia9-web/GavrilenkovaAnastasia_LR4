using EventAgency.Core.Exceptions;
using EventAgency.Core.Models;
using EventAgency.Core.Services;

namespace EventAgency.Tests
{
    /// <summary> </summary>
    [TestClass]
    public class EventBookingServiceExceptionTests
    {
        private IEventBookingService NewService() => new EventBookingService();

        [TestMethod]
        public void RegisterEvent_DuplicateId_ThrowsInvalidEventDataException()
        {
            var service = NewService();
            service.RegisterEvent(new AgencyEvent("ev1", "Открытие магазина", DateTime.Today, 10));

            Assert.ThrowsException<InvalidEventDataException>(() =>
                service.RegisterEvent(new AgencyEvent("ev1", "Дубликат", DateTime.Today, 10)));
        }

        [TestMethod]
        public void RegisterEvent_EmptyTitle_ThrowsInvalidEventDataException()
        {
            var service = NewService();

            Assert.ThrowsException<InvalidEventDataException>(() =>
                service.RegisterEvent(new AgencyEvent("ev1", "", DateTime.Today, 10)));
        }

        [TestMethod]
        public void GetEventById_UnknownId_ThrowsEventNotFoundException()
        {
            var service = NewService();

            Assert.ThrowsException<EventNotFoundException>(() => service.GetEventById("no-such-id"));
        }

        [TestMethod]
        public void BookSeats_UnknownEventId_ThrowsEventNotFoundException()
        {
            var service = NewService();

            Assert.ThrowsException<EventNotFoundException>(() => service.BookSeats("no-such-id", "Клиент", 1));
        }

        [TestMethod]
        public void BookSeats_EmptyClientName_ThrowsInvalidBookingDataException()
        {
            var service = NewService();
            service.RegisterEvent(new AgencyEvent("ev1", "Тимбилдинг", DateTime.Today.AddDays(2), 30));

            Assert.ThrowsException<InvalidBookingDataException>(() => service.BookSeats("ev1", "  ", 5));
        }

        [TestMethod]
        public void BookSeats_MoreGuestsThanAvailable_ThrowsNotEnoughSeatsException()
        {
            var service = NewService();
            service.RegisterEvent(new AgencyEvent("ev1", "Кинопоказ", DateTime.Today.AddDays(1), 10));
            service.BookSeats("ev1", "Первый клиент", 8);

            Assert.ThrowsException<NotEnoughSeatsException>(() => service.BookSeats("ev1", "Второй клиент", 5));
        }

        [TestMethod]
        public void BookSeats_CancelledEvent_ThrowsEventCancelledException()
        {
            var service = NewService();
            var agencyEvent = new AgencyEvent("ev1", "Отменённый концерт", DateTime.Today.AddDays(4), 50)
            {
                IsCancelled = true
            };
            service.RegisterEvent(agencyEvent);

            Assert.ThrowsException<EventCancelledException>(() => service.BookSeats("ev1", "Клиент", 2));
        }

        [TestMethod]
        public void CancelBooking_UnknownId_ThrowsBookingNotFoundException()
        {
            var service = NewService();

            Assert.ThrowsException<BookingNotFoundException>(() => service.CancelBooking("no-such-booking"));
        }

        [TestMethod]
        public void CancelBooking_AlreadyCancelled_ThrowsBookingAlreadyCancelledException()
        {
            var service = NewService();
            service.RegisterEvent(new AgencyEvent("ev1", "Дегустация", DateTime.Today.AddDays(6), 15));
            var booking = service.BookSeats("ev1", "Клиент", 2);
            service.CancelBooking(booking.Id);

            Assert.ThrowsException<BookingAlreadyCancelledException>(() => service.CancelBooking(booking.Id));
        }

        /// <summary>
        /// Явная демонстрация блока try-catch-finally: исключение
        /// перехватывается в catch, а finally выполняется в любом случае
        /// (в том числе при выброшенном исключении), что и проверяется
        /// флагом finallyExecuted.
        /// </summary>
        [TestMethod]
        public void BookSeats_TryCatchFinally_Demonstration()
        {
            var service = NewService();
            service.RegisterEvent(new AgencyEvent("ev1", "Мастер-класс", DateTime.Today.AddDays(9), 5));
            bool finallyExecuted = false;
            bool exceptionCaught = false;

            try
            {
                service.BookSeats("ev1", "Клиент", 100); // заведомо больше вместимости
            }
            catch (NotEnoughSeatsException)
            {
                exceptionCaught = true;
            }
            finally
            {
                finallyExecuted = true;
            }

            Assert.IsTrue(exceptionCaught, "исключение NotEnoughSeatsException должно быть перехвачено в catch");
            Assert.IsTrue(finallyExecuted, "блок finally должен выполниться в любом случае");
            Assert.AreEqual(5, service.GetAvailableSeats("ev1"));
        }
    }
}

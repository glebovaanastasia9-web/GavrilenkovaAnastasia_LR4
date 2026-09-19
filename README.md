# EventAgency

Библиотека классов для предметной области эвент-агентства (организация
мероприятий): регистрация мероприятий, бронирование мест на них клиентами,
отмена бронирований. Решение состоит из двух проектов и демонстрирует
интеграцию модулей через интерфейсы, а также обработку исключительных
ситуаций собственными классами исключений.

## Структура решения

```
EventAgency.sln
├── EventAgency.Core/            # библиотека классов (Class Library, .NET 8.0)
│   ├── Models/
│   │   ├── AgencyEvent.cs       # мероприятие: Id, Title, EventDate, Capacity, IsCancelled
│   │   └── Booking.cs           # бронирование: Id, EventId, ClientName, GuestCount, IsCancelled
│   ├── Services/
│   │   ├── IEventBookingService.cs   # контракт сервиса
│   │   └── EventBookingService.cs    # реализация (хранение в памяти)
│   └── Exceptions/
│       ├── EventAgencyException.cs           # общий абстрактный предок
│       ├── EventNotFoundException.cs
│       ├── InvalidEventDataException.cs
│       ├── InvalidBookingDataException.cs
│       ├── NotEnoughSeatsException.cs
│       ├── BookingNotFoundException.cs
│       ├── BookingAlreadyCancelledException.cs
│       └── EventCancelledException.cs
└── EventAgency.Tests/            # интегрирующий проект (MSTest Unit Test Project)
    ├── EventBookingServiceTests.cs           # штатные сценарии (6 тестов)
    └── EventBookingServiceExceptionTests.cs  # исключительные ситуации + try-catch-finally (10 тестов)
```

## Модули и их назначение

- **Models** – сущности предметной области, не зависят от других модулей.
- **Services** – интерфейс `IEventBookingService` и его реализация `EventBookingService`; при нарушении входных условий выбрасывает собственные исключения из `Exceptions`.
- **Exceptions** – иерархия из семи классов с общим абстрактным предком `EventAgencyException : Exception`, что позволяет перехватывать все ошибки библиотеки одним `catch (EventAgencyException)`.
- **EventAgency.Tests** – интегрирующий проект: подключается к `EventAgency.Core` через ссылку на проект и обращается к сервису только через интерфейс `IEventBookingService`, не зная деталей его реализации.

## Сборка и запуск тестов

1. Открыть `EventAgency.sln` в Visual Studio 2022 (или новее), дождаться восстановления NuGet-пакетов.
2. **Сборка → Пересобрать решение**.
3. Открыть **Тест → Обозреватель тестов** (Test → Test Explorer) и нажать **Запустить все тесты** (Run All Tests).
4. Ожидаемый результат – 16 из 16 тестов пройдены (Passed).

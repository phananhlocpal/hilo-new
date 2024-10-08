<<<<<<< HEAD
﻿using MessageBrokerService;
using RabbitMQ.Client.Events;
=======
﻿using RabbitMQ.Client.Events;
>>>>>>> 960a83c (commit)
using ScheduleService.Models;
using ScheduleService.Repositories.ScheduleRepository;
using System.Text;
using System.Text.Json;
<<<<<<< HEAD
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScheduleService.Helper;

namespace ScheduleService.Service.MessagerBrokerServices
{
    public class ScheduleConsumerService : BaseMessageBroker, IHostedService
    {
        private readonly ILogger<ScheduleConsumerService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private const string QueueName = "sale_schedule";

        public ScheduleConsumerService(
            ILogger<ScheduleConsumerService> logger,
            IServiceScopeFactory scopeFactory) : base(logger)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;

            DeclareQueue(QueueName);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting to consume messages from queue '{QueueName}'.", QueueName);
            ConsumeMessage(QueueName, HandleMessage);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping the message consumer for queue '{QueueName}'.", QueueName);
            Dispose();
            return Task.CompletedTask;
=======

namespace ScheduleService.Service.MessagerBrokerServices
{
    public class ScheduleConsumerService : BaseMessageService
    {
        private readonly IScheduleRepo _repository; 
        private const string QueueName = "invoice_schedule";

        public ScheduleConsumerService(
            ILogger<ScheduleConsumerService> logger,
            IScheduleRepo scheduleRepo) : base(logger)
        {
            _repository = scheduleRepo;
            DeclareQueue(QueueName);
            ConsumeMessage(QueueName, HandleMessage);
>>>>>>> 960a83c (commit)
        }

        private async void HandleMessage(object sender, BasicDeliverEventArgs e)
        {
<<<<<<< HEAD
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IScheduleRepo>();

            try
            {
                _logger.LogInformation("Message received from queue '{QueueName}'.", QueueName);

                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation("Message body: {Message}", message);

                // Replace Unicode escape sequences with normal characters
                var formattedMessage = System.Text.RegularExpressions.Regex.Unescape(message);

                _logger.LogInformation("Formatted message body: {FormattedMessage}", formattedMessage);

                // Deserialize the message
                Schedule scheduleUpdateMessage;
                var options = new JsonSerializerOptions
                {
                    Converters = { new DateOnlyJsonConverter(), new TimeOnlyJsonConverter() },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Use this if you need camelCase conversion
                };

                try
                {
                    scheduleUpdateMessage = JsonSerializer.Deserialize<Schedule>(formattedMessage, options);
                    _logger.LogInformation("Data after JSON deserializer: {ScheduleUpdateMessage}", scheduleUpdateMessage);
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError(jsonEx, "Error deserializing schedule message. Message: {FormattedMessage}", formattedMessage);
                    NacknowledgeMessage(e.DeliveryTag); // Optionally nack the message
                    return;
                }
=======
            try
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Deserialize the message
                var scheduleUpdateMessage = JsonSerializer.Deserialize<IEnumerable<Schedule>>(message);
>>>>>>> 960a83c (commit)

                if (scheduleUpdateMessage != null)
                {
                    // Process the schedule update
<<<<<<< HEAD
                    await ProcessScheduleUpdate(scheduleUpdateMessage, repository);

                    // Acknowledge the message
                    AcknowledgeMessage(e.DeliveryTag);
                    _logger.LogInformation("Message acknowledged.");
                }
                else
                {
                    _logger.LogWarning("Received an invalid schedule update message.");
                    NacknowledgeMessage(e.DeliveryTag); // Optionally nack the message
=======
                    await ProcessScheduleUpdate(scheduleUpdateMessage);
                }
                else
                {
                    // Handle invalid message
                    _logger.LogWarning("Received an invalid schedule update message.");
>>>>>>> 960a83c (commit)
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing schedule update message.");
<<<<<<< HEAD
                NacknowledgeMessage(e.DeliveryTag); // Optionally nack the message
            }
        }


        private async Task ProcessScheduleUpdate(Schedule schedule, IScheduleRepo scheduleRepo)
        {
            _logger.LogInformation("Processing schedule update for schedule with seatId {SeatId}.", schedule.SeatId);
            await scheduleRepo.UpdateScheduleAsync(schedule);
            _logger.LogInformation("Schedule with seatId {SeatId} processed successfully.", schedule.SeatId);
=======
            }
        }

        private async Task ProcessScheduleUpdate(IEnumerable<Schedule> Schedules)
        {
            foreach (Schedule schedule in Schedules)
            {
                _repository.UpdateScheduleAsync(schedule);
            }
>>>>>>> 960a83c (commit)
        }
    }
}

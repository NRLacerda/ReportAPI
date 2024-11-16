﻿using RabbitMQ.Client;
using ReportAPI.Interfaces;
using ReportAPI.Models;
using System.Text;
using System.Text.Json;


namespace ReportAPI.Services
{
    public class RabbitService : IRabbitService
    {
        private IConnection connection;
        private IChannel channel;

        private async Task InstantiateRabbit()
        {
            var factory = new ConnectionFactory 
            { 
                HostName = "localhost" ,    
                UserName = "guest",
                Password = "guest"
            };

            connection = await factory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();
        }

        /// <summary>
        /// Sends a message to the specified RabbitMQ queue.
        /// </summary>
        /// <param name="queueName">Name of the queue to send the message to.</param>
        /// <param name="reportReq">The message to send.</param>
        public async Task SendMessageAsync(string queueName, ReportModel reportReq)
        {
            try
            {
                await InstantiateRabbit();

                await channel.QueueDeclareAsync(queue: "reports", durable: false, exclusive: false, autoDelete: false,
                    arguments: null);

                var json = JsonSerializer.Serialize(reportReq);
                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);

                Console.WriteLine($"[x] Sent {reportReq} to queue: {queueName}");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Error while sending message: {ex.Message}");
                throw;
            }
        }
    }
}
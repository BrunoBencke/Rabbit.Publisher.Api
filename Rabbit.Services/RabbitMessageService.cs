using Rabbit.Models.Entities;
using Rabbit.Repositories.Interfaces;
using Rabbit.Services.Interfaces;

namespace Rabbit.Services
{
    public class RabbitMessageService : IRabbitMessageService
    {
        private readonly IRabbitMessageRepository _repostiory;

        public RabbitMessageService(IRabbitMessageRepository repository) {
            _repostiory = repository;
        }

        public void SendMessage(RabbitMessage message)
        {
            _repostiory.SendMessage(message);
        }
    }
}

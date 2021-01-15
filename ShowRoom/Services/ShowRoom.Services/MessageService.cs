using ShowRoom.Services.Interfaces;

namespace ShowRoom.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}

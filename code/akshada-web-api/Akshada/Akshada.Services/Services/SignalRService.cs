using Akshada.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class SignalRService : ISignalRService
    {
        private readonly IHubContext<HubMessage> hubContext;

        public SignalRService(IHubContext<HubMessage> hubContext) {
            this.hubContext = hubContext;
        }
        public async Task SendMessageAsync(string message)
        {
            await this.hubContext.Clients.All.SendAsync("Message", message);
        }
    }

    public class HubMessage:Hub
    {

    }
}

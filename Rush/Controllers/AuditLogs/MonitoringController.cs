using Microsoft.AspNetCore.Mvc;
using Rush.Application.Interfaces.AuditLogs;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Rush.WebAPI.Controllers.AuditLogs
{
    [ApiExplorerSettings(IgnoreApi = true)] 

    public class MonitoringController(IAuditLogService service) : ControllerBase
    {
        private readonly IAuditLogService _service = service;

        [Route("/ws")]
        public async Task GetLogs()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                while (webSocket.State == WebSocketState.Open)
                {
                    var data = await _service.GetAllAsync();
                    var json = JsonSerializer.Serialize(data);

                    await webSocket.SendAsync(
                        Encoding.UTF8.GetBytes(json),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None
                    );

                    await Task.Delay(5000);
                }
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

    }
}

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

        [Route("/getLogs")]
        public async Task GetLogs(int level = 0, int httpMethod = 0, int offset = 0, int pageSize = 10)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                while (webSocket.State == WebSocketState.Open)
                {
                    var data = await _service.GetAuditLogs(level, httpMethod, offset, pageSize);
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

        [Route("/getCountLogs")]
        public async Task GetCountLogs(int level = 0, int httpMethod = 0)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                while (webSocket.State == WebSocketState.Open)
                {
                    var data = await _service.GetCountLogs(level, httpMethod);
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

        [Route("/getEntitiesLogs")]
        public async Task GetEntitiesLogs()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                while (webSocket.State == WebSocketState.Open)
                {
                    var data = await _service.GetAuditEntities();
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

        [Route("/getLogsCount")]
        public async Task GetLogsCount()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                while (webSocket.State == WebSocketState.Open)
                {
                    var data = await _service.GetAuditLogsCount();
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

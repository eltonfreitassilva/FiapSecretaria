using MediatR;
using System.Text.Json.Serialization;

namespace FIAP.Secretaria.Shared.Common.Messages;

public abstract class Event : Message, INotification
{
    [JsonIgnore]
    public DateTime Timestamp { get; protected set; }

    protected Event()
    {
        Timestamp = DateTime.Now;
    }
}

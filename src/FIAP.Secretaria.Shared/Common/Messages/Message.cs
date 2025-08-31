using System.Text.Json.Serialization;

namespace FIAP.Secretaria.Shared.Common.Messages;

public abstract class Message
{
    [JsonIgnore]
    public string MessageGuid { get; protected set; }
    [JsonIgnore]
    public string MessageType { get; protected set; }
    [JsonIgnore]
    public int AggregateId { get; protected set; }

    protected Message()
    {
        MessageGuid = Guid.NewGuid().ToString();
        MessageType = GetType().Name;
    }

    public void SetAggregateId(int aggregateId) => AggregateId = aggregateId;
}

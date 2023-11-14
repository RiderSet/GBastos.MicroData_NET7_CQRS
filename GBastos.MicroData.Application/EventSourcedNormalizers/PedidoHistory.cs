using GBastos.MicroData.Domain.Core.Events;
using System.Text.Json;

namespace GBastos.MicroData.Application.EventSourcedNormalizers
{
    public static class PedidoHistory
    {
        public static IList<PedidoHistoryData> HistoryData { get; set; }

        public static IList<PedidoHistoryData> ToJavaScriptPedidoHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<PedidoHistoryData>();
            PedidoHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.Timestamp);
            var list = new List<PedidoHistoryData>();
            var last = new PedidoHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new PedidoHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email
                        ? ""
                        : change.Email,
                    BirthDate = string.IsNullOrWhiteSpace(change.BirthDate) || change.BirthDate == last.BirthDate
                        ? ""
                        : change.BirthDate.Substring(0,10),
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    Timestamp = change.Timestamp,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void PedidoHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var historyData = JsonSerializer.Deserialize<PedidoHistoryData>(e.Data);
                historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (e.MessageType)
                {
                    case "PedidoRegisteredEvent":
                        historyData.Action = "Registered";
                        historyData.Who = e.User;
                        break;
                    case "PedidoUpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.Who = e.User;
                        break;
                    case "PedidoRemovedEvent":
                        historyData.Action = "Removed";
                        historyData.Who = e.User;
                        break;
                    default:
                        historyData.Action = "Unrecognized";
                        historyData.Who = e.User ?? "Anonymous";
                        break;

                }
                HistoryData.Add(historyData);
            }
        }
    }
}
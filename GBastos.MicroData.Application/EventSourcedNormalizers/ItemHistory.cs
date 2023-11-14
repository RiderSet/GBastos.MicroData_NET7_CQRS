using GBastos.MicroData.Domain.Core.Events;
using System.Text.Json;

namespace GBastos.MicroData.Application.EventSourcedNormalizers
{
    public static class ItemHistory
    {
        public static IList<ItemHistoryData> HistoryData { get; set; }

        public static IList<ItemHistoryData> ToJavaScriptItemHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<ItemHistoryData>();
            ItemHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.Timestamp);
            var list = new List<ItemHistoryData>();
            var last = new ItemHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new ItemHistoryData
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

        private static void ItemHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var historyData = JsonSerializer.Deserialize<ItemHistoryData>(e.Data);
                historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (e.MessageType)
                {
                    case "ItemRegisteredEvent":
                        historyData.Action = "Registered";
                        historyData.Who = e.User;
                        break;
                    case "ItemUpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.Who = e.User;
                        break;
                    case "ItemRemovedEvent":
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
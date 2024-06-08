using Autoglass.Domain.Models;
using Dufry.Domain.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Autoglass.Infra.Data.LogManager
{
    public class AuditEntry
    {
        public EntityEntry Entry { get; }
        public int? UserId { get; set; }
        public string TableName { get; set; }
        public LogAuditType LogType { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<string> ChangedColumns { get; } = new List<string>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public AuditEntry(EntityEntry entry, int? userId)
        {
            Entry = entry;
            UserId = userId;
        }

        public LogAudit ToAudit()
        {
            var audit = new LogAudit(UserId, TableName, 
                                     JsonConvert.SerializeObject(KeyValues),
                                     OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues), 
                                     NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
                                     ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns));
            
            return audit;
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TypeLibrary.Models.Const;
using TypeLibrary.Models.Data;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Application
{
    public class EventLogAm
    {
        [Required]
        public string ProjectId { get; set; }

        [Required]
        public string DataId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public EventLogDataType EventLogDataType { get; set; }

        [Required]
        public WorkerStatus WorkerStatus { get; set; }

        public EventLogAm()
        {

        }

        public EventLogAm(Node node)
        {
            DataId = node?.Id;
            DateTime = DateTime.Now.ToUniversalTime();
            Data = node != null ? JsonConvert.SerializeObject(node, DefaultSettings.SerializerSettings) : null;
            EventLogDataType = EventLogDataType.Node;
        }

        public EventLogAm(Edge edge)
        {
            DataId = edge?.Id;
            DateTime = DateTime.Now.ToUniversalTime();
            Data = edge != null ? JsonConvert.SerializeObject(edge, DefaultSettings.SerializerSettings) : null;
            EventLogDataType = EventLogDataType.Edge;
        }
    }
}

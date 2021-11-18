using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Previgesst.Ressources;

namespace Previgesst.Models
{
    [Table("__LogEntries")]
    public class LogEntry
    {
        [Key]
        public int LogEntryId { get; set; }
        public string CallSite { get; set; }

        [Display(ResourceType = typeof(LogEntriesRES), Name = "Date")]
        public DateTime? Date { get; set; }

        [Display(ResourceType = typeof(LogEntriesRES), Name = "Exception")]
        public string Exception { get; set; }

        [MaxLength(50)]
        [Display(ResourceType = typeof(LogEntriesRES), Name = "Level")]
        public string Level { get; set; }

        [MaxLength(255)]
        [Display(ResourceType = typeof(LogEntriesRES), Name = "Logger")]
        public string Logger { get; set; }

        [Display(ResourceType = typeof(LogEntriesRES), Name = "MachineName")]
        public string MachineName { get; set; }

        [Display(ResourceType = typeof(LogEntriesRES), Name = "Message")]
        public string Message { get; set; }


        [Display(ResourceType = typeof(LogEntriesRES), Name = "StackTrace")]
        public string StackTrace { get; set; }

        public string Thread { get; set; }
        [MaxLength(255)]
        public string Username { get; set; }
    }
}
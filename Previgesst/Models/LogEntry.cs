using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    [Table("__LogEntries")]
    public class LogEntry
    {
        [Key]
        public int LogEntryId { get; set; }
        public string CallSite { get; set; }
        public DateTime? Date { get; set; }
        public string Exception { get; set; }
        [MaxLength(50)]
        public string Level { get; set; }
        [MaxLength(255)]
        public string Logger { get; set; }
        public string MachineName { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Thread { get; set; }
        [MaxLength(255)]
        public string Username { get; set; }
    }
}
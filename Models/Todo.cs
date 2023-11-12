using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Models
{
    public class Todo
    {
        public required string Message { get; set; }
        public required string FileName { get; set; }
        public required string FilePath { get; set; }
    }
}
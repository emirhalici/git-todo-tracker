using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Models
{
    public class RefreshToken
    {
        [Key]
        public required string Token { get; set; }
        [ForeignKey(nameof(UserId))]
        public required string UserId { get; set; }
        public required DateTime ExpiresAt { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HydroponicAppServer.Models
{
    public class SensorData
    {
        [Key]
        public int Id { get; set; } // Khóa chính tự tăng

        [Required]
        [StringLength(12)]
        public string UserId { get; set; } // Khóa ngoại

        public double? Temperature { get; set; }

        public double? Humidity { get; set; }

        public double? WaterLevel { get; set; }

        public DateTime? Time { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
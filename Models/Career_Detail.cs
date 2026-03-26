using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duffl_career.Models
{
    public class Career_Detail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^[0-9]{10}$",
            ErrorMessage = "Mobile number must be exactly 10 digits.")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please select Fresher or Experienced.")]
        public string ExperienceLevel { get; set; }

        // Only shows when Experienced
        public string? CurrentlyEmployed { get; set; }   // "Y" or "N"

        //  Decimal — accepts 1, 1.5, 2.5 — no strings
        [Range(0.0, 50.0, ErrorMessage = "Work experience must be between 0 and 50 years.")]
        public decimal? WorkExperience { get; set; }

        //  Only shows when CurrentlyEmployed = Y
        public string? CurrentEmployerName { get; set; }
        public string? CurrentDrawnSalary { get; set; }
        public string? NoticePeriod { get; set; }

        //  Only shows when CurrentlyEmployed = N
        public string? LastEmployerName { get; set; }
        public string? LastDrawnSalary { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Expected salary must be a number.")]
        public int? ExpectedSalary { get; set; }

        public string? CvFilePath { get; set; }
        public string AIResult { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
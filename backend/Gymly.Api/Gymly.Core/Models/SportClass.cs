using Gymly.Core.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gymly.Core.Models;

public class SportClass : BaseEntity
{
    [CustomColumn("class_name")]
    public string Name { get; set; }

    [CustomColumn("date")]
    public DateTime Date { get; set; }

    [CustomColumn("instructor_name")]
    public string InstructorName { get; set; }

    [CustomColumn("price")]
    public decimal Price { get; set; }
}

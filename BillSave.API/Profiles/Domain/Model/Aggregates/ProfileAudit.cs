using System.ComponentModel.DataAnnotations.Schema;

namespace BillSave.API.Profiles.Domain.Model.Aggregates;

public partial class Profile
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace BillSave.API.Portfolio.Domain.Model.Aggregates;

public partial class Portfolio
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}
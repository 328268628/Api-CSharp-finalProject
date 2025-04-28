using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entits;

namespace DTO
{
    public record OrderDTO(int Id, DateTime? OrderDate, double? OrderSum, ICollection<OrderItemDTO> OrderItems);
    public record OrderPostDTO(int UserId, DateTime? OrderDate, double? OrderSum, ICollection<OrderItemDTO> OrderItems);
}

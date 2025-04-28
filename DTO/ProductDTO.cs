using System.Diagnostics;
using System.Xml.Linq;
namespace DTO
{
    public record ProductDTO(int Id, string Name, double? Price, string? CategoryName , string? Description, string? Image);
   
}

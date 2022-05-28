using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Role
{
    public class CreateRole
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }
        public List<int> Permissions { get; set; } = new();
    }
}
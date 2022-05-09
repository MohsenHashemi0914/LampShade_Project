using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace CommentManagement.Application.Contracts.Comment
{
    public class AddComment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Message { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public long OwnerRecordId { get; set; }

        public byte Type { get; set; }
        public long? ParentId { get; set; }
    }
}

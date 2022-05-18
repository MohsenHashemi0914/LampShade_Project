using _0_Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : BaseEntity<long>
    {
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? WebSite { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public long OwnerRecordId { get; private set; }
        public byte Type { get; private set; }
        public long? ParentId { get; private set; }
        public Comment Parent { get; private set; }
        
        protected Comment()
        {
        }

        public Comment(string name, string email, string webSite, 
            string message, long ownerRecordId, byte type, long? parentId)
        {
            Name = name;
            Email = email;
            WebSite = webSite;
            Message = message;
            OwnerRecordId = ownerRecordId;
            Type = type;
            ParentId = parentId;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

        public void Confirm()
        {
            IsCanceled = false;
            IsConfirmed = true;
        }
    }
}

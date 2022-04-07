using System;

namespace _0_Framework.Domain
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; private set; }
        public DateTime CreationDate { get; private set; }

        public BaseEntity()
        {
            CreationDate = DateTime.Now;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBBS.Core
{
    public interface IEntity
    {
        long Id { get; set; }
    }

    public interface ICreationTimeEntity
    {
        DateTime CreationTime { get; set; }
    }

    public abstract class BaseEntity : IEntity, ICreationTimeEntity
    {
        public long Id { get; set; }

        public DateTime CreationTime { get; set; }


    }
}

using System;

namespace Nextens.Challenger.Model.Interface
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
    }
}

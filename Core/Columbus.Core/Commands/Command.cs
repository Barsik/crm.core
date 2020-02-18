using System;

namespace Columbus.InnerSource.Core.Commands
{
    public abstract class Command : ICommand
    {
        public Guid Id { get; }
        public DateTime CreatedOnUtc { get; }

        protected Command(Guid id)
        {
            Id = id;
            CreatedOnUtc = DateTime.UtcNow;
        }

        protected Command() : this(Guid.NewGuid())
        {
        }
    }
}

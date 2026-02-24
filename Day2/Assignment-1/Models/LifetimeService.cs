using Assignment_1.Interfaces;

namespace Assignment_1.Models
{
    public class LifetimeService : ITransientService, IScopedService, ISingletonService
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}

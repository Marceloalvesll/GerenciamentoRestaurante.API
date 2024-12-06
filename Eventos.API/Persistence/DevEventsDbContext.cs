using Eventos.API.Entities;

namespace Eventos.API.Persistence
{
    public class DevEventsDbContext
    {
       public List<DevEvent> DevEvents { get; set; }

        public DevEventsDbContext() {

            DevEvents = new List<DevEvent>();
        
        }
    }
}

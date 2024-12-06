namespace Eventos.API.Entities
{
    public class DevEvent
    {

        public DevEvent()
        {
            Speakers = new List<DevEventSpeakers> { };
            IsDeleted = false;
        }


        public Guid Id { get; set; }

        public string Title{ get; set; }

        public string Description{ get; set; }

        public DateTime StartDate{ get; set; }

        public DateTime EndDate{ get; set; }


        public List<DevEventSpeakers> Speakers { get; set; }

        public bool IsDeleted { get; set; }

        public void Update(string title, string description, DateTime starDate, DateTime endDate)
        {
            Title = title;
            Description = description;
            StartDate = starDate;
            EndDate = endDate;
        }

        public void Delete()
        { 
        IsDeleted = true;
        }
    }
}

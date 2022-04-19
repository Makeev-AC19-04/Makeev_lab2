namespace Makeev_lab2.Models
{
    public class Service
    {
        public long Id { get; set; }
        public string ServiceName { get; set; } //Было Name
        public int Price { get; set; }
        public long DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public void ChangeDoctor(long NewDoctor)
        {
            DoctorId = NewDoctor;
        }

        public string GetName()
        {
            return ServiceName; 
        }
    }
}

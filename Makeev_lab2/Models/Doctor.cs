namespace Makeev_lab2.Models
{
    public class Doctor
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long SpecialityId { get; set; }
        public Speciality? Speciality { get; set; }

        public void ChangeSpeciality(long NewSpec){
            SpecialityId = NewSpec;
            }

        public string GetName()
        {
            return Name;
        }
    }
}
        //public Speciality Speciality { get; set; }

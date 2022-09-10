using POC.HexagonalKafkaCache.Core.Domain.Enum;

namespace POC.HexagonalKafkaCache.Core.Domain.Entities
{
    public class Client
    {
        public Client(string name, DateTime birthDate, Gender gender)
        {
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
        }

        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }

        internal int GetAge() 
        { 
            int age = DateTime.Now.Year - BirthDate.Year;
            
            if (age <= 0) return 0;

            bool clientHasNotYetHadBirthdayThisYear = BirthDate.Date.AddYears(age) < DateTime.Now.Date;
            
            if (clientHasNotYetHadBirthdayThisYear) age--;

            return age;
        }
    }
}

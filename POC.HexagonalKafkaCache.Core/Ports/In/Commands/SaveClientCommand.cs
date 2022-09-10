using POC.HexagonalKafkaCache.Core.Domain.Enum;

namespace POC.HexagonalKafkaCache.Core.Ports.In.Commands
{
    public class SaveClientCommand
    {
        public SaveClientCommand(string name, DateTime birthDate, Gender gender)
        {
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
        }

        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }
    }
}

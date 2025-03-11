using static Rush.Domain.Common.Util.Enums;

namespace Rush.Domain.DTO.Auth
{
    public class RegisterEmployeeDTO: UserDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Sexo Sexo { get; set; }
        public string Curp { get; set; }
        public string Rfc { get; set; }
        public string Salary { get; set; }
    }
}

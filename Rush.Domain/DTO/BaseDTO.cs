namespace Rush.Domain.DTO
{
    public abstract class BaseDTO
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }

}

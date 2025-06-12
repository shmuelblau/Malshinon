namespace Malshinon
{
    public class RequestDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? SecretCode { get; set; }

        public string? TargetFirstName { get; set; }
        public string? TargetLastName { get; set; }

        public string? Data { get; set; }
    }
}
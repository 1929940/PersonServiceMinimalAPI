namespace PersonServiceMinimalAPI.Models {
    public class PostPersonDto {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual PostAddressDto Address { get; set; }
    }
}

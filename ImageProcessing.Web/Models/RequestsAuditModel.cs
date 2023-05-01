namespace ImageProcessing.Web.Models
{
    public class RequestsAuditModel
    {
        public int Id { get; set; }
        public string? Unique_Session_Key { get; set; }
        public string? Requested_Url { get; set; }
        public string? IpAddress { get; set; }
        public string? Location { get; set; }
        public string? User_Agent { get; set; }
        public DateTime? Requested_DateTime { get; set; }

        public string Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; }
    }
}

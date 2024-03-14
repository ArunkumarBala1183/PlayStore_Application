namespace Playstore.Contracts.Data.Entities
{
    public partial class AppLog
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public string Level { get; set; } = null!;
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; } = null!;
        public Guid? UserId { get; set; }
        public string Location { get; set; } = null!;
    }
}

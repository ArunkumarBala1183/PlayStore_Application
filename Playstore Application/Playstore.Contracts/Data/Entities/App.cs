namespace Playstore.Contracts.Data.Entities
{
    public class App : BaseEntity

    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Developer { get; set; }
        public string Type { get; set; }
    }
}
namespace BoardingHouse
{
    public class BoardingHouse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

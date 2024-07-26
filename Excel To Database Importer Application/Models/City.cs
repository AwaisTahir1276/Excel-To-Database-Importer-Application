namespace Read_ExcelFile.Models
{
    public class City
    {
        public int cityID { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public bool active { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public bool topFive { get; set; }
        public bool stateCapitol { get; set; }
        public virtual ICollection<MetroAreaCity> MetroAreaCities { get; set; }
    }
}

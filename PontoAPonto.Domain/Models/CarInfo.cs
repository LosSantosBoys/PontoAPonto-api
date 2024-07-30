namespace PontoAPonto.Domain.Models
{
    public class CarInfo
    {
        public CarInfo(string model, string year, string plate, string color)
        {
            Model = model;
            Year = year;
            Plate = plate;
            Color = color;
        }

        public string Model { get; private set; }
        public string Year { get; private set; }
        public string Plate { get; private set; }
        public string Color { get; private set; }
    }
}

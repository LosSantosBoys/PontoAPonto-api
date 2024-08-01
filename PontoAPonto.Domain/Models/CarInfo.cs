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

        public void UpdateCarInfo(string? model, string? year, string? plate, string? color)
        {
            if (!string.IsNullOrEmpty(model))
            {
                Model = model;
            }

            if (!string.IsNullOrEmpty(year))
            {
                Year = year;
            }

            if (!string.IsNullOrEmpty(plate))
            {
                Plate = plate;
            }

            if (!string.IsNullOrEmpty(color))
            {
                Color = color;
            }
        }
    }
}

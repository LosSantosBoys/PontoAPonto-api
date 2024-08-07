using Microsoft.AspNetCore.Http;
using PontoAPonto.Domain.Models;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;

namespace PontoAPonto.Domain.Helpers
{
    public class PdfHelper
    {
        public CarInfo ExtractCrlvData(IFormFile pdfFile)
        {
            var data = new CarInfo();

            using (var stream = pdfFile.OpenReadStream())
            using (var document = PdfDocument.Open(stream))
            {
                foreach (var page in document.GetPages())
                {
                    var text = page.Text;

                    var renavamMatch = Regex.Match(text, @"CÓDIGO RENAVAM\s*([\d]+)");
                    var plateMatch = Regex.Match(text, @"PLACA\s*([A-Z]{3}-\d{4})");
                    var fabricationYearMatch = Regex.Match(text, @"ANO DE FABRICAÇÃO\s*(\d{4})");
                    var modelYearMatch = Regex.Match(text, @"ANO MODELO\s*(\d{4})");
                    var brandModelVersionMatch = Regex.Match(text, @"MARCA/MODELO/VERSÃO\s*([A-Za-z\s]+)");
                    var typeMatch = Regex.Match(text, @"ESPÉCIE/TIPO\s*([A-Za-z\s]+)");
                    var colorMatch = Regex.Match(text, @"COR PREDOMINANTE\s*([A-Za-z\s]+)");
                    var locationMatch = Regex.Match(text, @"LOCAL\s*([A-Za-z\s]+)");
                    var ownerCpfCnpjMatch = Regex.Match(text, @"CPF/CNPJ\s*([\d./-]+)");

                    if (renavamMatch.Success)
                    {
                        data.Renavam = renavamMatch.Groups[1].Value;
                    }

                    if (plateMatch.Success)
                    {
                        data.Plate = plateMatch.Groups[1].Value;
                    }

                    if (fabricationYearMatch.Success)
                    {
                        data.FabricationYear = fabricationYearMatch.Groups[1].Value;
                    }

                    if (modelYearMatch.Success)
                    {
                        data.ModelYear = modelYearMatch.Groups[1].Value;
                    }

                    if (brandModelVersionMatch.Success)
                    {
                        data.Brand = brandModelVersionMatch.Groups[1].Value;
                        data.Model = ""; // TODO - Think how to extract these values
                        data.Version = "";
                    }

                    if (typeMatch.Success)
                    {
                        data.Type = typeMatch.Groups[1].Value;
                    }

                    if (colorMatch.Success)
                    {
                        data.Color = colorMatch.Groups[1].Value;
                    }

                    if (locationMatch.Success)
                    {
                        data.Location = locationMatch.Groups[1].Value;
                    }

                    if (ownerCpfCnpjMatch.Success)
                    {
                        data.OwnerCpfCnpj = ownerCpfCnpjMatch.Groups[1].Value;
                    }
                }
            }

            return data;
        }
    }
}
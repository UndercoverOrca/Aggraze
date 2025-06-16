using Aggraze.Domain;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Aggraze.Application;

public interface IFileWriterService
{
    void SaveWorkbook(Workbook workbook, string outputPath);
}
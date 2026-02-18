using ClosedXML.Excel;

namespace Aggraze.Application.Services;

public interface IFileWriterService
{
    void SaveWorkbook(XLWorkbook workbook, string outputPath);
}
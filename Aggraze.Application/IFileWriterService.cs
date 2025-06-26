using ClosedXML.Excel;

namespace Aggraze.Application;

public interface IFileWriterService
{
    void SaveWorkbook(XLWorkbook workbook, string outputPath);
}
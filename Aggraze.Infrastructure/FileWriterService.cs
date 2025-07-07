using Aggraze.Application;
using ClosedXML.Excel;

namespace Aggraze.Infrastructure;

public class FileWriterService : IFileWriterService
{
    public void SaveWorkbook(XLWorkbook workbook, string outputPath) =>
        workbook.SaveAs(outputPath);
}
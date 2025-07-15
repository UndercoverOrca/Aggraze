using Aggraze.Application.Services;
using ClosedXML.Excel;

namespace Aggraze.Infrastructure.Services;

public class FileWriterService : IFileWriterService
{
    public void SaveWorkbook(XLWorkbook workbook, string outputPath) =>
        workbook.SaveAs(outputPath);
}
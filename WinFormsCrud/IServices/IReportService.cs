using CommonLibrary.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsCrud.IServices
{
    public interface IReportService
    {
        ValueTask<List<ReportDto>> GetReport(int managerId);
    }
}

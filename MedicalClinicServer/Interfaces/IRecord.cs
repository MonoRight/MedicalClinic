using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.Interfaces
{
    public interface IRecord
    {
        List<Record> GetRecords();
        Task<Record> GetRecordAsync(Guid id);
        Task<Record> AddRecordAsync(Record record);
        Task DeleteRecordAsync(Record record);
        Task<Record> EditRecordAsync(Record record);
    }
}

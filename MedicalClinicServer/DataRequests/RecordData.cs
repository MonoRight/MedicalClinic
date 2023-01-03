using MedicalClinicServer.Context;
using MedicalClinicServer.Interfaces;
using MedicalClinicServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalClinicServer.DataRequests
{
    public class RecordData : IRecord
    {
        private ClinicContext _clinicContext;
        public RecordData(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }

        public async Task<Record> AddRecordAsync(Record record)
        {
            //record.Id = Guid.NewGuid();
            await _clinicContext.Records.AddAsync(record);
            await _clinicContext.SaveChangesAsync();
            return record;
        }

        public async Task DeleteRecordAsync(Record record)
        {
            _clinicContext.Records.Remove(record);
            await _clinicContext.SaveChangesAsync();
        }

        public async Task<Record> EditRecordAsync(Record record)
        {
            var existingRecord = await _clinicContext.Records.FindAsync(record.Id);

            if (existingRecord != null)
            {
                existingRecord.ClientId = record.ClientId;
                existingRecord.Date = record.Date;
                existingRecord.DoctorId = record.DoctorId;

                _clinicContext.Records.Update(existingRecord);
                await _clinicContext.SaveChangesAsync();
            }
            return record;
        }

        public async Task<Record> GetRecordAsync(int id)
        {
            var record = await _clinicContext.Records.FindAsync(id);
            return record;
        }

        public List<Record> GetRecords()
        {
            return _clinicContext.Records.ToList();
        }
    }
}

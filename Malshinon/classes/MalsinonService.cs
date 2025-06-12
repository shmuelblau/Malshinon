using Malshinon.classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using System.Collections.Generic;

namespace Malshinon
{
    public class MalshinonService
    {

        private MalshinonDbContext _context;

        public MalshinonService(MalshinonDbContext context)
        {
            _context = context;
        }


        //===============================================================================

        public async Task<ApiResponse<string>> InsertReport(People reporter , People target , string text)
        {
            try
            {
                IntelReport intelReport = new IntelReport();
                intelReport.TargetId = target.Id;
                intelReport.ReporterId = reporter.Id;
                intelReport.Text = text;

                await _context.IntelReport.AddAsync(intelReport);

                reporter.NumReports += 1;
                target.NumMentions += 1;
                await _context.SaveChangesAsync();

                Log.AddReport(intelReport);

                return ApiResponse<string>.Ok("דוח נוסף בהצלחה");
            }
            catch
            {
                return ApiResponse<string>.Fail("עדכון נכשל");
            }
        }
        //-----------------------------------------------------------------------------------------

        public async Task<bool> IsExists(string Fname ,string Lname , string? Seacrt = null)
        {
            People? p = await _context.People.Where(p => p.FirstName == Fname && p.LastName == Lname).FirstOrDefaultAsync();
            return p != null;
        }

        //-----------------------------------------------------------------------------------------

        public async Task<People?> GetPepole(string Fname, string Lname, string? Seacrt = null)
        {
            return await _context.People.Where(p => p.FirstName == Fname && p.LastName == Lname).FirstOrDefaultAsync();
        }

        //-----------------------------------------------------------------------------------------

        public async Task<ApiResponse<string>> AddReport(RequestDTO dto)
        {

            try
            {
                People? reporter = null ;

                if (dto.SecretCode != null)
                {
                    reporter = await _context.People.Where(s => s.SecretCode == dto.SecretCode).FirstOrDefaultAsync();
                }
                if(dto.FirstName != null && dto.LastName != null && reporter is null)
                {
                    reporter = await _context.People.Where(s => s.FirstName == dto.FirstName && s.LastName == dto.LastName).FirstOrDefaultAsync();

                    if (reporter is null)
                    {
                        AddPeople(dto.FirstName, dto.LastName , "reporter");
                        reporter = await _context.People.Where(s => s.FirstName == dto.FirstName && s.LastName == dto.LastName).FirstOrDefaultAsync();
                    }
                }


                People? target = null;

                if (dto.TargetFirstName != null && dto.TargetLastName != null)
                {
                    target = await _context.People.Where(s => s.FirstName == dto.TargetFirstName && s.LastName == dto.TargetLastName).FirstOrDefaultAsync();
                    if (target is null)
                    {

                        AddPeople(dto.TargetFirstName, dto.TargetLastName, "target");

                        target = await _context.People.Where(s => s.FirstName == dto.TargetFirstName && s.LastName == dto.TargetLastName).FirstOrDefaultAsync();
                    }
                }


                if (reporter is null)
                {
                    return ApiResponse<string>.Fail("חסרים נתוני מלשין");
                }


                if (target is null)
                {
                    return ApiResponse<string>.Fail("חסרים נתוני מולשן");
                }


                if (dto.Data is null)
                {
                    return ApiResponse<string>.Fail("חסרים נתוני הלשנה");
                }


                ApiResponse<string> result = await InsertReport(reporter, target, dto.Data);

                return result;

            }
            catch
            {
                return ApiResponse<string>.Fail("בעיה בנתונים שנשלחו"); 
            }

            
        }

        //============================================================================
        public void AddPeople(string first, string last , string type)
        {
            People people = new People();
            people.FirstName = first;
            people.LastName = last;
            people.SecretCode = Guid.NewGuid().ToString().Substring(1,10);
            people.Type = type;
            _context.People.Add(people);
            _context.SaveChanges();

            Log.AddPeople(people);

        }
        //---------------------------------------------------------------------------------------
        public async Task<int> GetId(string first, string last)
        {
            People? people = await _context.People.Where(p => p.FirstName == first && p.LastName == last).FirstOrDefaultAsync();

            return people != null ? people.Id : -1;
        }

        public async Task<int> GetId(string secrt)
        {
            People? people = await _context.People.Where(p => p.SecretCode == secrt).FirstOrDefaultAsync();

            return people != null ? people.Id : -1;
        }

        //---------------------------------------------------------------------------------------------



        public async Task<ApiResponse<List<People>>> GetAllPeople(RequestDTO dto)

        {
            People? people = await GetPepole(dto.FirstName!, dto.LastName!, dto.SecretCode);

            if (people != null && people.Type == "reporter")
            {
                List<People> result = await _context.People.ToListAsync();

                return ApiResponse<List<People>>.Ok(result);
            }

            return ApiResponse<List<People>>.Fail(" לא ניתן לגשת ללא פרטי סוכן נכונים");

        }

        //-------------------------------------------------------------------------------------
        public async Task<ApiResponse<List<IntelReport>>> GetAllReports(RequestDTO dto)

        {
            People? people = await GetPepole(dto.FirstName!, dto.LastName!, dto.SecretCode);

            if (people != null && people.Type == "reporter")
            {
                List<IntelReport> result = await _context.IntelReport.ToListAsync();

                return ApiResponse<List<IntelReport>>.Ok(result);
            }

            return ApiResponse<List<IntelReport>>.Fail(" לא ניתן לגשת ללא פרטי סוכן נכונים");

        }
        //----------------------------------------------------------------------------------------


        public async Task<ApiResponse<List<People>>> GetDangerous(RequestDTO dto)
        {
            People? people = await GetPepole(dto.FirstName!, dto.LastName!, dto.SecretCode);

            if (people != null && people.Type == "reporter")
            {
                var reports = await _context.IntelReport
                          .OrderBy(r => r.Timestamp)
                          .ToListAsync();

                var targetIds = reports
                    .GroupBy(r => r.TargetId)
                    .Where(g =>
                    {
                        var list = g.OrderBy(r => r.Timestamp).ToList();
                        for (int i = 0; i < (list.Count -2); i++)
                        {
                            TimeSpan span = list[i + 2].Timestamp - list[i].Timestamp;
                            if (span.TotalMinutes <= 30)
                                return true;
                        }
                        return false;
                    })
                    .Select(g => g.Key)
                    .ToList();

                List<People> result = await _context.People.Where(p => targetIds.Contains( p.Id)).ToListAsync();


                return ApiResponse<List<People>>.Ok(result);
            }

            return ApiResponse<List<People>>.Fail(" לא ניתן לגשת ללא פרטי סוכן נכונים");
        }
        //-------------------------------------------------------------------------------------------------

        public async Task<ApiResponse<List<People>>> GetPotentialAgents(RequestDTO dto)

        {
            People? people = await GetPepole(dto.FirstName!, dto.LastName!, dto.SecretCode);

            if (people != null && people.Type == "reporter")
            {

                List<People> result = await _context.People.Where(p => p.NumReports >= 10).ToListAsync();
               

                return ApiResponse<List<People>>.Ok(result);
            }

            return ApiResponse<List<People>>.Fail(" לא ניתן לגשת ללא פרטי סוכן נכונים");

        }
        //----------------------------------------------------------------------------------------
    }
}

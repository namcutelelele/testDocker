using ExceptionHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WorkingSpace_BusinessObject.DTOs;
using WorkingSpace_BusinessObject.Models;

namespace WorkingSpace_DataAccessLayer.DataLayer
{
    public class WorkingSpaceDAO
    {
        private readonly MyDbContext _myDbContext;

        public WorkingSpaceDAO(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        // Working Space
        public async Task<IEnumerable<WorkingSpace>> GetAllAsync()
        {
            return await _myDbContext.WorkingSpaces.ToListAsync();
        }

        public async Task<WorkingSpace> GetByIdAsync(int id)
        {
            return await _myDbContext.WorkingSpaces.FindAsync(id);
        }

        public async Task<WorkingSpace> InsertWorkingSpace(WorkingSpace workingSpace)
        {
            _myDbContext.WorkingSpaces.Add(workingSpace);
            await _myDbContext.SaveChangesAsync();
            return workingSpace;
        }

        public async Task<WorkingSpace> UpdateWorkingSpace(WorkingSpace workingSpace)
        {
            _myDbContext.WorkingSpaces.Update(workingSpace);
            await _myDbContext.SaveChangesAsync();
            return workingSpace;
        }

        public async Task<WorkingSpace> DeleteWorkingSpace(WorkingSpace workingSpace)
        {
            _myDbContext.WorkingSpaces.Remove(workingSpace);
            await _myDbContext.SaveChangesAsync();
            return workingSpace;
        }

        public async Task<WorkingSpace> FindByWorkingSpaceNameAsync(string workingSpaceDisplayName, int studentId)
        {
            return await _myDbContext.WorkingSpaces
                             .FirstOrDefaultAsync(c => c.WorkingSpaceDisplayName == workingSpaceDisplayName 
                                                    && c.StudentId == studentId);

        }

        public async Task<List<WorkingSpace>> ListAllWorkingSpaceByStudentIDAsync(int studentID)
        {
            return await _myDbContext.WorkingSpaces
                             .Where(c => c.StudentId == studentID)
                             .ToListAsync();
        }

        public async Task<Student> FindStudentByAccountID(int accountId)
        {
            return await _myDbContext.Students
                             .FirstOrDefaultAsync(c => c.AccountId == accountId);
        }

        // Student Notebook
        /*
        USE AES_db;
        SELECT ws.ID AS WorkingSpaceID, 
               ws.WorkingSpaceDisplayName, 
               ws.StudentId,
               wb.ID AS WorkbookID, 
               wb.Name AS WorkbookName, 
               wb.Description AS WorkbookDescription
        FROM WorkingSpace ws
        JOIN StudentNoteBook snb ON ws.ID = snb.WorkingSpaceID
        JOIN Workbook wb ON snb.WorkbookID = wb.ID
        Where snb.WorkingSpaceID = 2
         */

        public async Task<Workbook> FindWorkBookbyWorkBookID(int workbookID)
        {
            return await _myDbContext.Workbooks.FirstOrDefaultAsync(wb => wb.Id == workbookID);
        }

        public async Task<Account> FindAccountByAccountID(int accountid)
        {
            return await _myDbContext.Accounts.FirstOrDefaultAsync(wb => wb.Id == accountid);
        }

        public async Task<IEnumerable<StudentNoteBookDTO>> GetAllStudentNotebookAsync()
        {
            var result = from ws in _myDbContext.WorkingSpaces
                         join snb in _myDbContext.StudentNoteBooks
                         on ws.Id equals snb.WorkingSpaceId
                         join wb in _myDbContext.Workbooks
                         on snb.WorkbookId equals wb.Id
                         select new StudentNoteBookDTO
                         {
                             WorkingSpaceID = ws.Id,
                             WorkingSpaceDisplayName = ws.WorkingSpaceDisplayName,
                             StudentId = ws.StudentId,
                             WorkbookID = wb.Id,
                             WorkbookName = wb.Name,
                             WorkbookDescription = wb.Description
                         };

            return await result.ToListAsync();
        }

        public async Task<StudentNoteBook> GetByStudentNotebookIdAsync(int id)
        {
            return await _myDbContext.StudentNoteBooks.FindAsync(id);
        }

        public async Task<StudentNoteBook> GetStudentNotebookByWorkBookIDAndWorkingSpaceIDAsync(int workBookID,int workingSpaceID)
        {
            return await _myDbContext.StudentNoteBooks.Where(c => c.WorkingSpaceId == workingSpaceID && c.WorkbookId == workBookID).FirstOrDefaultAsync();
        }

        public async Task<List<StudentNoteBook>> GetByWorkingSpaceIdAsync(int id)
        {
            return await _myDbContext.StudentNoteBooks.Where(c => c.WorkingSpaceId == id).ToListAsync();
        }

        public async Task<List<StudentNoteBook>> GetByWorkingBookIdAsync(int id)
        {
            return await _myDbContext.StudentNoteBooks.Where(c => c.WorkbookId == id).ToListAsync();
        }

        public async Task<List<StudentNoteBookDTO>> ListWorkBookByWorkingSpaceID(int workingspaceId)
        {
            var result = from ws in _myDbContext.WorkingSpaces
                         join snb in _myDbContext.StudentNoteBooks
                         on ws.Id equals snb.WorkingSpaceId
                         join wb in _myDbContext.Workbooks
                         on snb.WorkbookId equals wb.Id
                         where snb.WorkingSpaceId == workingspaceId
                         select new StudentNoteBookDTO
                         {
                             WorkingSpaceID = ws.Id,
                             WorkingSpaceDisplayName = ws.WorkingSpaceDisplayName,
                             StudentId = ws.StudentId,
                             WorkbookID = wb.Id,
                             WorkbookName = wb.Name,
                             WorkbookDescription = wb.Description
                         };

            return await result.ToListAsync();
        }

        public async Task<List<StudentNoteBookDTO>> ListWorkingSpaceByWorkbookID(int workbookid)
        {
            var result = from ws in _myDbContext.WorkingSpaces
                         join snb in _myDbContext.StudentNoteBooks
                         on ws.Id equals snb.WorkingSpaceId
                         join wb in _myDbContext.Workbooks
                         on snb.WorkbookId equals wb.Id
                         where snb.WorkingSpaceId == workbookid
                         select new StudentNoteBookDTO
                         {
                             WorkingSpaceID = ws.Id,
                             WorkingSpaceDisplayName = ws.WorkingSpaceDisplayName,
                             StudentId = ws.StudentId,
                             WorkbookID = wb.Id,
                             WorkbookName = wb.Name,
                             WorkbookDescription = wb.Description
                         };

            return await result.ToListAsync();
        }

        public async Task<StudentNoteBookDTO> FindStudentNoteBookbyStudentIDAndWorkbookID(int wokingspaceid ,int workbookid)
        {
            var result = from ws in _myDbContext.WorkingSpaces
                         join snb in _myDbContext.StudentNoteBooks
                         on ws.Id equals snb.WorkingSpaceId
                         join wb in _myDbContext.Workbooks
                         on snb.WorkbookId equals wb.Id
                         where snb.WorkbookId == workbookid && snb.WorkingSpaceId == wokingspaceid
                         select new StudentNoteBookDTO
                         {
                             WorkingSpaceID = ws.Id,
                             WorkingSpaceDisplayName = ws.WorkingSpaceDisplayName,
                             StudentId = ws.StudentId,
                             WorkbookID = wb.Id,
                             WorkbookName = wb.Name,
                             WorkbookDescription = wb.Description
                         };

            return await result.FirstOrDefaultAsync();
        }


        public async Task<StudentNoteBook> InsertStudentNoteBook(StudentNoteBook studentNoteBook)
        {
            _myDbContext.StudentNoteBooks.Add(studentNoteBook);
            await _myDbContext.SaveChangesAsync();
            return studentNoteBook;
        }

        public async Task<StudentNoteBook> UpdateStudentNoteBook(StudentNoteBook studentNoteBook)
        {
            _myDbContext.StudentNoteBooks.Update(studentNoteBook);
            await _myDbContext.SaveChangesAsync();
            return studentNoteBook;
        }

        public async Task<StudentNoteBook> DeleteStudentNoteBook(StudentNoteBook studentNoteBook)
        {
            _myDbContext.StudentNoteBooks.Remove(studentNoteBook);
            await _myDbContext.SaveChangesAsync();
            return studentNoteBook;
        }






        //public async Task<IEnumerable<CommonMistakeCategory>> GetAllAsync(int offset, int limit, string direction, string sortBy)
        //{
        //    if (offset < 0) offset = 0;
        //    if (limit <= 0) limit = 10;
        //    if (direction != "asc" && direction != "desc") direction = "asc";

        //    var query = _myDbContext.CommonMistakeCategories.AsQueryable();

        //    switch (sortBy.ToLower())
        //    {
        //        case "name":
        //            query = direction == "asc" ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name);
        //            break;
        //        case "id":
        //            query = direction == "asc" ? query.OrderBy(e => e.Id) : query.OrderByDescending(e => e.Id);
        //            break;
        //        default:
        //            throw new CustomException(HttpStatusCode.BadRequest, "Invalid sortBy parameter.", "Invalid sortBy parameter.", null);
        //    }


        //    query = query.Skip(offset).Take(limit);

        //    return await query.ToListAsync();

        //    //return await _myDbContext.CommonMistakeCategories.ToListAsync();
        //}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingSpace_BusinessObject.DTOs;
using WorkingSpace_BusinessObject.Models;

namespace WorkingSpace_Repositories
{
    public interface IWorkingSpaceRepository
    {
        public Task<Account> FindAccountByAccountID(int accountid);
        // Working Space
        public  Task<IEnumerable<WorkingSpace>> GetAllAsync();

        public  Task<WorkingSpace> GetByIdAsync(int id);

        public  Task<WorkingSpace> InsertWorkingSpace(WorkingSpace workingSpace);
        public  Task<WorkingSpace> UpdateWorkingSpace(WorkingSpace workingSpace);
        public Task<WorkingSpace> DeleteWorkingSpace(WorkingSpace workingSpace);

        public Task<WorkingSpace> FindByWorkingSpaceNameAsync(string workingSpaceDisplayName, int studentId);
        public Task<List<WorkingSpace>> ListAllWorkingSpaceByStudentIDAsync(int studentID);

        public Task<Student> FindStudentByAccountID(int accountId);

        // Student NoteBook
        public Task<StudentNoteBook> InsertStudentNoteBook(StudentNoteBook studentNoteBook);
        public Task<StudentNoteBook> UpdateStudentNoteBook(StudentNoteBook studentNoteBook);
        public Task<StudentNoteBook> DeleteStudentNoteBook(StudentNoteBook studentNoteBook);
        public Task<IEnumerable<StudentNoteBookDTO>> GetAllStudentNotebookAsync();
        public Task<StudentNoteBook> GetByStudentNotebookIdAsync(int studentNotebookId);
        public Task<List<StudentNoteBook>> GetByWorkingSpaceId(int workingSpaceId);
        public Task<List<StudentNoteBook>> GetByWorkingBookId(int workingBookId);
        public Task<List<StudentNoteBookDTO>> ListWorkBookByWorkingSpaceID(int workingspaceId);
        public Task<List<StudentNoteBookDTO>> ListWorkingSpaceByWorkbookID(int workbookid);
        public Task<StudentNoteBookDTO> FindStudentNoteBookbyStudentIDAndWorkbookID(int studentid, int workbookid);
        public Task<Workbook> FindWorkBookbyWorkBookID(int workbookID);
        public Task<StudentNoteBook> GetStudentNotebookByWorkBookIDAndWorkingSpaceIDAsync(int workBookID, int workingSpaceID);

    }
}

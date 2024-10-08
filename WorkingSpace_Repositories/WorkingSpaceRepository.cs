using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingSpace_BusinessObject.DTOs;
using WorkingSpace_BusinessObject.Models;
using WorkingSpace_DataAccessLayer;
using WorkingSpace_DataAccessLayer.DataLayer;

namespace WorkingSpace_Repositories
{
    public class WorkingSpaceRepository : IWorkingSpaceRepository
    {
        private readonly WorkingSpaceDAO _workingSpaceDAO;

        public WorkingSpaceRepository(WorkingSpaceDAO workingSpaceDAO)
        {
            _workingSpaceDAO = workingSpaceDAO;
        }

        public Task<Account> FindAccountByAccountID(int accountid) => _workingSpaceDAO.FindAccountByAccountID(accountid);

        // Working Space

        public Task<WorkingSpace> FindByWorkingSpaceNameAsync(string workingSpaceDisplayName, int studentId) => _workingSpaceDAO.FindByWorkingSpaceNameAsync(workingSpaceDisplayName, studentId);

        public Task<List<WorkingSpace>> ListAllWorkingSpaceByStudentIDAsync(int studentID) => _workingSpaceDAO.ListAllWorkingSpaceByStudentIDAsync(studentID);

        public Task<IEnumerable<WorkingSpace>> GetAllAsync() => _workingSpaceDAO.GetAllAsync();

        public Task<WorkingSpace> GetByIdAsync(int id) => _workingSpaceDAO.GetByIdAsync(id);

        public Task<WorkingSpace> InsertWorkingSpace(WorkingSpace workingSpace) => _workingSpaceDAO.InsertWorkingSpace(workingSpace);

        public Task<WorkingSpace> UpdateWorkingSpace(WorkingSpace workingSpace) => _workingSpaceDAO.UpdateWorkingSpace(workingSpace);

        public Task<WorkingSpace> DeleteWorkingSpace(WorkingSpace workingSpace) => _workingSpaceDAO.DeleteWorkingSpace(workingSpace);
        public Task<Student> FindStudentByAccountID(int accountId) => _workingSpaceDAO.FindStudentByAccountID(accountId);

        // Student NoteBook

        public Task<StudentNoteBook> InsertStudentNoteBook(StudentNoteBook studentNoteBook) => _workingSpaceDAO.InsertStudentNoteBook(studentNoteBook);

        public Task<StudentNoteBook> UpdateStudentNoteBook(StudentNoteBook studentNoteBook) => _workingSpaceDAO.UpdateStudentNoteBook(studentNoteBook);

        public Task<StudentNoteBook> DeleteStudentNoteBook(StudentNoteBook studentNoteBook) => _workingSpaceDAO.DeleteStudentNoteBook(studentNoteBook);

        public Task<IEnumerable<StudentNoteBookDTO>> GetAllStudentNotebookAsync() => _workingSpaceDAO.GetAllStudentNotebookAsync();

        public Task<StudentNoteBook> GetByStudentNotebookIdAsync(int studentNotebookId) => _workingSpaceDAO.GetByStudentNotebookIdAsync(studentNotebookId);

        public Task<List<StudentNoteBook>> GetByWorkingSpaceId(int workingSpaceId) => _workingSpaceDAO.GetByWorkingSpaceIdAsync(workingSpaceId);

        public Task<List<StudentNoteBook>> GetByWorkingBookId(int workingBookId) => _workingSpaceDAO.GetByWorkingBookIdAsync(workingBookId);

        public Task<List<StudentNoteBookDTO>> ListWorkBookByWorkingSpaceID(int workingspaceId) => _workingSpaceDAO.ListWorkBookByWorkingSpaceID(workingspaceId);

        public Task<List<StudentNoteBookDTO>> ListWorkingSpaceByWorkbookID(int workbookid) => _workingSpaceDAO.ListWorkingSpaceByWorkbookID(workbookid);

        public Task<StudentNoteBookDTO> FindStudentNoteBookbyStudentIDAndWorkbookID(int studentid, int workbookid) => _workingSpaceDAO.FindStudentNoteBookbyStudentIDAndWorkbookID(studentid, workbookid);
        public Task<Workbook> FindWorkBookbyWorkBookID(int workbookID) => _workingSpaceDAO.FindWorkBookbyWorkBookID(workbookID);

        public Task<StudentNoteBook> GetStudentNotebookByWorkBookIDAndWorkingSpaceIDAsync(int workBookID, int workingSpaceID) => _workingSpaceDAO.GetStudentNotebookByWorkBookIDAndWorkingSpaceIDAsync(workBookID, workingSpaceID);
    }
}

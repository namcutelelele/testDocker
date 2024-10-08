using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingSpace_BusinessObject.DTOs;
using WorkingSpace_BusinessObject.Models;

namespace WorkingSpace_Services
{
    public interface IWorkingSpaceService
    {
        public Task<Account> FindAccountByAccountID(int accountId);
        // working space
        public Task<IEnumerable<ViewWorkingSpaceDTO>> GetAllAsync();
        public Task<ViewWorkingSpaceDTO> GetByWorkingSpaceIdAsync(int id);
        public Task<List<ViewWorkingSpaceDTO>> GetAllWorkingSpaceByStudentIdAsync(int studentId);
        public Task<ViewWorkingSpaceDTO> InsertWorkingSpace(AddNewWorkingSpaceDTO newWorkingSpaceDTO, int studentID);
        public Task<ViewWorkingSpaceDTO> UpdateWorkingSpace(ViewWorkingSpaceDTO viewWorkingSpaceDTO, int studentId);
        public Task<ViewWorkingSpaceDTO> DeleteWorkingSpace(ViewWorkingSpaceDTO viewWorkingSpaceDTO);
        public Task<ViewWorkingSpaceDTO> GetWorkingSpaceByNameAsync(string categoryName, int studentID);
        public Task<Student> FindStudentByAccountID(int accountId);

        // student note book
        public Task<StudentNoteBookDTO> InsertStudentNoteBook(AddNewAndDeleteWorkbookInWorkingSpaceDTO addNewWorkbookInWorkingSpaceDTO);
        public Task<StudentNoteBookDTO> DeleteStudentNoteBook(AddNewAndDeleteWorkbookInWorkingSpaceDTO deleteWorkbookInWorkingSpace);
        public Task<IEnumerable<StudentNoteBookDTO>> GetAllStudentNotebookAsync();
        public Task<List<StudentNoteBookDTO>> ListWorkBookByWorkingSpaceID(int workingspaceId);
        public Task<List<StudentNoteBookDTO>> ListWorkingSpaceByWorkbookID(int workbookid);
    }
}

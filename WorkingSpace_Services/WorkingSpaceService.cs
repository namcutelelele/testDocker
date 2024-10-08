using AutoMapper;
using ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WorkingSpace_BusinessObject.DTOs;
using WorkingSpace_BusinessObject.Models;
using WorkingSpace_Repositories;

namespace WorkingSpace_Services
{
    public class WorkingSpaceService : IWorkingSpaceService
    {
        private readonly IWorkingSpaceRepository _workingSpaceRepository;
        private readonly IMapper _mapper;

        public WorkingSpaceService(IWorkingSpaceRepository workingSpaceRepository, IMapper mapper)
        {
            _workingSpaceRepository = workingSpaceRepository;
            _mapper = mapper;
        }

        public async Task<Account> FindAccountByAccountID(int accountId)
        {
            var acc = await _workingSpaceRepository.FindAccountByAccountID(accountId);
            return acc;
        }

        // working space

        public async Task<IEnumerable<ViewWorkingSpaceDTO>> GetAllAsync()
        {
            var workingSpaces = await _workingSpaceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ViewWorkingSpaceDTO>>(workingSpaces);
        }

        public async Task<ViewWorkingSpaceDTO> GetByWorkingSpaceIdAsync(int id)
        {
            var workingSpace = await _workingSpaceRepository.GetByIdAsync(id);
            if (workingSpace == null)
            {
                return null;
            }
            return _mapper.Map<ViewWorkingSpaceDTO>(workingSpace);
        }

        public async Task<List<ViewWorkingSpaceDTO>> GetAllWorkingSpaceByStudentIdAsync(int studentId)
        {
            var workingSpace = await _workingSpaceRepository.ListAllWorkingSpaceByStudentIDAsync(studentId);
            if (workingSpace == null)
            {
                return null;
            }
            return _mapper.Map<List<ViewWorkingSpaceDTO>>(workingSpace);
        }

        public async Task<ViewWorkingSpaceDTO> InsertWorkingSpace(AddNewWorkingSpaceDTO newWorkingSpaceDTO, int studentID)
        {
            if (string.IsNullOrEmpty(newWorkingSpaceDTO.WorkingSpaceDisplayName))
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    "InsertWorkingSpace - WorkingSpace_Service : Working Space name cannot null",
                    "An unexpected error occurred",
                    null); 
            }

            var existingWorkingSpace = await _workingSpaceRepository.FindByWorkingSpaceNameAsync(newWorkingSpaceDTO.WorkingSpaceDisplayName, studentID);
            if (existingWorkingSpace != null)
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    "InsertWorkingSpace - WorkingSpace_Service : Working Space name must be unique",
                    "An unexpected error occurred",
                    null);
            }

            var workingSpace = _mapper.Map<WorkingSpace>(newWorkingSpaceDTO);

            workingSpace.StudentId = studentID;

            var createdWorkingSpace = await _workingSpaceRepository.InsertWorkingSpace(workingSpace);

            return _mapper.Map<ViewWorkingSpaceDTO>(createdWorkingSpace);
        }

        public async Task<ViewWorkingSpaceDTO> UpdateWorkingSpace(ViewWorkingSpaceDTO viewWorkingSpaceDTO, int studentId)
        {
            if (string.IsNullOrEmpty(viewWorkingSpaceDTO.WorkingSpaceDisplayName))
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    "UpdateWorkingSpace - WorkingSpace_Service : Working Space name cannot null",
                    "An unexpected error occurred",
                    null);
            }
            var existingWorkingSpace = await _workingSpaceRepository.FindByWorkingSpaceNameAsync(viewWorkingSpaceDTO.WorkingSpaceDisplayName, studentId);
            if (existingWorkingSpace != null)
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    "UpdateWorkingSpace - WorkingSpace_Service : Working Space name must be unique",
                    "An unexpected error occurred",
                    null);
            }

            var workingSpaceToUpdate = await _workingSpaceRepository.GetByIdAsync(viewWorkingSpaceDTO.Id);
            if (workingSpaceToUpdate == null)
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    "UpdateWorkingSpace - WorkingSpace_Service : Working Space not found",
                    "An unexpected error occurred",
                    null);
            }


            _mapper.Map(viewWorkingSpaceDTO, workingSpaceToUpdate);

            workingSpaceToUpdate.StudentId = studentId;

            var updatedWorkingSpace = await _workingSpaceRepository.UpdateWorkingSpace(workingSpaceToUpdate);

            return _mapper.Map<ViewWorkingSpaceDTO>(updatedWorkingSpace);
        }

        public async Task<ViewWorkingSpaceDTO> DeleteWorkingSpace(ViewWorkingSpaceDTO viewWorkingSpaceDTO)
        {
            var workingSpaceToDelete = await _workingSpaceRepository.GetByIdAsync(viewWorkingSpaceDTO.Id);
            if (workingSpaceToDelete == null)
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    "DeleteWorkingSpace - WorkingSpace_Service : Working Space not found",
                    "An unexpected error occurred",
                    null);
            }
            var deletedWorkingSpace = await _workingSpaceRepository.DeleteWorkingSpace(workingSpaceToDelete);
            return _mapper.Map<ViewWorkingSpaceDTO>(deletedWorkingSpace);
        }

        public async Task<ViewWorkingSpaceDTO> GetWorkingSpaceByNameAsync(string workingSpaceName, int studentId)
        {
            var workingSpace = await _workingSpaceRepository.FindByWorkingSpaceNameAsync(workingSpaceName, studentId);
            if (workingSpace == null)
            {
                return null;
            }
            return _mapper.Map<ViewWorkingSpaceDTO>(workingSpace);
        }

        public async Task<Student> FindStudentByAccountID(int accountId)
        {
            var student = await _workingSpaceRepository.FindStudentByAccountID(accountId);
            return student;
        }

        // student note book

        public async Task<StudentNoteBookDTO> InsertStudentNoteBook(AddNewAndDeleteWorkbookInWorkingSpaceDTO addNewWorkbookInWorkingSpaceDTO)
        {
            WorkingSpace workingSpace = await _workingSpaceRepository.FindByWorkingSpaceNameAsync(addNewWorkbookInWorkingSpaceDTO.workingSpaceName, addNewWorkbookInWorkingSpaceDTO.studentID);
            var existingWorkingSpaceWithWorkbookid = await _workingSpaceRepository.FindStudentNoteBookbyStudentIDAndWorkbookID(workingSpace.Id, addNewWorkbookInWorkingSpaceDTO.workbookID);

            if (existingWorkingSpaceWithWorkbookid != null)
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    "InsertStudentNoteBook - WorkingSpace_Service : Workbook exist in this Working Space",
                    "An unexpected error occurred",
                    null);
            }

            Workbook workbook = await _workingSpaceRepository.FindWorkBookbyWorkBookID(addNewWorkbookInWorkingSpaceDTO.workbookID);

            StudentNoteBook studentNoteBook = new StudentNoteBook() 
            {
                WorkingSpaceId = workingSpace.Id,
                WorkbookId = workbook.Id,
            };

            var createdWorkbookOnWorkingSpace = await _workingSpaceRepository.InsertStudentNoteBook(studentNoteBook);

            StudentNoteBookDTO studentNoteBookDTO = new StudentNoteBookDTO()
            {
                WorkingSpaceID = workingSpace.Id,
                WorkingSpaceDisplayName = workingSpace.WorkingSpaceDisplayName,
                StudentId = workingSpace.StudentId,
                WorkbookID = workbook.Id,
                WorkbookName = workbook.Name,
                WorkbookDescription = workbook.Description
            };

            return studentNoteBookDTO;
        }


        public async Task<StudentNoteBookDTO> DeleteStudentNoteBook(AddNewAndDeleteWorkbookInWorkingSpaceDTO deleteWorkbookInWorkingSpace)
        {
            WorkingSpace workingSpace = await _workingSpaceRepository.FindByWorkingSpaceNameAsync(deleteWorkbookInWorkingSpace.workingSpaceName, deleteWorkbookInWorkingSpace.studentID);
            
            var existingWorkingSpaceWithWorkbookid = await _workingSpaceRepository.FindStudentNoteBookbyStudentIDAndWorkbookID(workingSpace.Id, deleteWorkbookInWorkingSpace.workbookID);

            if (existingWorkingSpaceWithWorkbookid == null)
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    "DeleteStudentNoteBook - WorkingSpace_Service : this Workbook dont exist in this Working Space",
                    "An unexpected error occurred",
                    null);
            }

            Workbook workbook = await _workingSpaceRepository.FindWorkBookbyWorkBookID(deleteWorkbookInWorkingSpace.workbookID);

            StudentNoteBook studentNoteBook = await _workingSpaceRepository.GetStudentNotebookByWorkBookIDAndWorkingSpaceIDAsync(workbook.Id, workingSpace.Id);

            var deletedWorkbookOnWorkingSpace = await _workingSpaceRepository.DeleteStudentNoteBook(studentNoteBook);

            StudentNoteBookDTO studentNoteBookDTO = new StudentNoteBookDTO()
            {
                WorkingSpaceID = workingSpace.Id,
                WorkingSpaceDisplayName = workingSpace.WorkingSpaceDisplayName,
                StudentId = workingSpace.StudentId,
                WorkbookID = workbook.Id,
                WorkbookName = workbook.Name,
                WorkbookDescription = workbook.Description
            };

            return studentNoteBookDTO;
        }

        public async Task<IEnumerable<StudentNoteBookDTO>> GetAllStudentNotebookAsync()
        {
            var listAllWorkbook = await _workingSpaceRepository.GetAllStudentNotebookAsync();
            return listAllWorkbook;
        }

        public async Task<List<StudentNoteBookDTO>> ListWorkBookByWorkingSpaceID(int workingspaceId)
        {
            var existingWorkingSpace = await _workingSpaceRepository.GetByIdAsync(workingspaceId);
            if (existingWorkingSpace == null)
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    "ListWorkBookByWorkingSpaceID - WorkingSpace_Service : this Working Space dont exist",
                    "An unexpected error occurred",
                    null);
            }

            var ListWorkBookByWorkingSpaceID = await _workingSpaceRepository.ListWorkBookByWorkingSpaceID(workingspaceId);
            return ListWorkBookByWorkingSpaceID;
        }

        public async Task<List<StudentNoteBookDTO>> ListWorkingSpaceByWorkbookID(int workbookid)
        {
            var existingWorkbook = await _workingSpaceRepository.FindWorkBookbyWorkBookID(workbookid);
            if (existingWorkbook == null)
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    "ListWorkingSpaceByWorkbookID - WorkingSpace_Service : this Workbook dont exist",
                    "An unexpected error occurred",
                    null);
            }

            var listWorkingSpaceByWorkbookID = await _workingSpaceRepository.ListWorkingSpaceByWorkbookID(workbookid);
            return listWorkingSpaceByWorkbookID;
        }
    }

    //public async Task<IEnumerable<CommonMistakeCategory>> GetAllCategoriesAsync(int offset, int limit, string direction, string sortBy)
    //{

    //    try
    //    {
    //        var categories = await _workingSpaceRepository.GetAllAsync(offset, limit, direction, sortBy);
    //        if (categories == null)
    //        {
    //            throw new CustomException(HttpStatusCode.NotFound,
    //                "GetAllCategoriesAsync - WorkingSpace_Service : Categories could not be retrieved from the repository",
    //                "An unexpected error occurred",
    //                null);
    //        }
    //        return categories;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new CustomException(HttpStatusCode.BadRequest,
    //            $"GetAllCategoriesAsync - WorkingSpace_Service : {ex.Message}",
    //            "An unexpected error occurred",
    //            null);
    //    }
    //}
}

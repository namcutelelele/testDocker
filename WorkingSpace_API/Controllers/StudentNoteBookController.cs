using ExceptionHandling;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WorkingSpace_BusinessObject.DTOs;
using WorkingSpace_BusinessObject.Models;
using WorkingSpace_Services;

namespace WorkingSpace_API.Controllers
{
    [Route("api/StudentNoteBookService/[controller]")]
    [ApiController]
    public class StudentNoteBookController : Controller
    {
        private readonly IWorkingSpaceService _workingSpaceService;

        public StudentNoteBookController(IWorkingSpaceService workingSpaceService)
        {
            _workingSpaceService = workingSpaceService;
        }

        [HttpGet]
        public async Task<ActionResult> ListWorkBookByWorkingSpaceID([FromQuery] int accountID, [FromQuery] int workingspaceid)
        {
            if (accountID == 0)
            {
                return new JsonResult(new CustomException(HttpStatusCode.BadRequest,
                        "CreateWorkbookInWorkingSpace - WorkingSpace_API : account id cannot 0.",
                        "An unexpected error occurred",
                        null));
            }

            if (await _workingSpaceService.FindAccountByAccountID(accountID) == null)
            {
                return new JsonResult(new CustomException(HttpStatusCode.BadRequest,
                        "CreateWorkbookInWorkingSpace - WorkingSpace_API : account not exist.",
                        "An unexpected error occurred",
                        null));
            }

            Student student = await _workingSpaceService.FindStudentByAccountID(accountID);
            if (student == null)
            {
                return NotFound(new APIReturn()
                {
                    code = 404,
                    message = $"Not Found Student with accountid = {accountID}",
                    data = null
                });
            }
            int studentID = student.Id;

            var listWorkBookByWorkingSpaceID = await _workingSpaceService.ListWorkBookByWorkingSpaceID(workingspaceid);
            return new JsonResult(new APIReturn()
            {
                code = 200,
                message = "Success",
                data = listWorkBookByWorkingSpaceID.Cast<object>().ToList()
            });
        }

        [HttpPost]
        public async Task<ActionResult> CreateWorkbookInWorkingSpace([FromBody] AddNewAndDeleteWorkbookInWorkingSpaceDTO addNewWorkbookInWorkingSpaceDTO, [FromQuery] int accountId)
        {
            if (accountId == 0)
            {
                return new JsonResult(new CustomException(HttpStatusCode.BadRequest,
                        "CreateWorkbookInWorkingSpace - WorkingSpace_API : account id cannot 0.",
                        "An unexpected error occurred",
                        null));
            }
            
            if (await _workingSpaceService.FindAccountByAccountID(accountId) == null)
            {
                return new JsonResult(new CustomException(HttpStatusCode.BadRequest,
                        "CreateWorkbookInWorkingSpace - WorkingSpace_API : account not exist.",
                        "An unexpected error occurred",
                        null));
            }

            Student student = await _workingSpaceService.FindStudentByAccountID(accountId);
            if (student == null)
            {
                return NotFound(new APIReturn()
                {
                    code = 404,
                    message = $"Not Found Student with accountid = {accountId}",
                    data = null
                });
            }
            int studentID = student.Id;

            var createdWorkbookInWorkingSpace = await _workingSpaceService.InsertStudentNoteBook(addNewWorkbookInWorkingSpaceDTO);
            return new JsonResult(new APIReturn()
            {
                code = 200,
                message = "Success",
                data = new List<object> { createdWorkbookInWorkingSpace }
            });
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteWorkbookInWorkingSpace([FromBody] AddNewAndDeleteWorkbookInWorkingSpaceDTO deleteWorkbookInWorkingSpace, [FromQuery] int accountId)
        {
            if (accountId == 0)
            {
                return new JsonResult(new CustomException(HttpStatusCode.BadRequest,
                        "CreateWorkbookInWorkingSpace - WorkingSpace_API : account id cannot 0.",
                        "An unexpected error occurred",
                        null));
            }

            if (await _workingSpaceService.FindAccountByAccountID(accountId) == null)
            {
                return new JsonResult(new CustomException(HttpStatusCode.BadRequest,
                        "CreateWorkbookInWorkingSpace - WorkingSpace_API : account not exist.",
                        "An unexpected error occurred",
                        null));
            }

            Student student = await _workingSpaceService.FindStudentByAccountID(accountId);
            if (student == null)
            {
                return NotFound(new APIReturn()
                {
                    code = 404,
                    message = $"Not Found Student with accountid = {accountId}",
                    data = null
                });
            }
            int studentID = student.Id;

            var deletedWorkbookInWorkingSpace = await _workingSpaceService.DeleteStudentNoteBook(deleteWorkbookInWorkingSpace);
            return new JsonResult(new APIReturn()
            {
                code = 200,
                message = "Success",
                data = new List<object> { deletedWorkbookInWorkingSpace }
            });
        }
    }
}

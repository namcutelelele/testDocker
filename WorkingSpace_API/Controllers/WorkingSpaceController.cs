using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkingSpace_BusinessObject.Models;
using WorkingSpace_DataAccessLayer.DataLayer;
using WorkingSpace_Services;
using ExceptionHandling;
using System.Net;
using WorkingSpace_BusinessObject.DTOs;
using System.Text.RegularExpressions;


namespace WorkingSpace_API.Controllers
{
    [Route("api/WorkingSpaceService/[controller]")]
    [ApiController]
    public class WorkingSpaceController : ControllerBase
    {
        private readonly IWorkingSpaceService _workingSpaceService;

        public WorkingSpaceController(IWorkingSpaceService workingSpaceService)
        {
            _workingSpaceService = workingSpaceService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ViewWorkingSpaceDTO>>> GetAllWorkingspace()
        {
            var listAllWorkbooks = await _workingSpaceService.GetAllAsync();
            return new JsonResult(new APIReturn()
            {
                code = 200,
                message = "Success",
                data = listAllWorkbooks.Cast<object>().ToList()
            });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllWorkingSpaceByStudentId([FromQuery] int accountID)
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

            var listAllWorkbooks = await _workingSpaceService.GetAllWorkingSpaceByStudentIdAsync(studentID);
            return new JsonResult(new APIReturn()
            {
                code = 200,
                message = "Success",
                data = listAllWorkbooks.Cast<object>().ToList()
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewWorkingSpaceDTO>> GetByWorkingSpaceId(int id)
        {
            var workbook = await _workingSpaceService.GetByWorkingSpaceIdAsync(id);
            if (workbook == null)
            {
                return NotFound(new APIReturn()
                {
                    code = 404,
                    message = "Working space not found",
                    data = null
                });

            }
            return new JsonResult(new APIReturn()
            {
                code = 200,
                message = "Success",
                data = new List<object> { workbook }
            });
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewWorkingSpace([FromBody] AddNewWorkingSpaceDTO newWorkingSpaceDTO, [FromQuery] int accountId)
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

            var createdWorkingSpace = await _workingSpaceService.InsertWorkingSpace(newWorkingSpaceDTO, studentID);
            return new JsonResult(new APIReturn()
            {
                code = 200,
                message = "Success",
                data = new List<object> { createdWorkingSpace }
            });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateWorkingSpace([FromBody] ViewWorkingSpaceDTO updateWorkingSpaceDTO, [FromQuery] int accountId)
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

            var updatedWorkingSpace = await _workingSpaceService.UpdateWorkingSpace(updateWorkingSpaceDTO, studentID);
            return new JsonResult(new APIReturn()
            {
                code = 200,
                message = "Success",
                data = new List<object> { updatedWorkingSpace }
            });
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteWorkingSpace([FromBody] ViewWorkingSpaceDTO viewWorkingSpaceDTO, [FromQuery] int accountId)
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

            var deletedWorkingSpace = await _workingSpaceService.DeleteWorkingSpace(viewWorkingSpaceDTO);
            var listWorkingSpaceAfterDelete = await _workingSpaceService.GetAllAsync();
            return new JsonResult(new APIReturn()
            {
                code = 200,
                message = "Success",
                data = new List<object> { listWorkingSpaceAfterDelete }
            });
        }



        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<CommonMistakeCategory>>> GetCategories(
        //    [FromQuery] int offset = 0,     
        //    [FromQuery] int limit = 10,     
        //    [FromQuery] string direction = "asc",  
        //    [FromQuery] string sortBy = "ID"  
        //    )
        //{
        //    try
        //    {
        //        var categories = await _workingSpaceService.GetAllCategoriesAsync(offset, limit, direction, sortBy);
        //        return new JsonResult(new APIReturn()
        //        {
        //            code = 200,
        //            message = "Success",
        //            data = categories.Cast<object>().ToList()
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new CustomException(HttpStatusCode.NotFound, "An unexpected error occurred",
        //                "GetCategories - WorkingSpace_API : Categories could not be retrieved from the repository",
        //                null));
        //    }

        //}
    }
}

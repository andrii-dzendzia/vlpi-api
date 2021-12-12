using System.Threading.Tasks;
using Vlpi.Domain.Dto;

namespace Vlpi.Domain.Services.Interfaces
{
    public interface ITaskService
    {
        Task<Response> GetTask(int id);
        Task<Response> AddTask(AddTaskDto addTaskDto);
        Task<Response> EditTask(EditTaskDto editTaskDto);
        Task<Response> AnswerTask(TaskAnswerDto addTaskDto);
    }
}

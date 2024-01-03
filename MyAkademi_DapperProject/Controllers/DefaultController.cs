using Dapper;
using Microsoft.AspNetCore.Mvc;
using MyAkademi_DapperProject.DapperContext;
using MyAkademi_DapperProject.Dtos;
using System.Collections.Immutable;

namespace MyAkademi_DapperProject.Controllers
{
    public class DefaultController : Controller
    {
        private readonly Context _context;

        public DefaultController(Context context)
        {
            _context = context;
        }

        public async Task< IActionResult> Index()
        {
            string query = "Select*From Project";
            var connection=_context.CreateConnection();
            var values = await connection.QueryAsync<ResultProjectDto>(query);
            return View(values.ToList());
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectDto createProjectDto)
        {
            string query = "insert into Project (Title,Description,ProjectCategory,CompleteDay,Price) Values (@title,@description,@projectCategory,@completeDay,@price)";
            var parameters = new DynamicParameters();
            parameters.Add("@title", createProjectDto.Title);
            parameters.Add("@description", createProjectDto.Description);
            parameters.Add("@projectCategory", createProjectDto.ProjectCategory);
            parameters.Add("@completeDay", createProjectDto.CompleteDay);
            parameters.Add("@price", createProjectDto.Price);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query,parameters);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProject(int id)
        {
            string query = "Delete From Project where ProjectID=@ProjectID";
            var parameters=new DynamicParameters();
            parameters.Add("@ProjectID", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateProject(int id)
        {
            string query = "Select*From Project where ProjectID=@ProjectID";
            var parameters=new DynamicParameters();
            parameters.Add("@ProjectID", id);
            var connection = _context.CreateConnection();
            var values=await connection.QueryFirstOrDefaultAsync<UpdateProjectDto>(query,parameters);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(UpdateProjectDto updateProjectDto)
        {
            string query = "Update Project Set Title=@title,Description=@description,Price=@price,ProjectCategory=@projectCategory,CompleteDay=@completeDay where ProjectID=@ProjectID";
            var parameters=new DynamicParameters();
            parameters.Add("@title", updateProjectDto.Title);
            parameters.Add("@description", updateProjectDto.Description);
            parameters.Add("@projectCategory", updateProjectDto.ProjectCategory);
            parameters.Add("@completeDay", updateProjectDto.CompleteDay);
            parameters.Add("@price", updateProjectDto.Price);
            parameters.Add("@ProjectID", updateProjectDto.ProjectID);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
            return RedirectToAction("Index");

        }
    }
}

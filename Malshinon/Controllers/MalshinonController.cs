using Malshinon.classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;

namespace Malshinon.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MalshinonController : ControllerBase
    {


        private readonly ILogger<MalshinonController> _logger;

        private readonly MalshinonDbContext _context;

        private readonly MalshinonService _service;


        public MalshinonController(ILogger<MalshinonController> logger, MalshinonDbContext context)
        {
            _context = context;
            _logger = logger;
            _service = new MalshinonService(_context);
        }


        //------------------------------------------------------------------------------------------------
        [HttpPost("AllPeople")]
        public async Task<ApiResponse<List<People>>> AllPeople([FromBody] RequestDTO dto)
        {
            Log.request("AllPeople");
            return await _service.GetAllPeople(dto);
        }


        //------------------------------------------------------------------------------------------------
        [HttpPost("AllReports")]
        public async Task<ApiResponse<List<IntelReport>>> AllReports([FromBody] RequestDTO dto)
        {
            Log.request("AllReports");

            return await _service.GetAllReports(dto);
        }
        //------------------------------------------------------------------------------------------------


        [HttpPost("AddReport")]
        public async Task<ApiResponse<string>> AddReport([FromBody] RequestDTO dto)
        {
            Log.request("AddReport");

            return await _service.AddReport(dto);
        }

        //-----------------------------------------------------------------------------------------------------
        [HttpPost("Dangerous")]
        public async Task<ApiResponse<List<People>>> Dangerous([FromBody] RequestDTO dto)
        {
            Log.request("Dangerous");

            return await _service.GetDangerous(dto);
        }
     
        //-----------------------------------------------------------------------------------------------------
        [HttpPost("PotentialAgents")]
        public async Task<ApiResponse<List<People>>> PotentialAgents([FromBody] RequestDTO dto)
        {
            Log.request("PotentialAgents");

            return await _service.GetPotentialAgents(dto);
        }
        //------------------------------------------------------------------------------------------------








        [HttpGet("ping")]
        public IActionResult Ping()
        {
            Console.WriteLine(">>> ping called");
            return Ok("pong");
        }




    }
}








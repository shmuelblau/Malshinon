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


        public MalshinonController(ILogger<MalshinonController> logger , MalshinonDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "GetMalshinon")]
        public string Get()
        {
            return string.Join(", ", _context.People.Select(p => p.FirstName));
        }
    }
}

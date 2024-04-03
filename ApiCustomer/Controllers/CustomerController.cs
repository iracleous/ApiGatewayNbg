using ApiCustomer.Models;
using ApiCustomer.Requests;
using ApiCustomer.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ISecurityService _securityService;
        private readonly ApplicationDbContext _applicationDbContext;

        public CustomerController(ISecurityService securityService, ApplicationDbContext applicationDbContext)
        {
            _securityService = securityService;
            _applicationDbContext = applicationDbContext;
        }

        [AllowAnonymous]
        [HttpPost("Signup")]
        public void Signup([FromBody] Credentials credentials)
        {
            _applicationDbContext.Credentials.Add(credentials);
            _applicationDbContext.SaveChanges();
        }

        [HttpPost("Login")]
        public string? Login( [FromBody]  Credentials credentials )
        {
            //todo compare values in db

            var dbCredentials = _applicationDbContext.Credentials.Where (
                cr => cr.Username.Equals(credentials.Username) && cr.Password.Equals(credentials.Password))
                .FirstOrDefault();
            if (dbCredentials == null) return null;
            return _securityService.GenerateJwtToken(credentials.Username, "Administrator");
         }


        [Authorize]
        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return  [ new Customer{Id=1, Name="Dimitris" }] ;
        }

        [Authorize]
        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize(Roles = "HRManager,Finance")]
        // POST api/<CustomerController>
        [HttpPost]
        public Customer Post([FromBody] Customer value)
        {
            return value;
        }

        [Authorize]
        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [Authorize]
        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

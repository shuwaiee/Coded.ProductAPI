using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Models.Entites;
using ProductApi.Models.Requests;
using ProductApi.Models.Responses;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        BankContext _context;
        public BankController(BankContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get all the banks in the system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PageListResult<BankBranchResponse> GetAll(int page = 1, string search = "")
        {
            if (search == "")
            {
                return _context.BankBranches
                .Select(b => new BankBranchResponse
                {
                    BranchManager = b.BranchManager,
                    Location = b.Location,
                    Name = b.Name,
                    Id = b.Id
                }).ToPageList(page, 1);
            }

            return _context.BankBranches
                .Where(r=> r.Name.StartsWith(search))
                .Select(b => new BankBranchResponse
                {
                    BranchManager = b.BranchManager,
                    Location = b.Location,
                    Name = b.Name,
                    Id = b.Id
                }).ToPageList(page, 1);

        }
        [HttpGet("Details")]
        [ProducesResponseType(typeof(BankBranchResponse), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BankBranchResponse> Details([FromRoute] int id)
        {
          
            var bank = _context.BankBranches.Find(id);
            if (bank == null)
            {
                return NotFound();
            }
            return Ok(new BankBranchResponse
            {
                BranchManager = bank.BranchManager,
                Location = bank.Location,
                Name = bank.Name
            });
        }

        [HttpPatch("{id}")]
        public IActionResult Edit(int id, AddBankRequest req)
        {
            var bank = _context.BankBranches.Find(id);

            bank.Name = req.Name;
            bank.BranchManager = req.BranchManager;
            bank.Location = req.Location;

            _context.SaveChanges();

            return CreatedAtAction(nameof(Details), new { Id = bank.Id }, bank);
        }
        [HttpPost]
        public IActionResult Add(AddBankRequest req)
        {
            var newBank = new BankBranchEntity()
            {
                Name = req.Name,
                Location = req.Location
            };
            _context.BankBranches.Add(newBank);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Details), new { Id = newBank.Id }, newBank);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            var user = HttpContext.User;
            var bank = _context.BankBranches.Find(id);
            if (bank == null)
            {
                return BadRequest();
            }
              
            _context.BankBranches.Remove(bank);
            _context.SaveChanges();
     
            return Ok();
        }
        
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

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
        public List<BankBranchResponse> GetAll()
        {
            return _context.BankBranches
                .Select(b => new BankBranchResponse
            {
                BranchManager = b.BranchManager,
                Location = b.Location,
                Name = b.Name
            }).ToList();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BankBranchResponse), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BankBranchResponse> Details(int id)
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

            return Created(nameof(Details), new { Id = bank.Id });
        }
        [HttpPost]
        public IActionResult Add(AddBankRequest req)
        {
            var newBank = new BankBranch()
            {
                Name = req.Name,
                Location = req.Location
            };
            _context.BankBranches.Add(newBank);
            _context.SaveChanges();

            return Created(nameof(Details), new { Id = newBank.Id });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
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

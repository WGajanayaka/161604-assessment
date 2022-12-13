using Mbrrace.DataAccess;
using Mbrrace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mbrrace.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPersonRepository _peopleRepository;

        public PeopleController(IPersonRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        // GET: People
        public async Task<IActionResult> Index(string filterText)
        {
            var data = await _peopleRepository.GetPeople()
                .Where(p =>string.IsNullOrEmpty(filterText) || p.GivenName.Contains(filterText) || p.FamilyName.Contains(filterText))
                .ToListAsync();
            ViewBag.FilterText = filterText;
            return View(data);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _peopleRepository == null)
            {
                return NotFound();
            }

            var person = await _peopleRepository.GetPersonByIDAsync(id.Value);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GivenName,FamilyName,Address1,Address2,Town,Postcode,Email,DateOfBirth")] Person person)
        {
            if (ModelState.IsValid)
            {
                await _peopleRepository.InsertPersonAsync(person);
                await _peopleRepository.SaveAsync();
                return RedirectToAction(nameof(Details), new { ID = person.Id });
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _peopleRepository == null)
            {
                return NotFound();
            }

            var person = await _peopleRepository.GetPersonByIDAsync(id.Value);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GivenName,FamilyName,Address1,Address2,Town,Postcode,Email,DateOfBirth")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _peopleRepository.UpdatePerson(person);
                    await _peopleRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { ID = person.Id });
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _peopleRepository == null)
            {
                return NotFound();
            }

            var person = await _peopleRepository.GetPersonByIDAsync(id.Value);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _peopleRepository.GetPersonByIDAsync(id);
            if (person != null)
            {
                _peopleRepository.DeletePerson(person.Id);
            }
            
            await _peopleRepository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
          return _peopleRepository.GetPeople().Any(e => e.Id == id);
        }

        protected override void Dispose(bool disposing)
        {
            _peopleRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}

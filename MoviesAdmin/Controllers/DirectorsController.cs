
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin.Models;
using MoviesAdmin.Services;

[Authorize]
public class DirectorsController : Controller
{
    private readonly MoviesAdminContext _context;
    private readonly AzureStorageService _azureStorage;

    public DirectorsController(MoviesAdminContext context, AzureStorageService azureStorage)
    {
        _context = context;
        _azureStorage = azureStorage;
    }

    // GET: DIRECTORS
    public async Task<IActionResult> Index()    
    {
        {
            return View(await _context.Director
                .OrderBy(m => m.Name)
                .ToListAsync());
        }
    }

    // GET: DIRECTORS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var director = await _context.Director
            .Include(m => m.Movies)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (director == null)
        {
            return NotFound();
        }

        return View(director);
    }

    // GET: DIRECTORS/Create
    [Authorize(Roles = "administrator")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: DIRECTORS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "administrator")]
    public async Task<IActionResult> Create([Bind("Id,Name,Birthday,Birthplace,ImageUrl")] Director director, IFormFile? ImageUpload)
    {

        if (ImageUpload == null)
        {
            // ERROR VALIDATION
            ModelState.AddModelError("ImageUpload", "Image file is required");

        }

        if (ModelState.IsValid)
        {

            string extension = Path.GetExtension(ImageUpload!.FileName); // includes the leading dot, e.g. ".jpg"
            string newFileName = $"{Guid.NewGuid()}{extension}";

            // Upload file to Azure Storage
            string url = await _azureStorage.UploadFileAsync(ImageUpload, newFileName);

            // Set the image URL in trail record            
            director.ImageUrl = url;

            _context.Add(director);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(director);
    }

    // GET: DIRECTORS/Edit/5
    [Authorize(Roles = "administrator")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var director = await _context.Director.FindAsync(id);
        if (director == null)
        {
            return NotFound();
        }
        return View(director);
    }

    // POST: DIRECTORS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "administrator")]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Birthday,Birthplace,ImageUrl")] Director director, IFormFile? ImageUpload)
    {
        if (id != director.Id)
        {
            return NotFound();
        }

        // UPLOAD NEW FILE, GET URL, PLUG IN TO RECORD -- DELETE OLD FILE -- TODO
        if (ImageUpload != null)
        {
            string extension = Path.GetExtension(ImageUpload!.FileName); 
            string newFileName = $"{Guid.NewGuid()}{extension}";

            string url = await _azureStorage.UploadFileAsync(ImageUpload, newFileName);
            
            director.ImageUrl = url;
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(director);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(director.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(director);
    }

    // GET: DIRECTORS/Delete/5
    [Authorize(Roles = "administrator")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var director = await _context.Director
            .Include(m => m.Movies)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (director == null)
        {
            return NotFound();
        }

        return View(director);
    }

    // POST: DIRECTORS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "administrator")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var director = await _context.Director.FindAsync(id);
        if (director != null)
        {
            _context.Director.Remove(director);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DirectorExists(int? id)
    {
        return _context.Director.Any(e => e.Id == id);
    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin;
using MoviesAdmin.Models;
using MoviesAdmin.Services;

[Authorize]
public class MoviesController : Controller
{
    private readonly MoviesAdminContext _context;
    private readonly AzureStorageService _azureStorage;
    public MoviesController(MoviesAdminContext context, AzureStorageService azureStorage)
    {
        _context = context;
        _azureStorage = azureStorage;
    }



    // GET: MOVIES
    public async Task<IActionResult> Index()    
    {

        string userId = User.GetUserId();
        Console.WriteLine("Logged in user id: " + userId);

        List<Movie> movies = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.Rating)
            .Include(m => m.Director)
            .OrderByDescending(x => x.ReleaseDate) // order by descending to show the newest movies first, OrderBy returns the oldest first
            .ToListAsync();

          
        return View(movies);

    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.Rating)
            .Include(m => m.Director)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: MOVIES/Create
    [Authorize(Roles = "administrator")]
    public IActionResult Create()
    {

        var genres = _context.Genre.ToList().OrderBy(m => m.Title);
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var ratings = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(ratings, "Id", "Title");

        var directors = _context.Director.ToList();
        ViewBag.DirectorId = new SelectList(directors, "Id", "Name");


        return View();
    }

    // POST: MOVIES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "administrator")]
    public async Task<IActionResult> Create([Bind("Id,Title,Synopsis,GenreId,AudienceRatingId,Runtime,ReleaseDate,CreatedDate,DirectorId")] Movie movie, IFormFile? ImageUpload)
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
            movie.ImageUrl = url;

            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var genres = _context.Genre.ToList().OrderBy(m => m.Title);
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var ratings = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(ratings, "Id", "Title");

        var directors = _context.Director.ToList();
        ViewBag.DirectorId = new SelectList(directors, "Id", "Name");

        return View(movie);
    }

    // GET: MOVIES/Edit/5
    [Authorize(Roles = "administrator")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        var genres = _context.Genre.ToList().OrderBy(m=>m.Title);
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var ratings = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(ratings, "Id", "Title");

        var directors = _context.Director.ToList();
        ViewBag.DirectorId = new SelectList(directors, "Id", "Name");

        return View(movie);
    }

    // POST: MOVIES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "administrator")]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Synopsis,GenreId,AudienceRatingId,Runtime,ReleaseDate,CreatedDate,DirectorId,ImageUrl")] Movie movie, IFormFile? ImageUpload)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        // UPLOAD NEW FILE, GET URL, PLUG IN TO RECORD -- DELETE OLD FILE -- TODO
        if (ImageUpload != null)
        {
            string extension = Path.GetExtension(ImageUpload!.FileName); // includes the leading dot, e.g. ".jpg"
            string newFileName = $"{Guid.NewGuid()}{extension}";

            string url = await _azureStorage.UploadFileAsync(ImageUpload, newFileName);
         
            movie.ImageUrl = url;
        }

        if (ModelState.IsValid)
        {
            try
            {

                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
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

        var genres = _context.Genre.ToList().OrderBy(m => m.Title);
        ViewBag.GenreId = new SelectList(genres, "Id", "Title");

        var ratings = _context.AudienceRating.ToList();
        ViewBag.AudienceRatingId = new SelectList(ratings, "Id", "Title");

        var directors = _context.Director.ToList();
        ViewBag.DirectorId = new SelectList(directors, "Id", "Name");

        return View(movie);
    }

    // GET: MOVIES/Delete/5
    [Authorize(Roles = "administrator")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .Include(m => m.Genre)
            .Include(m => m.Rating)
            .Include(m => m.Director)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "administrator")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}

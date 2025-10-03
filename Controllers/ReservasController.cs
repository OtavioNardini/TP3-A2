namespace RestauranteAPP_TP3.Controllers
{
    // Controllers/ReservasController.cs
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore; // Needed for Include and ToListAsync
    using RestauranteAPP_TP3.Data; // Assuming your DbContext is here

    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReservasController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var reservas = await _db.Reservas
                .Include(r => r.Usuario)  // carrega dados do usuário
                .Include(r => r.Mesa)     // carrega dados da mesa
                .ToListAsync();

            return View(reservas);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int mesaId, DateTime dataHora)
        {
            // salvar reserva futuramente
            return RedirectToAction("Index");
        }
    }
}

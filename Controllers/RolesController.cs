using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteAPP_TP3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: /Roles
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        // GET: /Roles/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            return View(role);
        }

        // POST: /Roles/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string name)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.AddModelError("Name", "O nome da role é obrigatório.");
                return View(role);
            }

            role.Name = name.Trim();
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors) ModelState.AddModelError(string.Empty, e.Description);
                return View(role);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Roles/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            return View(role);
        }

        // POST: /Roles/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                TempData["Error"] = string.Join("; ", result.Errors.Select(e => e.Description));
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
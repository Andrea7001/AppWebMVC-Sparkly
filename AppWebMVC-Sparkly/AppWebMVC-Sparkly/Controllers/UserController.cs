using AppWebMVC_Sparkly.Models;
using AppWebMVC_Sparkly.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppWebMVC_Sparkly.Controllers
{ 
    public class UserController : Controller
{
     private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    // GET: UserController/Register
    public ActionResult Register()
    {
        return View();
    }

 [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Register(User user)
{
    if (ModelState.IsValid)
    {
        try
        {
            await _userService.RegisterUser(user);
            return RedirectToAction(nameof(Index)); // Redirige si es exitoso
        }
        catch (Exception ex)
        {
            // Registro de error con más detalles
            ModelState.AddModelError("", $"Error al crear el usuario: {ex.Message}");
            
        }
    }

    // Si hay errores, vuelve a la vista con los datos ingresados y mensajes de error
    return View(user);
}


    // GET: UserController/Login
    public ActionResult Login()
    {
        return View();
    }

    // POST: UserController/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginModel loginModel)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.Authenticate(loginModel.Email, loginModel.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Email o contraseña incorrectos");
                return View();
            }
            return RedirectToAction(nameof(Index)); // Redirige al panel de usuarios
        }
        return View(loginModel);
    }

    // GET: UserController/Index (obtener todos los usuarios)
    public async Task<ActionResult> Index()
    {
        var users = await _userService.GetAllUsers();
        return View(users);
    }

    // GET: UserController/Details/5 (ver un usuario específico)
    public async Task<ActionResult> Details(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: UserController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: UserController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            await _userService.UpdateUser(user);
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: UserController/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: UserController/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        await _userService.DeleteUser(id);
        return RedirectToAction(nameof(Index));
    }
}

}

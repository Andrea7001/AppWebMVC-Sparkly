using AppWebMVC_Sparkly.Models;

namespace AppWebMVC_Sparkly.Service;

  public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Método para obtener la lista de usuarios
    public async Task<List<User>> GetAllUsers()
    {
        var response = await _httpClient.GetAsync("api/User");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<User>>();
        }
        else
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error al obtener la lista de usuarios: {errorDetails}");
        }
    }

    // Método para obtener un usuario por su ID
    public async Task<User> GetUserById(int id)
    {
        var response = await _httpClient.GetAsync($"api/User/{id}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<User>();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null; // Si no se encuentra el usuario, devolver null
        }
        else
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error al obtener el usuario por ID: {errorDetails}");
        }
    }

    // Método para crear un nuevo usuario
    public async Task<User> RegisterUser(User user)
{
    // Cambiar la ruta al endpoint correcto
    var response = await _httpClient.PostAsJsonAsync("api/User/register", user);

    if (response.IsSuccessStatusCode)
    {
        return await response.Content.ReadFromJsonAsync<User>();
    }
    else
    {
        var errorDetails = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Error al crear el usuario: {errorDetails}");
    }
}



    // Método para autenticar un usuario (login)
    public async Task<User> Authenticate(string email, string password)
    {
        var loginModel = new { Email = email, Password = password };
        var response = await _httpClient.PostAsJsonAsync("api/User/login", loginModel);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<User>();
        }
        else
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error al autenticar el usuario: {errorDetails}");
        }
    }

    // Método para actualizar un usuario existente
    public async Task UpdateUser(User user)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/User/{user.Id}", user);

        if (!response.IsSuccessStatusCode)
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error al actualizar el usuario: {errorDetails}");
        }
    }

    // Método para eliminar un usuario
    public async Task<bool> DeleteUser(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/User/{id}");

        if (response.IsSuccessStatusCode)
        {
            return true; // Eliminación exitosa
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false; // Usuario no encontrado
        }
        else
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error al eliminar el usuario: {errorDetails}");
        }
    }
}





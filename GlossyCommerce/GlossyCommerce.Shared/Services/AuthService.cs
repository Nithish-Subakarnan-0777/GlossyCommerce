using GlossyCommerce.Shared.Models;
using System.Net.Http.Json;

namespace GlossyCommerce.Shared.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        public User? CurrentUser { get; private set; }
        public event Action? OnAuthStateChanged;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        // Now returns a Tuple: (Was it successful?, What was the error?)
        public async Task<(bool Success, string ErrorMessage)> Login(string username, string password)
        {
            try
            {
                var users = await _http.GetFromJsonAsync<List<User>>("api/users");
                var user = users?.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    CurrentUser = user;
                    OnAuthStateChanged?.Invoke();
                    return (true, "");
                }
                return (false, "Invalid username or password.");
            }
            catch (Exception ex)
            {
                // This captures the exact reason the server failed to connect!
                return (false, $"SERVER ERROR: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> Register(string username, string password)
        {
            try
            {
                var newUser = new User { Username = username, Password = password, Role = "Customer" };
                var response = await _http.PostAsJsonAsync("api/users", newUser);
                return response.IsSuccessStatusCode ? (true, "") : (false, "Failed to create account.");
            }
            catch (Exception ex)
            {
                return (false, $"SERVER ERROR: {ex.Message}");
            }
        }

        public void Logout()
        {
            CurrentUser = null;
            OnAuthStateChanged?.Invoke();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyProject
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MainController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MainController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IResult> PostData(UserModelRequest request)
        {
            // валидация 
            if (request.Name == "" || request.Name == null || request.Name.Length > 50)
            {
                return Results.BadRequest("Name - обязательно вводимые данные. Длина строки должна быть больше 0 и не превышать 50 символов");
            }

            if (request.Age < 18 || request.Age > 101)
            {
                return Results.BadRequest("Age должен быть в диапазоне от 18 до 100");
            }
            // Создание и сохранение модели
            var userModel = new UserModel
            {
                user_id = Guid.NewGuid(),
                name = request.Name,
                age = request.Age
            };
            _context.users.Add(userModel);
            await _context.SaveChangesAsync();

            // Видоизменение данных и отправка в Python микросервис

            var processedUser = new ProcessedUserModel
            {
                user_id = userModel.user_id,
                Name = userModel.name,
                Age = userModel.age,
                ProcessedAt = DateTime.UtcNow
            };

            var httpClient = new HttpClient();
            var pythonServiceUrl = Environment.GetEnvironmentVariable("PYTHON_SERVICE_URL");

            var response = await httpClient.PostAsJsonAsync(
                $"{pythonServiceUrl}/process",  
                new { 
                    id = processedUser.user_id, 
                    name = processedUser.Name, 
                    age = processedUser.Age, 
                    processed_at = processedUser.ProcessedAt.ToString("o")
                });
            return Results.Ok(userModel);
        }
    }

    public class UserModelRequest
    {
        public required string Name { get; set; }
        public int Age { get; set; }
    }

    public class ProcessedUserModel()
    {
        public required Guid user_id { get; set; }
        public required string Name { get; set; }
        public int? Age { get; set; }
        public DateTime ProcessedAt { get; set; }

    }
}

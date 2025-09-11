using Moq;
using Xunit;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;
using SindautoHub.Application.Service;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Interfaces;

namespace SindautoHubTestes.Entities.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IunitOfwork> _unitOfWorkMock;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _unitOfWorkMock = new Mock<IunitOfwork>();
            _passwordHasher = new BCryptPasswordHasher();

            // Aqui instanciamos o AuthService real
            _authService = new AuthService(
                _userRepoMock.Object,
                _passwordHasher,
                _tokenServiceMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenPasswordIsCorrect()
        {
            // Arrange
            var plainPassword = "admin1234";
            var hashedPassword = _passwordHasher.HashPassword(plainPassword);

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Password = hashedPassword,
                Role = "Admin"
            };

            _userRepoMock.Setup(r => r.GetByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync(user);

            _tokenServiceMock.Setup(t => t.GenerateToken(It.IsAny<User>()))
                             .Returns("fake-jwt-token");

            var request = new LoginRequest
            {
                UserName = "admin",
                Password = plainPassword
            };

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("fake-jwt-token", result.Token);
            Assert.Equal("admin", result.user.UserName);
            Assert.Equal("Admin", result.user.Role);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrow_WhenPasswordIsIncorrect()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Password = _passwordHasher.HashPassword("admin1234"),
                Role = "Admin"
            };

            _userRepoMock.Setup(r => r.GetByNameAsync("admin"))
                         .ReturnsAsync(user);

            var request = new LoginRequest
            {
                UserName = "admin",
                Password = "wrongpassword"
            };


            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(
                async () => await _authService.LoginAsync(request)
            );
        }
    }
}

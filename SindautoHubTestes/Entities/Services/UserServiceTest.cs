using AutoMapper;
using Moq;
using SindautoHub.Application.Dtos.UserDtos;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interfaces;
using Xunit;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IunitOfwork> _unitOfWorkMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<ICacheService> _cacheMock;
    private readonly IMapper _mapper;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IunitOfwork>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _cacheMock = new Mock<ICacheService>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateUserRequest, User>();
            cfg.CreateMap<User, UserResponse>();
        });
        _mapper = config.CreateMapper();

        _userService = new UserService(
            _userRepoMock.Object,
            _unitOfWorkMock.Object,
            _mapper,
            _passwordHasherMock.Object,
            _cacheMock.Object
        );
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateUser_WhenValidRequest()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Name = "Marcos",
            Email = "Marcos@test.com",
            Password = "987654321",
         
            Cpf = "123.456.789-10"
        };

        _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                     .ReturnsAsync((User)null);
        _userRepoMock.Setup(r => r.GetByCpfAsync("12345678910"))
                     .ReturnsAsync((User)null);

        _passwordHasherMock.Setup(h => h.HashPassword(request.Password))
                           .Returns("hashedPassword");

        User SavedUser = null;
        _userRepoMock.Setup(r=> r.CreateAsync(It.IsAny<User>()))
            .Callback<User>(r => SavedUser = r);
        // Act
        var result = await _userService.CreateAsync(request);

        // Assert
        Assert.Equal("Marcos", result.Name);
        Assert.Equal("Marcos@test.com", result.Email);


        Assert.Equal("hashedPassword", SavedUser.Password);
        Assert.Equal("12345678910", SavedUser.Cpf);
    
        _userRepoMock.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact]
    public async Task CreateAsync_ShouldCreateuser_WhenValidRequest()
    {

        var request = new CreateUserRequest
        {
            Name = "Jeffeson",
            Email = "Jefferson@teste.com",
            Password = "987456321",
            Cpf = "13265465846"
        };


        _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email))
                .ReturnsAsync(new User { Email = request.Email });

        await Assert.ThrowsAsync<BadRequestException>(() => _userService.CreateAsync(request));
    }
}




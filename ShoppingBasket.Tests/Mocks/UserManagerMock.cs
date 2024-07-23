using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ShoppingBasket.Domain.Entities;

namespace ShoppingBasket.Tests.Mocks;

public static class UserManagerMock
{
    public static Mock<UserManager<Customer>> CreateUserManagerMock()
    {
        var userStoreMock = new Mock<IUserStore<Customer>>();
        var optionsMock = new Mock<IOptions<IdentityOptions>>();
        var passwordHasherMock = new Mock<IPasswordHasher<Customer>>();
        var userValidatorsMock = new List<IUserValidator<Customer>> { new Mock<IUserValidator<Customer>>().Object };
        var passwordValidatorsMock = new List<IPasswordValidator<Customer>> { new Mock<IPasswordValidator<Customer>>().Object };
        var keyNormalizerMock = new Mock<ILookupNormalizer>();
        var errorsMock = new Mock<IdentityErrorDescriber>();
        var servicesMock = new Mock<IServiceProvider>();
        var loggerMock = new Mock<ILogger<UserManager<Customer>>>();

        return new Mock<UserManager<Customer>>(
            userStoreMock.Object,
            optionsMock.Object,
            passwordHasherMock.Object,
            userValidatorsMock,
            passwordValidatorsMock,
            keyNormalizerMock.Object,
            errorsMock.Object,
            servicesMock.Object,
            loggerMock.Object);
    }
}
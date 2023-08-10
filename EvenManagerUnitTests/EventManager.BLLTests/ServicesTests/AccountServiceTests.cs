using AutoMapper;
using EventManager.BLL.Services;
using EventManager.DAL.Contracts;
using EventManager.Models.DTO;
using EventManager.Models.Models;
using EventManager.Util.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using EventManager.BLL.Services;  
using EventManager.DAL.Contracts; 
using EventManager.Models.DTO;  
using EventManager.Models.Models;
using EventManager.Util.Models;
using EventManager.BLL.Contracts;
using EventManager.BLL.Mappers;

namespace EvenManagerUnitTests.EventManager.BLLTests.ServicesTests
{
    public  class AccountServiceTests
    {
        [Fact]
        public async Task RegisterUser_ShouldReturnTrue_WhenUserIsRegisteredSuccessfully()
        {
            // Arrange
            var authRepositoryMock = new Mock<IAccountRepository>();
            authRepositoryMock.Setup(repo => repo.RegisterUser(It.IsAny<RegisterUser>())).ReturnsAsync(true);

            var mapperMock = new Mock<IMapper>();
            var accountService = new AccountService(authRepositoryMock.Object, mapperMock.Object);

            var newUser = new RegisterUser { /* Set user properties */ };

            // Act
            var result = await accountService.RegisterUser(newUser);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Login_ShouldReturnTrue_WhenUserLoginIsSuccessful()
        {
            // Arrange
            var authRepositoryMock = new Mock<IAccountRepository>();
            authRepositoryMock.Setup(repo => repo.Login(It.IsAny<LoginUser>())).ReturnsAsync(true);

            var mapperMock = new Mock<IMapper>();
            var accountService = new AccountService(authRepositoryMock.Object, mapperMock.Object);

            var loginUser = new LoginUser { /* Set login user properties */ };

            // Act
            var result = await accountService.Login(loginUser);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllOrganisers_ShouldReturnPagedList_WhenDataIsAvailable()
        {
            // Arrange
            var pagerSettings = new PagerSettings { /* Set pager settings */ };
            var expectedOrganisers = new List<Organizer> { /* Create a list of Organiser objects */ };

            var authRepositoryMock = new Mock<IAccountRepository>();
            authRepositoryMock.Setup(repo => repo.GetAllOrganisers(pagerSettings)).ReturnsAsync(
                new PagedList<Organiser> { CurrentPage = 1, PageCount = 2, PageSize = 10, RowCount = 20, Data = expectedOrganisers });

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<OrganiserDTO>>(expectedOrganisers))
                .Returns(new List<OrganiserDTO> { /* Map Organiser objects to OrganiserDTO objects */ });

            var accountService = new AccountService(authRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await accountService.GetAllOrganisers(pagerSettings);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(2, result.PageCount);
            Assert.Equal(10, result.PageSize);
            Assert.Equal(20, result.RowCount);
            Assert.NotEmpty(result.Data);
        }

    }
}

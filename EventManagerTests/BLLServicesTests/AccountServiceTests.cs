using EventManager.BLL.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerTests.BLLServicesTests
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> mockRepository;
        private readonly IMapper mapper;
    }
}

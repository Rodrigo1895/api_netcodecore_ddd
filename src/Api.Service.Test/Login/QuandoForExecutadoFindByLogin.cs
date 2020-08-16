using System;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Login {
    public class QuandoForExecutadoFindByLogin {
        private ILoginService _service;
        private Mock<ILoginService> _serviceMock;

        [Fact(DisplayName = "É Possível executar o método FindByLogin.")]
        public async Task E_Possivel_Executar_Metodo_FindByLogin() {
            var email = Faker.Internet.Email();
            var objetoRetorno = new {
                authenticated = true,
                created = DateTime.UtcNow,
                expiration = DateTime.UtcNow,
                accessToken = Guid.NewGuid(),
                userName = email,
                name = Faker.Name.FullName(),
                message = "Usuário logado com successo"
            };

            var loginDto = new LoginDto{
                Email = email
            };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m => m.FindByLogin(loginDto)).ReturnsAsync(objetoRetorno);
            _service = _serviceMock.Object;

            var result = await _service.FindByLogin(loginDto);
            Assert.NotNull(result);
        }
    }
}
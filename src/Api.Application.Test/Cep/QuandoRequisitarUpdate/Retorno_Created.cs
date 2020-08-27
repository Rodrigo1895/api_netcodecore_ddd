using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.QuandoRequisitarUpdate {

    public class Retorno_Created {
        private CepsController _controller;

        [Fact(DisplayName = "É possível realizar o Updated.")]
        public async Task E_Possivel_Invocar_a_Controller_Update() {
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(m => m.Put(It.IsAny<CepDtoUpdate>())).ReturnsAsync(
                new CepDtoUpdateResult {
                    Id = Guid.NewGuid(),
                    Logradouro = "Teste de Rua",
                    UpdateAt = DateTime.UtcNow
                }
            );

            _controller = new CepsController(serviceMock.Object);

            var cepDtoUpdate = new CepDtoUpdate {
                Logradouro = "Teste de Rua",
                Numero = ""
            };

            var result = await _controller.Put(cepDtoUpdate);
            Assert.True(result is OkObjectResult);
        }
    }
}
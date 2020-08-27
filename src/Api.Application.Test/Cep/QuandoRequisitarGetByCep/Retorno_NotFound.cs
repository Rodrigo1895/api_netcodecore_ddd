using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.QuandoRequisitarGetByCep {
    public class Retorno_NotFound {
        private CepsController _controller;

        [Fact(DisplayName = "É possível realizar o Get by Cep.")]
        public async Task E_Possivel_Invocar_a_Controller_GetByCep() {
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(m => m.Get(It.IsAny<string>())).Returns(Task.FromResult((CepDto)null));

            _controller = new CepsController(serviceMock.Object);
            var result = await _controller.Get("1");
            Assert.True(result is NotFoundResult);
        }
    }
}
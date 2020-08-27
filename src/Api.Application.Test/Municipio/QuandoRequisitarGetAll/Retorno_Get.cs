using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.QuandoRequisitarGetAll {
    public class Retorno_Get {
        private MunicipiosController _controller;

        [Fact(DisplayName = "É possível realizar o GetAll.")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll() {
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<MunicipioDto> {
                    new MunicipioDto {
                        Id = Guid.NewGuid(),
                        Nome = "São Paulo"
                    },
                    new MunicipioDto {
                        Id = Guid.NewGuid(),
                        Nome = "Rio de Janeiro"
                    }
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);
        }
    }
}
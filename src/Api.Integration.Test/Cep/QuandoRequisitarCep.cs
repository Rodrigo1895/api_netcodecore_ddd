using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Cep {
    public class QuandoRequisitarCep : BaseIntegration {
        [Fact]
        public async Task E_Possivel_Realizar_Crud_Cep() {
            await AdicionarToken();

            var municipioDto = new MunicipioDtoCreate() {
                Nome = "São Paulo",
                CodIBGE = 3550308,
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
            };

            // POST Municipio
            HttpResponseMessage response = await PostJsonAsync(municipioDto, $"{hostApi}municipios", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPostMunicipio = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("São Paulo", registroPostMunicipio.Nome);
            Assert.Equal(3550308, registroPostMunicipio.CodIBGE);
            Assert.True(registroPostMunicipio.Id != default(Guid));
            
            var cepDto = new CepDtoCreate() {
                Cep = "13480180",
                Logradouro = "Rua 1",
                Numero = "1 até 1000",
                MunicipioId = registroPostMunicipio.Id
            };

            // POST
            response = await PostJsonAsync(cepDto, $"{hostApi}ceps", client);
            postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<CepDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(cepDto.Cep, registroPost.Cep);
            Assert.Equal(cepDto.Logradouro, registroPost.Logradouro);
            Assert.Equal(cepDto.Numero, registroPost.Numero);            
            Assert.True(registroPost.Id != default(Guid));

            // PUT
            var updateCepDto = new CepDtoUpdate() {
                Id = registroPost.Id,
                Cep = "13480180",
                Logradouro = "Rua 2",
                Numero = "1 até 1000",
                MunicipioId = registroPostMunicipio.Id
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateCepDto), Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}ceps", stringContent);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<CepDtoCreateResult>(jsonResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(updateCepDto.Logradouro, registroAtualizado.Logradouro);

            // GET ID
            response = await client.GetAsync($"{hostApi}ceps/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<CepDto>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroAtualizado.Cep, registroSelecionado.Cep);
            Assert.Equal(registroAtualizado.Logradouro, registroSelecionado.Logradouro);
            Assert.Equal(registroAtualizado.Numero, registroSelecionado.Numero);

            // GET Cep
            response = await client.GetAsync($"{hostApi}ceps/bycep/{registroAtualizado.Cep}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            registroSelecionado = JsonConvert.DeserializeObject<CepDto>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroAtualizado.Cep, registroSelecionado.Cep);
            Assert.Equal(registroAtualizado.Logradouro, registroSelecionado.Logradouro);
            Assert.Equal(registroAtualizado.Numero, registroSelecionado.Numero);

            // DELETE
            response = await client.DeleteAsync($"{hostApi}ceps/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // GET ID depois do delete
            response = await client.GetAsync($"{hostApi}ceps/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // DELETE municipio
            response = await client.DeleteAsync($"{hostApi}municipios/{registroPostMunicipio.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
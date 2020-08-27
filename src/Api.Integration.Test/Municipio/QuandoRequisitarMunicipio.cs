using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Muninipio {
    public class QuandoRequisitarMunicipio : BaseIntegration {
        [Fact]
        public async Task E_Possivel_Realizar_Crud_Municipio() {
            await AdicionarToken();

            var municipioDto = new MunicipioDtoCreate() {
                Nome = "São Paulo",
                CodIBGE = 3550308,
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
            };

            // POST
            HttpResponseMessage response = await PostJsonAsync(municipioDto, $"{hostApi}municipios", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("São Paulo", registroPost.Nome);
            Assert.Equal(3550308, registroPost.CodIBGE);
            Assert.True(registroPost.Id != default(Guid));

            // GET ALL
            response = await client.GetAsync($"{hostApi}municipios");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<MunicipioDto>>(jsonResult);
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() > 0);
            Assert.True(listaFromJson.Where(r => r.Id == registroPost.Id).Count() == 1);

            // PUT
            var updateMunicipioDto = new MunicipioDtoUpdate() {
                Id = registroPost.Id,
                Nome = "Campinas",
                CodIBGE = 3526902,
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateMunicipioDto), Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}municipios", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(jsonResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Campinas", registroAtualizado.Nome);
            Assert.Equal(3526902, registroAtualizado.CodIBGE);

            // GET ID
            response = await client.GetAsync($"{hostApi}municipios/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<MunicipioDto>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroAtualizado.Nome, registroSelecionado.Nome);
            Assert.Equal(registroAtualizado.CodIBGE, registroSelecionado.CodIBGE);

            // GET Complete ID
            response = await client.GetAsync($"{hostApi}municipios/complete/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionadoCompleto = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            Assert.NotNull(registroSelecionadoCompleto);
            Assert.Equal(registroAtualizado.Nome, registroSelecionadoCompleto.Nome);
            Assert.Equal(registroAtualizado.CodIBGE, registroSelecionadoCompleto.CodIBGE);
            Assert.NotNull(registroSelecionadoCompleto.Uf);
            Assert.Equal("São Paulo", registroSelecionadoCompleto.Uf.Nome);
            Assert.Equal("SP", registroSelecionadoCompleto.Uf.Sigla);

            // GET Complete IBGE
            response = await client.GetAsync($"{hostApi}municipios/byibge/{registroAtualizado.CodIBGE}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            registroSelecionadoCompleto = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            Assert.NotNull(registroSelecionadoCompleto);
            Assert.Equal(registroAtualizado.Nome, registroSelecionadoCompleto.Nome);
            Assert.Equal(registroAtualizado.CodIBGE, registroSelecionadoCompleto.CodIBGE);
            Assert.NotNull(registroSelecionadoCompleto.Uf);
            Assert.Equal("São Paulo", registroSelecionadoCompleto.Uf.Nome);
            Assert.Equal("SP", registroSelecionadoCompleto.Uf.Sigla);

            // DELETE
            response = await client.DeleteAsync($"{hostApi}municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // GET ID depois do delete
            response = await client.GetAsync($"{hostApi}municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
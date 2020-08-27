using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Uf {
    public class QuandoRequisitarUf : BaseIntegration {
        [Fact]
        public async Task E_Possivel_Realizar_Get_Uf() {
            await AdicionarToken();

            //GetAll
            response = await client.GetAsync($"{hostApi}ufs");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonresult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<UfDto>>(jsonresult);
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() == 27);
            Assert.True(listaFromJson.Where(r => r.Sigla == "SP").Count() == 1);

            //GetId
            var id = listaFromJson.Where(r => r.Sigla == "SP").FirstOrDefault().Id;
            response = await client.GetAsync($"{hostApi}ufs/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonresult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<UfDto>(jsonresult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal("São Paulo", registroSelecionado.Nome);
            Assert.Equal("SP", registroSelecionado.Sigla);
        }
    }
}
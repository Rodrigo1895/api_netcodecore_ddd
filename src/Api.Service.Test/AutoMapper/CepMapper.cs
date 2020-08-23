using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Cep;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper {
    public class CepMapper : BaseTesteService {
        [Fact(DisplayName = "É possível mapear os modelos de Cep")]
        public void E_Possivel_Mapear_os_Modelos_Cep() {
            var model = new CepModel() {
                Id = Guid.NewGuid(),
                Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                Logradouro = Faker.Address.StreetName(),
                Numero = "",
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                MunicipioId = Guid.NewGuid()
            };

            var listaEntity = new List<CepEntity>();
            for (int i = 0; i < 5; i++) {
                var item = new CepEntity() {
                    Id = Guid.NewGuid(),
                    Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = Faker.RandomNumber.Next(1, 10000).ToString(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    MunicipioId = Guid.NewGuid(),
                    Municipio = new MunicipioEntity
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.City(),
                        CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                        UfId = Guid.NewGuid(),
                        Uf = new UfEntity 
                        {
                            Id = Guid.NewGuid(),
                            Nome = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsState().Substring(1, 3),
                        }
                    }
                };
                listaEntity.Add(item);
            }

            // Model => Entity
            var entity = Mapper.Map<CepEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Cep, model.Cep);
            Assert.Equal(entity.Logradouro, model.Logradouro);
            Assert.Equal(entity.Numero, model.Numero);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            // Entity => Dto
            var dto = Mapper.Map<CepDto>(entity);
            Assert.Equal(dto.Id, entity.Id);
            Assert.Equal(dto.Cep, entity.Cep);
            Assert.Equal(dto.Logradouro, entity.Logradouro);
            Assert.Equal(dto.Numero, entity.Numero);

            var dtoCompleto = Mapper.Map<CepDto>(listaEntity.FirstOrDefault());
            Assert.Equal(dtoCompleto.Id, listaEntity.FirstOrDefault().Id);
            Assert.Equal(dtoCompleto.Cep, listaEntity.FirstOrDefault().Cep);
            Assert.Equal(dtoCompleto.Logradouro, listaEntity.FirstOrDefault().Logradouro);
            Assert.Equal(dtoCompleto.Numero, listaEntity.FirstOrDefault().Numero);
            Assert.NotNull(dtoCompleto.Municipio);
            Assert.NotNull(dtoCompleto.Municipio.Uf);

            var listaDto = Mapper.Map<List<CepDto>>(listaEntity);
            Assert.True(listaDto.Count() == listaEntity.Count());
            for (int i = 0; i < listaDto.Count(); i++) {
                Assert.Equal(listaDto[i].Id, listaEntity[i].Id);
                Assert.Equal(listaDto[i].Cep, listaEntity[i].Cep);
                Assert.Equal(listaDto[i].Logradouro, listaEntity[i].Logradouro);
                Assert.Equal(listaDto[i].Numero, listaEntity[i].Numero);
            }

            var dtoCreateResult = Mapper.Map<CepDtoCreateResult>(entity);
            Assert.Equal(dtoCreateResult.Id, entity.Id);
            Assert.Equal(dtoCreateResult.Cep, entity.Cep);
            Assert.Equal(dtoCreateResult.Logradouro, entity.Logradouro);
            Assert.Equal(dtoCreateResult.Numero, entity.Numero);
            Assert.Equal(dtoCreateResult.CreateAt, entity.CreateAt);

            var dtoUpdateResult = Mapper.Map<CepDtoUpdateResult>(entity);
            Assert.Equal(dtoUpdateResult.Id, entity.Id);
            Assert.Equal(dtoUpdateResult.Cep, entity.Cep);
            Assert.Equal(dtoUpdateResult.Logradouro, entity.Logradouro);
            Assert.Equal(dtoUpdateResult.Numero, entity.Numero);
            Assert.Equal(dtoUpdateResult.UpdateAt, entity.UpdateAt);

            // Dto => Model
            dto.Numero = "";
            var cepModel = Mapper.Map<CepModel>(dto);
            Assert.Equal(cepModel.Id, dto.Id);
            Assert.Equal(cepModel.Cep, dto.Cep);
            Assert.Equal(cepModel.Logradouro, dto.Logradouro);
            Assert.Equal(cepModel.Numero, "S/N");

            var dtoCreate = Mapper.Map<CepDtoCreate>(cepModel);
            Assert.Equal(dtoCreate.Cep, cepModel.Cep);
            Assert.Equal(dtoCreate.Logradouro, cepModel.Logradouro);
            Assert.Equal(dtoCreate.Numero, cepModel.Numero);

            var dtoUpdate = Mapper.Map<CepDtoUpdate>(cepModel);
            Assert.Equal(dtoUpdate.Id, cepModel.Id);
            Assert.Equal(dtoUpdate.Cep, cepModel.Cep);
            Assert.Equal(dtoUpdate.Logradouro, cepModel.Logradouro);
            Assert.Equal(dtoUpdate.Numero, cepModel.Numero);
        }
    }
}
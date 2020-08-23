using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper {
    public class MunicipioMapper : BaseTesteService {
        [Fact(DisplayName = "É possível mapear os modelos de Municipio")]
        public void E_Possivel_Mapear_os_Modelos_Municipio() {
            var model = new MunicipioModel() {
                Id = Guid.NewGuid(),
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var listaEntity = new List<MunicipioEntity>();
            for (int i = 0; i < 5; i++) {
                var item = new MunicipioEntity() {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = Guid.NewGuid(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    Uf = new UfEntity 
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3),
                    }
                };
                listaEntity.Add(item);
            }

            // Model => Entity
            var entity = Mapper.Map<MunicipioEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Nome, model.Nome);
            Assert.Equal(entity.CodIBGE, model.CodIBGE);
            Assert.Equal(entity.UfId, model.UfId);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            // Entity => Dto
            var dto = Mapper.Map<MunicipioDto>(entity);
            Assert.Equal(dto.Id, entity.Id);
            Assert.Equal(dto.Nome, entity.Nome);
            Assert.Equal(dto.CodIBGE, entity.CodIBGE);
            Assert.Equal(dto.UfId, entity.UfId);

            var dtoCompleto = Mapper.Map<MunicipioDtoCompleto>(listaEntity.FirstOrDefault());
            Assert.Equal(dtoCompleto.Id, listaEntity.FirstOrDefault().Id);
            Assert.Equal(dtoCompleto.Nome, listaEntity.FirstOrDefault().Nome);
            Assert.Equal(dtoCompleto.CodIBGE, listaEntity.FirstOrDefault().CodIBGE);
            Assert.Equal(dtoCompleto.UfId, listaEntity.FirstOrDefault().UfId);
            Assert.NotNull(dtoCompleto.Uf);

            var listaDto = Mapper.Map<List<MunicipioDtoCompleto>>(listaEntity);
            Assert.True(listaDto.Count() == listaEntity.Count());
            for (int i = 0; i < listaDto.Count(); i++) {
                Assert.Equal(listaDto[i].Id, listaEntity[i].Id);
                Assert.Equal(listaDto[i].Nome, listaEntity[i].Nome);
                Assert.Equal(listaDto[i].CodIBGE, listaEntity[i].CodIBGE);
                Assert.Equal(listaDto[i].UfId, listaEntity[i].UfId);
            }

            var dtoCreateResult = Mapper.Map<MunicipioDtoCreateResult>(entity);
            Assert.Equal(dtoCreateResult.Id, entity.Id);
            Assert.Equal(dtoCreateResult.Nome, entity.Nome);
            Assert.Equal(dtoCreateResult.CodIBGE, entity.CodIBGE);
            Assert.Equal(dtoCreateResult.UfId, entity.UfId);
            Assert.Equal(dtoCreateResult.CreateAt, entity.CreateAt);

            var dtoUpdateResult = Mapper.Map<MunicipioDtoUpdateResult>(entity);
            Assert.Equal(dtoUpdateResult.Id, entity.Id);
            Assert.Equal(dtoUpdateResult.Nome, entity.Nome);
            Assert.Equal(dtoUpdateResult.CodIBGE, entity.CodIBGE);
            Assert.Equal(dtoUpdateResult.UfId, entity.UfId);
            Assert.Equal(dtoUpdateResult.UpdateAt, entity.UpdateAt);

            // Dto => Model
            var municipioModel = Mapper.Map<MunicipioModel>(dto);
            Assert.Equal(municipioModel.Id, dto.Id);
            Assert.Equal(municipioModel.Nome, dto.Nome);
            Assert.Equal(municipioModel.CodIBGE, dto.CodIBGE);
            Assert.Equal(municipioModel.UfId, dto.UfId);

            var dtoCreate = Mapper.Map<MunicipioDtoCreate>(municipioModel);
            Assert.Equal(dtoCreate.Nome, municipioModel.Nome);
            Assert.Equal(dtoCreate.CodIBGE, municipioModel.CodIBGE);
            Assert.Equal(dtoCreate.UfId, municipioModel.UfId);

            var dtoUpdate = Mapper.Map<MunicipioDtoUpdate>(municipioModel);
            Assert.Equal(dtoUpdate.Id, municipioModel.Id);
            Assert.Equal(dtoUpdate.Nome, municipioModel.Nome);
            Assert.Equal(dtoUpdate.CodIBGE, municipioModel.CodIBGE);
            Assert.Equal(dtoUpdate.UfId, municipioModel.UfId);
        }
    }
}
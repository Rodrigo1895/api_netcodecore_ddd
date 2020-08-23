using System;
using System.Collections.Generic;
using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;

namespace Api.Service.Test.Cep {
    public class CepTestes {
        public static Guid IdCep { get; set; }
        public static string Cep { get; set; }
        public static string Logradouro { get; set; }
        public static string Numero { get; set; }
        public static string CepAlterado { get; set; }
        public static string LogradouroAlterado { get; set; }
        public static string NumeroAlterado { get; set; }
        public static Guid IdMunicipio { get; set; }

        public List<CepDto> listaDto = new List<CepDto>();
        public CepDto cepDto;
        public CepDtoCreate cepDtoCreate;
        public CepDtoCreateResult cepDtoCreateResult;
        public CepDtoUpdate cepDtoUpdate;
        public CepDtoUpdateResult cepDtoUpdateResult;

        public CepTestes() {
            IdCep = Guid.NewGuid();
            Cep = Faker.RandomNumber.Next(10000, 99999).ToString();
            Logradouro = Faker.Address.StreetAddress();
            Numero = Faker.RandomNumber.Next(1, 1000).ToString();
            CepAlterado = Faker.RandomNumber.Next(10000, 99999).ToString();
            LogradouroAlterado = Faker.Address.StreetAddress();
            NumeroAlterado = Faker.RandomNumber.Next(1, 1000).ToString();
            IdMunicipio = Guid.NewGuid();

            for (int i = 0; i < 10; i++) {
                var dto = new CepDto() {
                    Id = Guid.NewGuid(),
                    Logradouro = Faker.Address.StreetAddress(),
                    Numero = Faker.RandomNumber.Next(1, 1000).ToString(),
                    MunicipioId = Guid.NewGuid(),
                    Municipio = new MunicipioDtoCompleto() {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid(),
                    Uf = new UfDto() {
                    Id = Guid.NewGuid(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    Nome = Faker.Address.UsState()
                    }
                    }
                };

                listaDto.Add(dto);
            }

            cepDto = new CepDto() {
                Id = IdCep,
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = IdMunicipio,
            };

            cepDtoCreate = new CepDtoCreate() {
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = IdMunicipio,
            };

            cepDtoCreateResult = new CepDtoCreateResult() {
                Id = IdCep,
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = IdMunicipio,
                CreateAt = DateTime.UtcNow
            };

            cepDtoUpdate = new CepDtoUpdate() {
                Id = IdCep,
                Cep = CepAlterado,
                Logradouro = LogradouroAlterado,
                Numero = NumeroAlterado,
                MunicipioId = IdMunicipio,
            };

            cepDtoUpdateResult = new CepDtoUpdateResult() {
                Id = IdMunicipio,
                Cep = CepAlterado,
                Logradouro = LogradouroAlterado,
                Numero = NumeroAlterado,
                MunicipioId = IdMunicipio,
                UpdateAt = DateTime.UtcNow
            };
        }
    }
}
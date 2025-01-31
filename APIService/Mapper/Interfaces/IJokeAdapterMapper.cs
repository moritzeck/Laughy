﻿using Laughy.Adapter.ApiService.ApiModels;
using Laughy.Models.DomainModels;

namespace Laughy.Adapter.ApiService.Mapper.Interfaces
{
    public interface IJokeAdapterMapper
    {
        JokeApiModel MapToApiModel(JokeDomainModel jokeDomainModel);

        JokeDomainModel MapToDomainModel(JokeApiModel jokeApiModel);
    }
}

﻿using System.Net.Http;
using System.Threading.Tasks;
using ImageHuntTelegramBot.Responses;

namespace ImageHuntTelegramBot.WebServices
{
  public class GameWebService : AbstractWebService, IGameWebService
  {
    public async Task<GameResponse> GetGameById(int gameId)
    {
      return await GetAsync<GameResponse>($"{_httpClient.BaseAddress}api/Game/ById/{gameId}");
    }

    public GameWebService(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task StartGameForTeam(int gameId, int teamId)
    {
      await PutAsync($"{_httpClient.BaseAddress}api/Team/StartTeam/{gameId}/{teamId}");
    }
  }
}
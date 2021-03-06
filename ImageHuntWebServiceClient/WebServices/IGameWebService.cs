﻿using System.Threading;
using System.Threading.Tasks;
using ImageHuntWebServiceClient.Responses;

namespace ImageHuntWebServiceClient.WebServices
{
  public interface IGameWebService
  {
    Task<GameResponse> GetGameById(int gameId, CancellationToken cancellationToken = default (CancellationToken));
    Task<NodeResponse> StartGameForTeam(int gameId, int teamId, CancellationToken cancellationToken=default (CancellationToken));
  }
}
using ImageHunt.Model;
using ImageHunt.Model.Node;

namespace ImageHunt.Services
{
  public interface IPlayerService
  {
    Player CreatePlayer(string name, string chatLogin);
    Player JoinTeam(int teamId, int playerId);
    Player GetPlayerById(int playerId);
    Player GetPlayerByChatId(string chatId);
  }
}

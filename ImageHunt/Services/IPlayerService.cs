using ImageHunt.Model;
using ImageHunt.Model.Node;

namespace ImageHunt.Services
{
  public interface IPlayerService
  {
    Player CreatePlayer(string name, string chatLogin);
    Player JoinTeam(int teamId, int playerId);
    void StartPlayer(string name);
    Node NextNodeForPlayer(string playerName, double playerLatitude, double playerLongitude);
    void UploadImage(string playerName, double latitude, double longitude, byte[] image);
    Player GetPlayerById(int playerId);
    Player GetPlayerByChatId(string chatId);
  }
}

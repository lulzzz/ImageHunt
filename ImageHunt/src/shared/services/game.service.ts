import { Injectable } from "@angular/core";
import { JwtHttp } from "ng2-ui-auth";
import { Game } from "../game";
import { Node } from "../node";
import { Http, RequestOptions, Headers } from "@angular/http";
import {NodeRequest} from "../nodeRequest";
import { Observable } from "rxjs/Observable";

@Injectable()
export class GameService {
  constructor(private http: Http,
    private jwtHttp: JwtHttp) { }
  getGameForAdmin(adminId: number) {
    return this.jwtHttp.get('api/game/ByAdminId/' + adminId);
  }
  getGameById(gameId: number) {
    return this.jwtHttp.get('api/game/byId/' + gameId).map(g => g.json());
  }
  createGame(adminId: number, game: Game) {
    return this.jwtHttp.post('api/game/' + adminId, game);
  }
  addNode(gameId: number, node: NodeRequest) {
    return this.jwtHttp.post(`api/game/AddNode/${gameId}`, node);
  }
  upload(files: File[], gameId) {
    let headers = new Headers();
    headers.delete('Content-Type');
    const formData = new FormData();
    for (var file of files) {
      formData.append("files", file);
    }
    let options = new RequestOptions({ headers: headers });

    return this.http.put(`api/game/AddPictures/${gameId}`, formData, options);
  }

  centerMap(gameId: number) {
    return this.jwtHttp.post(`api/game/CenterGameByNodes/${gameId}`, null);

  }

  getNodeRelations(gameId: number) {
     return this.jwtHttp.get(`api/game/NodesRelations/${gameId}`);
  }

  addRelation(orgNodeId: number, destNodeId: number) {

    return this.jwtHttp.post("api/node/AddRelationToNode", { nodeId: orgNodeId, childrenId: destNodeId });

  }
  removeRelation(orgNodeId: number, destNodeId: number) {
    return this.jwtHttp.post("api/node/RemoveRelationToNode", { nodeId: orgNodeId, childrenId: destNodeId });
  }

  setZoom(gameId: number, zoom: number) { return this.jwtHttp.patch(`api/game/UpdateZoom/${gameId}/${zoom}`, null); }
  getQuestionNodesOfGame(gameId: number)  {
    return this.jwtHttp.get(`api/game/GetQuestionNodeOfGame/${gameId}`).map(j=>j.json());
  }
}

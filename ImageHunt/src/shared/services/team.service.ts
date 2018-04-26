import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import { JwtHttp } from "ng2-ui-auth";
import {Player} from "../player";
import {Team} from "../team";

@Injectable()
export class TeamService {
  constructor(private http: JwtHttp) { }
  getTeams(gameId: number) {
    return this.http.get('api/Team/ByGame/' + gameId);

  }
  getTeam(teamId) {
    return this.http.get('api/Team/' + teamId);
  }
  addMemberToTeam(teamId: number, player: Player) {
    return this.http.post('api/Team/AddPlayer/' + teamId, player);
  }
  removePlayerToTeam(teamId: number, player: Player) {
    return this.http.delete('api/Team/Remove/' + teamId + '/' + player.id);
  }
  createTeam(gameId: number, team: Team) {
    return this.http.post('api/Team/' + gameId, team);
  }
  deleteTeam(teamId: number) {
    return this.http.delete('api/Team/' + teamId);
  }
}

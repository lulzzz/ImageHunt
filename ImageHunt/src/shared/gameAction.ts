import {Game} from "./game";
import {Node} from "./node";
import { Team } from "./team";
import { Picture } from "./picture";

export class GameAction {
  id: number;
  dateOccured: Date;
  latitude: number;
  longitude: number;
  game: Game;
  team: Team;
  action: string;
  node: Node;
  isValidated: boolean;
  delta: number;
  picture: Picture;
}

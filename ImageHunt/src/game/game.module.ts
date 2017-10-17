import { NgModule } from '@angular/core';
import {CommonModule} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import {CalendarModule} from "primeng/components/calendar/calendar";
import {GameCreateComponent} from "./game-create/game.create.component";
import {GameService} from "../shared/services/game.service";
import {MapModule} from "../map/map.module";
import {BsDropdownModule, TabsModule, AlertModule, BsDatepickerModule } from "ngx-bootstrap";
import {GameDetailComponent} from "./game-detail/game.detail.component";
import {TeamService} from "../shared/services/team.service";
import {AlertService} from "../shared/services/alert.service";
import {GameListComponent} from "./game-list/game-list.component";
import {NodeCreateComponent} from "./node-create/node.create.component";
import {NodeListComponent} from "./node-list/node.list.component";
import {NodeRelationComponent} from "./node-relation/node.relation.component";
import { BrowserModule } from "@angular/platform-browser";

@NgModule({
  imports: [CommonModule, FormsModule, RouterModule, CalendarModule, CommonModule, RouterModule, FormsModule,
    MapModule, BsDropdownModule, TabsModule, AlertModule, BsDatepickerModule, BrowserModule],
  declarations: [GameCreateComponent, GameDetailComponent, GameListComponent, NodeCreateComponent, NodeListComponent, NodeRelationComponent],
  exports: [GameCreateComponent, GameDetailComponent, GameListComponent, NodeCreateComponent, NodeListComponent, NodeRelationComponent],
  bootstrap: [GameCreateComponent, GameDetailComponent, GameListComponent, NodeCreateComponent, NodeListComponent, NodeRelationComponent],
  providers: [GameService, TeamService, AlertService]
})
export class GameModule
{
}

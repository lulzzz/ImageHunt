import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import {GameListComponent} from "./game-list.component";
import {GameCreateModule} from "../game-create/game.create.module";
import { FormsModule } from "@angular/forms";
import { BsDatepickerModule, TimepickerModule} from 'ngx-bootstrap';

@NgModule({
  imports: [CommonModule, GameCreateModule, RouterModule, FormsModule, BsDatepickerModule, TimepickerModule],
declarations:[GameListComponent],
exports: [GameListComponent]
})
export class GameListModule
{
}

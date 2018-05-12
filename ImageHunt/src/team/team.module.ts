import { NgModule } from '@angular/core';
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import {TeamDetailComponent} from "./team-detail/team.detail.component";
import {TeamService} from "../shared/services/team.service";
import {TeamListComponent} from "./team-list/team-list.component";
import { ConfirmDialogModule } from "primeng/confirmdialog";
import {ConfirmationService} from "primeng/api";
import { TeamCreateComponent } from "./team-create/team-create.component";
import { PanelModule } from "primeng/panel";
import {QRCodeModule } from "angular2-qrcode";

@NgModule({
  imports: [CommonModule, RouterModule, FormsModule, ConfirmDialogModule,
    PanelModule, QRCodeModule,
    ReactiveFormsModule],
  declarations: [TeamDetailComponent, TeamListComponent, TeamCreateComponent],
  exports: [TeamDetailComponent, TeamListComponent, TeamCreateComponent],
  providers: [TeamService, ConfirmationService]})
export class TeamModule
{
}

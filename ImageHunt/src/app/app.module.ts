import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgmCoreModule } from '@agm/core';
import { LocalStorageModule } from 'angular-2-local-storage';
import { AppComponent } from './app.component';
import { BsDatepickerModule, TimepickerModule, BsDropdownModule, ModalModule, TabsModule } from 'ngx-bootstrap';
import {AlertModule } from 'ngx-bootstrap/alert';

import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule } from '@angular/http';
import { Ng2UiAuthModule, CustomConfig } from 'ng2-ui-auth';
import { HomeModule } from "../home/home.module";
import { PageNotFoundModule } from "../page-not-found/page.not.found.module";
import { TeamModule } from "../team/team.module";
import { TeamListComponent } from "../team/team-list/team-list.component";
import { HomeComponent } from "../home/home.component";
import { PageNotFoundComponent } from "../page-not-found/page.not.found.component";
import { NavmenuModule } from "../navmenu/navmenu.module";
import {AdminListModule} from "../admin/admin-list/admin-list.module";
import {AdminListComponent} from "../admin/admin-list/admin-list.component";
import {GameModule} from "../game/game.module";
import {GameListComponent} from "../game/game-list/game-list.component";
import {GameDetailComponent} from "../game/game-detail/game.detail.component";
import {GoogleButtonModule} from "../shared/google-button/google.button.module";
import {environment} from "../environments/environment";
import {Globals} from "../shared/globals";
import { TeamDetailComponent } from "../team/team-detail/team.detail.component";
import {MapModule} from "../map/map.module";
import {MapDetailComponent} from "../map/map-detail/map-detail.component";
import { ContextMenuModule } from "primeng/primeng";
import {NodeRelationComponent} from "../game/node-relation/node.relation.component";
import {NodeCreateComponent} from "../game/node-create/node.create.component";
import { QuestionNodeComponent } from "../game/question-node/question.node.component";

export class MyAuthConfig extends CustomConfig {
  defaultHeaders = { 'Content-Type': 'application/json' };
  providers = {
    google: { clientId: environment.GOOGLE_CLIENT_ID },
  };
  tokenName = 'accessToken';
  tokenPrefix = '';
  baseUrl = environment.API_ENDPOINT;
}
@NgModule({
  declarations: [
    AppComponent
  ],
  bootstrap: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    LocalStorageModule.withConfig({
      prefix: 'Img-Hunt',
      storageType: 'localStorage'
    }),
    AgmCoreModule.forRoot({apiKey:environment.GOOGLE_MAP_API_KEY}),
    FormsModule,
    HttpModule,
    HomeModule,
    GameModule,
    TeamModule,
    MapModule,
    AdminListModule,
    ContextMenuModule,
    NavmenuModule,
    PageNotFoundModule,
    GoogleButtonModule,
    Ng2UiAuthModule.forRoot(MyAuthConfig),
    BsDatepickerModule.forRoot(),
    BsDropdownModule.forRoot(),
    AlertModule.forRoot(),
    TabsModule.forRoot(),
    ModalModule.forRoot(),
    //TimepickerModule.forRoot(),
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent },
      { path: 'game', component: GameListComponent },
      { path: 'game/:gameId', component: GameDetailComponent },
      { path: 'team/:teamId', component: TeamDetailComponent },
      { path: 'team', component: TeamListComponent },
      { path: 'map', component: MapDetailComponent },
      { path: 'admin', component: AdminListComponent },
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: '**', component: PageNotFoundComponent }])
  ],
  providers: [Globals],
  entryComponents: [NodeCreateComponent, NodeRelationComponent, QuestionNodeComponent]

})
export class AppModule {
} 

import { Component, OnInit } from '@angular/core';
import {AuthService} from "ng2-ui-auth";
import {LocalStorageService} from "angular-2-local-storage";

@Component({
    selector: 'navmenu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.scss']
})
/** navmenu component*/
export class NavmenuComponent implements OnInit
{

  get isAuthenticated(): boolean { return <boolean>(this.localStorageService.get('isAuthenticated')); }

  isCollapsed: boolean;
    /** navmenu ctor */
  constructor(private auth: AuthService, private localStorageService: LocalStorageService) { }

    /** Called by Angular after navmenu component initialized */
    ngOnInit(): void {
    }
}

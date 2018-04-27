import { Component, Input, Output, EventEmitter, OnInit, OnDestroy } from '@angular/core';

@Component({
    selector: 'node-create2',
    templateUrl: './node-create2.component.html',
    styleUrls: ['./node-create2.component.scss']
})
/** nodeCreate2 component*/
export class NodeCreate2Component implements OnInit, OnDestroy {

  @Output() displayChange = new EventEmitter();
  @Input() display:boolean;
    /** nodeCreate2 ctor */
    constructor() {

    }
    ngOnInit(): void { }

  onClose() {
    this.displayChange.emit(false);
  }
  ngOnDestroy(): void {
    this.displayChange.unsubscribe();
  }
}

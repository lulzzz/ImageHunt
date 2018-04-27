/// <reference path="../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { NodeCreate2Component } from './node-create2.component';

let component: NodeCreate2Component;
let fixture: ComponentFixture<NodeCreate2Component>;

describe('nodeCreate2 component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ NodeCreate2Component ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(NodeCreate2Component);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
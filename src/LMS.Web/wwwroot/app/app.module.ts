import { NgModule }      from '@angular/core';
import { FormsModule }      from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { LmsAppComponent }   from './components/lms.app.component';
import { HomepageComponent }      from './components/homepage.component';

import { routing } from './app.routing';

@NgModule({
    imports: [  BrowserModule,
                FormsModule,
                routing],
    declarations: [ LmsAppComponent,
                    HomepageComponent],
  bootstrap: [LmsAppComponent ]
})
export class AppModule { }
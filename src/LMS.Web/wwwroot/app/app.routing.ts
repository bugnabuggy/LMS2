import { ModuleWithProviders }  from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LmsAppComponent }      from './components/lms.app.component';
import { HomepageComponent }      from './components/homepage.component';

const appRoutes: Routes = [
    {
        path: '',
        component: HomepageComponent
    }
    
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);


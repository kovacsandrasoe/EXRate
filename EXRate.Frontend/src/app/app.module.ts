import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';

import { AppComponent } from './app.component';
import { EditComponent } from './components/edit/edit.component';
import { CreateComponent } from './components/create/create.component';
import { RateComponent } from './components/rate/rate.component';
import { ListComponent } from './components/list/list.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AuthService } from './services/auth.service';

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'list', component: ListComponent, canActivate: [AuthService] },
  {path: 'create', component: CreateComponent, canActivate: [AuthService]},
  {path: 'create/:id', component: CreateComponent, canActivate: [AuthService]},
  {path: 'edit/:id', component: EditComponent, canActivate: [AuthService]},
  {path: 'rate', component: RateComponent, canActivate: [AuthService]}
];


@NgModule({
  declarations: [
    AppComponent,
    EditComponent,
    CreateComponent,
    RateComponent,
    ListComponent,
    RegisterComponent,
    LoginComponent,

  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

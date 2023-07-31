import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { Loginmodel } from 'src/app/viewmodels/loginmodel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  lm: Loginmodel = new Loginmodel();
  isError: boolean = false;
  constructor(private auth: AuthService, private router: Router) { }

  login(){
    this.auth.login(this.lm).catch(t => {
      this.isError = true;
    }).then(t => {
      if (!this.isError){
        this.router.navigate(['list']);
      }
    })
  }

  clear(){
    if (this.isError){
      this.lm = new Loginmodel();
      this.isError = false;
    }
  }

  ngOnInit(): void {
  }

}

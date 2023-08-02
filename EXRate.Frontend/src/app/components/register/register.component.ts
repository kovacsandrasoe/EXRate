import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { Registermodel } from 'src/app/viewmodels/registermodel';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  rm: Registermodel = new Registermodel();
  isError: boolean = false;
  errorMsg: string = '';
  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  register(){
    if (this.rm.firstName.length > 3 && this.rm.lastName.length > 3)
    {
      this.auth.register(this.rm).catch(t => {
        console.log(t);
        this.isError = true;
        this.errorMsg = t.error.message;
      }).then(t => {
        if (!this.isError){
          this.router.navigate(['login']);
        }
      })
    }
    else{
      this.isError = true;
      this.errorMsg = 'First name is required,Last name is required';
    }
  }

}

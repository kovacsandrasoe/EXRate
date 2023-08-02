import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Tokenmodel } from '../viewmodels/tokenmodel';
import { Loginmodel } from '../viewmodels/loginmodel';
import { throwError } from 'rxjs';
import { CanActivate, Router } from '@angular/router';
import { Registermodel } from '../viewmodels/registermodel';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements CanActivate {

  token: string = '';
  expiration: Date = new Date();

  private url: string = 'https://localhost:7050/auth';
  constructor(private http: HttpClient, private router: Router) { }

  async login(lm: Loginmodel) {
    return new Promise<void>((resolve, reject) => {
      this.http.post<Tokenmodel>(this.url, lm).subscribe({
        next: (v) => {
          this.saveToken(v);
          resolve();
        },
        error: (e) => reject()
      })
    })
  }

  async register(rm: Registermodel) {
    return new Promise<void>((resolve, reject) => {
      this.http.put(this.url, rm).subscribe({
        next: (v) => {
          resolve();
        },
        error: (e) => reject(e)
      })
    })
  }

  isLoggedIn(): boolean{
    return localStorage.getItem('token') != null;
  }

  logout(){
    localStorage.clear();
    this.router.navigate(['login']);
  }

  canActivate(): boolean {
    if (!this.isLoggedIn()) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }

  getToken(){
    return localStorage.getItem('token') ?? '';
  }

  saveToken(tm: Tokenmodel) {
    tm.expiration = new Date(tm.expiration); //ugly, but effective!
    localStorage.setItem('token', tm.token);    
  }

}

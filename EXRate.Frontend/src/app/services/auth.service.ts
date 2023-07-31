import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Tokenmodel } from '../viewmodels/tokenmodel';
import { Loginmodel } from '../viewmodels/loginmodel';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  token: string = '';
  expiration: Date = new Date();

  private url: string = 'https://localhost:7050/auth';
  constructor(private http: HttpClient) { }

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

  isLoggedIn(): boolean{
    return this.token != '';
  }



  saveToken(tm: Tokenmodel) {
    this.token = tm.token;
    this.expiration = new Date(tm.expiration);
  }

}

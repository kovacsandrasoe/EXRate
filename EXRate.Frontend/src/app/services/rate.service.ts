import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Rate } from '../models/rate';
import { Raterecord } from '../models/raterecord';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RateService {

  public rates : Rate[] = [];

  private url: string = 'https://localhost:7050/rate';
  constructor(private auth: AuthService, private http: HttpClient) {
    
  }

  findByCurrency(curr: string){
    return this.rates.find(t => t.current.toUpperCase() == curr.toUpperCase());
  }

  getHeaders(){
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + this.auth.token
    });
  }

  addRecord(record: Raterecord){
    return new Promise<void>((resolve, reject) => {
      this.http.post(this.url, record, {headers: this.getHeaders()}).subscribe({
        next: (v) => {
          resolve();
        },
        error: (e) => reject()
      })
    })
  }

  getRates(){
    return new Promise<Rate[]>((resolve, reject) => {
      this.http.get<Rate[]>(this.url).subscribe({
        next: (v) => {
          this.rates = v;
          resolve(v);
        },
        error: (e) => reject()
      })
    })
  }

}

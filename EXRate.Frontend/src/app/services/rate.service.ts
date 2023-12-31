import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Rate } from '../models/rate';
import { Raterecord } from '../models/raterecord';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RateService {

  private url: string = 'https://localhost:7050/rate';
  private urlRecord: string = 'https://localhost:7050/raterecord';
  
  constructor(private auth: AuthService, private http: HttpClient) {
    
  }

  findByCurrency(curr: string){
    return new Promise<Rate>((resolve, reject) => {
      this.getRates().subscribe(t => {
        let r = t.find(z => z.currency.toUpperCase() == curr.toUpperCase());
        if (r != undefined){
          resolve(r);
        }
        else{
          reject();
        }
      })
    })
  }

  findRecordById(id: string){
    return new Promise<Raterecord>((resolve, reject) => {
      this.getRecords().subscribe(t => {
        let r = t.find(z => z.id == id);
        if (r != undefined){
          resolve(r);
        }
        else{
          reject();
        }
      })
    })
  }

  getHeaders(){
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + this.auth.getToken()
    });
  }

  addRecord(record: Raterecord){
    return new Promise<void>((resolve, reject) => {
      this.http.post(this.urlRecord, record, {headers: this.getHeaders()}).subscribe({
        next: (v) => {
          resolve();
        },
        error: (e) => reject()
      })
    })
  }

  updateRecord(record: Raterecord){
    return new Promise<void>((resolve, reject) => {
      this.http.put(this.urlRecord, record, {headers: this.getHeaders()}).subscribe({
        next: (v) => {
          resolve();
        },
        error: (e) => reject()
      })
    })
  }

  removeRecord(id: string){
    return new Promise<void>((resolve, reject) => {
      this.http.delete(this.urlRecord + '/' + id, {headers: this.getHeaders()}).subscribe({
        next: (v) => {
          resolve();
        },
        error: (e) => reject()
      })
    })
  }

  getRecords(){
    return this.http.get<Raterecord[]>(this.urlRecord, {headers: this.getHeaders()});
  }

  getRates(){
    return this.http.get<Rate[]>(this.url, {headers: this.getHeaders()});
  }

}

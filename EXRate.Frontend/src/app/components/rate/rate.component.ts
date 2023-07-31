import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Rate } from 'src/app/models/rate';
import { RateService } from 'src/app/services/rate.service';

@Component({
  selector: 'app-rate',
  templateUrl: './rate.component.html',
  styleUrls: ['./rate.component.css']
})
export class RateComponent implements OnInit {

  rates !: Observable<Rate[]>;
  constructor(public rateService: RateService) {
    this.rates = rateService.getRates();
  }

  ngOnInit(): void {
    this.rateService.getRates();
  }

  getWidth(): number{
    return window.innerWidth;
  }

}

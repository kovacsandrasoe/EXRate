import { Component, OnInit } from '@angular/core';
import { RateService } from 'src/app/services/rate.service';

@Component({
  selector: 'app-rate',
  templateUrl: './rate.component.html',
  styleUrls: ['./rate.component.css']
})
export class RateComponent implements OnInit {

  constructor(public rateService: RateService) {
    
  }

  ngOnInit(): void {
    this.rateService.getRates();
  }

  getWidth(): number{
    return window.innerWidth;
  }

}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Rate } from 'src/app/models/rate';
import { Raterecord } from 'src/app/models/raterecord';
import { RateService } from 'src/app/services/rate.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  rate : Rate = new Rate();
  selectedCurrency: string = '';
  withParam: boolean = false;
  record: Raterecord = new Raterecord();

  constructor(private route: ActivatedRoute, public rateService: RateService) {
    rateService.getRates().then(z => {
      this.route.params.subscribe(t => {
        this.rate = rateService.findByCurrency(t['id']) ?? new Rate();
        this.withParam = true;
      });
    });
  }

  updateValue(){
    this.rate = this.rateService.findByCurrency(this.selectedCurrency) ?? new Rate();
  }

  send(){
    this.record.current = this.rate.current;
    
  }

  ngOnInit(): void {
  }

}

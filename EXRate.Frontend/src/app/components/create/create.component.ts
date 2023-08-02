import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
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
  rates !: Observable<Rate[]>;

  constructor(private route: ActivatedRoute, public rateService: RateService, private router: Router) {
    this.rates = rateService.getRates();
    rateService.getRates().subscribe(z => {
      this.route.params.subscribe(t => {
        if (t['id'] != undefined){
          rateService.findByCurrency(t['id']).then(z => {
            this.rate = z;
            if (this.rate.currency == ''){
              this.router.navigate(['rate']);
            }
            this.withParam = true;
          });
        }
      });
    });
  }

  updateValue(){
    this.rateService.findByCurrency(this.selectedCurrency).then(z => {
      this.rate = z;
    });
  }

  send(){
    this.record.currency = this.rate.currency;
    this.rateService.addRecord(this.record).then(z => {
      this.router.navigate(['list']);
    })
  }

  ngOnInit(): void {
  }

}

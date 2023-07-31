import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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

  constructor(private route: ActivatedRoute, public rateService: RateService, private router: Router) {
    rateService.getRates().subscribe(z => {
      this.route.params.subscribe(t => {
        rateService.findByCurrency(t['id']).then(z => {
          this.rate = z;
          if (this.rate.current == ''){
            this.router.navigate(['rate']);
          }
          this.withParam = true;
        });
      });
    });
  }

  updateValue(){
    this.rateService.findByCurrency(this.selectedCurrency).then(z => {
      this.rate = z;
    });
  }

  send(){
    this.record.currency = this.rate.current;
    this.rateService.addRecord(this.record);
  }

  ngOnInit(): void {
  }

}

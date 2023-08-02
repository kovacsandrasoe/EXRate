import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Rate } from 'src/app/models/rate';
import { Raterecord } from 'src/app/models/raterecord';
import { RateService } from 'src/app/services/rate.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  record: Raterecord = new Raterecord();
  rates !: Observable<Rate[]>;

  constructor(private route: ActivatedRoute, public rateService: RateService, private router: Router) {
    this.rates = rateService.getRates();
    rateService.getRates().subscribe(z => {
      this.route.params.subscribe(t => {
        if (t['id'] != undefined){
          rateService.findRecordById(t['id']).then(z => {
            this.record = z;
            if (this.record.currency == ''){
              this.router.navigate(['list']);
            }
          });
        }
      });
    });
  }

  edit(){
    this.rateService.updateRecord(this.record).then(z => {
      this.router.navigate(['list']);
    })
  }

  ngOnInit(): void {
  }

}

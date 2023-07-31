import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Raterecord } from 'src/app/models/raterecord';
import { RateService } from 'src/app/services/rate.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  records !: Observable<Raterecord[]>;

  constructor(public rateService: RateService) {
    this.records = rateService.getRecords();
  }

  ngOnInit(): void {
  }

}

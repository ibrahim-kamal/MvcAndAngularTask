import { Component,OnInit,ViewChild } from '@angular/core';

import { Chart, registerables} from 'chart.js';
import { HttpClient } from '@angular/common/http';

Chart.register(...registerables);
@Component({
  selector: 'app-dashboard-component',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  
  public data:any;
  public _http ;
  constructor(http: HttpClient) {
    this._http = http;
  }
  ngOnInit(): void {

    this.getData();
  }
  getData(){
    

    this._http.get<any>('https://localhost:7196/reservation/getDoctorReservationNumber').subscribe(result => {
      this.data = result;
      this.getChart();
    }, error => console.error(error));
  }

  getChart(){
    const labels = this.data.names;
    const data = {
      labels: labels,
      datasets: [{
        label:"Reservation Number By Doctors",
        data: this.data.numbers,
        fill: false,
        // borderColor: 'rgb(75, 192, 192)',
        tension: 0.1
      }]
    };
    
    new Chart("myChart", {
      type: 'line',
      data: data,
    });
    
  }
}

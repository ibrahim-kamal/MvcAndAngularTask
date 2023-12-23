import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html'
})
export class DoctorComponent {
  public Reservations:any;
  public _http ;
  public myDate:any ;
  constructor(http: HttpClient) {
    const today = new Date();
    this.myDate = today.toISOString().split('T')[0];
    this._http = http;
  }

  getData(SearchResult:any){
    
    let data:FormData = new FormData();
    data.append('DoctorId' , SearchResult.DoctorId);
    data.append('fromDate' , SearchResult.fromDate);
    data.append('toDate' , SearchResult.toDate);
    this._http.post<any>('https://localhost:7196/Reservation/getDoctorReservation' , data).subscribe(result => {
      this.Reservations = result;
    }, error => console.error(error))
  }


}



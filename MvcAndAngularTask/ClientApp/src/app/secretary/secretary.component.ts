import { Component ,Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-secretary',
  templateUrl: './secretary.component.html',
  styleUrls: ['./secretary.component.css']
})
export class SecretaryComponent {

  public doctors:any;
  public Reservations:any;
  public slots:any;
  public myDate;
  public totime = "";
  public _http:HttpClient;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._http = http;
    this.myDate = new Date();
    this.getDoctors()
    this.getReservations()
  }
  getDoctors(){

    this._http.get<any>('https://localhost:7196/Doctors').subscribe(result => {
      this.doctors = result;
    }, error => console.error(error));
  }
  getReservations(){
    this._http.get<any>('https://localhost:7196/Reservation/getReservation').subscribe(result => {
      this.Reservations = result;
    }, error => console.error(error));
  }

  getDoctorFreeAppoinment(doctor:any , date:any){
    if(doctor.value && date.value){
      let data:FormData = new FormData();
      data.append('DoctorId' , doctor.value);
      data.append('date' , date.value);
      this._http.post<any>('https://localhost:7196/Appoinment/AvaliableAppoinment' , data).subscribe(result => {
        this.slots = result;
      }, error => console.error(error))
    }
    else{
      this.slots = [];
    }
  }
  
  selectTime(time:string){
    let from = time.split(':');
    var m = parseInt(from[1]) + 30;
    if(m == 60){
      var h = parseInt(from[0]) + 1;
      this.totime  = h+":00:00";
    }
    else{
      this.totime  = from[0]+":30:00";
      
    }
    // this.totime = time;
  }
  reservation(form_data:any){
    
    var reservationData:FormData = new FormData();
    
    // for( let key in formData){

    for (const key in form_data) {
        
      reservationData.append(key+"" , form_data[key]+"") ;
    }
    
    reservationData.append("To" , this.totime) ;
    
    this._http.post<any>('https://localhost:7196/Reservation/Reservation' , reservationData).subscribe(result => {
      this.getReservations();
      alert(result.message);
      if(result.status == 1){

      }
    }, error => console.error(error))
  }
}





interface doctors {
  Name: string;
  DoctorId: string;
  Id: number;
}

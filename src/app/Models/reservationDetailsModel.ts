export class DateOnly{
    year:number;
    month:number;
    day:number;

    constructor(year:number,month:number,day:number){
        this.year=year;
        this.month=month;
        this.day = day;
    }
}
export class reservationDetailsModel {
    Email :string;
    LicensePlate:string ;
    PickUpDate :DateOnly;
    DropOffDate :DateOnly;

     constructor(){
        this.Email='';
        this.LicensePlate='';
        this.PickUpDate= new DateOnly(1,1,1);
        this.DropOffDate= new DateOnly(1,1,1);
     }
}
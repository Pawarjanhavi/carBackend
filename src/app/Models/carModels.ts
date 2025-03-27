
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
export class carModels {
    make :string;
    model:string;
    year:number; 
    colour:string ;
    totalSeats :number;
    licensePlate:string ;
    pricePerDay:number ;
    imageUrl:string;
    // description:string ;
    availableStatus:boolean ;
    availableDate :DateOnly;
    city:string;
    address :string;
    state :string;
    country :string;
    zipCode:string ;

     constructor(){
        this.make='';
        this.model='';
        this.year=0;
        this.colour='';
        this.totalSeats=0;
        this.licensePlate='';
        this.pricePerDay=0;
        this.imageUrl=''
        // this.description='';
        this.availableStatus=false;
        this.availableDate= new DateOnly(1,1,1);
        this.city='';
        this.address='';
        this.state='';
        this.country='';
        this.zipCode='';
     }
}
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
export class reviewModel{
    make: string='';
    model: string='';
    year: number = 0;
    colour: string='';
    totalSeats: number = 0;
    licensePlate: string='';
    pricePerDay: number = 0;
    imageUrl: string='';
    availableStatus: boolean = false;
    availableDate: DateOnly = new DateOnly(1,1,1);
    city: string='';
    address: string='';
    state: string='';
    country: string='';
    zipCode: string='';
    rating: number = 0;
    totalRatings: number = 0;
  }
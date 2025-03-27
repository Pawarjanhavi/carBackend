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
export class adminReservationDetailsModel{
    reservationStatus: string = '';
    pickUpDate: DateOnly= new DateOnly(1,1,1);
    dropOffDate:DateOnly= new DateOnly(1,1,1);
    userEmail: string = '';
    firstName: string = '';
    lastName: string = '';
    carMake: string = '';
    carModel: string = '';
    carNumber: string = '';
    carColour: string = '';
    totalSeats: number = 0;
    totalAmount: number = 0;
    imageUrl: string = '';
    city: string = '';
    address: string = '';
    state: string = '';
    country: string = '';
    zipCode: string='';
}
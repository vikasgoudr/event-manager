import { Address } from "./address.model";

export class Profile {
    id: number;
    firstName: string;
    lastName: string;
    gender: number;
    rating: number;
    userName: string;
    displayPicture: any;
    phoneNumber: string;
    age: number;
    role:number;
    addressId: number;
    address:Address;
    email:string;


    constructor(data: Profile) {
        this.id = data.id,
        this.firstName=data.firstName,
        this.lastName=data.lastName,
        this.gender=data.gender,
        this.rating=data.rating,
        this.userName=data.userName,
        this.displayPicture=data.displayPicture,
        this.phoneNumber=data.phoneNumber,
        this.age=data.age,
        this.addressId=data.addressId,
        this.email=data.email,
        this.role=data.role,
        this.address=data.address
    }
}
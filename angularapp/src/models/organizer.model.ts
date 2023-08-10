import { Address } from "./address.model";

export interface Organiser {
    id?: number;
    firstName: string
    lastName: string
    gender: number;
    approvalStatus: number;
    userName: string
    phoneNumber: string
    age: number;
    address: Address;
}

export const getDefaultOrganizerData = () => ({
    id: 0,
    firstName: "",
    lastName: "",
    gender: 0,
    approvalStatus: 0,
    userName: "",
    phoneNumber: "",
    age: 0,
    address: {} as Address,
})
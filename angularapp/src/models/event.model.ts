import { Address } from "./address.model";

export class EventDto {
  id?: number;
  name: string;
  startDate: Date;
  endDate: Date;
  organizerId: number;
  ageLimitLower: number;
  ageLimitUpper: number;
  hasAgeLimit: boolean;
  posterImage: any;
  isFreeToAttend: boolean;
  eventCapacity: number;
  isPublished:boolean;
  address: Address;



  constructor(eventDto: EventDto) {
    this.id = eventDto.id;
    this.name = eventDto.name;
    this.startDate = eventDto.startDate;
    this.endDate = eventDto.endDate;
    this.organizerId = eventDto.organizerId;
    this.ageLimitLower = eventDto.ageLimitLower;
    this.ageLimitUpper = eventDto.ageLimitUpper;
    this.hasAgeLimit = eventDto.hasAgeLimit;
    this.posterImage = eventDto.posterImage;
    this.isFreeToAttend = eventDto.isFreeToAttend;
    this.eventCapacity = eventDto.eventCapacity;
    this.isPublished = eventDto.isPublished;
    this.address = eventDto.address;
  }
}

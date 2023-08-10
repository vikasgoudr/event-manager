export interface Question {
  id?: number;
  type: string;
  name: string;
  options?: string;
  defaultOption?: string;
  defaultValue?: string;
  isRequired: boolean;
  eventId: number;
}

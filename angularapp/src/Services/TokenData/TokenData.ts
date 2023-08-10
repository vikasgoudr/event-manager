export class TokenData {
  userId!: number;
  email!: string ;
  name!: string;
  role!: number;
  age!: number ;
  gender!: number;
  approvalStatus!: number;
  

  constructor(userId: number, email: string, name: string, role: number, age: number, gender: number, approvalStatus: number) {
    this.userId = userId;
    this.email = email;
    this.name = name;
    this.role = role;
    this.age = age;
    this.gender = gender;
    this.approvalStatus = approvalStatus;
  }
}

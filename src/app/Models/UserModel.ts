export class UserModel {
    firstName: string;
    lastName: string | null;
    email: string | null;
    password: string | null; // Nullable or optional, depending on use case
    phoneNumber: string | null;
    roleType: string[];
    accountStatus: string;
  
    constructor(
      firstName: string = '',
      lastName: string | null = null,
      email: string | null = null,
      password: string | null = null,
      phoneNumber: string | null = null,
      roleType: string[] = [],
      accountStatus: string = 'Inactive'
    ) {
      this.firstName = firstName;
      this.lastName = lastName;
      this.email = email;
      this.password = password;
      this.phoneNumber = phoneNumber;
      this.roleType = roleType;
      this.accountStatus = accountStatus;
    }
  }
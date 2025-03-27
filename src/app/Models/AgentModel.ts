export class AgentModel {
    firstName: string;
    lastName: string | null;
    email: string | null;
    password: string | null;
    phoneNumber: string | null;
    roleType: string = 'Agent'; // Default role as 'Agent'
    accountStatus: string;
  
    constructor(
      firstName: string = '',
      lastName: string | null = null,
      email: string | null = '',
      password: string | null = '',
      phoneNumber: string | null = null,
      accountStatus: string = 'Inactive'
    ) {
      this.firstName = firstName;
      this.lastName = lastName;
      this.email = email;
      this.password = password;
      this.phoneNumber = phoneNumber;
      this.accountStatus = accountStatus;
    }
  }
  
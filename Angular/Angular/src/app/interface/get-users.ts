import { Role } from "./role";

export interface GetUsers 
{
    name: string,
    dateOfBirth: string,
    emailId: string,
    mobileNumber: string,
    userRoles: Role[]
}

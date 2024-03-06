import { Role } from "./role";

export interface GetUsers 
{
    name: string,
    emailId: string,
    userRoles: Role[]
}

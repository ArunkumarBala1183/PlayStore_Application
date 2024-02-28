export interface Login{
    emailId:string,
    password:string
}
export interface Register
{
      name:string,
      emailId:string,
      mobileNumber:string,
      password:string,
      confirmPassword:string,
      dateOfBirth:string
      
}

export interface EmailExists{
    emailId:string
}

export interface otpVerify{
   emailId:string,
   otp:string   
}

export interface PasswordChange
{
    emailId:string,
    password:string,
    confirmPassword:string
}
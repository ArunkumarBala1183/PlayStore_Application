export interface Login{
    emailId:string,
    password:string
}
export interface Register
{
      userName:string,
      emailId:string,
      mobile:string,
      password:string,
      confirmPassword:string,
      dob:string
}

export interface Email{
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
    comfirmPassword:string
}
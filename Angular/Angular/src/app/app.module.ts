import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginModule } from './login/login.module';
import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { HomeModule } from './home/home.module';
import { UserModule } from './user/user.module';
import { FormsModule } from '@angular/forms';
import { JwtInterceptor } from './jwt.interceptor';
import { JwtModule } from '@auth0/angular-jwt';
import { ToastrModule } from 'ngx-toastr';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LoginModule,
    HttpClientModule,
    HomeModule,
    UserModule,
    FormsModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('token')
      }}),
  ],
  providers: [
    {
      provide:HTTP_INTERCEPTORS,useClass:JwtInterceptor,multi:true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

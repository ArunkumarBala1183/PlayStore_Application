import { NgModule } from '@angular/core';
import { AdminModule } from './admin/admin.module';
import { RouterModule } from '@angular/router';
import { SharedModule } from './shared/shared.module';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginModule } from './login/login.module';
import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { HomeModule } from './home/home.module';
import { JwtInterceptor } from './jwt.interceptor';
import { JwtModule } from '@auth0/angular-jwt';
import { ToastrModule } from 'ngx-toastr';
import { UserModule } from './user/user.module';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AdminModule,
    RouterModule,
    SharedModule,
    FormsModule,
    MatButtonModule,
    MatIconModule,
    LoginModule,
    HomeModule,
    UserModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('token')
      }}),
      ToastrModule.forRoot()
  ],
  providers: [
    {
      provide:HTTP_INTERCEPTORS,useClass:JwtInterceptor,multi:true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

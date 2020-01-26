import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CookieService } from 'ngx-cookie-service';

@NgModule({
	declarations: [LoginComponent],
	imports: [CommonModule, BrowserModule, AuthRoutingModule, FormsModule],
	providers: [CookieService],
})
export class AuthModule {}

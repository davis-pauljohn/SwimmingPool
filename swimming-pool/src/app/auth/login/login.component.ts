import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { Login } from './login';
import { Router } from '@angular/router';
import { HttpResponse } from '@angular/common/http';

@Component({
	selector: 'sp-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss'],
	encapsulation: ViewEncapsulation.None,
})
export class LoginComponent implements OnInit {
	public loginForm: Login = new Login();
	public isLoggingIn: boolean;

	constructor(private authService: AuthService, private router: Router) {}

	ngOnInit() {}

	onSubmit() {
		this.isLoggingIn = true;
		this.authService.login(this.loginForm).subscribe(
			(response: HttpResponse<string>) => {},
			err => console.log(err),
			() => {
				this.router.navigate(['dashboard']);
				this.isLoggingIn = false;
			},
		);
	}
}

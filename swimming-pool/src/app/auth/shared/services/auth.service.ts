import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Login } from '../../login/login';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class AuthService {
	constructor(private http: HttpClient) {}
	public login(loginForm: Login): Observable<HttpResponse<string>> {
		return this.http.post('https://swimmingpool.com/api/authentication/login', loginForm, { responseType: 'text', observe: 'response' });
	}
}

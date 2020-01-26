import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class DashboardService {
	constructor(private http: HttpClient) {}

	public secure(): Observable<HttpResponse<number[]>> {
		return this.http.get<number[]>('https://swimmingpool.com/api/secure', { observe: 'response' });
	}
}

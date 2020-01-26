import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../shared/dashboard.service';

@Component({
	selector: 'sp-dashboard',
	templateUrl: './dashboard.component.html',
	styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
	public secureNumbers: number[] = [];

	constructor(private dashboardService: DashboardService) {}

	ngOnInit() {}

	public getSecureNumbers() {}
}

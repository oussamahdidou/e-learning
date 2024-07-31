import { Component, OnInit } from '@angular/core';
import { Chart, ChartConfiguration, ChartOptions } from 'chart.js';
import {
  BarController,
  BarElement,
  CategoryScale,
  LinearScale,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';
import { DashboardService } from '../../services/dashboard.service';
Chart.register(
  BarController,
  BarElement,
  CategoryScale,
  LinearScale,
  Title,
  Tooltip,
  Legend
);
@Component({
  selector: 'app-dashboardtable',
  templateUrl: './dashboardtable.component.html',
  styleUrl: './dashboardtable.component.css',
})
export class DashboardtableComponent implements OnInit {
  moduleslabels: any[] = [];
  modulesnmbr: any[] = [];
  constructor(private readonly dashboardservice: DashboardService) {}
  ngOnInit(): void {
    this.dashboardservice.getmostcheckedmodules().subscribe(
      (response) => {
        this.extractTopColumnsAbscences(response);
      },
      (error) => {}
    );
  }
  public barChartOptions: ChartOptions<'bar'> = {
    responsive: true,
    maintainAspectRatio: false,
    aspectRatio: 20 / 20,
    scales: {
      y: {
        beginAtZero: true,
        min: 0,
        max: 100,
        ticks: {
          stepSize: 20,
        },
      },
    },
  };

  public barChartData1: ChartConfiguration<'bar'>['data'] = {
    labels: this.moduleslabels,
    datasets: [
      {
        data: this.modulesnmbr,
        label: 'Les cours les plus populaires parmi les étudiants',
        backgroundColor: 'rgba(0,0,255,0.3)',
        borderColor: 'black',
        borderWidth: 1,
      },
    ],
  };

  public barChartData2: ChartConfiguration<'bar'>['data'] = {
    labels: ['Label 1', 'Label 2', 'Label 3', 'Label 4'],
    datasets: [
      {
        data: [20, 30, 40, 50],
        label: 'Dataset 2',
        backgroundColor: 'rgba(0,0,255,0.3)',
        borderColor: 'black',
        borderWidth: 1,
      },
    ],
  };

  public barChartData3: ChartConfiguration<'bar'>['data'] = {
    labels: ['Label 1', 'Label 2', 'Label 3', 'Label 4'],
    datasets: [
      {
        data: [30, 40, 50, 60],
        label: 'Dataset 3',
        backgroundColor: 'rgba(0,0,255,0.3)',
        borderColor: 'black',
        borderWidth: 1,
      },
    ],
  };

  public barChartData4: ChartConfiguration<'bar'>['data'] = {
    labels: ['Label 1', 'Label 2', 'Label 3', 'Label 4'],
    datasets: [
      {
        data: [40, 50, 60, 70],
        label: 'Dataset 4',
        backgroundColor: 'rgba(0,0,255,0.3)',
        borderColor: 'black',
        borderWidth: 1,
      },
    ],
  };
  extractTopColumnsAbscences(objects: any[]) {
    objects.forEach((obj) => {
      this.moduleslabels.unshift(obj.name);
      this.modulesnmbr.unshift(obj.count);
    });
  }
}

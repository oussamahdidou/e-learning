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
  topmoduleslabels: any[] = [];
  topmodulesnmbr: any[] = [];
  leastmoduleslabels: any[] = [];
  leastmodulesnmbr: any[] = [];
  toptestniveaulabels: any[] = [];
  toptestniveaunmbr: any[] = [];
  leasttestniveaulabels: any[] = [];
  leasttestniveaunmbr: any[] = [];
  stats: any;
  constructor(private readonly dashboardservice: DashboardService) {}
  ngOnInit(): void {
    this.dashboardservice.getmostcheckedmodules().subscribe(
      (response) => {
        this.extractTopmodules(response);
        console.log(response);
      },
      (error) => {}
    );
    this.dashboardservice.getleastcheckedmodules().subscribe(
      (response) => {
        this.extractleastmodules(response);
        console.log(response);
      },
      (error) => {}
    );
    this.dashboardservice.gettoptestniveaumodules().subscribe(
      (response) => {
        this.extractToptestniveau(response);
        console.log(response);
      },
      (error) => {}
    );
    this.dashboardservice.getworsttestniveaumodules().subscribe(
      (response) => {
        this.extractleasttestniveau(response);
        console.log(response);
      },
      (error) => {}
    );
    this.dashboardservice.getstats().subscribe(
      (response) => {
        this.stats = response;
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
    labels: this.topmoduleslabels,
    datasets: [
      {
        data: this.topmodulesnmbr,
        label: 'Les cours les plus populaires parmi les étudiants',
        backgroundColor: 'rgba(0,0,255,0.3)',
        borderColor: 'black',
        borderWidth: 1,
      },
    ],
  };

  public barChartData2: ChartConfiguration<'bar'>['data'] = {
    labels: this.leastmoduleslabels,
    datasets: [
      {
        data: this.leastmodulesnmbr,
        label: 'Les cours les moins populaires parmi les étudiants',
        backgroundColor: 'rgba(255,0,0,0.3)',
        borderColor: 'black',
        borderWidth: 1,
      },
    ],
  };

  public barChartData3: ChartConfiguration<'bar'>['data'] = {
    labels: this.toptestniveaulabels,
    datasets: [
      {
        data: this.toptestniveaunmbr,
        label: 'Top Performance aux test niveau',
        backgroundColor: 'rgba(0,0,255,0.3)',
        borderColor: 'black',
        borderWidth: 1,
      },
    ],
  };

  public barChartData4: ChartConfiguration<'bar'>['data'] = {
    labels: this.leasttestniveaulabels,
    datasets: [
      {
        data: this.leasttestniveaunmbr,
        label: 'Mauvaise Performance aux test niveau',
        backgroundColor: 'rgba(255,0,0,0.3)',
        borderColor: 'black',
        borderWidth: 1,
      },
    ],
  };
  extractleastmodules(objects: any[]) {
    objects.forEach((obj) => {
      this.leastmoduleslabels.unshift(obj.name);
      this.leastmodulesnmbr.unshift(obj.count);
    });
  }
  extractTopmodules(objects: any[]) {
    objects.forEach((obj) => {
      this.topmoduleslabels.unshift(obj.name);
      this.topmodulesnmbr.unshift(obj.count);
    });
  }
  extractleasttestniveau(objects: any[]) {
    objects.forEach((obj) => {
      this.leasttestniveaulabels.unshift(obj.name);
      this.leasttestniveaunmbr.unshift(obj.count);
    });
  }
  extractToptestniveau(objects: any[]) {
    objects.forEach((obj) => {
      this.toptestniveaulabels.unshift(obj.name);
      this.toptestniveaunmbr.unshift(obj.count);
    });
  }
}

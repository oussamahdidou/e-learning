import { Component, OnInit, ViewChild } from '@angular/core';
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
import { BaseChartDirective } from 'ng2-charts';
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
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;
  topmoduleslabels: any[] = [];
  topmodulesnmbr: any[] = [];
  leastmoduleslabels: any[] = [];
  leastmodulesnmbr: any[] = [];
  toptestniveaulabels: any[] = [];
  toptestniveaunmbr: any[] = [];
  leasttestniveaulabels: any[] = [];
  leasttestniveaunmbr: any[] = [];
  topteachersnmbrs: any[] = [];
  worstteachersnmbrs: any[] = [];
  topteacherslabels: any[] = [];
  worstteacherslabels: any[] = [];
  stats: any;
  constructor(private readonly dashboardservice: DashboardService) {}
  ngOnInit(): void {
    this.dashboardservice.getmostcheckedmodules().subscribe(
      (response) => {
        this.extractTopmodules(response);
        console.log(this.topmoduleslabels);
        console.log(this.topmodulesnmbr);
      },
      (error) => {}
    );
    this.dashboardservice.getleastcheckedmodules().subscribe(
      (response) => {
        this.extractleastmodules(response);
        console.log(this.leastmoduleslabels);
        console.log(this.leastmodulesnmbr);
      },
      (error) => {}
    );
    this.dashboardservice.gettoptestniveaumodules().subscribe(
      (response) => {
        this.extractToptestniveau(response);
        console.log(this.toptestniveaulabels);
        console.log(this.toptestniveaunmbr);
      },
      (error) => {}
    );
    this.dashboardservice.getworsttestniveaumodules().subscribe(
      (response) => {
        this.extractleasttestniveau(response);
        console.log(this.leasttestniveaulabels);
        console.log(this.leasttestniveaunmbr);
      },
      (error) => {}
    );
    this.dashboardservice.gettopteachers().subscribe(
      (response) => {
        this.extractTopteachers(response);
        console.log(this.topteacherslabels);
        console.log(this.topteachersnmbrs);
      },
      (error) => {}
    );
    this.dashboardservice.getworstteachers().subscribe(
      (response) => {
        this.extractWorstteachers(response);
        console.log(this.worstteacherslabels);
        console.log(this.worstteachersnmbrs);
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
  public modulescharts: ChartOptions<'bar'> = {
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
  public testniveauscharts: ChartOptions<'bar'> = {
    responsive: true,
    maintainAspectRatio: false,
    aspectRatio: 20 / 20,
    scales: {
      y: {
        beginAtZero: true,
        min: 0,
        max: 20,
        ticks: {
          stepSize: 2,
        },
      },
    },
  };
  public teacherscharts: ChartOptions<'bar'> = {
    responsive: true,
    maintainAspectRatio: false,
    aspectRatio: 20 / 20,
    scales: {
      y: {
        beginAtZero: true,
        min: 0,
        max: 50,
        ticks: {
          stepSize: 5,
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
  public barChartData5: ChartConfiguration<'bar'>['data'] = {
    labels: this.topteacherslabels,
    datasets: [
      {
        data: this.topteachersnmbrs,
        label: 'Les prof les plus performans',
        backgroundColor: 'rgba(0,0,255,0.3)',
        borderColor: 'black',
        borderWidth: 1,
      },
    ],
  };
  public barChartData6: ChartConfiguration<'bar'>['data'] = {
    labels: this.worstteacherslabels,
    datasets: [
      {
        data: this.worstteachersnmbrs,
        label: 'Les prof les moins performans',
        backgroundColor: 'rgba(255,0,0,0.3)',
        borderColor: 'black',
        borderWidth: 1,
      },
    ],
  };
  extractleastmodules(objects: any[]) {
    objects.reverse();
    objects.forEach((obj) => {
      this.leastmoduleslabels.unshift(obj.name);
      this.leastmodulesnmbr.unshift(obj.count);
    });
  }
  extractTopmodules(objects: any[]) {
    objects.reverse();
    objects.forEach((obj) => {
      this.topmoduleslabels.unshift(obj.name);
      this.topmodulesnmbr.unshift(obj.count);
    });
  }
  extractleasttestniveau(objects: any[]) {
    objects.reverse();
    objects.forEach((obj) => {
      this.leasttestniveaulabels.unshift(obj.name);
      this.leasttestniveaunmbr.unshift(obj.count);
    });
  }
  extractToptestniveau(objects: any[]) {
    objects.reverse();
    objects.forEach((obj) => {
      this.toptestniveaulabels.unshift(obj.name);
      this.toptestniveaunmbr.unshift(obj.count);
    });
  }
  extractTopteachers(objects: any[]) {
    objects.reverse();
    objects.forEach((obj) => {
      this.topteacherslabels.unshift(obj.name);
      this.topteachersnmbrs.unshift(obj.count);
    });
  }
  extractWorstteachers(objects: any[]) {
    objects.reverse();
    objects.forEach((obj) => {
      this.worstteacherslabels.unshift(obj.name);
      this.worstteachersnmbrs.unshift(obj.count);
    });
  }
  updateChart() {
    if (this.chart) {
      this.chart.update();
    }
  }
}

import { Component, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { StatisticReportDto, StatisticReportServiceProxy } from '@shared/service-proxies/service-proxies';
import { ChartOptions, ChartType } from 'chart.js';
import * as moment from 'moment';
import { Label, SingleDataSet } from 'ng2-charts';
@Component({
  selector: 'app-statistic-report',
  templateUrl: './statistic-report.component.html',
  styleUrls: ['./statistic-report.component.css'],
  animations: [appModuleAnimation()]
})
export class StatisticReportComponent extends AppComponentBase implements OnInit {

  statistics: StatisticReportDto[];
  listBorrows: StatisticReportDto[];
  fromDate;
  toDate;
  month: number;
  quarter: number;
  status: string;

  dMonth: undefined;
  dQuarter: undefined;
  dSatus: undefined;

  pageIndex: number = 1;
  count: number;
  pageSize: number=9;

  active = 1;

  listMonth = [
    { m: 1 }, { m: 2 }, { m: 3 }, { m: 4 }, { m: 5 }, { m: 6 }, { m: 7 }, { m: 8 }, { m: 9 }, { m: 10 }, { m: 11 }, { m: 12 }
  ]

  listQuarter = [
    { q: 1 }, { q: 2 }, { q: 3 }, { q: 4 }
  ]

  listStatus = [
    { s: "Đã trả" }, { s: "Đang mượn" }, { s: "Quá hạn"}
  ]

  valueFromDate(value: Date): void {
    this.fromDate = moment(value, "YYYY-MM-DD");
  }

  valueToDate(value: Date): void {
    this.toDate = moment(value, "YYYY-MM-DD");
  }

  public pieChartOptions: ChartOptions = {
    responsive: true,
    tooltips: {
      enabled: true,
      callbacks: {
          label: function(tooltipItem, data) {
              return data.labels[tooltipItem.index] + ': ' + data.datasets[0].data[tooltipItem.index] + '%';
          }
      }
  }
  };

  pieChartLabels: Label[] = [];
  pieChartData: SingleDataSet = [];
  pieChartType: ChartType = 'pie';
  pieChartLegend = true;
  pieChartPlugins = [];

  public pieChartColors: Array<any> = [{
    backgroundColor: ['#fc5858', '#19d863', '#fdf57d', '#1b9980', '#230dc2', '#9b5913', '#ad5039'],
  }];

  constructor(
    injector: Injector,
    private _statisticService: StatisticReportServiceProxy,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.listByCategory();
    this.listBorrow();
  }

  listByCategory(): void {
    this._statisticService
      .getStatisticByCategory(this.pageIndex , this.fromDate, this.toDate,  this.month, this.quarter)
      .subscribe(response => {        
        this.statistics = response.items;
        this.count = response.count;
        this.pageIndex = response.pageIndex;
        this.pageSize = response.pageSize;                        
      }
      );
  }

  filterByCategory() {
    if (this.fromDate === undefined && this.toDate === undefined && this.month === undefined
      && this.quarter === undefined) {
      this.listByCategory();
    } else {
      this.listByCategory();
    }
  }

  setupByCategory() {
    this.fromDate = undefined;
    this.toDate = undefined;
    this.month = this.dMonth;
    this.quarter = this.dQuarter;
    this.pieChartLabels = [];
    this.pieChartData = [];
    this.statistics = undefined;
    this.listByCategory();
  }

  chart() {
    let qty = 0;
    let total = 0;
    for (let i = 0; i < this.statistics.length; i++) {
      total = total + this.statistics[i].quantity
    }
    
    for (let i = 0; i < this.statistics.length; i++) {
      this.pieChartLabels.push(this.statistics[i].categoryName);
      qty = (this.statistics[i].quantity / total) * 100
      this.pieChartData.push(Math.ceil(qty));
    }
  }

  listBorrow(): void {
    this._statisticService
      .getStatisticByBorrow(this.pageIndex , this.fromDate, this.toDate,  this.month, this.quarter, this.status)
      .subscribe(response => {        
        this.listBorrows= response.items;
        this.count = response.count;
        this.pageIndex = response.pageIndex;
        this.pageSize = response.pageSize;                
      }
      );
  }

  filterBorrow() {
    if (this.status ===undefined &&this.fromDate === undefined && this.toDate === undefined && this.month === undefined
      && this.quarter === undefined) {
      this.listBorrow();
    } else {
      this.listBorrow();
    }
  }

  setupBorrow() {
    this.fromDate = undefined;
    this.toDate = undefined;
    this.month = this.dMonth;
    this.quarter = this.dQuarter;
    this.status = this.dSatus;
    this.listBorrow();
  }

}

import { Component, OnInit } from '@angular/core';
import {IOrder} from '../../shared/models/order';
import {OrdersService} from '../orders.service';
import {ActivatedRoute} from '@angular/router';
import {BreadcrumbService} from 'xng-breadcrumb';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss']
})
export class OrderDetailComponent implements OnInit {
  order: IOrder;
  constructor(private ordersService: OrdersService,
              private route: ActivatedRoute,
              private breadcrumbService: BreadcrumbService
              ){
    this.breadcrumbService.set('@OrderDetailed', '');
  }

  ngOnInit(): void {
    this.ordersService.getOrderDetailed(+this.route.snapshot.paramMap.get('id'))
      .subscribe((order: IOrder) => {
        this.order = order;
        this.breadcrumbService.set('@OrderDetailed', `Order# ${order.id} - ${order.status}`);
      }, err => {
        console.log(err);
      });
  }

}

import { Component, OnInit } from '@angular/core';
import {IProduct} from '../../shared/models/product';
import {ShopService} from '../shop.service';
import {ActivatedRoute} from '@angular/router';
import {BreadcrumbService} from 'xng-breadcrumb';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  product: IProduct;
  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute, private bcService: BreadcrumbService) {
    this.bcService.set('@productDetails', '');
  }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct() {
    this.shopService.getProduct(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.product = response;
      this.bcService.set('@productDetails', response.name);
    }, error => {
      console.log(error);
    });
  }

}

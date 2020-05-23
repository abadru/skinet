import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import {CheckoutRoutingModule} from './checkout-routing.module';
import {SharedModule} from '../shared/shared.module';
import { CheckoutAddressComponent } from './checkout-address/checkout-address.component';
import { CheckoutDeliveryComponent } from './checkout-delivery/checkout-delivery.component';
import { CheckoutReviewComponent } from './checkout-review/checkout-review.component';
import { CheckoutPaymentComponent } from './checkout-payment/checkout-payment.component';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';
import {A11yModule} from '@angular/cdk/a11y';



@NgModule({
  declarations: [CheckoutComponent, CheckoutAddressComponent, CheckoutDeliveryComponent, CheckoutReviewComponent, CheckoutPaymentComponent, CheckoutSuccessComponent],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    SharedModule,
    A11yModule
  ]
})
export class CheckoutModule { }

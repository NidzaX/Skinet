import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { MatStepperModule } from '@angular/material/stepper';
import { StripeAddressElement } from '@stripe/stripe-js';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import {
  MatCheckboxChange,
  MatCheckboxModule,
} from '@angular/material/checkbox';

import { OrderSummaryComponent } from '../../shared/components/order-summary/order-summary.component';
import { StripeService } from '../../core/services/stripe.service';
import { SnackbarService } from '../../core/services/snackbar.service';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { Address } from '../../shared/models/user';
import { firstValueFrom } from 'rxjs';
import { AccountService } from '../../core/services/account.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [
    OrderSummaryComponent,
    MatStepperModule,
    MatButton,
    RouterLink,
    MatCheckboxModule,
  ],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss',
})
export class CheckoutComponent implements OnInit, OnDestroy {
  private stripeService = inject(StripeService);
  private snackBar = inject(SnackbarService);
  private accountService = inject(AccountService);
  addressElement?: StripeAddressElement;
  saveAddress = false;

  async ngOnInit() {
    try {
      this.stripeService.disposeElements();

      const user = await firstValueFrom(this.accountService.getUserInfo());
      console.log('Fetched user:', user);

      this.addressElement = await this.stripeService.createAddressElements(
        user
      );
      this.addressElement.mount('#address-element');

      this.accountService.getUserInfo();
    } catch (error: any) {
      this.snackBar.error(error.message);
    }
  }

  async onStepChange(event: StepperSelectionEvent) {
    if (event.selectedIndex === 1) {
      if (this.saveAddress) {
        const result = await this.addressElement?.getValue();

        const addr = result?.value.address;
        const name = result?.value.name;

        if (addr) {
          const address: Address = {
            line1: addr.line1,
            line2: addr.line2 || undefined,
            city: addr.city,
            state: addr.state || '',
            country: addr.country,
            postalCode: addr.postal_code,
          };

          await firstValueFrom(this.accountService.updateAddress(address));
        }

        if (name) {
          const [firstName, ...rest] = name.trim().split(' ');
          const lastName = rest.join(' ') || '';

          if (firstName && lastName) {
            await firstValueFrom(
              this.accountService.updateName({ firstName, lastName })
            );
          }
        }
      }
    }
  }

  onSaveAddressCheckboxChange(event: MatCheckboxChange) {
    this.saveAddress = event.checked;
  }

  ngOnDestroy() {
    this.stripeService.disposeElements();
  }
}

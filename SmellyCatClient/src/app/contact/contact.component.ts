import { Component } from '@angular/core';
import {
  FormGroup,
  FormControl,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ContactService } from './contact.service';
import { ContactRequest } from './contact-request.model';
import { Subject, catchError, finalize, switchMap, timer } from 'rxjs';
import { AsyncPipe, CommonModule } from '@angular/common';

interface Status {
  isSuccess: boolean;
  message: string;
}

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, AsyncPipe],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css',
})
export class ContactComponent {
  private static readonly statusMessageSuccess = 'Message sent successfully!';
  private static readonly statusMessageFailure =
    'Something went wrong, try again later.';
  private static readonly statusMessageDisplayDurationMs = 2500;

  private readonly status = new Subject<Status | null>();
  status$ = this.status.asObservable();

  contactForm = new FormGroup({
    fullName: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    city: new FormControl('', Validators.required),
    postalCode: new FormControl('', Validators.required),
    address: new FormControl('', Validators.required),
    message: new FormControl(''),
    agreedToTerms: new FormControl(false, Validators.requiredTrue),
  });

  constructor(private readonly contactService: ContactService) {}

  onSubmit() {
    if (this.contactForm.valid) {
      const contactRequest: ContactRequest = {
        fullName: this.contactForm.controls.fullName.value!,
        email: this.contactForm.controls.email.value!,
        city: this.contactForm.controls.city.value!,
        postalCode: this.contactForm.controls.postalCode.value!,
        address: this.contactForm.controls.address.value!,
        message: this.contactForm.controls.message.value,
      };

      this.contactService
        .submitContactRequest(contactRequest)
        .pipe(
          switchMap((response) => {
            console.log(response);
            this.status.next({
              message: ContactComponent.statusMessageSuccess,
              isSuccess: true,
            });
            this.contactForm.reset();
            return timer(ContactComponent.statusMessageDisplayDurationMs);
          }),
          catchError((error) => {
            console.log(error);
            this.status.next({
              message: ContactComponent.statusMessageFailure,
              isSuccess: false,
            });
            return timer(ContactComponent.statusMessageDisplayDurationMs);
          }),
          finalize(() => {
            this.status.next(null);
          })
        )
        .subscribe();
    }
  }
}

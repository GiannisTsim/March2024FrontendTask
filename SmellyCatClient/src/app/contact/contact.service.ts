import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ContactRequest } from './contact-request.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  constructor(private readonly httpClient: HttpClient) {}

  submitContactRequest(contactRequest: ContactRequest) {
    return this.httpClient.post(
      `${environment.apiBaseUrl}/contact`,
      contactRequest
    );
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Contact } from 'src/models/contacts/contact';

@Injectable({ providedIn: 'root' })
export class ContactsService {
  constructor(private readonly http: HttpClient) { }

  getContacts(): Observable<Contact[]> {
    const url = `${environment.api.contacts}/GetContacts`;

    return this.http.get<Contact[]>(url);
  }
}

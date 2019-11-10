import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticatedUser, LoginDto } from 'src/models/auth/login';
import { RegisterDto } from 'src/models/auth/register';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private readonly http: HttpClient) { }

  login(model: LoginDto): Observable<AuthenticatedUser> {
    const url = `${environment.api.auth}/Login`;

    return this.http.post<AuthenticatedUser>(url, model);
  }

  register(model: RegisterDto): Observable<void> {
    const url = `${environment.api.auth}/Register`;

    return this.http.post<void>(url, model);
  }

  isEmailTaken(email: string): Observable<boolean> {
    const url = `${environment.api.auth}/IsEmailTaken`;

    const options = {
      params: new HttpParams().set('email', email),
    };

    return this.http.get<boolean>(url, options);
  }

  test() {
    this.http.get(`${environment.api.chats}/GetChats`).subscribe(console.log);
  }
}

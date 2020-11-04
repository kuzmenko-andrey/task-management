import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Token } from '../model/token'

export const ACCESS_TOKEN_KEY = 'access_token'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) { }

  apiUrl: string = "api/auth";

  singUp(email: string, username: string, password: string): void {
    this.http.post(`${this.apiUrl}/sign_up`, {
      email, username, password
    })
  }

  login(email: string, password: string): Observable<Token> {
    return this.http.post<Token>(`${this.apiUrl}/login`, {
      email, password
    }).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
      })
    )
  }

  logout(email: string): void {
    this.http.post(`${this.apiUrl}/logout`, {
      email
    }).pipe(
      tap(token => {
        localStorage.removeItem(ACCESS_TOKEN_KEY);
        this.router.navigate[''];
      })
    )
  }

  resetPassword(code: number, username: string): void {
    this.http.post(`${this.apiUrl}/reset_password/${code}`, {
      username
    }).pipe(
      tap(token => {
        console.log("reset_password")
      })
    )
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token && !this.jwtHelper.isTokenExpired(token);
  }
}

import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs/index';
import { StoreCharacters } from './store.model';
import { catchError, map } from 'rxjs/internal/operators';

@Injectable()
export class StoreService {
  constructor(private http: HttpClient,
              @Inject('BASE_URL') private baseUrl: string) {
  }

  public getStores(): Observable<StoreCharacters[]> {
    return this.http.get<StoreCharacters[]>(this.baseUrl + 'api/Stores/Stores')
      .pipe(
        map((resp: StoreCharacters[]) => resp),
        catchError((err: HttpErrorResponse) => throwError(err.error)));
  }

  public addStore(item: StoreCharacters): Observable<number> {
    return this.http.post<number>(this.baseUrl + 'api/Stores/AddStore', item)
      .pipe(
        map((resp: number) => resp),
        catchError((err: HttpErrorResponse) => throwError(err.error)));
  }

  public updateStore(item: StoreCharacters): Observable<number> {
    return this.http.put<number>(this.baseUrl + 'api/Stores/UpdateStore', item)
      .pipe(
        map((resp: number) => resp),
        catchError((err: HttpErrorResponse) => throwError(err.error)));
  }

  public removeStore(id: number): Observable<number> {
    return this.http.delete<number>(this.baseUrl + 'api/Stores/DeleteStore/' + id)
      .pipe(
        map((resp: number) => resp),
        catchError((err: HttpErrorResponse) => throwError(err.error)));
  }
}

import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RaceModel } from 'src/models/RaceModel';

@Injectable({
  providedIn: 'root',
})
export class RaceService {
  private apiUrl = environment.api + 'races/';

  constructor(private http: HttpClient) {}

  public getRace(id: string): Observable<RaceModel> {
    return this.http.get<RaceModel>(`${this.apiUrl}${id}`);
  }
}

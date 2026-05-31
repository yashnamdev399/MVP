import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Car, UserPreference, RecommendationResult } from '../models/car.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class CarService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/cars`;

  getAll(): Observable<Car[]> {
    return this.http.get<Car[]>(this.baseUrl);
  }

  getRecommendations(prefs: UserPreference): Observable<RecommendationResult[]> {
    return this.http.post<RecommendationResult[]>(`${this.baseUrl}/recommend`, prefs);
  }
}

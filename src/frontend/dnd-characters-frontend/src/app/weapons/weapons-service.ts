import { inject, Service } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetWeaponsResponse } from './models/get-weapons/get-weapons-response.model';
import { PagedListResponse } from '../shared/models/pagination/paged-list-response';

@Service()
export class WeaponsService {

    private readonly url : string = `${environment.apiUrl}/weapons`;
    private readonly httpClient : HttpClient = inject(HttpClient);

    getWeapons() : Observable<PagedListResponse<GetWeaponsResponse>> {
        return this.httpClient.get<PagedListResponse<GetWeaponsResponse>>(this.url);
    }
}

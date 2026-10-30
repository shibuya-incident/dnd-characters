import { inject, Service } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetWeaponsResponse } from './models/get-weapons/get-weapons-response.model';
import { PagedListResponse } from '../shared/models/pagination/paged-list-response';
import { PageListRequest } from '../shared/models/pagination/page-list-request';
import { GetWeaponsRequest } from './models/get-weapons/get-weapons-request.model';

@Service()
export class WeaponsService {

    private readonly url: string = `${environment.apiUrl}/weapons`;
    private readonly httpClient: HttpClient = inject(HttpClient);

    getWeapons(request: GetWeaponsRequest): Observable<PagedListResponse<GetWeaponsResponse>> {
        let queryParams = new HttpParams();

        queryParams = queryParams.set('page', request.page);
        queryParams = queryParams.set('pageSize', request.pageSize);
        queryParams = queryParams.set('sortBy', request.sortBy.toString());
        queryParams = queryParams.set('sortDirection', request.sortDirection);
        
        if (request.name) {
            queryParams = queryParams.set('name', request.name);
        }
        if (request.type) {
            queryParams = queryParams.set('type', request.type);
        }
        if (request.characterId != null) {
            queryParams = queryParams.set('characterId', request.characterId);
        }

        return this.httpClient.get<PagedListResponse<GetWeaponsResponse>>(this.url, { params: queryParams });
    }
}

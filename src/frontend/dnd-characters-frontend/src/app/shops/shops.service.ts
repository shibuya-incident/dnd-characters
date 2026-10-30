import { inject, Service } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetShopsResponse } from './models/get-shops/get-shops-response.model';
import { PagedListResponse } from '../shared/models/pagination/paged-list-response';

@Service()
export class ShopsService {

    private readonly url : string = `${environment.apiUrl}/shops`;
    private readonly httpClient : HttpClient = inject(HttpClient);

    getShops() : Observable<PagedListResponse<GetShopsResponse>> {
        return this.httpClient.get<PagedListResponse<GetShopsResponse>>(this.url);
    }
}
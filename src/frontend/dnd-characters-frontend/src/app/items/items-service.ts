import { inject, Service } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetItemsResponse } from './models/get-items/get-items-response.model';
import { PagedListResponse } from '../shared/models/pagination/paged-list-response';

@Service()
export class ItemsService {

    private readonly url : string = `${environment.apiUrl}/items`;
    private readonly httpClient : HttpClient = inject(HttpClient);

    getItems() : Observable<PagedListResponse<GetItemsResponse>> {
        return this.httpClient.get<PagedListResponse<GetItemsResponse>>(this.url);
    }
}
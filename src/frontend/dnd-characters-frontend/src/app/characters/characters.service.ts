import { inject, Service } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetCharactersResponse } from './models/get-characters/get-characters-response.model';
import { PagedListResponse } from '../shared/models/pagination/paged-list-response';

@Service()
export class CharactersService {

    private readonly url : string = `${environment.apiUrl}/characters`;
    private readonly httpClient : HttpClient = inject(HttpClient);

    getCharacters() : Observable<PagedListResponse<GetCharactersResponse>> {
        return this.httpClient.get<PagedListResponse<GetCharactersResponse>>(this.url);
    }
}
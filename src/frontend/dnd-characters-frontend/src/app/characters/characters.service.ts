import { inject, Service } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetCharactersResponse } from './models/get-characters/get-characters-response.model';
import { PagedListResponse } from '../shared/models/pagination/paged-list-response';
import { GetCharactersRequest } from './models/get-characters/get-characters-request.model';
import { GetCharacterByIdRequest } from './models/get-character-by-id/get-character-by-id-request.model';
import { GetCharacterByIdResponse } from './models/get-character-by-id/get-character-by-id-response.model';
import { DeleteCharacterRequest } from './models/delete-character/delete-character-request.model';

@Service()
export class CharactersService {

    private readonly url : string = `${environment.apiUrl}/characters`;
    private readonly httpClient : HttpClient = inject(HttpClient);

    getCharacters(request: GetCharactersRequest) : Observable<PagedListResponse<GetCharactersResponse>> {

        let queryParams = new HttpParams();

        queryParams = queryParams.set('page', request.page);
        queryParams = queryParams.set('pageSize', request.pageSize);
        queryParams = queryParams.set('sortBy', request.sortBy.toString());
        queryParams = queryParams.set('sortDirection', request.sortDirection);

        if(request.name) {
            queryParams = queryParams.set('name', request.name);
        }

        if(request.race) {
            queryParams = queryParams.set('race', request.race.toString());
        }

        if(request.class) {
            queryParams = queryParams.set('class', request.class.toString());
        }

        return this.httpClient.get<PagedListResponse<GetCharactersResponse>>(this.url, {params: queryParams});
    }

    getCharacterById(request: GetCharacterByIdRequest) : Observable<GetCharacterByIdResponse> {
        return this.httpClient.get<GetCharacterByIdResponse>(`${this.url}/${request.id}`);
    }

    deleteCharacter(request: DeleteCharacterRequest) : Observable<void> {
        return this.httpClient.delete<void>(`${this.url}/${request.id}`);
    }
}
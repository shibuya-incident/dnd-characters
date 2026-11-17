import { inject, Service } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetWeaponsResponse } from './models/get-weapons/get-weapons-response.model';
import { PagedListResponse } from '../shared/models/pagination/paged-list-response';
import { GetWeaponsRequest } from './models/get-weapons/get-weapons-request.model';
import { GetWeaponByIdRequest } from './models/get-weapon-by-id/get-weapon-by-id-request.model';
import { GetWeaponByIdResponse } from './models/get-weapon-by-id/get-weapon-by-id-response.model';
import { DeleteWeaponRequest } from './models/delete-weapon/delete-weapon.model';
import { CreateWeaponResponse } from './models/create-weapon/create-weapon-response.model';
import { CreateWeaponRequest } from './models/create-weapon/create-weapon-request.model';
import { UpdateWeaponResponse } from './models/update-weapon/update-weapon-response.model';
import { UpdateWeaponRequest } from './models/update-weapon/update-weapon-request.model';

@Service()
export class WeaponsService {

    private readonly url : string = `${environment.apiUrl}/weapons`;
    private readonly httpClient : HttpClient = inject(HttpClient);

    getWeapons(request : GetWeaponsRequest) : Observable<PagedListResponse<GetWeaponsResponse>> {

        let queryParams = new HttpParams();

        queryParams = queryParams.set('page', request.page);
        queryParams = queryParams.set('pageSize', request.pageSize);
        queryParams = queryParams.set('sortBy', request.sortBy.toString());
        queryParams = queryParams.set('sortDirection', request.sortDirection);

        if(request.name) {
            queryParams = queryParams.set('name', request.name);
        }

        if(request.type) {
            queryParams = queryParams.set('type', request.type.toString());
        }

        if(request.characterId != null) {
            queryParams = queryParams.set('characterId', request.characterId);
        }

        return this.httpClient.get<PagedListResponse<GetWeaponsResponse>>(this.url, {params: queryParams});
    }

    getWeaponById(request: GetWeaponByIdRequest) : Observable<GetWeaponByIdResponse> {
        return this.httpClient.get<GetWeaponByIdResponse>(`${this.url}/${request.id}`);
    }

    deleteWeapon(request: DeleteWeaponRequest) : Observable<void> {
        return this.httpClient.delete<void>(`${this.url}/${request.id}`);
    }

    createWeapon(request: CreateWeaponRequest) : Observable<CreateWeaponResponse> {
        return this.httpClient.post<CreateWeaponResponse>(this.url, request);
    }

    updateWeapon(id: number, request: UpdateWeaponRequest) : Observable<UpdateWeaponResponse> {
        return this.httpClient.put<UpdateWeaponResponse>(`${this.url}/${id}`, request);
    }
}

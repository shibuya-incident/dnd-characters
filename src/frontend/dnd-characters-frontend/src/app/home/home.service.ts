import {inject, Service} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GetHomeCountersResponse} from './models/get-home-counters-response.model';

@Service()
export class HomeService {

    private readonly url : string = `${environment.apiUrl}/home`;
    private readonly httpClient : HttpClient = inject(HttpClient);

    getCounters() : Observable<GetHomeCountersResponse> {
        return this.httpClient.get<GetHomeCountersResponse>(`${this.url}/counters`);
    }
}    
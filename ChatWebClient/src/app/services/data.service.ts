import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from "@aspnet/signalr";
import { HttpClient } from "@angular/common/http";
import { Message } from '../models/message';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private httpClient: HttpClient) { }

  private readonly BaseUrl: string = 'https://localhost:44389/api';

  private hubConnection: HubConnection;

  public getMessages(): Observable<any> {
    return this.httpClient.get(`${this.BaseUrl}/chat/getmessages`);
  }

  public getCurrentUser(): Observable<User> {
    return this.httpClient.get<User>(`${this.BaseUrl}/chat/getcurrentuser`);
  }

  public startConnection(): void {
    this.hubConnection = new HubConnectionBuilder()
    .withUrl('https://localhost:44389/chat')
    .build();

    this.hubConnection.start();
  }

  public onReceived(method: (message: Message) => void) {
    this.hubConnection.on('received', method);
  }

  public sendMessage(message: any) {
    this.hubConnection.invoke("Send", message);
  }
}

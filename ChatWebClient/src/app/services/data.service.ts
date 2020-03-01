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

  private readonly baseUri: string = 'https://localhost:44389/api/';

  private hubConnection: HubConnection;

  public getMessages(): Observable<Message[]> {
    return this.httpClient.get<Message[]>(`${this.baseUri}chat/getmessages`);
  }

  public getUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(`${this.baseUri}chat/getusers`);
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

  public sendMessage(message: Message) {
    this.hubConnection.invoke("Send", message);
  }
}

import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from "@aspnet/signalr";
import { Message } from '../models/message';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  private hubConnection: HubConnection;

  public startConnection(): void {
    this.hubConnection = new HubConnectionBuilder()
    .withUrl('https://localhost:44389/chat')
    .build();

    this.hubConnection.start();
  }

  public onReceived(method: (message: Message) => void) {
    this.hubConnection.on('received', method)
  }

  public sendMessage(message: Message) {
    this.hubConnection.invoke("Send", message);
  }
}

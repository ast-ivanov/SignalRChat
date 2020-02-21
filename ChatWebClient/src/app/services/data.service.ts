import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from "@aspnet/signalr";
import { Observable } from 'rxjs';

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

  public onReceived(method: (message: string) => void) {
    this.hubConnection.on('received', method)
  }

  public sendMessage(text: string) {
    this.hubConnection.invoke("Send", text);
  }
}

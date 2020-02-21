import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/services/data.service';
import { Message } from 'src/app/models/message';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  constructor(private dataService: DataService) { }

  name: string = "Арчи";

  messages: Message[] = [];

  message: string;

  ngOnInit() {
    this.dataService.startConnection();

    this.dataService.onReceived(this.onReceived.bind(this));
  }

  private onReceived(message: string): void {
    let mes = new Message();
    mes.name = this.name;
    mes.text = message;
    mes.time = new Date();
    this.messages.push(mes);
  }

  public send(): void {
    this.dataService.sendMessage(this.message);
    this.message = '';
  }
}

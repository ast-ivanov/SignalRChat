import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/services/data.service';
import { Message } from 'src/app/models/message';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  constructor(private dataService: DataService) { }

  messageForm = new FormGroup({
    name: new FormControl(),
    text: new FormControl()
  });

  messages: Message[] = [];

  ngOnInit() {
    this.dataService.startConnection();
    this.dataService.onReceived(this.onReceived.bind(this));
  }

  private onReceived(message: Message): void {
    this.messages.push(message);
  }

  public send(): void {
    let message = this.messageForm.value;
    message.time = new Date();
    this.dataService.sendMessage(message);
    this.messageForm.get('text').setValue('');
  }
}

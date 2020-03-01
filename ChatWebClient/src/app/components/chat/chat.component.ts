import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/services/data.service';
import { Message } from 'src/app/models/message';
import { FormGroup, FormControl } from '@angular/forms';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  constructor(private dataService: DataService) { }

  messageForm = new FormGroup({
    user: new FormGroup({
      id: new FormControl()
    }),
    text: new FormControl()
  });

  users: User[];

  messages: Message[];

  ngOnInit() {
    this.dataService.startConnection();
    this.dataService.onReceived(this.onReceived.bind(this));
    this.dataService.getMessages().subscribe(messages => {
      this.messages = messages;
    });
    this.dataService.getUsers().subscribe(users => {
      this.users = users;
    });
  }

  private onReceived(message: Message): void {
    this.messages.push(message);
  }

  public send(): void {
    let message = this.messageForm.value;
    message.time = new Date();
    message.user.id = Number.parseInt(message.user.id);
    message.user.name = this.users.find(u => u.id == message.user.id).name;
    this.dataService.sendMessage(message);
    this.messageForm.get('text').reset();
  }
}

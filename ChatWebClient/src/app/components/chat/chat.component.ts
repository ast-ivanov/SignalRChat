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

  user: User

  text: string

  messages: Message[]

  ngOnInit() {
    this.dataService.startConnection();
    this.dataService.onReceived(this.onReceived.bind(this));
    this.dataService.getCurrentUser().subscribe(user => {
      this.user = user;
    })
    this.dataService.getMessages().subscribe(response => {
      this.messages = response.messages;
    });
  }

  private onReceived(message: Message): void {
    this.messages.push(message);
  }

  public send(): void {
    let message = {
      text: this.text,
      time: new Date(),
      user: this.user
    };
    this.dataService.sendMessage(message);
    this.text = '';
  }
}

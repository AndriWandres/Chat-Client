import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  constructor() { }

  ngOnInit() {

  }

  onSendMessage(event: any): void {
    console.log(event);
  }

  onAttachFile(event: any): void {
    console.log(event);
  }
}

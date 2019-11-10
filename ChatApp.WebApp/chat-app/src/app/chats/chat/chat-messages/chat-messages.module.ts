import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ChatBubbleModule } from './chat-bubble/chat-bubble.module';
import { ChatMessagesComponent } from './chat-messages.component';

@NgModule({
  declarations: [ChatMessagesComponent],
  imports: [
    CommonModule,
    ChatBubbleModule
  ],
  exports: [ChatMessagesComponent]
})
export class ChatMessagesModule { }

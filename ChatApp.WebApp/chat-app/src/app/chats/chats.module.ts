import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ChatsRoutingModule } from './chats-routing.module';
import { ChatsComponent } from './chats.component';
import { ContactsModule } from './contacts/contacts.module';
import { ChatModule } from './chat/chat.module';

@NgModule({
  declarations: [ChatsComponent],
  imports: [
    CommonModule,
    ChatsRoutingModule,
    ContactsModule,

    ChatModule, // eventually remove due to lazy loading
  ]
})
export class ChatsModule { }

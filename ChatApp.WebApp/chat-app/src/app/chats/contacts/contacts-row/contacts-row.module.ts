import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { UserAvatarModule } from 'src/app/shared/components/user-avatar/user-avatar.module';
import { ChatTimestampModule } from 'src/app/shared/pipes/chat-timestamp/chat-timestamp.module';
import { ContactsRowComponent } from './contacts-row.component';

@NgModule({
  declarations: [ContactsRowComponent],
  imports: [CommonModule, ChatTimestampModule, UserAvatarModule],
  exports: [ContactsRowComponent]
})
export class ChatsRowModule { }

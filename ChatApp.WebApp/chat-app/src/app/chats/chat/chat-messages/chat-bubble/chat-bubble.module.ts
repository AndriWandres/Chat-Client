import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { UserAvatarModule } from 'src/app/shared/components/user-avatar/user-avatar.module';
import { ChatBubbleComponent } from './chat-bubble.component';

@NgModule({
  declarations: [ChatBubbleComponent],
  imports: [
    CommonModule,
    UserAvatarModule
  ],
  exports: [ChatBubbleComponent]
})
export class ChatBubbleModule { }

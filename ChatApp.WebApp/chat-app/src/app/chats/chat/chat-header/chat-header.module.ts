import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatHeaderComponent } from './chat-header.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { UserAvatarModule } from 'src/app/shared/components/user-avatar/user-avatar.module';

@NgModule({
  declarations: [ChatHeaderComponent],
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    UserAvatarModule,
  ],
  exports: [ChatHeaderComponent]
})
export class ChatHeaderModule { }

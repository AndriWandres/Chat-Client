// Angular Modules
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

// Declared Pipe
import { ChatTimestampPipe } from './chat-timestamp.pipe';

@NgModule({
  declarations: [ChatTimestampPipe],
  imports: [CommonModule],
  exports: [ChatTimestampPipe],
})
export class ChatTimestampModule { }

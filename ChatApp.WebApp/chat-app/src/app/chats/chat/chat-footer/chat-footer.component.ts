import { ChangeDetectionStrategy, Component, EventEmitter, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-chat-footer',
  templateUrl: './chat-footer.component.html',
  styleUrls: ['./chat-footer.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatFooterComponent {
  @Output() readonly sendMessage = new EventEmitter<string>(true);
  @Output() readonly attachFile = new EventEmitter<void>(true);

  readonly messageControl = new FormControl('', [
    Validators.required,
    Validators.pattern(/.*[^\s].*/),
  ]);
}

import { Component, Input, HostBinding, OnInit } from '@angular/core';

@Component({
  selector: 'app-chat-bubble',
  templateUrl: './chat-bubble.component.html',
  styleUrls: ['./chat-bubble.component.scss']
})
export class ChatBubbleComponent implements OnInit {
  @Input() contact: any;

  @Input() isAnchor = false;
  @Input() direction: 'left' | 'right' = 'left';

  @HostBinding('style.flex-direction') flexDirection: string;

  ngOnInit(): void {
    this.flexDirection = this.direction === 'left' ? 'row' : 'row-reverse';
  }
}

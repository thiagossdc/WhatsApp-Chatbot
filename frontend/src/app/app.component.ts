import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <div class="container">
      <h1>Chatbot de Atendimento</h1>
      <app-chatbot></app-chatbot>
    </div>
  `,
  styles: [`
    .container {
      text-align: center;
      margin: 20px;
    }
  `]
})
export class AppComponent { }

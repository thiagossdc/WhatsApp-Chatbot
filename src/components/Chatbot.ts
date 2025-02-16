import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-chatbot',
  template: `
    <div class="chat-container">
      <div *ngFor="let message of messages" class="message" [ngClass]="{'user': message.sender === 'user', 'bot': message.sender === 'bot'}">
        {{ message.text }}
      </div>
      <input [(ngModel)]="userInput" (keyup.enter)="sendMessage()" placeholder="Digite sua mensagem..." />
      <button (click)="sendMessage()">Enviar</button>
    </div>
  `,
  styles: [`
    .chat-container {
      width: 300px;
      height: 400px;
      border: 1px solid #ccc;
      display: flex;
      flex-direction: column;
      padding: 10px;
      overflow-y: auto;
    }
    .message {
      padding: 5px;
      margin: 5px;
      border-radius: 5px;
      max-width: 80%;
    }
    .user {
      background-color: #d1e7ff;
      text-align: right;
      align-self: flex-end;
    }
    .bot {
      background-color: #f0f0f0;
      text-align: left;
      align-self: flex-start;
    }
    input {
      width: calc(100% - 60px);
      padding: 5px;
      border: 1px solid #ccc;
      border-radius: 5px;
    }
    button {
      padding: 5px 10px;
      margin-left: 5px;
      border: none;
      background-color: #007bff;
      color: white;
      border-radius: 5px;
      cursor: pointer;
    }
  `]
})
export class ChatbotComponent {
  userInput: string = '';
  messages: { text: string, sender: 'user' | 'bot' }[] = [];

  constructor(private http: HttpClient) {}

  sendMessage() {
    if (!this.userInput.trim()) return;
    
    this.messages.push({ text: this.userInput, sender: 'user' });
    const userMessage = this.userInput;
    this.userInput = '';
    
    this.http.post<{ reply: string }>('http://localhost:5000/api/chatbot/message', { text: userMessage })
      .subscribe(response => {
        this.messages.push({ text: response.reply, sender: 'bot' });
      }, error => {
        this.messages.push({ text: 'Erro ao conectar com o servidor.', sender: 'bot' });
      });
  }
}
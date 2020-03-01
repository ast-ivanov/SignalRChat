import { User } from './user';

export class Message {
    public id: string;
    public time: Date;
    public user: User;
    public text: string;
}
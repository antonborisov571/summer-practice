import { Question } from "./Question.ts";


export type Test = {
  id: string;
  title: string;
  description: string;
  endDate: Date;
  duration: number;
  numQuestions: number;
  maxAttempts: number;
  isCompleted: boolean;
  questions: Question[];
};
